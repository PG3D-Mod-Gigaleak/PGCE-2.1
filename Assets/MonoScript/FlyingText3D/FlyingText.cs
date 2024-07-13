using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using FlyingText3D;
using UnityEngine;

[AddComponentMenu("FlyingText3D/FlyingText")]
public class FlyingText : MonoBehaviour
{
	private const int PREPROCESS = 2;

	private const int BUILDMESH = 1;

	public List<FontData> m_fontData;

	public int m_defaultFont = 0;

	public Material m_defaultMaterial;

	public Material m_defaultEdgeMaterial;

	public bool m_useEdgeMaterial = false;

	public Color m_defaultColor = Color.white;

	public int m_defaultResolution = 5;

	public float m_defaultSize = 2f;

	public float m_defaultDepth = 0.25f;

	public float m_defaultLetterSpacing = 1f;

	public float m_defaultLineSpacing = 1f;

	public float m_defaultLineWidth = 0f;

	public float m_tabStop = 0f;

	public bool m_wordWrap = true;

	public Justify m_defaultJustification = Justify.Left;

	public bool m_verticalLayout = false;

	public bool m_includeBackface = true;

	public bool m_texturePerLetter = true;

	public TextAnchor m_anchor = TextAnchor.UpperLeft;

	public ZAnchor m_zAnchor = ZAnchor.Front;

	public ColliderType m_colliderType = ColliderType.None;

	public bool m_addRigidbodies = false;

	public PhysicMaterial m_physicsMaterial;

	public float m_smoothingAngle = 50f;

	public static int defaultFont;

	public static Material defaultMaterial;

	public static Material defaultEdgeMaterial;

	public static bool useEdgeMaterial = false;

	public static Color defaultColor;

	public static int defaultResolution;

	public static float defaultSize;

	public static float defaultDepth;

	public static float defaultLetterSpacing;

	public static float defaultLineSpacing;

	public static float defaultLineWidth;

	public static float tabStop;

	public static bool wordWrap;

	public static Justify defaultJustification;

	public static bool verticalLayout;

	public static bool includeBackface;

	public static bool texturePerLetter;

	public static TextAnchor anchor;

	public static ZAnchor zAnchor;

	public static ColliderType colliderType;

	public static bool addRigidbodies;

	public static PhysicMaterial physicsMaterial;

	public static float smoothingAngle;

	private static FlyingText _instance;

	private static bool _initialized = false;

	private static bool _noFontsAvailable = false;

	private static TTFFontInfo[] _fontInfo;

	private static string[] _fontNames;

	private static char[] _removeChars = new char[6] { ' ', '\n', '\r', '"', '\'', '\t' };

	private static Dictionary<string, Color32> _colorDictionary;

	private static GameObject[] objectArray;

