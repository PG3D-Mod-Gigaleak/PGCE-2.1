using System.Collections.Generic;

public static class Storager
{
	private const bool useCryptoPlayerPrefs = true;

	private static bool iCloudAvailable;

	private static IDictionary<string, int> _keychainCache;

	private static int _readCount;

	private static int _writeCount;

	public static int ReadCount
	{
		get
		{
			return _readCount;
		}
	}

	public static int WriteCount
	{
		get
		{
			return _writeCount;
		}
	}

	static Storager()
	{
		iCloudAvailable = false;
		_keychainCache = new Dictionary<string, int>();
		int salt = 0x6B3C41E4 ^ Defs.SaltSeed;
		CryptoPlayerPrefs.setSalt(salt);
		CryptoPlayerPrefs.useRijndael(true);
		CryptoPlayerPrefs.useXor(true);
	}

	public static void Initialize(bool cloudAvailable)
	{
	}

	public static bool synchronize()
	{
		CryptoPlayerPrefs.Save();
		return true;
	}

	public static bool hasKey(string key)
	{
		return CryptoPlayerPrefs.HasKey(key);
	}

	public static void removeObjectForKey(string key)
	{
		CryptoPlayerPrefs.DeleteKey(key);
	}

	public static void removeAll()
	{
		CryptoPlayerPrefs.DeleteAll();
	}

	public static void setInt(string key, int val, bool useICloud)
	{
		_writeCount++;
		CryptoPlayerPrefs.SetInt(key, val);
	}

	public static void setString(string key, string val)
	{
		setString(key, val, false);
	}

	public static void setString(string key, string val, bool useICloud)
	{
		_writeCount++;
		CryptoPlayerPrefs.SetString(key, val);
	}

	public static int getInt(string key, bool useICloud)
	{
		_readCount++;
		if (CryptoPlayerPrefs.HasKey(key))
		{
			return CryptoPlayerPrefs.GetInt(key);
		}
		return 0;
	}

	public static float getFloat(string key, bool useICloud)
	{
		_readCount++;
		if (CryptoPlayerPrefs.HasKey(key))
		{
			return CryptoPlayerPrefs.GetFloat(key);
		}
		return 0f;
	}

	public static string getString(string key, bool useICloud)
	{
		_readCount++;
		if (CryptoPlayerPrefs.HasKey(key))
		{
			return CryptoPlayerPrefs.GetString(key);
		}
		return "";
	}
}
