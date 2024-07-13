using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor;

public class AutomaticLoading : EditorWindow
{
    public static RenderTexture output;
    public static EditorBuildSettingsScene[] scenes;
    private static int currentSceneIndex = 0;
	private static float timer;
	private static bool rendering;
	private static Camera renderCamera;

    [MenuItem("Automatic Trollage/Generate Loading Screens")]
    public static void GetWindow()
    {
        EditorWindow.GetWindow(typeof(AutomaticLoading), true, "the funny");
    }

    void OnGUI()
    {
        output = (RenderTexture)EditorGUILayout.ObjectField(output, typeof(RenderTexture), false);

        if (scenes == null || scenes.Length == 0)
        {
            scenes = EditorBuildSettings.scenes;
        }

        if (GUILayout.Button("Generate Them ."))
        {
            if (!Directory.Exists(Application.dataPath + "/LoadingScreens"))
            {
                Directory.CreateDirectory(Application.dataPath + "/LoadingScreens");
            }

            currentSceneIndex = 0;
            EditorApplication.update += GenerateLoadingScreens;
        }
    }

    private static void GenerateLoadingScreens()
    {
        if (currentSceneIndex >= scenes.Length)
        {
            EditorApplication.update -= GenerateLoadingScreens;
            return;
        }

		if (timer > 0)
		{
			timer -= Time.deltaTime;
			return;
		}

        MapInfo.Map map = Resources.Load<GameObject>("MapInfo").GetComponent<MapInfo>().deathmatchMaps[currentSceneIndex];
		if (rendering)
		{
			FinishRendering(map.sceneName);
			rendering = false;
		}
        BeginRendering(map.sceneName, map.position, map.rotation);

        currentSceneIndex++;
    }

	private static void BeginRendering(string sceneName, Vector3 position, Quaternion rotation)
	{
		if (File.Exists(Application.dataPath + "/LoadingScreens/" + sceneName + ".png"))
        {
            return;
        }

        File.Create(Application.dataPath + "/LoadingScreens/" + sceneName + ".png");

        EditorSceneManager.OpenScene(System.Array.Find(scenes, x => x.path.Contains(sceneName + ".unity")).path, OpenSceneMode.Single);
        renderCamera = Instantiate(Resources.Load<Camera>("RenderCamera"), position, rotation);
        renderCamera.targetTexture = output;
        renderCamera.Render();
		timer = 1f;
		rendering = true;
	}

    private static void FinishRendering(string sceneName)
    {
        Texture2D tex = new Texture2D(output.width, output.height, TextureFormat.RGB24, false);
        RenderTexture.active = output;
        tex.ReadPixels(new Rect(0, 0, output.width, output.height), 0, 0);
        tex.Apply();
        File.WriteAllBytes(Application.dataPath + "/LoadingScreens/" + sceneName + ".png", tex.EncodeToPNG());
        DestroyImmediate(renderCamera.gameObject);
    }
}
