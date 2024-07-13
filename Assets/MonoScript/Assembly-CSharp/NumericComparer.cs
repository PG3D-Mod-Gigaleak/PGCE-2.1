using System.Collections;
using UnityEngine;

public class NumericComparer : IComparer
{
	private static int baseLngth = "multi_skin_".Length;

	public int Compare(object x, object y)
	{
		Texture xt = x as Texture;
		Texture yt = y as Texture;
		string name = xt.name;
		string name2 = yt.name;
		name = name.Substring(baseLngth);
		name2 = name2.Substring(baseLngth);
		int num = int.Parse(name);
		int num2 = int.Parse(name2);
		return num - num2;
	}
	public int Compare(Texture x, Texture y)
	{
		string name = x.name;
		string name2 = y.name;
		name = name.Substring(baseLngth);
		name2 = name2.Substring(baseLngth);
		int num = int.Parse(name);
		int num2 = int.Parse(name2);
		return num - num2;
	}
}
