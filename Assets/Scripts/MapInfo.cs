using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(MapInfo))]
public class MapInfoInspector : Editor
{
    private static bool inMenu;

    private static bool inDeathmatch;

    private static string filter;

    private static MapInfo.Map current;

    private GUIStyle defaultStyle;

    private GUIStyle importantStyle;

    private GUIStyle specialStyle;

    private GUIStyle defaultStyleBG;

    private GUIStyle importantStyleBG;

    private GUIStyleState bgState;

    private GUIStyleState menuBgState;

    private GUIStyle menuDefaultStyle;

    public override void OnInspectorGUI()
    {
        MapInfo thisClass = (MapInfo)target;
        SerializedObject obj = new SerializedObject(thisClass);

        if (current != null)
        {
            if (GUILayout.Button("Back"))
            {
                current = null;
                return;
            }

            GUILayout.Space(20);

            Font font = Resources.Load<Font>("Ponderosa");
            Texture2D bg = Resources.Load<Texture2D>("editor bg");

            if (defaultStyle == null)
            {
                defaultStyle = new GUIStyle()
                {
                    fontSize = 24,
                    font = font,

                    normal = new GUIStyleState
                    {
                        textColor = Color.white
                    }
                };
            }

            if (importantStyle == null)
            {
                GUIStyle importantStyle = new GUIStyle()
                {
                    fontSize = 60,
                    font = font,

                    normal = new GUIStyleState
                    {
                        textColor = Color.red
                    },

                    alignment = TextAnchor.MiddleCenter
                };
            }

            if (specialStyle == null)
            {
                GUIStyle specialStyle = new GUIStyle()
                {
                    fontSize = 40,
                    font = font,

                    normal = new GUIStyleState
                    {
                        textColor = Color.yellow
                    },

                    alignment = TextAnchor.MiddleCenter
                };
            }

            if (defaultStyleBG == null)
            {
                GUIStyle defaultStyleBG = new GUIStyle()
                {
                    fontSize = 24,
                    font = font,

                    normal = new GUIStyleState
                    {
                        textColor = Color.white,
                        background = bg
                    }
                };
            }

            if (importantStyleBG == null)
            {
                GUIStyle importantStyleBG = new GUIStyle()
                {
                    fontSize = 60,
                    font = font,

                    normal = new GUIStyleState
                    {
                        textColor = Color.red,
                        background = bg
                    },

                    alignment = TextAnchor.MiddleCenter
                };
            }

            GUILayout.Label("Scene Name", defaultStyle);
            current.sceneName = GUILayout.TextField(current.sceneName, defaultStyleBG);

            GUILayout.Space(30);

            GUILayout.Label("Map Name", defaultStyle);
            current.mapName = GUILayout.TextField(current.mapName, defaultStyleBG);

            GUILayout.Space(30);

            GUILayout.Label("Icon", defaultStyle);

            if (bgState == null) bgState = new GUIStyleState { background = (Texture2D)current.icon };
            GUILayout.Button(new Texture2D(0, 0), new GUIStyle { normal = bgState, hover = bgState, active = bgState }, GUILayout.MaxWidth(512), GUILayout.MaxHeight(288));

            current.icon = (Texture2D)EditorGUILayout.ObjectField(current.icon, typeof(Texture2D), current.icon);

            GUILayout.Space(30);

            GUILayout.Label("BGM Index", defaultStyle);
            current.backgroundMusic = int.Parse(GUILayout.TextField(current.backgroundMusic.ToString(), defaultStyleBG));

            GUILayout.Space(40);

            GUILayout.Label("OBSOLETE", importantStyle);

            current.rotation = Quaternion.Euler(EditorGUILayout.Vector3Field("USE InitCam - obsolete rotation", new Vector4(current.rotation.eulerAngles.x, current.rotation.eulerAngles.y, current.rotation.eulerAngles.z)));
            current.position = EditorGUILayout.Vector3Field("USE InitCam - obsolete position", new Vector4(current.position.x, current.position.y, current.position.z));

            current.hasVarY = GUILayout.Toggle(current.hasVarY, "REMOVED (hasVarY)");

            GUILayout.Space(40);

            GUILayout.Label("Special", specialStyle);

            GUILayout.Space(20);

            current.negateAbominatorDamage = GUILayout.Toggle(current.negateAbominatorDamage, "Negate Abominator Damage");

            GUILayout.Space(50);

            if (GUILayout.Button("REMOVE", importantStyleBG))
            {
                if (inDeathmatch)
                {
                    thisClass.deathmatchMaps.Remove(current);
                }
                else
                {
                    thisClass.coopMaps.Remove(current);
                }

                current = null;
            }
        }
        else if (inMenu)
        {
            if (menuDefaultStyle == null)
            {
                menuDefaultStyle = new GUIStyle()
                {
                    fontSize = 12,
                    font = Resources.Load<Font>("Ponderosa"),

                    normal = new GUIStyleState
                    {
                        textColor = Color.white
                    },
                };
            }

            if (GUILayout.Button("Back"))
            {
                inMenu = false;
            }

            filter = GUILayout.TextField(filter);
            bool markedReadable = false;

            foreach (MapInfo.Map map in inDeathmatch ? thisClass.deathmatchMaps : thisClass.coopMaps)
            {
                if (!string.IsNullOrEmpty(filter) && !map.mapName.ToLower().Contains(filter.ToLower()) && !map.sceneName.ToLower().Contains(filter.ToLower()))
                {
                    continue;
                }

                if (!map.icon.isReadable)
                {
                    File.WriteAllText(AssetDatabase.GetAssetPath(map.icon) + ".meta", File.ReadAllText(AssetDatabase.GetAssetPath(map.icon) + ".meta").Replace("isReadable: 0", "isReadable: 1"));
                    markedReadable = true;

                    continue;
                }

                GUILayout.Space(20);

                GUILayout.Label(map.mapName + " (" + map.sceneName + ".unity)", menuDefaultStyle);
                if (menuBgState == null) menuBgState = new GUIStyleState { background = (Texture2D)map.icon };

                if (GUILayout.Button(new Texture2D(0, 0), new GUIStyle { normal = menuBgState, hover = menuBgState, active = menuBgState }, GUILayout.MaxWidth(256), GUILayout.MaxHeight(144)))
                {
                    current = map;
                }
            }

            if (markedReadable)
            {
                AssetDatabase.Refresh();
            }
        }
        else
        {
            if (GUILayout.Button("Deathmatch"))
            {
                inMenu = true;
                inDeathmatch = true;
            }

            if (GUILayout.Button("COOP"))
            {
                inMenu = true;
                inDeathmatch = false;
            }
        }

        EditorUtility.SetDirty(thisClass);
        obj.ApplyModifiedProperties();
    }
}
#endif

public class MapInfo : MonoBehaviour
{
    [System.Serializable]
    public class Map
    {
        public string sceneName, mapName;

        public Texture icon;

        public int backgroundMusic;

        public Quaternion rotation;

        public Vector3 position;

        public bool hasVarY;

        public bool negateAbominatorDamage;
    }

    public static MapInfo Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameObject>("MapInfo").GetComponent<MapInfo>();
            }
            return instance;
        }
    }

    public List<Map> CurrentMapsList
    {
        get
        {
            return (PlayerPrefs.GetInt("COOP", 0) == 1 ? coopMaps : deathmatchMaps);
        }
    }

    private static MapInfo instance;

    public List<Map> deathmatchMaps, coopMaps;
}
