using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour 
{

public static Rect screenScaleRect(float left, float top, float width, float height)
{
	return new Rect((float)Screen.width * left, (float)Screen.height * top, (float)Screen.width * width, (float)Screen.height * height);
}

public static GameObject LoadObject(string str)
{
	return Resources.Load(str) as GameObject;
}

public static AnimationState GetAnimState(Animation anim, string str)
{
	foreach (AnimationState AS in anim)
	{
		if (AS.name == str)
		{
			return AS;
		}
	}
	return null;
}

public static GameObject InsGobj(Object original)
{
	GameObject gobj = Instantiate(original) as GameObject;
	gobj.name = gobj.name.Replace("(Clone)", string.Empty);
	return gobj;
}

public static GameObject InsGobj(Object original, Transform parent)
{
	GameObject gobj = Instantiate(original, parent) as GameObject;
	gobj.name = gobj.name.Replace("(Clone)", string.Empty);
	return gobj;
}

public static GameObject InsGobj(Object original, Transform parent, bool instantiateInWorldSpace)
{
	GameObject gobj = Instantiate(original, parent, instantiateInWorldSpace) as GameObject;
	gobj.name = gobj.name.Replace("(Clone)", string.Empty);
	return gobj;
}

public static GameObject InsGobj(Object original, Vector3 position, Quaternion rotation)
{
	GameObject gobj = Instantiate(original, position, rotation) as GameObject;
	gobj.name = gobj.name.Replace("(Clone)", string.Empty);
	return gobj;
}

public static GameObject InsGobj(Object original, Vector3 position, Quaternion rotation, Transform parent)
{
	GameObject gobj = Instantiate(original, position, rotation, parent) as GameObject;
	gobj.name = gobj.name.Replace("(Clone)", string.Empty);
	return gobj;
}

public static Texture2D CropTexture(Texture2D texture, Vector2 coords)
{
    Texture2D croppedTexture = new Texture2D((int)coords.x, (int)coords.y);
    croppedTexture.SetPixels(texture.GetPixels((int)coords.x, (int)coords.y, (int)coords.x, (int)coords.y));
    croppedTexture.Apply();
    return croppedTexture;
}

public static T GetClass<T>(string path) where T: MonoBehaviour
{
	return (T)Resources.Load<GameObject>(path).GetComponent<T>();
}

public static void SpitOutList(string[] lines, string fileName = "nothing")
{
	if (fileName == "nothing")
	{
		fileName = "aRandomListFile" + UnityEngine.Random.Range(0, 999999999) + UnityEngine.Random.Range(0, 999999999) + UnityEngine.Random.Range(0, 999999999) + UnityEngine.Random.Range(0, 999999999);
	}
	if (!fileName.Contains("."))
	{
		fileName += ".txt";
	}
	File.Create(Application.streamingAssetsPath + "/" + fileName).Dispose();
	File.WriteAllLines(Application.streamingAssetsPath + "/" + fileName, lines);
}

}
