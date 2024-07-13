using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorOnlyDebug : MonoBehaviour
{
    public static void Log(object text)
    {
        #if USES_WEBSOCKET
        #if UNITY_EDITOR
        handler.logger.Log.AddLine($"[EditorOnlyDebug::Log] {text}");
        #endif
        #else
        Debug.LogWarning($"[EditorOnlyDebug::Log] {text}");
        #endif
    }
    public static void LogWarning(object text)
    {
        #if UNITY_EDITOR
        Debug.LogWarning($"[EditorOnlyDebug::LogWarning] {text}");
        #endif
    }
    public static void LogError(object text)
    {
        #if UNITY_EDITOR
        Debug.LogError($"[EditorOnlyDebug::LogError] {text}");
        #endif
    }
}
