using System;
using System.IO;
using UnityEngine;

public class SkinsManager
{
	public static string _PathBase
	{
		get
		{
			return Application.persistentDataPath;
		}
	}

	private static void _WriteImageAtPathToGal(string pathToImage)
	{
		try
		{
		}
		catch (Exception ex)
		{
			Debug.Log("Exception in _ScreenshotWriteToAlbum: " + ex);
		}
	}

	public static void SaveTextureToGallery(Texture2D t, string nm)
	{
		string pathToImage = Path.Combine(_PathBase, nm);
		_WriteImageAtPathToGal(pathToImage);
	}

	public static bool SaveTextureWithName(Texture2D t, string nm, bool writeToGallery = true)
	{
		string text = Path.Combine(_PathBase, nm);
		try
		{
			byte[] bytes = t.EncodeToPNG();
			File.WriteAllBytes(text, bytes);
		}
		catch (Exception message)
		{
			Debug.Log(message);
		}
		if (writeToGallery)
		{
			_WriteImageAtPathToGal(text);
		}
		return true;
	}

	public static Texture2D TextureForName(string nm)
	{
		Texture2D texture2D = new Texture2D(64, 32);
		try
		{
			byte[] data = File.ReadAllBytes(Path.Combine(_PathBase, nm));
			texture2D.LoadImage(data);
			return texture2D;
		}
		catch (Exception message)
		{
			Debug.Log(message);
			return texture2D;
		}
	}

	public static bool DeleteTexture(string nm)
	{
		try
		{
			File.Delete(Path.Combine(_PathBase, nm));
		}
		catch (Exception message)
		{
			Debug.Log(message);
		}
		return true;
	}

	public static Texture currentMultiplayerSkin()
	{
		//PlayerPrefs.SetString("base64_multskin", "iVBORw0KGgoAAAANSUhEUgAAAEAAAAAgCAYAAACinX6EAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAR9SURBVGhD5Zi/byNFFMef19nzJrazTnLBvnDSgagQDQUSDRIoDQUlHcUV/AX8F/wl9BRXp0c0SBSkQYAQ4iJCYgfH6/3lZb7P+zaTZXZnFwnZFz7RaGfezI7fe/P2zUw6ZGE8HmfhMqTpbEojf8Qyqfe8HqGvDjXW+hubxMmfVsRgFHHEQ8DqAFlhNr6XF1UHttV/FbA6QFYdhu/t7XERJ4gjXmUaR4CJ/0UECDezG1osFlxQfyg0dgAIw5DLQ6JRDjCFOmQPIQd01JaW5fV/GCsGiszWBuU5bGz6nODs+/uEAsqKoz2Nn+atdVsfgz7TO0DmRRFH4QCFAiCT390kHZz0UCkbAgWx3YGL+THtpj9yXQi6b9F48AfXOTcY3hekT2R6++LiYrMRoK+YDuTI9s4Hn9N7X35Fo0++yHvUqVDVIUMfxpSNl7kg143dxoNUkQNEKSho2ubQrxtjUh6Gyy6h94vxOEQBbKUSNZvOAfeSIBBlge4IGKcbUO4Dpk8BiMNkHN4V2dY5AEA5fuaOOD09pZ9+/oXefOMZt6V+dnbGbdOqC2Ko1IHe3nQO6BweHmardJU37xBlBSitG2DqL+N0HZK5MV43XOobj4D8WSARMXj0Ps2jb1imY1PYFFFtsM2PXUsiE5F3cHBAL39/SZMnE5adn5+3cmiro/A2IMlUbqXLxZImr01oerU+X7Sl0gFY/XKYbyNez6Mojkh9ykUuakOlA2C8hNo2ASPlVsoGd4lmsxmt1N+/0bdyF5BQ0/dsiQh39S4/TTlCR8abEqSJqvFluW1evd+WU4wOAJgETtCNFw+vgrf5WecAfbzMUQUSLnB2f+Bnk/mbYnNA5ScAhXXFJSJQjnr2f4oW71uMFzCnaf6T0Wf0bPgxvfP4OT09fE6+9yHLZZXrGO9/mteqqd0FdMVhCD4H8GfYLOOyExoYL8i3reM4O+Tvvk677oiSdEndrldEV50TmjgINN4GZUWvr6+LFZLQ/a9xOi4F8ZSyLKUkmRfRWOcE9Lk7/bxVTWMHgLYr2hbcEcoXsdUqoTD5i+uRckLX8YporPu80Hdze/8Kb6KVAzj0GoZWU+SCBMrzo2+evaDfkhf0a/w1pY++5bKjPotoGfEql8E7J09OKIkSdUj4PpdW09gBrBxCzvCjoI1j9LFpnOa1apIkUZGwvlP0+33yfV/lAnUAMIAIQnR4npdL6mnuAGW4ZOlyEiyc08AJ5bG6Q3GuR9EZjda7g+u6bHRwG/AFC6c/hHmZrtOl28Utj0ek2CgcAIUQPlLKxsh3hxI73+XSO4NsSQmYxl5dX+W9VbuAQ4PBgOtYebWv0zJc0tHBkdFAdVmi48fHlKYpxXGcS6thB+iKSSkbI7tA2esih+LcX5MkTWPh7GU856himSqoQwbm8znLhsMhXV5ekj/yKQgDCqKA+sP7WV6u31kn4whoAjtAV0yKKKjD41SZuB/xVignNXlfHw/nmaLJNNZz1ytsIooiPutPp1OOhizLeHHwRFtH/veAzwRU5Yk7iP4G1nDHYeWAUZoAAAAASUVORK5CYII=");
		if (PlayerPrefs.GetString("base64_multskin") != "")
		{
			byte[]  imageBytes = Convert.FromBase64String(PlayerPrefs.GetString("base64_multskin"));
     		Texture2D tex = new Texture2D(2, 2);
     		tex.LoadImage(imageBytes);
			return tex;
		}
		return Resources.Load(Path.Combine(Defs.MultSkinsDirectoryName, "multi_skin_1")) as Texture;
		//if (!File.Exists(Path.Combine(_PathBase, Defs.SkinBaseName + 1)))
		//{
		//	return Resources.Load(Path.Combine(Defs.MultSkinsDirectoryName, "multi_skin_1")) as Texture;
		//}
		//string @string = PlayerPrefs.GetString(Defs.SkinNameMultiplayer, Defs.SkinBaseName + 0);
		//return TextureForName(@string);
	}
}
