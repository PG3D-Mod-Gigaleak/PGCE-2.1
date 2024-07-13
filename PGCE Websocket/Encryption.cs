using System;
using System.Text;

public class Encryption
{
	private static long[] InitialKey = {0xDEADCAFE, 0xDAD15DEAD, 0xE621};
	public static Dictionary<string, object> Encrypt(Dictionary<string, object> input)
	{
		var encryptedDictionary = new Dictionary<string, object>();

		foreach (var kvp in input)
		{
			var encryptedKey = EncryptValue(kvp.Key.ToString());
			var encryptedValue = EncryptValue(kvp.Value.ToString());
			encryptedDictionary.Add(encryptedKey, encryptedValue);
		}

		return encryptedDictionary;
	}

	public static Dictionary<string, object> Decrypt(Dictionary<string, object> encryptedInput)
	{
		var decryptedDictionary = new Dictionary<string, object>();

		foreach (var kvp in encryptedInput)
		{
			var decryptedKey = DecryptValue(kvp.Key.ToString());
			var decryptedValue = DecryptValue(kvp.Value.ToString());
			decryptedDictionary.Add(decryptedKey, decryptedValue);
		}

		return decryptedDictionary;
	}

	public static string EncryptValue(string value)
	{
		long[] EncryptionKey = InitialKey;
		byte[] bytes = Encoding.UTF8.GetBytes(value);
		for (int i = 0; i < bytes.Length; i++)
		{
			bytes[i] ^= (byte)EncryptionKey[i % EncryptionKey.Length];
			EncryptionKey[i % EncryptionKey.Length] ^= (long)Math.Pow(i, i%3) + 1;
		}
		return Convert.ToBase64String(bytes);
	}

	public static string DecryptValue(string encryptedValue)
	{
		long[] EncryptionKey = InitialKey;
		byte[] bytes = Convert.FromBase64String(encryptedValue);
		for (int i = 0; i < bytes.Length; i++)
		{
			bytes[i] ^= (byte)EncryptionKey[i % EncryptionKey.Length];
			EncryptionKey[i % EncryptionKey.Length] ^= (long)Math.Pow(i, i%3) + 1;
		}
		return Encoding.UTF8.GetString(bytes);
	}
}