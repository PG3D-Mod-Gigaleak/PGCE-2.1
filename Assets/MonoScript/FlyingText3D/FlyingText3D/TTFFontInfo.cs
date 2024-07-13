using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace FlyingText3D
{
	public class TTFFontInfo
	{
		private const byte ON_CURVE = 1;

		private const byte X_SHORT = 2;

		private const byte Y_SHORT = 4;

		private const byte REPEAT = 8;

		private const byte X_SAME = 16;

		private const byte Y_SAME = 32;

		private const byte HORIZONTAL = 1;

		private const byte MINIMUM = 2;

		private const byte CROSS_STREAM = 4;

		private const byte OVERRIDE = 8;

		private const byte KERN_VERTICAL = 128;

		private const ushort ARG_1_AND_2_ARE_WORDS = 1;

		private const ushort ARGS_ARE_XY_VALUES = 2;

		private const ushort ROUND_XY_TO_GRID = 4;

		private const ushort WE_HAVE_A_SCALE = 8;

		private const ushort MORE_COMPONENTS = 32;

		private const ushort WE_HAVE_AN_X_AND_Y_SCALE = 64;

		private const ushort WE_HAVE_A_TWO_BY_TWO = 128;

		private const ushort WE_HAVE_INSTRUCTIONS = 256;

		private const ushort USE_MY_METRICS = 512;

		private byte[] _ttfData;

		private bool _isAvailable;

		private string _name;

		private int _unitsPerEm;

		private int _ascent;

		private int _descent;

		private int _lineHeight;

		private int _fontXMin;

		private int _fontYMin;

		private int _fontXMax;

		private int _fontYMax;

		private uint _glyphDataOffset;

		private int _cmapFormat;

		private int[] _glyphIndexArray;

		private int[] _endCodeArray;

		private int[] _startCodeArray;

		private int[] _idDeltaArray;

		private int[] _idRangeOffsetArray;

		private int[] _glyphIdArray;

		private uint[] _locationIndexArray;

		private int[] _advanceArray;

		private bool _hasKerning;

		private Dictionary<KernPair, short> _kernDictionary;

		private Dictionary<char, GlyphData> _glyphDictionary;

		public string name
		{
			get
			{
				return _name;
			}
		}

		public int unitsPerEm
		{
			get
			{
				return _unitsPerEm;
			}
		}

		public int lineHeight
		{
			get
			{
				return _lineHeight;
			}
		}

		public int fontXMin
		{
			get
			{
				return _fontXMin;
			}
		}

		public int fontYMin
		{
			get
			{
				return _fontYMin;
			}
		}

		public int fontXMax
		{
			get
			{
				return _fontXMax;
			}
		}

		public int fontYMax
		{
			get
			{
				return _fontYMax;
			}
		}

		public int[] advanceArray
		{
			get
			{
				return _advanceArray;
			}
		}

		public bool hasKerning
		{
			get
			{
				return _hasKerning;
			}
		}

		public Dictionary<KernPair, short> kernDictionary
		{
			get
			{
				return _kernDictionary;
			}
		}

		public Dictionary<char, GlyphData> glyphDictionary
		{
			get
			{
				return _glyphDictionary;
			}
		}

		public TTFFontInfo(byte[] ttfData)
		{
			//Discarded unreachable code: IL_080d
			_ttfData = ttfData;
			_glyphDictionary = new Dictionary<char, GlyphData>();
			try
			{
				if (_ttfData.Length < 10)
				{
					throw new Exception("No data found in file");
				}
				uint idx = 0u;
				uint length = 0u;
				uint @uint = GetUint(_ttfData, ref idx);
				if (@uint != 65536 && @uint != 1953658213)
				{
					throw new Exception("Not a supported TTF file");
				}
				bool flag = @uint == 1953658213;
				if (!TagSearch(_ttfData, "name", ref idx, ref length))
				{
					throw new Exception("name tag not found");
				}
				_name = GetName(_ttfData, ref idx);
				if (_name == "")
				{
					throw new Exception("Name not retrieved");
				}
				if (!TagSearch(_ttfData, "cmap", ref idx, ref length))
				{
					throw new Exception("cmap tag not found");
				}
				uint num = idx;
				idx += 2;
				int @ushort = GetUshort(_ttfData, ref idx);
				int[] array = new int[@ushort];
				int[] array2 = new int[@ushort];
				uint[] array3 = new uint[@ushort];
				for (int i = 0; i < @ushort; i++)
				{
					array[i] = GetUshort(_ttfData, ref idx);
					array2[i] = GetUshort(_ttfData, ref idx);
					array3[i] = GetUint(_ttfData, ref idx);
				}
				bool flag2 = false;
				int num2 = Array.IndexOf(array, 0);
				if (num2 != -1 && array2[num2] == 3)
				{
					idx = num + array3[num2];
					int ushort2 = GetUshort(_ttfData, ref idx);
					if (ushort2 == 4)
					{
						flag2 = true;
						SetFormat4(ref idx);
					}
				}
				if (!flag2)
				{
					int num3 = Array.IndexOf(array, 3);
					if (num3 != -1 && array2[num3] == 1)
					{
						idx = num + array3[num3];
						int ushort3 = GetUshort(_ttfData, ref idx);
						if (ushort3 == 4)
						{
							flag2 = true;
							SetFormat4(ref idx);
						}
					}
				}
				if (!flag2)
				{
					int num4 = Array.IndexOf(array, 1);
					if (num4 != -1 && array2[num4] == 0)
					{
						idx = num + array3[num4];
						switch (GetUshort(_ttfData, ref idx))
						{
						case 0:
						{
							flag2 = true;
							_cmapFormat = 0;
							_glyphIndexArray = new int[256];
							int num5 = 0;
							for (uint num6 = idx + 4; num6 < idx + 260; num6++)
							{
								_glyphIndexArray[num5++] = _ttfData[num6];
							}
							break;
						}
						case 6:
						{
							flag2 = true;
							_cmapFormat = 6;
							idx += 4;
							ushort ushort4 = GetUshort(_ttfData, ref idx);
							int ushort5 = GetUshort(_ttfData, ref idx);
							_glyphIndexArray = new int[ushort5];
							for (int j = 0; j < ushort5; j++)
							{
								_glyphIndexArray[j] = GetUshort(_ttfData, ref idx) - ushort4;
							}
							break;
						}
						}
					}
				}
				if (!flag2)
				{
					throw new Exception("Didn't get cmap index array");
				}
				if (!TagSearch(_ttfData, "head", ref idx, ref length))
				{
					throw new Exception("head tag not found");
				}
				idx += 18;
				_unitsPerEm = GetUshort(_ttfData, ref idx);
				idx += 16;
				_fontXMin = GetShort(_ttfData, ref idx);
				_fontYMin = GetShort(_ttfData, ref idx);
				_fontXMax = GetShort(_ttfData, ref idx);
				_fontYMax = GetShort(_ttfData, ref idx);
				idx += 6;
				int @short = GetShort(_ttfData, ref idx);
				if (!TagSearch(_ttfData, "maxp", ref idx, ref length))
				{
					throw new Exception("maxp tag not found");
				}
				idx += 4;
				int ushort6 = GetUshort(_ttfData, ref idx);
				_locationIndexArray = new uint[ushort6];
				if (!TagSearch(_ttfData, "loca", ref idx, ref length))
				{
					throw new Exception("loca tag not found");
				}
				if (@short == 0)
				{
					for (int k = 0; k < ushort6; k++)
					{
						_locationIndexArray[k] = (uint)(GetUshort(_ttfData, ref idx) * 2);
					}
				}
				else
				{
					for (int l = 0; l < ushort6; l++)
					{
						_locationIndexArray[l] = GetUint(_ttfData, ref idx);
					}
				}
				if (!TagSearch(_ttfData, "glyf", ref _glyphDataOffset, ref length))
				{
					throw new Exception("glyf tag not found");
				}
				if (!TagSearch(_ttfData, "hhea", ref idx, ref length))
				{
					throw new Exception("hhea tag not found");
				}
				idx += 4;
				_ascent = GetShort(_ttfData, ref idx);
				_descent = GetShort(_ttfData, ref idx);
				int short2 = GetShort(_ttfData, ref idx);
				idx += 24;
				int short3 = GetShort(_ttfData, ref idx);
				if (TagSearch(_ttfData, "OS/2", ref idx, ref length) && length > 68)
				{
					idx += 68;
					_ascent = GetShort(_ttfData, ref idx);
					_descent = GetShort(_ttfData, ref idx);
					short2 = GetShort(_ttfData, ref idx);
				}
				_lineHeight = _ascent - _descent + short2;
				if (!TagSearch(_ttfData, "hmtx", ref idx, ref length))
				{
					throw new Exception("hmtx tag not found");
				}
				if (short3 > 10)
				{
					_advanceArray = new int[short3];
					for (int m = 0; m < short3; m++)
					{
						_advanceArray[m] = GetUshort(_ttfData, ref idx);
						idx += 2;
					}
				}
				else
				{
					_advanceArray = new int[ushort6];
					int ushort7 = GetUshort(_ttfData, ref idx);
					for (int n = 0; n < ushort6; n++)
					{
						_advanceArray[n] = ushort7;
					}
				}
				_hasKerning = false;
				if (TagSearch(_ttfData, "kern", ref idx, ref length))
				{
					bool flag3 = false;
					byte b = 0;
					if (flag)
					{
						idx += 4;
						int uint2 = (int)GetUint(_ttfData, ref idx);
						for (int num7 = 0; num7 < uint2; num7++)
						{
							length = GetUint(_ttfData, ref idx);
							byte b2 = _ttfData[idx++];
							b = _ttfData[idx++];
							idx += 2;
							if (b == 0)
							{
								if ((b2 & 0x80) == 0)
								{
									flag3 = true;
								}
								break;
							}
							idx += length - 8;
						}
					}
					else
					{
						idx += 2;
						int ushort8 = GetUshort(_ttfData, ref idx);
						for (int num8 = 0; num8 < ushort8; num8++)
						{
							idx += 2;
							length = GetUshort(_ttfData, ref idx);
							b = _ttfData[idx++];
							byte b3 = _ttfData[idx++];
							if (b == 0)
							{
								if ((b3 & 2) == 0)
								{
									flag3 = true;
								}
								break;
							}
							idx += length - 6;
						}
					}
					if (b == 0 && flag3)
					{
						_hasKerning = true;
						int ushort9 = GetUshort(_ttfData, ref idx);
						idx += 6;
						_kernDictionary = new Dictionary<KernPair, short>(ushort9);
						for (int num9 = 0; num9 < ushort9; num9++)
						{
							_kernDictionary.Add(new KernPair(GetUshort(_ttfData, ref idx), GetUshort(_ttfData, ref idx)), GetShort(_ttfData, ref idx));
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.Message);
				_isAvailable = false;
				return;
			}
			_isAvailable = true;
		}

		public bool SetGlyphData(char character)
		{
			//Discarded unreachable code: IL_03d3, IL_0426
			if (!_isAvailable)
			{
				Debug.LogError("Font information not available");
				return false;
			}
			if (_glyphDictionary.ContainsKey(character))
			{
				return true;
			}
			int num = Convert.ToInt32(character);
			try
			{
				uint glyphDataOffset = _glyphDataOffset;
				int num2 = 0;
				if (_cmapFormat == 4)
				{
					int num3 = _endCodeArray.Length;
					for (int i = 0; i < num3; i++)
					{
						if (_endCodeArray[i] >= num && _startCodeArray[i] <= num)
						{
							if (_idRangeOffsetArray[i] == 0)
							{
								num2 = _idDeltaArray[i] + num;
								break;
							}
							int num4 = (_idRangeOffsetArray[i] + 2 * (num - _startCodeArray[i])) / 2 - (num3 - i);
							num2 = _glyphIdArray[num4];
							break;
						}
					}
				}
				else if (_cmapFormat == 6)
				{
					num2 = _glyphIndexArray[num];
				}
				else if (_cmapFormat == 0)
				{
					if (num > 255)
					{
						Debug.LogWarning("Character code " + num + " (" + Convert.ToChar(num) + ") not found in font");
						_glyphDictionary.Add(character, new GlyphData(null, null, 0, 0, 0, 0, _unitsPerEm, num2));
						return true;
					}
					num2 = _glyphIndexArray[num];
				}
				if (num2 >= 1 && num2 <= 3)
				{
					_glyphDictionary.Add(character, new GlyphData(null, null, 0, 0, 0, 0, _unitsPerEm, num2));
					return true;
				}
				glyphDataOffset += _locationIndexArray[num2];
				int @short = GetShort(_ttfData, ref glyphDataOffset);
				int short2 = GetShort(_ttfData, ref glyphDataOffset);
				int short3 = GetShort(_ttfData, ref glyphDataOffset);
				int short4 = GetShort(_ttfData, ref glyphDataOffset);
				int short5 = GetShort(_ttfData, ref glyphDataOffset);
				if (@short > 0)
				{
					List<Vector2[]> pointsList = new List<Vector2[]>(@short);
					List<bool[]> onCurvesList = new List<bool[]>(@short);
					ReadGlyphData(glyphDataOffset, @short, pointsList, onCurvesList, 0, 0);
					_glyphDictionary.Add(character, new GlyphData(pointsList, onCurvesList, short2, short3, short4, short5, _unitsPerEm, num2));
					return true;
				}
				if (@short < 0)
				{
					uint idx = glyphDataOffset;
					List<Vector2[]> pointsList2 = new List<Vector2[]>();
					List<bool[]> onCurvesList2 = new List<bool[]>();
					int glyphIndex = num2;
					int xOffset = 0;
					int yOffset = 0;
					ushort @ushort;
					do
					{
						@ushort = GetUshort(_ttfData, ref idx);
						num2 = GetUshort(_ttfData, ref idx);
						if ((@ushort & 0x200u) != 0)
						{
							glyphIndex = num2;
						}
						if (((uint)@ushort & (true ? 1u : 0u)) != 0)
						{
							if ((@ushort & 2u) != 0)
							{
								xOffset = GetShort(_ttfData, ref idx);
								yOffset = GetShort(_ttfData, ref idx);
							}
						}
						else if ((@ushort & 2u) != 0)
						{
							xOffset = (sbyte)_ttfData[idx++];
							yOffset = (sbyte)_ttfData[idx++];
						}
						if ((@ushort & 8u) != 0)
						{
							idx += 2;
						}
						else if ((@ushort & 0x40u) != 0)
						{
							idx += 4;
						}
						else if ((@ushort & 0x80u) != 0)
						{
							idx += 8;
						}
						glyphDataOffset = _glyphDataOffset + _locationIndexArray[num2];
						@short = GetShort(_ttfData, ref glyphDataOffset);
						glyphDataOffset += 8;
						ReadGlyphData(glyphDataOffset, @short, pointsList2, onCurvesList2, xOffset, yOffset);
					}
					while ((@ushort & 0x20u) != 0);
					_glyphDictionary.Add(character, new GlyphData(pointsList2, onCurvesList2, short2, short3, short4, short5, _unitsPerEm, glyphIndex));
					return true;
				}
				_glyphDictionary.Add(character, new GlyphData(null, null, 0, 0, 0, 0, _unitsPerEm, num2));
				return true;
			}
			catch (Exception ex)
			{
				Debug.LogError("charCode: " + num + " (" + character + "), " + ex.Message);
				return false;
			}
		}

		private void ReadGlyphData(uint idx, int numberOfContours, List<Vector2[]> pointsList, List<bool[]> onCurvesList, int xOffset, int yOffset)
		{
			ushort[] ushortArray = GetUshortArray(_ttfData, ref idx, numberOfContours);
			uint @ushort = GetUshort(_ttfData, ref idx);
			idx += @ushort;
			int num = ushortArray[numberOfContours - 1] + 1;
			List<bool> list = new List<bool>(num);
			List<byte> list2 = new List<byte>(num);
			int num2 = 0;
			while (num2 < num)
			{
				byte b = _ttfData[idx++];
				list2.Add(b);
				list.Add((b & 1) != 0);
				num2++;
				if ((b & 8u) != 0)
				{
					int num3 = _ttfData[idx++];
					for (int i = 0; i < num3; i++)
					{
						list2.Add(b);
						list.Add((b & 1) != 0);
						num2++;
					}
				}
			}
			bool[] array = list.ToArray();
			byte[] array2 = list2.ToArray();
			Vector2[] array3 = new Vector2[num];
			int num4 = 0;
			for (int j = 0; j < num; j++)
			{
				int num5 = 0;
				bool flag = (array2[j] & 2) != 0;
				bool flag2 = (array2[j] & 0x10) != 0;
				if (flag)
				{
					num5 = ((!flag2) ? (-_ttfData[idx++]) : _ttfData[idx++]);
				}
				else if (!flag2)
				{
					num5 = GetShort(_ttfData, ref idx);
				}
				num4 += num5;
				array3[j].x = num4 + xOffset;
			}
			num4 = 0;
			for (int k = 0; k < num; k++)
			{
				int num6 = 0;
				bool flag3 = (array2[k] & 4) != 0;
				bool flag4 = (array2[k] & 0x20) != 0;
				if (flag3)
				{
					num6 = ((!flag4) ? (-_ttfData[idx++]) : _ttfData[idx++]);
				}
				else if (!flag4)
				{
					num6 = GetShort(_ttfData, ref idx);
				}
				num4 += num6;
				array3[k].y = num4 + yOffset;
			}
			if (numberOfContours > 1)
			{
				int num7 = 0;
				for (int l = 0; l < numberOfContours; l++)
				{
					int num8 = ushortArray[l] + 1 - num7;
					Vector2[] array4 = new Vector2[num8];
					bool[] array5 = new bool[num8];
					Array.Copy(array3, num7, array4, 0, num8);
					Array.Copy(array, num7, array5, 0, num8);
					pointsList.Add(array4);
					onCurvesList.Add(array5);
					num7 = ushortArray[l] + 1;
				}
			}
			else
			{
				pointsList.Add(array3);
				onCurvesList.Add(array);
			}
		}

		private void SetFormat4(ref uint idx)
		{
			_cmapFormat = 4;
			int @ushort = GetUshort(_ttfData, ref idx);
			idx += 2u;
			int num = (int)GetUshort(_ttfData, ref idx) / 2;
			idx += 6u;
			_endCodeArray = GetUshortToIntArray(_ttfData, ref idx, num);
			idx += 2u;
			_startCodeArray = GetUshortToIntArray(_ttfData, ref idx, num);
			_idDeltaArray = GetShortToIntArray(_ttfData, ref idx, num);
			_idRangeOffsetArray = GetUshortToIntArray(_ttfData, ref idx, num);
			int arrayLength = (@ushort - (16 + 4 * (num * 2))) / 2;
			_glyphIdArray = GetUshortToIntArray(_ttfData, ref idx, arrayLength);
		}

		public static string GetFontName(byte[] fontBytes)
		{
			uint idx = 0u;
			uint length = 0u;
			uint @uint = GetUint(fontBytes, ref idx);
			if (@uint != 65536 && @uint != 1953658213)
			{
				return "Not a supported TTF file";
			}
			if (TagSearch(fontBytes, "name", ref idx, ref length))
			{
				return GetName(fontBytes, ref idx);
			}
			return "";
		}

		private static string GetName(byte[] ttfData, ref uint idx)
		{
			uint num = idx;
			idx += 2u;
			int @ushort = GetUshort(ttfData, ref idx);
			uint num2 = GetUshort(ttfData, ref idx) + num;
			string result = "";
			for (int i = 0; i < @ushort; i++)
			{
				int ushort2 = GetUshort(ttfData, ref idx);
				int ushort3 = GetUshort(ttfData, ref idx);
				idx += 2u;
				int ushort4 = GetUshort(ttfData, ref idx);
				int ushort5 = GetUshort(ttfData, ref idx);
				int ushort6 = GetUshort(ttfData, ref idx);
				if (ushort4 == 4 && ushort2 == 1 && ushort3 == 0)
				{
					idx = (uint)(num2 + ushort6);
					result = new ASCIIEncoding().GetString(GetByteArray(ttfData, ref idx, ushort5));
					break;
				}
				if (ushort4 == 4 && ushort2 == 3 && ushort3 == 1)
				{
					idx = (uint)(num2 + ushort6);
					result = new UnicodeEncoding().GetString(GetLittleEndianByteArray(ttfData, ref idx, ushort5));
					break;
				}
			}
			return result;
		}

		private static bool TagSearch(byte[] ttfData, string tag, ref uint offset, ref uint length)
		{
			uint idx = 4u;
			int @ushort = GetUshort(ttfData, ref idx);
			idx = 12u;
			for (int i = 0; i < @ushort; i++)
			{
				if (GetTag(ttfData, ref idx) == tag)
				{
					idx += 4;
					offset = GetUint(ttfData, ref idx);
					length = GetUint(ttfData, ref idx);
					return true;
				}
				idx += 12;
			}
			return false;
		}

		private static string GetTag(byte[] ttfData, ref uint idx)
		{
			if (idx < ttfData.Length)
			{
				char[] value = new char[4]
				{
					(char)ttfData[idx],
					(char)ttfData[idx + 1],
					(char)ttfData[idx + 2],
					(char)ttfData[idx + 3]
				};
				idx += 4u;
				return new string(value);
			}
			return null;
		}

		private static uint GetUint(byte[] ttfData, ref uint idx)
		{
			uint result = (uint)((ttfData[idx] << 24) | (ttfData[idx + 1] << 16) | (ttfData[idx + 2] << 8) | ttfData[idx + 3]);
			idx += 4u;
			return result;
		}

		private static ushort GetUshort(byte[] ttfData, ref uint idx)
		{
			ushort result = (ushort)((ttfData[idx] << 8) | ttfData[idx + 1]);
			idx += 2u;
			return result;
		}

		private static short GetShort(byte[] ttfData, ref uint idx)
		{
			short result = (short)((ttfData[idx] << 8) | ttfData[idx + 1]);
			idx += 2u;
			return result;
		}

		private static ushort[] GetUshortArray(byte[] ttfData, ref uint idx, int arrayLength)
		{
			ushort[] array = new ushort[arrayLength];
			for (int i = 0; i < arrayLength; i++)
			{
				array[i] = (ushort)((ttfData[idx] << 8) | ttfData[idx + 1]);
				idx += 2u;
			}
			return array;
		}

		private static int[] GetUshortToIntArray(byte[] ttfData, ref uint idx, int arrayLength)
		{
			int[] array = new int[arrayLength];
			for (int i = 0; i < arrayLength; i++)
			{
				array[i] = (ushort)((ttfData[idx] << 8) | ttfData[idx + 1]);
				idx += 2u;
			}
			return array;
		}

		private static int[] GetShortToIntArray(byte[] ttfData, ref uint idx, int arrayLength)
		{
			int[] array = new int[arrayLength];
			for (int i = 0; i < arrayLength; i++)
			{
				array[i] = (short)((ttfData[idx] << 8) | ttfData[idx + 1]);
				idx += 2u;
			}
			return array;
		}

		private static short[] GetShortArray(byte[] ttfData, ref uint idx, int arrayLength)
		{
			short[] array = new short[arrayLength];
			for (int i = 0; i < arrayLength; i++)
			{
				array[i] = (short)((ttfData[idx] << 8) | ttfData[idx + 1]);
				idx += 2u;
			}
			return array;
		}

		private static byte[] GetByteArray(byte[] ttfData, ref uint idx, int arrayLength)
		{
			byte[] array = new byte[arrayLength];
			Array.Copy(ttfData, idx, array, 0L, arrayLength);
			idx += (uint)arrayLength;
			return array;
		}

		private static byte[] GetLittleEndianByteArray(byte[] ttfData, ref uint idx, int arrayLength)
		{
			byte[] array = new byte[arrayLength];
			for (int i = 0; i < arrayLength; i += 2)
			{
				array[i + 1] = ttfData[idx];
				array[i] = ttfData[idx + 1];
				idx += 2u;
			}
			return array;
		}
	}
}
