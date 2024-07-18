using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EarsHelper : MonoBehaviour
{
	public static Material EarsDynamic(Texture2D skin)
	{
		Material material = new Material(Shader.Find("Legacy Shaders/Diffuse"));
		material.SetTexture("_MainTex", null);
		material.color = GetAverageTopColorInSkin(skin);
		return material;
	}
	public static Color GetAverageTopColorInSkin(Texture2D skin)
	{
		float rAverage = 1, gAverage = 1, bAverage = 1;

		try
		{
			float rSum = 0, gSum = 0, bSum = 0;
			ushort count = 0;

			foreach (Color color in skin.GetPixels(8, skin.height-8, 8, 8))
			{
				rSum += color.r;
				gSum += color.g;
				bSum += color.b;

				count++;
			}

			rAverage = rSum / count;
			gAverage = gSum / count;
			bAverage = bSum / count;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}

		return new Color(rAverage, gAverage, bAverage);
	}
}
