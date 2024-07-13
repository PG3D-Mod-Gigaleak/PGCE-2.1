using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class SkinImporter : MonoBehaviour
{
    private static Color GetSkinColor(string skinData)
    {
        string[] colorNums = skinData.Split(' ')[0].Split(new string[]{"c:"}, StringSplitOptions.None)[1].Split(',');
        return new Color(float.Parse(colorNums[0]), float.Parse(colorNums[1]), float.Parse(colorNums[2]), float.Parse(colorNums[3]));
    }

    private static string GetSkinName(string skinData)
    {
        return skinData.Split(new string[]{"n:"}, StringSplitOptions.None)[1];
    }

    public static Texture2D GetStreamingTex(string path, int width = 2, int height = 2, FilterMode filterMode = FilterMode.Point)
    {
        //thank you NitrogenDioxide.
        byte[] x = File.ReadAllBytes(path);
        Texture2D a = new Texture2D(width, height, TextureFormat.RGBA32, false);
        ImageConversion.LoadImage(a, x);
        a.filterMode = filterMode;
        return a;
    }

    public SkinMenu skinMenu;

    public static Skins.Skin[] ImportedSkins()
    {
        List<Skins.Skin> skins = new List<Skins.Skin>();
        string[] skinlist = null;
        try
        {
            skinlist = File.ReadAllLines(Application.streamingAssetsPath + "/skins/skinlist.txt");
        }
        catch
        {
            return skins.ToArray();
        }

        foreach (string skin in skinlist)
        {
            string skinData = File.ReadAllText(Application.streamingAssetsPath + "/skins/" + skin + ".txt");
            Texture skinFile = GetStreamingTex(Application.streamingAssetsPath + "/skins/" + skin + ".png");
            if (skinFile.width != 64 || skinFile.height != 32)
            {
                continue;
            }
            skins.Add(new Skins.Skin(GetSkinName(skinData), skinFile, GetSkinColor(skinData)));
        }

        return skins.ToArray();
    }
}
