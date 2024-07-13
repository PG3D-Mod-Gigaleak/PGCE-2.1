using System;
using UnityEngine;

namespace FlyingText3D
{
	[Serializable]
	public class FontData
	{
		public TextAsset ttfFile;

		public string fontName;

		public FontData()
		{
			ttfFile = null;
			fontName = "";
		}
	}
}
