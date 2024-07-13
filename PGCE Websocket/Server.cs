using System;
using System.Data.SQLite;
using WebSocketSharp;
using WebSocketSharp.Server;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace PGCE
{
	public class Action : WebSocketBehavior
	{
		protected override void OnMessage(MessageEventArgs e)
		{
			Server.SessionsBridge = Sessions;
			/*Console.WriteLine($"------------------------------------------------------------");
			Console.WriteLine($"[Action::OnMessage] Received message");*/
			Dictionary<string, object> givenInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(e.Data);
			if (givenInput == null) {
				Send("-X-");
			}
			givenInput = Encryption.Decrypt(givenInput);
			string action = (string)givenInput["action"];
			//Console.WriteLine($"[Action::OnMessage] Received request for action {action}");
			Dictionary<string, object> output = new Dictionary<string, object>();
			if (action == "ensure_ws_alive")
			{
				try
				{
					if (givenInput.ContainsKey("uid") && givenInput.ContainsKey("ak"))
					{
						AccountParameters? result = Helpers.GetAccountInfo(givenInput["uid"]);
						if (result == null) {
							throw new Exception("Result was NULL!");
						}
						AccountParameters confirmedResult = (AccountParameters)result;
						if (!Helpers.MatchingHash2Nonhash((string)givenInput["ak"], confirmedResult.AuthKey))
							throw new Exception("Authkey invalid");
						if (Helpers.AccountBanned(confirmedResult))
							throw new Exception("The account is banned");
					}
					output["response"] = "success";
				}
				catch (Exception exception)
				{
					output["response"] = "failed";
					output["cause"] = $"{exception.Message}";
				}
			}
			else if (action == "close_session")
			{
				output["response"] = "success";
			}
			else if (action == "create_user")
			{
				try
				{
					AccountParameters? result = Helpers.CreateAccount();
					if (result == null) {
						throw new Exception("Result was NULL!");
					}
					output["givenID"] = $"{((AccountParameters)result).ID}";
					output["authcret"] = $"{((AccountParameters)result).AuthKey}";
					output["response"] = "success";
				}
				catch (Exception exception)
				{
					Server.SendEmbed("Error while creating account", $"", 0xFF0000, new dField[]{
						new dField("Error", exception.ToString(), false),
					});
					output["response"] = "failed";
					output["cause"] = $"{exception.Message}";
				}
			}
			else if (action == "user_banned")
			{
				try
				{
					AccountParameters? result = Helpers.GetAccountInfo(givenInput["uid"]);
					if (result == null) {
						throw new Exception("Result was NULL!");
					}
					AccountParameters confirmedResult = (AccountParameters)result;
					if (!Helpers.MatchingHash2Nonhash((string)givenInput["ak"], confirmedResult.AuthKey))
						throw new Exception("Authkey invalid");
					output["banned"] = confirmedResult.Banned;
					output["response"] = "success";
				}
				catch (Exception exception)
				{
					output["response"] = "failed";
					output["cause"] = $"{exception.Message}";
				}
			}
			else if (action == "user_exists")
			{
				try
				{
					AccountParameters? result = Helpers.GetAccountInfo(givenInput["uid"]);
					if (result == null) {
						throw new Exception("User does not exist!");
					}
					AccountParameters confirmedResult = (AccountParameters)result;
					if (!Helpers.MatchingHash2Nonhash((string)givenInput["ak"], confirmedResult.AuthKey))
						throw new Exception("Authkey invalid");
					output["response"] = "success";
				}
				catch (Exception exception)
				{
					output["response"] = "failed";
					output["cause"] = $"{exception.Message}";
				}
			}
			else if (action == "send_chat")
			{
				try
				{
					AccountParameters? result = Helpers.GetAccountInfo(givenInput["uid"]);
					if (result == null) {
						throw new Exception("User does not exist!");
					}
					AccountParameters confirmedResult = (AccountParameters)result;
					if (!Helpers.MatchingHash2Nonhash((string)givenInput["ak"], confirmedResult.AuthKey))
						throw new Exception("Authkey invalid");
					if (Helpers.AccountBanned(confirmedResult))
						throw new Exception("The account is banned");
					if (Helpers.AccountChatBanned(confirmedResult))
					{
						Send(JsonConvert.SerializeObject(Encryption.Encrypt(new Dictionary<string, object>(){
							{"type", "send"},
							{"recvid", Convert.ToInt64((string)givenInput["uid"])},
							{"action", "recv-chatbanned"},
							{"response", "success"},
						})));
						throw new Exception("The account is chat banned");
					}
					string nameColorTag = "[FFFFFF]";
					if (confirmedResult.IsAdmin)
						nameColorTag = "[E6213D]";
					Sessions.Broadcast(JsonConvert.SerializeObject(Encryption.Encrypt(new Dictionary<string, object>(){
						{"type", "send"},
						{"text", $"<{nameColorTag}{confirmedResult.Name.RemoveColorCode()}[FFFFFF]> {PG3D.FilterBadWorld.FilterString((string)givenInput["msg"])}"},
						{"action", "recv-chat"},
						{"response", "success"},
					})));
					output["response"] = "success";
				}
				catch (Exception exception)
				{
					Server.SendEmbed("Error while sending chat message", $"", 0xFF0000, new dField[]{
						new dField("Dispatcher ID", Convert.ToString((string)givenInput["uid"]), false),
						new dField("Error", exception.ToString(), false),
					});
					output["response"] = "failed";
					output["cause"] = $"{exception.Message}";
				}
			}
			else if (action == "called__")
			{
				try
				{
					AccountParameters? result = Helpers.GetAccountInfo(givenInput["uid"]);
					if (result == null) {
						throw new Exception("User does not exist!");
					}
					AccountParameters confirmedResult = (AccountParameters)result;
					if (!Helpers.MatchingHash2Nonhash((string)givenInput["ak"], confirmedResult.AuthKey))
						throw new Exception("Authkey invalid");
					if (Helpers.AccountBanned(confirmedResult))
						throw new Exception("The account is banned");
					HelicopterController.ReasonEnume reason = (HelicopterController.ReasonEnume)givenInput["r"];
					switch (reason)
					{
						case HelicopterController.ReasonEnume.INJECTION:
						case HelicopterController.ReasonEnume.DEFAULT:
							Helpers.BanAccount(confirmedResult.ID);
							break;
						default:
							break;
					}
					output["response"] = "success";
				}
				catch (Exception exception)
				{
					Server.SendEmbed("Error while sending banning user:", $"", 0xFF0000, new dField[]{
						new dField("Dispatcher ID", Convert.ToString((string)givenInput["uid"]), false),
						new dField("Error", exception.ToString(), false),
					});
					output["response"] = "failed";
					output["cause"] = $"{exception.Message}";
				}
			}
			else if (action == "update_player")
			{
				try
				{
					AccountParameters? result = Helpers.GetAccountInfo(givenInput["uid"]);
					if (result == null) {
						throw new Exception("User does not exist!");
					}
					AccountParameters confirmedResult = (AccountParameters)result;
					if (!Helpers.MatchingHash2Nonhash((string)givenInput["ak"], confirmedResult.AuthKey))
						throw new Exception("Authkey invalid");
					if (Helpers.AccountBanned(confirmedResult))
						throw new Exception("The account is banned");
					if (givenInput.ContainsKey("newname"))
					{
						string r = PG3D.FilterBadWorld.FilterString((string)givenInput["newname"]);
						confirmedResult.Name = r.Substring(0, Math.Clamp(r.Length, 0, 20)).RemoveColorCode();
					}
					if (givenInput.ContainsKey("newcoins"))
						confirmedResult.Coins = Convert.ToInt64((string)givenInput["newcoins"]);
					if (givenInput.ContainsKey("newcatears"))
						confirmedResult.CatEars = Convert.ToBoolean((string)givenInput["newcatears"]);
					if (givenInput.ContainsKey("newskin"))
						confirmedResult.SkinData = (string)givenInput["newskin"];
					Helpers.UpdateParameters(Convert.ToInt64((string)givenInput["uid"]), confirmedResult);
					if (givenInput.ContainsKey("newname"))
						output["name_set"] = confirmedResult.Name;
					if (givenInput.ContainsKey("newcoins"))
						output["coins_set"] = confirmedResult.Coins;
					if (givenInput.ContainsKey("newcatears"))
						output["catears_set"] = confirmedResult.CatEars;
					if (givenInput.ContainsKey("newskin"))
						output["skin_set"] = confirmedResult.SkinData;
					output["response"] = "success";
				}
				catch (Exception exception)
				{
					Server.SendEmbed("Error while updating player data", $"", 0xFF0000, new dField[]{
						new dField("Dispatcher ID", Convert.ToString((string)givenInput["uid"]), false),
						new dField("Error", exception.ToString(), false),
					});
					output["response"] = "failed";
					output["cause"] = $"{exception.Message}";
				}
			}
			else if (action == "admin_give_coins")
			{
				try
				{
					AccountParameters? result = Helpers.GetAccountInfo(givenInput["uid"]);
					if (result == null) {
						throw new Exception("User does not exist!");
					}
					AccountParameters confirmedResult = (AccountParameters)result;
					if (!Helpers.MatchingHash2Nonhash((string)givenInput["ak"], confirmedResult.AuthKey))
						throw new Exception("Authkey invalid");
					if (Helpers.AccountBanned(confirmedResult))
						throw new Exception("The account is banned");
					if (!confirmedResult.IsAdmin)
						throw new Exception("The account isn't admin");
					confirmedResult.Coins += 99999;
					Helpers.UpdateParameters(Convert.ToInt64((string)givenInput["uid"]), confirmedResult);
					Send(JsonConvert.SerializeObject(Encryption.Encrypt(new Dictionary<string, object>(){
						{"type", "send"},
						{"recvid", Convert.ToInt64((string)givenInput["uid"])},
						{"action", "recv-forceupdate-user"},
						{"response", "success"},
					})));
					output["response"] = "success";
				}
				catch (Exception exception)
				{
					Server.SendEmbed("Error while giving admin coins", $"", 0xFF0000, new dField[]{
						new dField("Dispatcher ID", Convert.ToString((string)givenInput["uid"]), false),
						new dField("Error", exception.ToString(), false),
					});
					output["response"] = "failed";
					output["cause"] = $"{exception.Message}";
				}
			}
			else if (action == "admin_clear_achievements")
			{
				try
				{
					AccountParameters? result = Helpers.GetAccountInfo(givenInput["uid"]);
					if (result == null) {
						throw new Exception("User does not exist!");
					}
					AccountParameters confirmedResult = (AccountParameters)result;
					if (!Helpers.MatchingHash2Nonhash((string)givenInput["ak"], confirmedResult.AuthKey))
						throw new Exception("Authkey invalid");
					if (Helpers.AccountBanned(confirmedResult))
						throw new Exception("The account is banned");
					if (!confirmedResult.IsAdmin)
						throw new Exception("The account isn't admin");
					confirmedResult.Achievements.Clear();
					Helpers.UpdateParameters(Convert.ToInt64((string)givenInput["uid"]), confirmedResult);
					Send(JsonConvert.SerializeObject(Encryption.Encrypt(new Dictionary<string, object>(){
						{"type", "send"},
						{"recvid", Convert.ToInt64((string)givenInput["uid"])},
						{"action", "recv-forceupdate-user"},
						{"response", "success"},
					})));
					output["response"] = "success";
				}
				catch (Exception exception)
				{
					Server.SendEmbed("Error while giving admin coins", $"", 0xFF0000, new dField[]{
						new dField("Dispatcher ID", Convert.ToString((string)givenInput["uid"]), false),
						new dField("Error", exception.ToString(), false),
					});
					output["response"] = "failed";
					output["cause"] = $"{exception.Message}";
				}
			}
			else if (action == "buy_weapon")
			{
				try
				{
					AccountParameters? result = Helpers.GetAccountInfo(givenInput["uid"]);
					if (result == null) {
						throw new Exception("User does not exist!");
					}
					AccountParameters confirmedResult = (AccountParameters)result;
					if (!Helpers.MatchingHash2Nonhash((string)givenInput["ak"], confirmedResult.AuthKey))
						throw new Exception("Authkey invalid");
					if (Helpers.AccountBanned(confirmedResult))
						throw new Exception("The account is banned");
					string weaponName = (string)givenInput["wn"];
					if (confirmedResult.BoughtWeapons.Contains(weaponName))
						throw new Exception("Duplicate");
					long price = Convert.ToInt64((string)givenInput["wp"]);
					long category = Convert.ToInt64((string)givenInput["wc"]);
					if (confirmedResult.Coins < price)
						throw new Exception("The account doesn't have enough coins");
					confirmedResult.BoughtWeapons.Add(weaponName);
					long oldCoins = confirmedResult.Coins;
					confirmedResult.Coins -= price;
					confirmedResult.CategoryEquipList.SetWeaponForCat((CategoryType)category, weaponName);
					Helpers.UpdateParameters(Convert.ToInt64((string)givenInput["uid"]), confirmedResult);
					Server.SendEmbed("User successfully bought weapon", $"The ID {givenInput["uid"]} bought a weapon", 0xFFFF00, new dField[]{
						new dField("Dispatcher ID", Convert.ToString((string)givenInput["uid"]), true),
						new dField("Dispatcher name", confirmedResult.Name, true),
						new dField("Weapon name", weaponName, false),
						new dField("Weapon price", Convert.ToString(price), false),
						new dField("Expected end-result coins", Convert.ToString(oldCoins-price), false),
						new dField("Actual end-result coins", Convert.ToString(confirmedResult.Coins), false),
					});
					Send(JsonConvert.SerializeObject(Encryption.Encrypt(new Dictionary<string, object>(){
						{"type", "send"},
						{"recvid", Convert.ToInt64((string)givenInput["uid"])},
						{"action", "recv-forceupdate-user"},
						{"response", "success"},
					})));
					output["response"] = "success";
				}
				catch (Exception exception)
				{
					Server.SendEmbed("Error while buying weapon", $"", 0xFF0000, new dField[]{
						new dField("Dispatcher ID", Convert.ToString((string)givenInput["uid"]), false),
						new dField("Error", exception.ToString(), false),
					});
					output["response"] = "failed";
					output["cause"] = $"{exception.Message}";
				}
			}
			else if (action == "give_achievement")
			{
				try
				{
					AccountParameters? result = Helpers.GetAccountInfo(givenInput["uid"]);
					if (result == null) {
						throw new Exception("User does not exist!");
					}
					AccountParameters confirmedResult = (AccountParameters)result;
					if (!Helpers.MatchingHash2Nonhash((string)givenInput["ak"], confirmedResult.AuthKey))
						throw new Exception("Authkey invalid");
					if (Helpers.AccountBanned(confirmedResult))
						throw new Exception("The account is banned");
					string achievement = (string)givenInput["achievement"];
					if (Helpers.GetAccountInfo(givenInput["uid"]).Value.Achievements.Contains(achievement))
						throw new Exception("Duplicate");
					AccountParameters x = Helpers.GetAccountInfo(givenInput["uid"]).Value;
					x.Achievements.Add(achievement);
					Helpers.UpdateParameters(Convert.ToInt64((string)givenInput["uid"]), x);
					Server.SendEmbed("Successfully awarded user an achievement", $"The ID {givenInput["uid"]} was awarded an achievement", 0xFFFF00, new dField[]{
						new dField("Dispatcher ID", Convert.ToString((string)givenInput["uid"]), true),
						new dField("Dispatcher name", x.Name, true),
						new dField("Achievement ID", achievement, false),
					});
					output["response"] = "success";
					Send(JsonConvert.SerializeObject(Encryption.Encrypt(new Dictionary<string, object>(){
						{"type", "send"},
						{"recvid", Convert.ToInt64((string)givenInput["uid"])},
						{"action", "recv-forceupdate-user"},
						{"response", "success"},
					})));
					return;
				}
				catch (Exception exception)
				{
					Server.SendEmbed("Error while awarding achievement", $"", 0xFF0000, new dField[]{
						new dField("Dispatcher ID", Convert.ToString((string)givenInput["uid"]), false),
						new dField("Error", exception.ToString(), false),
					});
					output["response"] = "failed";
					output["cause"] = $"{exception.Message}";
				}
			}
			else if (action == "equip_weapon")
			{
				try
				{
					AccountParameters? result = Helpers.GetAccountInfo(givenInput["uid"]);
					if (result == null) {
						throw new Exception("User does not exist!");
					}
					AccountParameters confirmedResult = (AccountParameters)result;
					if (!Helpers.MatchingHash2Nonhash((string)givenInput["ak"], confirmedResult.AuthKey))
						throw new Exception("Authkey invalid");
					if (Helpers.AccountBanned(confirmedResult))
						throw new Exception("The account is banned");
					string weaponName = (string)givenInput["wn"];
					if (!confirmedResult.BoughtWeapons.Contains(weaponName))
						throw new Exception("The account doesn't own the weapon");
					long category = Convert.ToInt64((string)givenInput["wc"]);
					confirmedResult.CategoryEquipList.SetWeaponForCat((CategoryType)category, weaponName);
					Helpers.UpdateParameters(Convert.ToInt64((string)givenInput["uid"]), confirmedResult);
					Server.SendEmbed("User successfully equipped weapon", $"The ID {givenInput["uid"]} equipped a weapon", 0xFFFF00, new dField[]{
						new dField("Dispatcher ID", Convert.ToString((string)givenInput["uid"]), true),
						new dField("Dispatcher name", confirmedResult.Name, true),
						new dField("Weapon name", weaponName, false),
					});
					Send(JsonConvert.SerializeObject(Encryption.Encrypt(new Dictionary<string, object>(){
						{"type", "send"},
						{"recvid", Convert.ToInt64((string)givenInput["uid"])},
						{"action", "recv-forceupdate-user"},
						{"response", "success"},
					})));
					output["response"] = "success";
				}
				catch (Exception exception)
				{
					Server.SendEmbed("Error while equipping weapon", $"", 0xFF0000, new dField[]{
						new dField("Dispatcher ID", Convert.ToString((string)givenInput["uid"]), false),
						new dField("Error", exception.ToString(), false),
					});
					output["response"] = "failed";
					output["cause"] = $"{exception.Message}";
				}
			}
			else if (action == "get_my_player_info")
			{
				try
				{
					AccountParameters? result = Helpers.GetAccountInfo(givenInput["uid"]);
					if (result == null) {
						throw new Exception("User does not exist!");
					}
					AccountParameters confirmedResult = (AccountParameters)result;
					if (!Helpers.MatchingHash2Nonhash((string)givenInput["ak"], confirmedResult.AuthKey))
						throw new Exception("Authkey invalid");
					if (Helpers.AccountBanned(confirmedResult))
						throw new Exception("The account is banned");
					output["name_set"] = confirmedResult.Name;
					output["coins_set"] = confirmedResult.Coins;
					output["catears_set"] = confirmedResult.CatEars;
					output["skin_set"] = confirmedResult.SkinData;
					output["isAdmin"] = confirmedResult.IsAdmin;
					output["catlistserialized_set"] = JsonConvert.SerializeObject(confirmedResult.CategoryEquipList);
					output["achievementsserialized_set"] = JsonConvert.SerializeObject(confirmedResult.Achievements);
					output["boughtweaponsserialized_set"] = JsonConvert.SerializeObject(confirmedResult.BoughtWeapons);
					output["response"] = "success";
				}
				catch (Exception exception)
				{
					Server.SendEmbed("Error while getting account info", $"", 0xFF0000, new dField[]{
						new dField("Dispatcher ID", Convert.ToString((string)givenInput["uid"]), false),
						new dField("Error", exception.ToString(), false),
					});
					output["response"] = "failed";
					output["cause"] = $"{exception.Message}";
				}
			}
			else if (action == "request_connection")
			{
				try
				{
					AccountParameters? result = Helpers.GetAccountInfo(givenInput["uid"]);
					if (result == null) {
						throw new Exception("User does not exist!");
					}
					AccountParameters confirmedResult = (AccountParameters)result;
					if (!Helpers.MatchingHash2Nonhash((string)givenInput["ak"], confirmedResult.AuthKey))
						throw new Exception("Authkey invalid");
					if (Helpers.AccountBanned(confirmedResult))
						throw new Exception("The account is banned");
					string s = Helpers.GUIDFromTime(DateTime.Today);
					if (Convert.ToString((string)givenInput["coop"]) == "1")
						output["settings"] = $"{s}COOP";
					else
						output["settings"] = s;
					Server.SendEmbed("Photon Connection Requested", "", 0x00FF00, new dField[]{
						new dField("User ID", Convert.ToString((string)givenInput["uid"]), true),
						new dField("Given connection settings", (string)output["settings"], true),
						new dField("Is COOP", Convert.ToString((string)givenInput["coop"] == "1"), true),
					});
					output["response"] = "success";
				}
				catch (Exception exception)
				{
					Server.SendEmbed("Error while requesting photon connection", $"", 0xFF0000, new dField[]{
						new dField("Dispatcher ID", Convert.ToString((string)givenInput["uid"]), false),
						new dField("Error", exception.ToString(), false),
					});
					output["response"] = "failed";
					output["cause"] = $"{exception.Message}";
				}
			}
			else
			{
				Server.SendEmbed("Called nonexistent action", $"The action called was {action}, which is not implemented or does not exist.", 0xFF0000, new dField[]{
					new dField("Dispatcher ID (or null)", (givenInput.ContainsKey("uid") ? Convert.ToString((string)givenInput["uid"]) : ""), false),
				});
				output["response"] = "failed";
				output["cause"] = $"Unimplemented action {action}";
			}
			Send(JsonConvert.SerializeObject(Encryption.Encrypt(output)));
			/*Console.WriteLine($"[Action::OnMessage] Finalized request for action {action}");
			Console.WriteLine($"[Action::OnMessage] Output: {JsonConvert.SerializeObject(output)}");*/
		}
	}

	public class Server
	{
		private const bool IsProduction = false;
		private const bool ClearDatabase = false;
		public static SQLiteConnection? DB { get; set; }
		public static SQLiteCommand? CMD { get; set; }
		public static WebSocketSessionManager SessionsBridge { get; set; }
		private static void InitDB(bool clearNoMatterWhat = false)
		{
			if (ClearDatabase || clearNoMatterWhat)
			{
				if (DB != null)
				{
					DB.Close();
				}
				if (CMD != null)
				{
					CMD.Dispose();
				}
				if (clearNoMatterWhat)
				{
					File.Move("db.sqlite", "db-BKP.sqlite", true);
				}
				SQLiteConnection.CreateFile("db.sqlite");
				string connectionString = "Data Source=db.sqlite;Version=3;";
				DB = new SQLiteConnection(connectionString);
				DB.Open();
				CMD = new SQLiteCommand(DB);
				// not null default 0 not null default 0 not null default 0 not null default 0 not null default 0 not null default 0 not null default 0 not null default 0 not null default 0 not null default 0 not null default 0 not null default 0 not null default 0 not null default 0 not null default 0
				CMD.CommandText = "CREATE TABLE Users(id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, skin TEXT NOT NULL DEFAULT \"\", authkey TEXT NOT NULL DEFAULT \"\", coins INTEGER NOT NULL DEFAULT 15, banned INTEGER NOT NULL DEFAULT 0, catears INTEGER NOT NULL DEFAULT 0, catequiplist TEXT NOT NULL DEFAULT \"\", achievements TEXT NOT NULL DEFAULT \"\", boughtWeapons TEXT NOT NULL DEFAULT \"\", isadmin INTEGER NOT NULL DEFAULT 0)";
				CMD.ExecuteNonQuery();
				if (clearNoMatterWhat)
				{
					Console.WriteLine("[COMMAND::CLEAR-DB] Database cleared");
				}
			}
			else 
			{
				string connectionString = "Data Source=db.sqlite;Version=3;";
				DB = new SQLiteConnection(connectionString);
				DB.Open();
				CMD = new SQLiteCommand(DB);
			}
		}
		public static void SendMs(string message)
		{
			string webhook = "https://ptb.discord.com/api/webhooks/1139180048012804167/3KD5OpGBeIiIGC31D7YSMUJ47AdnExtJzrZ129uvzQipF8e2QBQKh0SFge0VRqX9NeRw";
			using (dWebHook dcWeb = new dWebHook())
			{
				dcWeb.ProfilePicture = "https://cdn.discordapp.com/icons/1133132217854464061/e7841c83fd21d36aaf62a0ee51f52700.webp?size=2048";
				dcWeb.UserName = "PGCE Websocket Calls";
				dcWeb.WebHook = webhook;
				dcWeb.SendMessage(message);
			}
		}
		public static void SendEmbed(string title, string desc, int color, dField[] fields)
		{
			try
			{
				string webhook = "https://ptb.discord.com/api/webhooks/1140044762943070289/Ac7y6Rn9RfaLpt0W0X4AERxWHPeUImPTv6S9o-2qJweVdeg-n3A2ZzoFBsxJT47S3TxJ";
				using (dWebHook dcWeb = new dWebHook())
				{
					dcWeb.ProfilePicture = "https://cdn.discordapp.com/icons/1133132217854464061/e7841c83fd21d36aaf62a0ee51f52700.webp?size=2048";
					dcWeb.UserName = "PGCE Websocket Calls";
					dcWeb.WebHook = webhook;
					dcWeb.SendEmbed(title, desc, color, fields);
				}
			} catch (Exception e)
			{
				// fuck you
			}
		}
		public static void Main(string[] args)
		{
			Console.Clear();
			InitDB();
			Helpers.InitializeEncryption();
			var wssv = new WebSocketServer(8083);
			wssv.AddWebSocketService<Action>("/action");
			wssv.Start();
			//SendEmbed("Server started", $"Server started at {wssv.Address}:{wssv.Port}", 0x00FF00, null);
			Console.WriteLine("[Server] Started server successfully");
			while (true)
			{
				string? line = Console.ReadLine();
				if (line == null)
					continue;
				if (line == "stop")
				{
					if (Server.SessionsBridge != null)
					{
						Dictionary<string, object> output = new Dictionary<string, object>();
						output["type"] = "send";
						output["action"] = "alert-downtime";
						output["response"] = "success";
						Server.SessionsBridge.Broadcast(JsonConvert.SerializeObject(Encryption.Encrypt(output)));
					}
					break;
				}
				if (line == "clear-db")
				{
					InitDB(true);
				}
				if (line.StartsWith("."))
				{
					List<string> commandWArgs = line.Split('"')
						.Select((element, index) => index % 2 == 0
							? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
							: new string[] { element })
						.SelectMany(element => element).ToList();
					string command = commandWArgs[0];
					if (command == ".encrypt")
					{
						if (commandWArgs.Count > 1)
						{
							Console.WriteLine($"[COMMAND::ENCRYPT] Encrypted result: \"{Helpers.EncryptString(commandWArgs[1])}\"");
						}
					}
					if (command == ".unban_user")
					{
						if (commandWArgs.Count > 1)
						{
							long id = 0;
							if (!long.TryParse(commandWArgs[1], out id))
							{
								Console.WriteLine("[COMMAND::UNBAN_USER] Given User ID is invalid!");
								continue;
							}
							SQLiteCommand newCmd = new SQLiteCommand(Server.DB);
							newCmd.CommandText = "UPDATE Users SET banned = 0 WHERE id = @id";
							newCmd.Parameters.Add(new SQLiteParameter("@id", id));
							newCmd.ExecuteNonQuery();
							Server.SendEmbed("Unbanned user", "The user was unbanned via console:", 0x00FF00, new dField[]{
								new dField("User ID", Convert.ToString(id), false),
								new dField("Username", Helpers.GetAccountInfo(id).Value.Name, false),
							});
							Console.WriteLine($"[COMMAND::UNBAN_USER] Unbanned user {Helpers.GetAccountInfo(id).Value.Name}");
						}
					}
					if (command == ".set_admin")
					{
						if (commandWArgs.Count > 2)
						{
							long id = 0;
							int isAdmin = 0;
							if (!long.TryParse(commandWArgs[1], out id))
							{
								Console.WriteLine("[COMMAND::SET_ADMIN] Given User ID is invalid!");
								continue;
							}
							if (!int.TryParse(commandWArgs[2], out isAdmin))
							{
								Console.WriteLine("[COMMAND::SET_ADMIN] Invalid admin variable! Use either 0 or 1!");
								continue;
							}
							if (isAdmin > 1)
							{
								isAdmin = 1;
							}
							if (isAdmin < 0)
							{
								isAdmin = 0;
							}
							SQLiteCommand newCmd = new SQLiteCommand(Server.DB);
							newCmd.CommandText = "UPDATE Users SET isadmin = @admin WHERE id = @id";
							newCmd.Parameters.Add(new SQLiteParameter("@id", id));
							newCmd.Parameters.Add(new SQLiteParameter("@admin", isAdmin));
							newCmd.ExecuteNonQuery();
							Server.SendEmbed("Set user to admin", "The user was set to admin via console:", 0x00AA00, new dField[]{
								new dField("User ID", Convert.ToString(id), false),
								new dField("Username", Helpers.GetAccountInfo(id).Value.Name, false),
								new dField("Admin value", Convert.ToString(Helpers.GetAccountInfo(id).Value.IsAdmin), false),
							});
							Console.WriteLine($"[COMMAND::SET_ADMIN] Set {Helpers.GetAccountInfo(id).Value.Name}'s admin to {Helpers.GetAccountInfo(id).Value.IsAdmin}");
						}
					}
					if (command == ".ban_user")
					{
						if (commandWArgs.Count > 1)
						{
							long id = 0;
							if (!long.TryParse(commandWArgs[1], out id))
							{
								Console.WriteLine("[COMMAND::BAN_USER] Given User ID is invalid!");
								continue;
							}
							if (Helpers.BanAccount(id))
							{
								Server.SendEmbed("Banned user", "The user was banned via console:", 0x00FF00, new dField[]{
									new dField("User ID", Convert.ToString(id), false),
									new dField("Username", Helpers.GetAccountInfo(id).Value.Name, false),
								});
								Console.WriteLine($"[COMMAND::BAN_USER] Banned user {Helpers.GetAccountInfo(id).Value.Name}");
							}
							else
							{
								Console.WriteLine($"[COMMAND::BAN_USER] User going by that ID doesn't exist!");
							}
						}
					}
					if (command == ".get_user_info")
					{
						if (commandWArgs.Count > 1)
						{
							long id = 0;
							if (!long.TryParse(commandWArgs[1], out id))
							{
								Console.WriteLine("[COMMAND::GET_USER_INFO] Given User ID is invalid!");
								continue;
							}
							if (Helpers.AccountExists(id))
							{
								Console.WriteLine($"[COMMAND::GET_USER_INFO] Username: {Helpers.GetAccountInfo(id).Value.Name}");
								Console.WriteLine($"[COMMAND::GET_USER_INFO] Coins: {Helpers.GetAccountInfo(id).Value.Coins}");
								Console.WriteLine($"[COMMAND::GET_USER_INFO] Has cat ears: {Helpers.GetAccountInfo(id).Value.CatEars}");
							}
							else
							{
								Console.WriteLine($"[COMMAND::GET_USER_INFO] User going by that ID doesn't exist!");
							}
						}
					}
				}
			}
			Console.WriteLine("[Server] Stopping server");
			wssv.Stop();
			DB.Close();
			//SendEmbed("Server stopped", $"Server stopped at {wssv.Address}:{wssv.Port}", 0xFF0000, null);
			Console.Clear();
			Console.WriteLine("[Server] Server stopped");
		}
	}
}