	public static FlyingText instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = UnityEngine.Object.FindObjectOfType(typeof(FlyingText)) as FlyingText;
			}
			return _instance;
		}
	}

	private void Awake()
	{
		if (UnityEngine.Object.FindObjectsOfType(typeof(FlyingText)).Length > 1)
		{
			UnityEngine.Object.Destroy(this);
		}
		else if (!_initialized)
		{
			Initialize();
		}
	}

	public void Initialize()
	{
		defaultFont = m_defaultFont;
		defaultMaterial = m_defaultMaterial;
		defaultEdgeMaterial = m_defaultEdgeMaterial;
		useEdgeMaterial = m_useEdgeMaterial;
		defaultColor = m_defaultColor;
		defaultResolution = m_defaultResolution;
		defaultSize = m_defaultSize;
		defaultDepth = m_defaultDepth;
		defaultLetterSpacing = m_defaultLetterSpacing;
		defaultLineSpacing = m_defaultLineSpacing;
		defaultLineWidth = m_defaultLineWidth;
		tabStop = m_tabStop;
		wordWrap = m_wordWrap;
		defaultJustification = m_defaultJustification;
		verticalLayout = m_verticalLayout;
		includeBackface = m_includeBackface;
		texturePerLetter = m_texturePerLetter;
		anchor = m_anchor;
		zAnchor = m_zAnchor;
		colliderType = m_colliderType;
		addRigidbodies = m_addRigidbodies;
		physicsMaterial = m_physicsMaterial;
		smoothingAngle = m_smoothingAngle;
		if (defaultMaterial == null)
		{
			SetMaterial(ref defaultMaterial);
		}
		if (defaultEdgeMaterial == null)
		{
			SetMaterial(ref defaultEdgeMaterial);
		}
		if (m_fontData.Count == 0)
		{
			_noFontsAvailable = true;
			_initialized = false;
			return;
		}
		_noFontsAvailable = false;
		_fontInfo = new TTFFontInfo[m_fontData.Count];
		_fontNames = new string[m_fontData.Count];
		for (int i = 0; i < m_fontData.Count; i++)
		{
			if (m_fontData[i].ttfFile != null)
			{
				_fontInfo[i] = new TTFFontInfo(m_fontData[i].ttfFile.bytes);
				string text = _fontInfo[i].name;
				text = text.Replace(" ", "");
				text = text.ToLower();
				_fontNames[i] = text;
			}
		}
		Dictionary<string, Color32> dictionary = new Dictionary<string, Color32>();
		dictionary.Add("red", Color.red);
		dictionary.Add("green", Color.green);
		dictionary.Add("blue", Color.blue);
		dictionary.Add("white", Color.white);
		dictionary.Add("black", Color.black);
		dictionary.Add("yellow", Color.yellow);
		dictionary.Add("cyan", Color.cyan);
		dictionary.Add("magenta", Color.magenta);
		dictionary.Add("gray", Color.gray);
		dictionary.Add("grey", Color.grey);
		_colorDictionary = dictionary;
		UnityEngine.Object.DontDestroyOnLoad(this);
		_initialized = true;
	}

	private void SetMaterial(ref Material thisMaterial)
	{
		Material material = Resources.Load("VertexColored") as Material;
		if ((bool)material)
		{
			thisMaterial = material;
			return;
		}
		Shader shader = Shader.Find("Diffuse");
		if ((bool)shader)
		{
			thisMaterial = new Material(shader);
		}
	}

	private static bool CheckSetup()
	{
		if (!_initialized)
		{
			if (_noFontsAvailable)
			{
				Debug.LogError("No fonts have been defined. Please add at least one font to the FlyingText3D inspector.");
				return false;
			}
			Debug.LogError("FlyingText hasn't been initialized yet...use script execution order to make sure your Awake functions run first, or use Start");
			return false;
		}
		return true;
	}

	public static void PrimeText(string text)
	{
		if (CheckSetup())
		{
			GetObject(text, defaultMaterial, defaultEdgeMaterial, defaultSize, defaultDepth, defaultResolution, defaultLetterSpacing, defaultLineSpacing, defaultLineWidth, true, true, false, Vector3.zero, Quaternion.identity, null);
		}
	}

	public static void PrimeText(string text, float size, float extrudeDepth, int resolution)
	{
		if (CheckSetup())
		{
			GetObject(text, defaultMaterial, defaultEdgeMaterial, size, extrudeDepth, resolution, defaultLetterSpacing, defaultLineSpacing, defaultLineWidth, true, true, false, Vector3.zero, Quaternion.identity, null);
		}
	}

	public static GameObject GetObject(string text)
	{
		if (!CheckSetup())
		{
			return null;
		}
		return GetObject(text, defaultMaterial, defaultEdgeMaterial, defaultSize, defaultDepth, defaultResolution, defaultLetterSpacing, defaultLineSpacing, defaultLineWidth, false, false, false, Vector3.zero, Quaternion.identity, null);
	}

	public static GameObject GetObject(string text, Material material, Material edgeMaterial, float size, float extrudeDepth, int resolution)
	{
		if (!CheckSetup())
		{
			return null;
		}
		return GetObject(text, material, edgeMaterial, size, extrudeDepth, resolution, defaultLetterSpacing, defaultLineSpacing, defaultLineWidth, false, false, false, Vector3.zero, Quaternion.identity, null);
	}

	public static GameObject GetObject(string text, Material material, Material edgeMaterial, float size, float extrudeDepth, int resolution, float characterSpacing, float lineSpacing, float lineWidth)
	{
		if (!CheckSetup())
		{
			return null;
		}
		return GetObject(text, material, edgeMaterial, size, extrudeDepth, resolution, characterSpacing, lineSpacing, lineWidth, false, false, false, Vector3.zero, Quaternion.identity, null);
	}

	public static GameObject GetObject(string text, Vector3 position, Quaternion rotation)
	{
		if (!CheckSetup())
		{
			return null;
		}
		return GetObject(text, defaultMaterial, defaultEdgeMaterial, defaultSize, defaultDepth, defaultResolution, defaultLetterSpacing, defaultLineSpacing, defaultLineWidth, false, false, false, position, rotation, null);
	}

	public static GameObject GetObject(string text, Material material, Material edgeMaterial, float size, float extrudeDepth, int resolution, Vector3 position, Quaternion rotation)
	{
		if (!CheckSetup())
		{
			return null;
		}
		return GetObject(text, material, edgeMaterial, size, extrudeDepth, resolution, defaultLetterSpacing, defaultLineSpacing, defaultLineWidth, false, false, false, position, rotation, null);
	}

	public static GameObject GetObject(string text, Material material, Material edgeMaterial, float size, float extrudeDepth, int resolution, float characterSpacing, float lineSpacing, float lineWidth, Vector3 position, Quaternion rotation)
	{
		if (!CheckSetup())
		{
			return null;
		}
		return GetObject(text, material, edgeMaterial, size, extrudeDepth, resolution, characterSpacing, lineSpacing, lineWidth, false, false, false, position, rotation, null);
	}

	public static GameObject GetObjects(string text)
	{
		if (!CheckSetup())
		{
			return null;
		}
		return GetObject(text, defaultMaterial, defaultEdgeMaterial, defaultSize, defaultDepth, defaultResolution, defaultLetterSpacing, defaultLineSpacing, defaultLineWidth, false, true, false, Vector3.zero, Quaternion.identity, null);
	}

	public static GameObject GetObjects(string text, Material material, Material edgeMaterial, float size, float extrudeDepth, int resolution)
	{
		if (!CheckSetup())
		{
			return null;
		}
		return GetObject(text, material, edgeMaterial, size, extrudeDepth, resolution, defaultLetterSpacing, defaultLineSpacing, defaultLineWidth, false, true, false, Vector3.zero, Quaternion.identity, null);
	}

	public static GameObject GetObjects(string text, Material material, Material edgeMaterial, float size, float extrudeDepth, int resolution, float characterSpacing, float lineSpacing, float lineWidth)
	{
		if (!CheckSetup())
		{
			return null;
		}
		return GetObject(text, material, edgeMaterial, size, extrudeDepth, resolution, characterSpacing, lineSpacing, lineWidth, false, true, false, Vector3.zero, Quaternion.identity, null);
	}

	public static GameObject GetObjects(string text, Vector3 position, Quaternion rotation)
	{
		if (!CheckSetup())
		{
			return null;
		}
		return GetObject(text, defaultMaterial, defaultEdgeMaterial, defaultSize, defaultDepth, defaultResolution, defaultLetterSpacing, defaultLineSpacing, defaultLineWidth, false, true, false, position, rotation, null);
	}

	public static GameObject GetObjects(string text, Material material, Material edgeMaterial, float size, float extrudeDepth, int resolution, Vector3 position, Quaternion rotation)
	{
		if (!CheckSetup())
		{
			return null;
		}
		return GetObject(text, material, edgeMaterial, size, extrudeDepth, resolution, defaultLetterSpacing, defaultLineSpacing, defaultLineWidth, false, true, false, position, rotation, null);
	}

	public static GameObject GetObjects(string text, Material material, Material edgeMaterial, float size, float extrudeDepth, int resolution, float characterSpacing, float lineSpacing, float lineWidth, Vector3 position, Quaternion rotation)
	{
		if (!CheckSetup())
		{
			return null;
		}
		return GetObject(text, material, edgeMaterial, size, extrudeDepth, resolution, characterSpacing, lineSpacing, lineWidth, false, true, false, position, rotation, null);
	}

	public static GameObject[] GetObjectsArray(string text)
	{
		if (!CheckSetup())
		{
			return null;
		}
		GetObject(text, defaultMaterial, defaultEdgeMaterial, defaultSize, defaultDepth, defaultResolution, defaultLetterSpacing, defaultLineSpacing, defaultLineWidth, false, true, true, Vector3.zero, Quaternion.identity, null);
		return objectArray;
	}

	public static GameObject[] GetObjectsArray(string text, Material material, Material edgeMaterial, float size, float extrudeDepth, int resolution)
	{
		if (!CheckSetup())
		{
			return null;
		}
		GetObject(text, material, edgeMaterial, size, extrudeDepth, resolution, defaultLetterSpacing, defaultLineSpacing, defaultLineWidth, false, true, true, Vector3.zero, Quaternion.identity, null);
		return objectArray;
	}

	public static GameObject[] GetObjectsArray(string text, Material material, Material edgeMaterial, float size, float extrudeDepth, int resolution, float characterSpacing, float lineSpacing, float lineWidth)
	{
		if (!CheckSetup())
		{
			return null;
		}
		GetObject(text, material, edgeMaterial, size, extrudeDepth, resolution, characterSpacing, lineSpacing, lineWidth, false, true, true, Vector3.zero, Quaternion.identity, null);
		return objectArray;
	}

	public static GameObject[] GetObjectsArray(string text, Vector3 position, Quaternion rotation)
	{
		if (!CheckSetup())
		{
			return null;
		}
		GetObject(text, defaultMaterial, defaultEdgeMaterial, defaultSize, defaultDepth, defaultResolution, defaultLetterSpacing, defaultLineSpacing, defaultLineWidth, false, true, true, position, rotation, null);
		return objectArray;
	}

	public static GameObject[] GetObjectsArray(string text, Material material, Material edgeMaterial, float size, float extrudeDepth, int resolution, Vector3 position, Quaternion rotation)
	{
		if (!CheckSetup())
		{
			return null;
		}
		GetObject(text, material, edgeMaterial, size, extrudeDepth, resolution, defaultLetterSpacing, defaultLineSpacing, defaultLineWidth, false, true, true, position, rotation, null);
		return objectArray;
	}

	public static GameObject[] GetObjectsArray(string text, Material material, Material edgeMaterial, float size, float extrudeDepth, int resolution, float characterSpacing, float lineSpacing, float lineWidth, Vector3 position, Quaternion rotation)
	{
		if (!CheckSetup())
		{
			return null;
		}
		GetObject(text, material, edgeMaterial, size, extrudeDepth, resolution, characterSpacing, lineSpacing, lineWidth, false, true, true, position, rotation, null);
		return objectArray;
	}

	public static void UpdateObject(GameObject go, string text)
	{
		if (go == null)
		{
			Debug.LogError("UpdateObject can't use a null GameObject");
		}
		else
		{
			GetObject(text, defaultMaterial, defaultEdgeMaterial, defaultSize, defaultDepth, defaultResolution, defaultLetterSpacing, defaultLineSpacing, defaultLineWidth, false, false, false, Vector3.zero, Quaternion.identity, go);
		}
	}

	private static GameObject GetObject(string s, Material material, Material edgeMaterial, float size, float extrudeDepth, int resolution, float characterSpacing, float lineSpacing, float lineWidth, bool prime, bool separateObjects, bool useObjectsArray, Vector3 position, Quaternion rotation, GameObject gObject)
	{
		if (!_initialized)
		{
			Debug.LogError("FlyingText: No font information available");
			return null;
		}
		if (s == null)
		{
			Debug.LogError("FlyingText: String can't be null");
			return null;
		}
		bool flag = gObject != null;
		Mesh mesh;
		bool flag2;
		if (flag)
		{
			MeshFilter component = gObject.GetComponent<MeshFilter>();
			if (component == null)
			{
				Debug.LogError("The GameObject must have a MeshFilter component");
				return null;
			}
			mesh = component.mesh;
			flag2 = mesh.subMeshCount == 2;
			TextObjectData component2 = gObject.GetComponent<TextObjectData>();
			if (component2 == null)
			{
				Debug.LogError("The GameObject must have a TextObjectData component");
				return null;
			}
			component2.InitializeData(ref size, ref extrudeDepth, ref resolution, ref characterSpacing, ref lineSpacing, ref lineWidth);
		}
		else
		{
			mesh = null;
			flag2 = useEdgeMaterial && extrudeDepth > 0f;
			if (material == null)
			{
				material = defaultMaterial;
			}
			if (edgeMaterial == null)
			{
				edgeMaterial = defaultEdgeMaterial;
			}
		}
		Material[] sharedMaterials = ((!flag2 || flag) ? null : new Material[2] { material, edgeMaterial });
		if (resolution < 1)
		{
			resolution = 1;
		}
		if (size < 0.001f)
		{
			size = 0.001f;
		}
		if (extrudeDepth < 0f)
		{
			extrudeDepth = 0f;
		}
		if (lineWidth < 0f)
		{
			lineWidth = 0f;
		}
		if (tabStop < 0f)
		{
			tabStop = 0f;
		}
		bool flag3 = wordWrap;
		if (verticalLayout)
		{
			flag3 = false;
			lineWidth = 0f;
		}
		defaultFont = Mathf.Clamp(defaultFont, 0, _fontInfo.Length - 1);
		int num = 0;
		List<CommandData> commandData;
		List<char> list = ParseString(s, out commandData);
		List<int> list2 = new List<int>(list.Count);
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		bool flag4 = extrudeDepth > 0f;
		float num5;
		float num6;
		if (characterSpacing > 0f)
		{
			num5 = ((!(characterSpacing < 1f)) ? 1f : characterSpacing);
			num6 = ((!(characterSpacing > 1f)) ? 0f : (characterSpacing - 1f));
		}
		else
		{
			num5 = ((!(characterSpacing > -1f)) ? 1f : (0f - characterSpacing));
			num6 = ((!(characterSpacing < -1f)) ? 0f : (characterSpacing + 1f));
		}
		TTFFontInfo tTFFontInfo = _fontInfo[defaultFont];
		int num7 = defaultFont;
		for (int i = 0; i < list.Count; i++)
		{
			CommandData commandData2 = commandData[num];
			while (commandData2.index == i)
			{
				if (commandData2.command == Command.Font)
				{
					int num8 = (int)commandData2.data;
					if (num8 >= 0 && num8 < _fontInfo.Length)
					{
						num7 = num8;
						tTFFontInfo = _fontInfo[num7];
					}
				}
				commandData2 = commandData[++num];
			}
			if (tTFFontInfo == null)
			{
				Debug.LogError("Font is null");
				return null;
			}
			char c = list[i];
			if (c == '\0')
			{
				continue;
			}
			if (!tTFFontInfo.glyphDictionary.ContainsKey(c) && !tTFFontInfo.SetGlyphData(c))
			{
				return null;
			}
			GlyphData glyphData = tTFFontInfo.glyphDictionary[c];
			list2.Add(glyphData.glyphIndex);
			if (!glyphData.isVisibleChar)
			{
				continue;
			}
			if (glyphData.resolution != resolution && !glyphData.SetMeshData(resolution))
			{
				Debug.LogWarning("Triangulation failed for char code " + Convert.ToInt32(c) + " (" + c + ")");
				continue;
			}
			if (!glyphData.triDataComputed || glyphData.useSubmesh != flag2 || glyphData.useBack != includeBackface)
			{
				if (!flag4)
				{
					glyphData.SetFrontTriData();
				}
				else if (includeBackface)
				{
					glyphData.SetTriData(flag2);
				}
				else
				{
					glyphData.SetFrontAndEdgeTriData(flag2);
				}
			}
			if (!separateObjects)
			{
				num2 += glyphData.vertexCount;
				num3 += glyphData.triCount;
				if (flag2)
				{
					num4 += glyphData.triCount2;
				}
			}
		}
		if (num2 > 65534)
		{
			Debug.LogError("Too many vertices...use fewer characters or reduce resolution");
			return null;
		}
		if (prime)
		{
			return null;
		}
		GameObject gameObject = ((!separateObjects || useObjectsArray || flag) ? null : new GameObject());
		List<GameObject> list3 = ((!useObjectsArray) ? null : new List<GameObject>());
		Color32 color = defaultColor;
		bool flag5 = false;
		if (color == Color.white)
		{
			for (int j = 0; j < commandData.Count; j++)
			{
				if (commandData[j].command == Command.Color && (Color32)commandData[j].data != Color.white)
				{
					flag5 = true;
					break;
				}
			}
		}
		else
		{
			flag5 = true;
		}
		Vector3[] array = new Vector3[num2];
		int[] array2 = new int[num3];
		int[] array3 = new int[num4];
		Vector2[] array4 = new Vector2[num2];
		Color32[] array5 = new Color32[num2];
		int num9 = 0;
		int num10 = 0;
		int num11 = 0;
		float num12 = 1f / (float)tTFFontInfo.unitsPerEm;
		bool flag6 = separateObjects || texturePerLetter;
		KernPair key = default(KernPair);
		float num13 = float.MaxValue;
		float num14 = float.MinValue;
		float num15 = float.MaxValue;
		float num16 = float.MinValue;
		List<float> list4 = new List<float>();
		List<Justify> list5 = new List<Justify>();
		List<EdgeData> list6 = new List<EdgeData>();
		int num17 = 1;
		bool flag7 = false;
		int num18 = 0;
		float num19 = 0f;
		Justify justify = ((!verticalLayout) ? defaultJustification : Justify.Left);
		if (list.Contains('\n'))
		{
			flag7 = true;
			num17 = 2;
		}
		if (lineWidth > 0f)
		{
			num17 = 2;
		}
		while (num17 > 0)
		{
			float num20 = 0f;
			float num21 = 0f;
			float num22 = 0f;
			int num23 = 0;
			float num24 = 0f;
			float num25 = 0f;
			float num26 = size;
			float num27 = size;
			char c2 = ' ';
			char c3 = ' ';
			tTFFontInfo = _fontInfo[defaultFont];
			TTFFontInfo tTFFontInfo2 = tTFFontInfo;
			num = 0;
			int num28 = 0;
			if (num17 == 1 && flag7)
			{
				justify = list5[0];
			}
			CommandData commandData3 = commandData[0];
			CommandData commandData4 = commandData3;
			bool flag8 = true;
			float num29 = 0f;
			for (int k = 0; k < list.Count; k++)
			{
				while (commandData3.index == k)
				{
					switch (commandData3.command)
					{
					case Command.Size:
						num26 = (float)commandData3.data;
						if (num26 < 0.001f)
						{
							num26 = 0.001f;
						}
						break;
					case Command.Color:
						if (num17 == 1)
						{
							color = (Color32)commandData3.data;
						}
						break;
					case Command.Font:
						num7 = (int)commandData3.data;
						if (num7 >= 0 && num7 < _fontInfo.Length)
						{
							tTFFontInfo = _fontInfo[num7];
							num12 = 1f / (float)tTFFontInfo.unitsPerEm;
						}
						break;
					case Command.Zpos:
						if (num17 == 1)
						{
							num25 = (float)commandData3.data;
						}
						break;
					case Command.Depth:
						if (num17 == 1)
						{
							extrudeDepth = (float)commandData3.data;
							if (extrudeDepth < 0f)
							{
								extrudeDepth = 0f;
							}
						}
						break;
					case Command.Space:
						if (!verticalLayout)
						{
							num21 = num20;
							num20 += (float)commandData3.data * num26;
						}
						break;
					case Command.Justify:
						if (num17 == 2 && !verticalLayout)
						{
							justify = (Justify)commandData3.data;
						}
						break;
					}
					commandData3 = commandData[++num];
				}
				float num30 = num12 * num26;
				c3 = c2;
				c2 = list[k];
				if (c2 == '\0')
				{
					continue;
				}
				switch (c2)
				{
				case ' ':
					if (!verticalLayout)
					{
						num22 = num20;
						num27 = num26;
						tTFFontInfo2 = tTFFontInfo;
						num28 = num;
						commandData4 = commandData3;
						num23 = k;
					}
					else
					{
						num24 -= (float)tTFFontInfo.lineHeight * lineSpacing * num30;
					}
					break;
				case '\t':
					if (tabStop > 0f && !verticalLayout)
					{
						num20 = Mathf.Ceil(num20 / tabStop) * tabStop;
						continue;
					}
					break;
				case '\n':
					if (num17 == 2)
					{
						list4.Add(num20);
						list5.Add(justify);
						num22 = 0f;
					}
					else if (++num18 < list5.Count)
					{
						justify = list5[num18];
					}
					if (!verticalLayout)
					{
						num24 -= (float)tTFFontInfo.lineHeight * lineSpacing * num30;
						num20 = 0f;
					}
					else
					{
						num24 = 0f;
						num20 += (num29 + num6 / num12) * (num30 * num5);
						num29 = 0f;
					}
					flag8 = true;
					continue;
				}
				int num31 = list2[k];
				if (tTFFontInfo.hasKerning && !flag8 && !verticalLayout)
				{
					key.left = list2[k - 1];
					key.right = num31;
					if (tTFFontInfo.kernDictionary.ContainsKey(key))
					{
						num20 += (float)tTFFontInfo.kernDictionary[key] * num30;
					}
				}
				GlyphData glyphData2 = tTFFontInfo.glyphDictionary[c2];
				int vertexCount = glyphData2.vertexCount;
				if (vertexCount > 0 && num17 == 1)
				{
					if (verticalLayout && !flag8)
					{
						num24 -= (float)glyphData2.yMax * num30 - (float)glyphData2.yMin * num30 + (float)tTFFontInfo.lineHeight * (lineSpacing - 1f) * num30;
					}
					if (glyphData2.scaleFactor != num30)
					{
						glyphData2.ScaleVertices(num30, flag4, includeBackface);
					}
					if (flag4 && glyphData2.extrudeDepth != extrudeDepth)
					{
						glyphData2.SetExtrudeDepth(extrudeDepth, includeBackface);
					}
					Vector3[] vertices = glyphData2.vertices;
					if (separateObjects)
					{
						array = new Vector3[vertexCount];
						array2 = new int[glyphData2.triCount];
						if (flag2)
						{
							array3 = new int[glyphData2.triCount2];
							num11 = 0;
						}
						array4 = new Vector2[vertexCount];
						if (flag5)
						{
							array5 = new Color32[vertexCount];
						}
						num9 = 0;
						num10 = 0;
					}
					float num32 = (float)glyphData2.xMax * num30 + num20;
					float num33 = (float)glyphData2.xMin * num30 + num20;
					if (num32 > num14)
					{
						num14 = num32;
					}
					if (num33 < num13)
					{
						num13 = num33;
					}
					num32 = (float)tTFFontInfo.fontYMax * num30 + num24;
					num33 = (float)tTFFontInfo.fontYMin * num30 + num24;
					if (num32 > num16)
					{
						num16 = num32;
					}
					if (num33 < num15)
					{
						num15 = num33;
					}
					if (verticalLayout)
					{
						float num34 = glyphData2.xMax - glyphData2.xMin;
						if (num34 > num29)
						{
							num29 = num34;
						}
					}
					if (flag6)
					{
						float num35 = (float)glyphData2.xMax * num30;
						float num36 = (float)glyphData2.xMin * num30;
						float num37 = (float)glyphData2.yMax * num30;
						float num38 = (float)glyphData2.yMin * num30;
						float num39 = num35 - num36;
						float num40 = num37 - num38;
						if (!flag2)
						{
							for (int l = 0; l < vertexCount; l++)
							{
								array4[l + num9].x = (vertices[l].x - num36) / num39;
								array4[l + num9].y = (vertices[l].y - num38) / num40;
							}
						}
						else
						{
							int num41 = glyphData2.frontVertIndex * ((!includeBackface) ? 1 : 2);
							for (int m = 0; m < num41; m++)
							{
								array4[m + num9].x = (vertices[m].x - num36) / num39;
								array4[m + num9].y = (vertices[m].y - num38) / num40;
							}
							for (int n = num41; n < vertexCount; n += 2)
							{
								array4[n + num9].x = 0f;
								array4[n + num9].y = (vertices[n].y - num38) / num40;
								array4[n + num9 + 1].x = 1f;
								array4[n + num9 + 1].y = (vertices[n + 1].y - num38) / num40;
							}
						}
					}
					if (!flag6 && flag2)
					{
						list6.Add(new EdgeData(glyphData2.frontVertIndex * ((!includeBackface) ? 1 : 2), vertexCount));
					}
					if (flag5)
					{
						for (int num42 = 0; num42 < vertexCount; num42++)
						{
							array5[num42 + num9] = color;
						}
					}
					int[] triangles = glyphData2.triangles;
					int triCount = glyphData2.triCount;
					for (int num43 = 0; num43 < triCount; num43 += 3)
					{
						array2[num10] = triangles[num43] + num9;
						array2[num10 + 1] = triangles[num43 + 1] + num9;
						array2[num10 + 2] = triangles[num43 + 2] + num9;
						num10 += 3;
					}
					if (flag2)
					{
						triangles = glyphData2.triangles2;
						triCount = glyphData2.triCount2;
						for (int num44 = 0; num44 < triCount; num44 += 3)
						{
							array3[num11] = triangles[num44] + num9;
							array3[num11 + 1] = triangles[num44 + 1] + num9;
							array3[num11 + 2] = triangles[num44 + 2] + num9;
							num11 += 3;
						}
					}
					if (justify != 0)
					{
						float num45 = 0f;
						switch (justify)
						{
						case Justify.Right:
							num45 = num19 - list4[num18];
							break;
						case Justify.Center:
							num45 = (num19 - list4[num18]) / 2f;
							break;
						}
						if (!separateObjects)
						{
							for (int num46 = 0; num46 < vertexCount; num46++)
							{
								array[num9].x = vertices[num46].x + num20 + num45;
								array[num9].y = vertices[num46].y + num24;
								array[num9++].z = vertices[num46].z + num25;
							}
						}
						else
						{
							for (int num47 = 0; num47 < vertexCount; num47++)
							{
								array[num9].x = vertices[num47].x + num45;
								array[num9].y = vertices[num47].y;
								array[num9++].z = vertices[num47].z + num25;
							}
						}
					}
					else if (!separateObjects)
					{
						for (int num48 = 0; num48 < vertexCount; num48++)
						{
							array[num9].x = vertices[num48].x + num20;
							array[num9].y = vertices[num48].y + num24;
							array[num9++].z = vertices[num48].z + num25;
						}
					}
					else
					{
						for (int num49 = 0; num49 < vertexCount; num49++)
						{
							array[num9].x = vertices[num49].x;
							array[num9].y = vertices[num49].y;
							array[num9++].z = vertices[num49].z + num25;
						}
					}
					if (separateObjects)
					{
						Mesh mesh2 = new Mesh();
						mesh2.name = c2.ToString();
						mesh2.vertices = array;
						mesh2.uv = array4;
						if (flag5)
						{
							mesh2.colors32 = array5;
						}
						if (flag2)
						{
							mesh2.subMeshCount = 2;
							mesh2.SetTriangles(array2, 0);
							mesh2.SetTriangles(array3, 1);
						}
						else
						{
							mesh2.triangles = array2;
						}
						mesh2.RecalculateNormals();
						GameObject gameObject2 = new GameObject(c2.ToString(), typeof(MeshFilter), typeof(MeshRenderer));
						gameObject2.GetComponent<MeshFilter>().mesh = mesh2;
						if (colliderType == ColliderType.Mesh || colliderType == ColliderType.ConvexMesh)
						{
							MeshCollider meshCollider = gameObject2.AddComponent<MeshCollider>();
							meshCollider.sharedMesh = mesh2;
							meshCollider.convex = colliderType == ColliderType.ConvexMesh;
							meshCollider.sharedMaterial = physicsMaterial;
						}
						else if (colliderType == ColliderType.Box)
						{
							gameObject2.AddComponent<BoxCollider>().sharedMaterial = physicsMaterial;
						}
						if (addRigidbodies)
						{
							gameObject2.AddComponent(typeof(Rigidbody));
						}
						if (flag2)
						{
							gameObject2.GetComponent<Renderer>().sharedMaterials = sharedMaterials;
						}
						else
						{
							gameObject2.GetComponent<Renderer>().sharedMaterial = material;
						}
						if (!useObjectsArray)
						{
							gameObject2.transform.parent = gameObject.transform;
							gameObject2.transform.position = new Vector3(num20, num24, num25);
						}
						else
						{
							gameObject2.transform.rotation = rotation;
							gameObject2.transform.Translate(new Vector3(num20, num24, num25) + position);
							list3.Add(gameObject2);
						}
					}
				}
				num21 = num20;
				if (!verticalLayout)
				{
					num20 += ((float)tTFFontInfo.advanceArray[num31] + num6 / num12) * (num30 * num5);
				}
				if (num17 == 2 && lineWidth > 0f && num20 > lineWidth && !flag8)
				{
					if (flag3 && num22 > 0f)
					{
						list[num23] = '\n';
						k = num23 - 1;
						num20 = num22;
						num26 = num27;
						tTFFontInfo = tTFFontInfo2;
						num = num28;
						commandData3 = commandData4;
						num22 = 0f;
					}
					else
					{
						if (c2 == ' ')
						{
							list[k] = '\n';
						}
						else
						{
							list.Insert(k, '\n');
							list2.Insert(k, num31);
							for (int num50 = num; num50 < commandData.Count; num50++)
							{
								if (commandData[num50].index != -1)
								{
									commandData[num50].index++;
								}
							}
						}
						num20 = ((c3 != ' ') ? num21 : num22);
						k--;
					}
					flag7 = true;
				}
				flag8 = false;
			}
			if (num17-- != 2)
			{
				continue;
			}
			list4.Add(num20);
			list5.Add(justify);
			num19 = list4[0];
			for (int num51 = 1; num51 < list4.Count; num51++)
			{
				if (list4[num51] > num19)
				{
					num19 = list4[num51];
				}
			}
			if (num19 < lineWidth)
			{
				num19 = lineWidth;
			}
		}
		if (!flag6)
		{
			float num52 = num14 - num13;
			float num53 = num16 - num15;
			if (!flag2)
			{
				for (int num54 = 0; num54 < num2; num54++)
				{
					array4[num54].x = (array[num54].x - num13) / num52;
					array4[num54].y = (array[num54].y - num15) / num53;
				}
			}
			else
			{
				num9 = 0;
				for (int num55 = 0; num55 < list6.Count; num55++)
				{
					int frontVertIndex = list6[num55].frontVertIndex;
					for (int num56 = 0; num56 < frontVertIndex; num56++)
					{
						array4[num56 + num9].x = (array[num56 + num9].x - num13) / num52;
						array4[num56 + num9].y = (array[num56 + num9].y - num15) / num53;
					}
					int vertexCount2 = list6[num55].vertexCount;
					for (int num57 = frontVertIndex; num57 < vertexCount2; num57 += 2)
					{
						array4[num57 + num9].x = 0f;
						array4[num57 + num9].y = (array[num57 + num9].y - num15) / num53;
						array4[num57 + num9 + 1].x = 1f;
						array4[num57 + num9 + 1].y = (array[num57 + num9 + 1].y - num15) / num53;
					}
					num9 += vertexCount2;
				}
			}
		}
		Vector3 zero = Vector3.zero;
		switch (anchor)
		{
		case TextAnchor.UpperLeft:
			zero.y = num16;
			break;
		case TextAnchor.UpperCenter:
			zero.x = (num14 - num13) * 0.5f;
			zero.y = num16;
			break;
		case TextAnchor.UpperRight:
			zero.x = num14 - num13;
			zero.y = num16;
			break;
		case TextAnchor.MiddleLeft:
			zero.y = (num15 - num16) * 0.5f + num16;
			break;
		case TextAnchor.MiddleCenter:
			zero.x = (num14 - num13) * 0.5f;
			zero.y = (num15 - num16) * 0.5f + num16;
			break;
		case TextAnchor.MiddleRight:
			zero.x = num14 - num13;
			zero.y = (num15 - num16) * 0.5f + num16;
			break;
		case TextAnchor.LowerLeft:
			zero.y = num15 - num16 + num16;
			break;
		case TextAnchor.LowerCenter:
			zero.x = (num14 - num13) * 0.5f;
			zero.y = num15 - num16 + num16;
			break;
		case TextAnchor.LowerRight:
			zero.x = num14 - num13;
			zero.y = num15 - num16 + num16;
			break;
		}
		if (flag4)
		{
			switch (zAnchor)
			{
			case ZAnchor.Middle:
				zero.z = defaultDepth * 0.5f;
				break;
			case ZAnchor.Back:
				zero.z = defaultDepth;
				break;
			}
		}
		if (!separateObjects)
		{
			for (int num58 = 0; num58 < num2; num58++)
			{
				array[num58] -= zero;
			}
		}
		else if (!useObjectsArray)
		{
			IEnumerator enumerator = gameObject.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					((Transform)enumerator.Current).position -= zero;
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = enumerator as IDisposable) != null)
				{
					disposable.Dispose();
				}
			}
		}
		else
		{
			for (int num59 = 0; num59 < list3.Count; num59++)
			{
				list3[num59].transform.Translate(-zero);
			}
		}
		string text = new string(list.ToArray());
		string text2 = text.Substring(0, Mathf.Min(20, text.Length));
		text2 = text2.Replace("\n", " ");
		text2 = text2.Replace("\0", "");
		if (separateObjects)
		{
			if (!useObjectsArray)
			{
				gameObject.name = "3DText " + text2;
				gameObject.transform.position = position;
				gameObject.transform.rotation = rotation;
				return gameObject;
			}
			objectArray = list3.ToArray();
			return null;
		}
		if (!flag)
		{
			mesh = new Mesh();
			mesh.name = text2;
		}
		else
		{
			mesh.Clear();
		}
		mesh.vertices = array;
		mesh.uv = array4;
		if (flag5)
		{
			mesh.colors32 = array5;
		}
		else
		{
			int num60 = array.Length;
			array5 = new Color32[num60];
			Color32 color2 = Color.white;
			for (int num61 = 0; num61 < num60; num61++)
			{
				array5[num61] = color2;
			}
			mesh.colors32 = array5;
		}
		if (flag2)
		{
			mesh.subMeshCount = 2;
			mesh.SetTriangles(array2, 0);
			mesh.SetTriangles(array3, 1);
		}
		else
		{
			mesh.triangles = array2;
		}
		mesh.RecalculateNormals();
		if (flag)
		{
			mesh.RecalculateBounds();
			MeshCollider component3 = gObject.GetComponent<MeshCollider>();
			if (component3 != null)
			{
				component3.sharedMesh = mesh;
			}
			return null;
		}
		GameObject gameObject3 = new GameObject("3DText " + text2, typeof(MeshFilter), typeof(MeshRenderer));
		gameObject3.GetComponent<MeshFilter>().mesh = mesh;
		if (flag2)
		{
			gameObject3.GetComponent<Renderer>().sharedMaterials = sharedMaterials;
		}
		else
		{
			gameObject3.GetComponent<Renderer>().sharedMaterial = material;
		}
		if (colliderType == ColliderType.Mesh || colliderType == ColliderType.ConvexMesh)
		{
			MeshCollider meshCollider2 = gameObject3.AddComponent<MeshCollider>();
			meshCollider2.sharedMesh = mesh;
			meshCollider2.convex = colliderType == ColliderType.ConvexMesh;
			meshCollider2.sharedMaterial = physicsMaterial;
		}
		else if (colliderType == ColliderType.Box)
		{
			gameObject3.AddComponent<BoxCollider>().sharedMaterial = physicsMaterial;
		}
		if (addRigidbodies)
		{
			gameObject3.AddComponent<Rigidbody>();
		}
		gameObject3.transform.position = position;
		gameObject3.transform.rotation = rotation;
		gameObject3.AddComponent<TextObjectData>().SetData(size, extrudeDepth, resolution, characterSpacing, lineSpacing, lineWidth);
		return gameObject3;
	}

	private static List<char> ParseString(string s, out List<CommandData> commandData)
	{
		commandData = new List<CommandData>();
		s = s.Replace("\0", "");
		s = s.Replace("<<", "\01");
		s = s.Replace(">>", "\02");
		s = s.Replace("<br>", "\n");
		s = s.Replace("<BR>", "\n");
		int startIndex = 0;
		for (int num = s.IndexOf("<", startIndex); num != -1; num = s.IndexOf("<", startIndex))
		{
			int num2 = s.IndexOf(">", startIndex);
			if (num2 == -1 || num2 < num)
			{
				break;
			}
			string s2 = s.Substring(num + 1, num2 - num - 1);
			s = s.Remove(num, num2 - num + 1);
			startIndex = num;
			string text;
			string data;
			if (GetTagData(ref s2, out text, out data))
			{
				text = text.ToLower();
				float result;
				switch (text)
				{
				case "size":
					if (float.TryParse(data, out result))
					{
						commandData.Add(new CommandData(num, Command.Size, result));
					}
					break;
				case "color":
				{
					Color32 color;
					if (TryParseColor(ref data, out color))
					{
						commandData.Add(new CommandData(num, Command.Color, color));
						break;
					}
					data = data.ToLower();
					if (_colorDictionary.ContainsKey(data))
					{
						commandData.Add(new CommandData(num, Command.Color, _colorDictionary[data]));
					}
					break;
				}
				case "font":
				{
					int result2;
					if (int.TryParse(data, out result2))
					{
						commandData.Add(new CommandData(num, Command.Font, result2));
						break;
					}
					data = data.ToLower();
					for (int i = 0; i < _fontNames.Length; i++)
					{
						if (data == _fontNames[i])
						{
							commandData.Add(new CommandData(num, Command.Font, i));
							break;
						}
					}
					break;
				}
				case "zpos":
					if (float.TryParse(data, out result))
					{
						commandData.Add(new CommandData(num, Command.Zpos, result));
					}
					break;
				case "depth":
					if (float.TryParse(data, out result))
					{
						commandData.Add(new CommandData(num, Command.Depth, result));
					}
					break;
				case "space":
					if (float.TryParse(data, out result))
					{
						commandData.Add(new CommandData(num, Command.Space, result));
					}
					break;
				case "justify":
					switch (data)
					{
					case "left":
						commandData.Add(new CommandData(num, Command.Justify, Justify.Left));
						break;
					case "right":
						commandData.Add(new CommandData(num, Command.Justify, Justify.Right));
						break;
					case "center":
					case "centre":
						commandData.Add(new CommandData(num, Command.Justify, Justify.Center));
						break;
					}
					break;
				default:
					Debug.LogWarning("Unknown tag: " + text);
					break;
				}
			}
		}
		commandData.Add(new CommandData(-1, Command.None, null));
		s = s.Replace("\01", "\0<");
		s = s.Replace("\02", "\0>");
		List<char> list = new List<char>(s.ToCharArray());
		list.Add('\0');
		return list;
	}

	private static bool GetTagData(ref string s, out string tag, out string data)
	{
		if (s.IndexOfAny(_removeChars) != -1)
		{
			s = string.Join("", s.Split(_removeChars));
		}
		string[] array = s.Split('=');
		if (array.Length != 2)
		{
			tag = "";
			data = "";
			return false;
		}
		tag = array[0];
		data = array[1];
		return true;
	}

	private static bool TryParseColor(ref string s, out Color32 color)
	{
		color = Color.white;
		if (s.Length != 7 || !s.StartsWith("#"))
		{
			return false;
		}
		int result;
		if (int.TryParse(s.Substring(1, 6), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result))
		{
			color = new Color32((byte)(result >> 16), (byte)((uint)(result >> 8) & 0xFFu), (byte)((uint)result & 0xFFu), byte.MaxValue);
			return true;
		}
		return false;
	}
}
