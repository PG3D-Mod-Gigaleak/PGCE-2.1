using System;
using System.Data.SQLite;
using WebSocketSharp;
using WebSocketSharp.Server;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace PGCE
{
	public class Helpers
	{
		// set this up with parameters & shit
		// INTEGER is 64bit in sqlite for some reason, don't ask
		public static AccountParameters? CreateAccount()
		{
			Console.WriteLine("Creating account");
			SQLiteCommand newCmd = new SQLiteCommand(Server.DB);
			newCmd.CommandText = "INSERT INTO Users(name, authkey, coins, banned, catequiplist, achievements, boughtWeapons) VALUES('Player', @authkey, 15, 0, @defaultcatequiplist, @defaultachievements, @defaultboughtweapons) RETURNING id";
			string uuid = Guid.NewGuid().ToString();
			newCmd.Parameters.Add(new SQLiteParameter("@authkey", SHA384Hash(uuid)));
			newCmd.Parameters.Add(new SQLiteParameter("@defaultcatequiplist", JsonConvert.SerializeObject(new PG3D.Serialization.CategoryEquipList())));
			newCmd.Parameters.Add(new SQLiteParameter("@defaultachievements", JsonConvert.SerializeObject(new List<string>())));
			newCmd.Parameters.Add(new SQLiteParameter("@defaultboughtweapons", JsonConvert.SerializeObject(new List<string>(){"Weapon1", "Weapon2", "Weapon9"})));
			object result = newCmd.ExecuteScalar();
			if (result == null) {
				return null;
			}
			Console.WriteLine($"Created account! Resulting ID is {result}, Authkey is {uuid} (maybe account is reserved?)");
			return new AccountParameters((long)result, uuid);
		}
		public static string SHA384Hash(string a)
		{
			SHA384 hasher = SHA384.Create();
			byte[] bytes = Encoding.UTF8.GetBytes(a);
			byte[] hashBytes = hasher.ComputeHash(bytes);
			string plaintext = "";
			foreach (byte hashByte in hashBytes) {
				plaintext += hashByte.ToString("x2");
			}
			return plaintext;
		}
		public static bool MatchingHash2Nonhash(string a, string b)
		{
			return SHA384Hash(a) == b;
		}
		public static bool AccountExists(long id)
		{
			if (GetAccountInfo(id) == null)
				return false;
			return true;
		}
		public static bool AccountBanned(long id)
		{
			if (GetAccountInfo(id) == null)
				return false;
			AccountParameters accountParameters = GetAccountInfo(id).GetValueOrDefault();
			return AccountBanned(accountParameters);
		}
		public static bool AccountBanned(AccountParameters accountParameters)
		{
			if (accountParameters.Banned)
				return true;
			return DateTime.Now < accountParameters.BanTime;
		}
		public static bool AccountChatBanned(long id)
		{
			if (GetAccountInfo(id) == null)
				return false;
			AccountParameters accountParameters = GetAccountInfo(id).GetValueOrDefault();
			return AccountChatBanned(accountParameters);
		}
		public static bool AccountChatBanned(AccountParameters accountParameters)
		{
			if (accountParameters.Banned)
				return true;
			return DateTime.Now < accountParameters.ChatBanTime;
		}
		public static bool BanAccount(long id, bool dontSend = false)
		{
			if (GetAccountInfo(id) == null)
				return false;
			try 
			{
				SQLiteCommand newCmd = new SQLiteCommand(Server.DB);
				newCmd.CommandText = "UPDATE Users SET banned = 1 WHERE id = @id";
				newCmd.Parameters.Add(new SQLiteParameter("@id", id));
				newCmd.ExecuteNonQuery();
				if (Server.SessionsBridge != null && !dontSend)
				{
					Dictionary<string, object> output = new Dictionary<string, object>();
					output["bannedID"] = $"{id}";
					output["type"] = "send";
					output["action"] = "alert-ban";
					output["response"] = "success";
					Server.SessionsBridge.Broadcast(JsonConvert.SerializeObject(Encryption.Encrypt(output)));
				}
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine($"[Helpers::BanAccount] Error while banning: {e.Message}");
				return false;
			}
		}
		public static bool UpdateParameters(long id, AccountParameters newParameters)
		{
			if (GetAccountInfo(id) == null)
				return false;
			try 
			{
				SQLiteCommand newCmd = new SQLiteCommand(Server.DB);
				// expand this once we have more parameters
				newCmd.CommandText = "UPDATE Users SET name = @newname, coins = @newcoins, catears = @newcatears, skin = @newskin, catequiplist = @newcatequiplist, achievements = @newachievements, boughtWeapons = @newboughtweapons WHERE id = @id";
				newCmd.Parameters.Add(new SQLiteParameter("@id", id));
				newCmd.Parameters.Add(new SQLiteParameter("@newname", newParameters.Name));
				newCmd.Parameters.Add(new SQLiteParameter("@newcoins", newParameters.Coins));
				newCmd.Parameters.Add(new SQLiteParameter("@newcatears", newParameters.CatEars));
				newCmd.Parameters.Add(new SQLiteParameter("@newskin", newParameters.SkinData));
				newCmd.Parameters.Add(new SQLiteParameter("@newcatequiplist", JsonConvert.SerializeObject(newParameters.CategoryEquipList)));
				newCmd.Parameters.Add(new SQLiteParameter("@newachievements", JsonConvert.SerializeObject(newParameters.Achievements)));
				newCmd.Parameters.Add(new SQLiteParameter("@newboughtweapons", JsonConvert.SerializeObject(newParameters.BoughtWeapons)));
				Server.SendEmbed("Player Parameters Updated", $"The ID {id} is updating account parameters", 0xFFFF00, new dField[]{
					new dField("New username", newParameters.Name, false),
					new dField("New coin count", Convert.ToString(newParameters.Coins), false),
				});
				newCmd.ExecuteNonQuery();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine($"[Helpers::BanAccount] Error while banning: {e.Message}");
				return false;
			}
		}
		public static byte[] Key { get; set; }
		public static byte[] IV { get; set; }
		public static string Password 
		{ 
			get
			{
				// https://pinetools.com/random-string-generator
				// length: 64
				return "1LKJJ3%T5nn7xupp.}S36cie3i[D_V@2CB/%.+k/[@Z&6KMDZxCJ6_5$SxFui*0f";
			} 
		}
		public static void InitializeEncryption()
		{
			byte[] salt = GenerateSalt();
			Rfc2898DeriveBytes keyDerivation = new Rfc2898DeriveBytes(Password, salt, 10000, HashAlgorithmName.SHA256);
			Key = keyDerivation.GetBytes(32); // AES-256 key size
			IV = keyDerivation.GetBytes(16); // AES block size
		}
		public static byte[] GenerateSalt()
		{
			byte[] salt = new byte[16];
			using (RandomNumberGenerator rngCsp = RandomNumberGenerator.Create())
			{
				rngCsp.GetBytes(salt);
			}
			return salt;
		}
		public static string EncryptString(string plainText)
		{
			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = Key;
				aesAlg.IV = IV;

				ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

				using (MemoryStream msEncrypt = new MemoryStream())
				{
					using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
						{
							swEncrypt.Write(plainText);
						}
					}

					return Convert.ToBase64String(msEncrypt.ToArray());
				}
			}
		}

		public static string DecryptString(string cipherText)
		{
			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = Key;
				aesAlg.IV = IV;

				ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

				using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
				{
					using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader srDecrypt = new StreamReader(csDecrypt))
						{
							return srDecrypt.ReadToEnd();
						}
					}
				}
			}
		}
		public static string GUIDFromTime(DateTime time)
		{
			string result = EncryptString(time.ToLongDateString() + "///" + time.ToLongTimeString() + "-v3.0");
			return result;
		}
		public static AccountParameters? GetAccountInfo(object id)
		{
			return GetAccountInfo(Convert.ToInt64(Convert.ToString(id)));
		}
		public static AccountParameters? GetAccountInfo(long id)
		{
			SQLiteCommand newCmd = new SQLiteCommand(Server.DB);
			newCmd.CommandText = "SELECT name, authkey, coins, banned, catears, skin, catequiplist, achievements, boughtWeapons, isadmin FROM Users WHERE id = @id";
			newCmd.Parameters.Add(new SQLiteParameter("@id", id));
			SQLiteDataReader result = newCmd.ExecuteReader();
			if (result.Read())
			{
				AccountParameters foundUserData = new AccountParameters(id, (string)result["authkey"]);
				foundUserData.Coins = (long)result["coins"];
				foundUserData.Name = (string)result["name"];
				foundUserData.SkinData = (string)result["skin"];
				foundUserData.CategoryEquipList = JsonConvert.DeserializeObject<PG3D.Serialization.CategoryEquipList>((string)result["catequiplist"]);
				foundUserData.Achievements = JsonConvert.DeserializeObject<List<string>>((string)result["achievements"]);
				foundUserData.BoughtWeapons = JsonConvert.DeserializeObject<List<string>>((string)result["boughtWeapons"]);
				foundUserData.Banned = ((long)result["banned"] == 0 ? false : true);
				foundUserData.CatEars = ((long)result["catears"] == 0 ? false : true);
				foundUserData.IsAdmin = ((long)result["isadmin"] == 0 ? false : true);
				return foundUserData;
			}
			else
			{
				return null;
			}
		}
	}
}
