#if USES_WEBSOCKET
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace handler.logger
{
	public class Log
	{
		public static bool Initialized { get; set; }
		private static void EnsureInitialized()
		{
			if (!Initialized)
			{
				Init();
			}
		}
		public static void Init()
		{
			// Clear the log file, FUCK YOU
			File.WriteAllText("./output-log.txt", 
				$"-----------------------------------------------------\n" +
				$"Unity specific HWID: {SystemInfo.deviceUniqueIdentifier}\n" +
				$"Device name: {SystemInfo.deviceName}\n" +
				$"Device type: {(SystemInfo.deviceType).ToString()}\n" +
				$"Device model: {SystemInfo.deviceModel}\n" +
				$"-----------------------------------------------------\n"
			);
			Initialized = true;
			Application.logMessageReceived += HandleLog;
			AddLine("[Log::Init] Initialized logger!");
		}
		private static void HandleLog(string logString, string stackTrace, LogType type)
		{
			Log.AddLine($"{logString}", type);
		}
		public static void AddLine(object line)
		{
			Log.AddLine(line, LogType.Log, true);
		}
		public static void AddWarning(object line)
		{
			Log.AddLine(line, LogType.Warning, true);
		}
		public static void AddError(object line)
		{
			Log.AddLine(line, LogType.Error, true);
		}
		public static void AddLine(object line, LogType logType, bool hideLogFromConsole = false)
		{
			EnsureInitialized();
			string text = $"[{DateTime.Now.ToShortDateString()}///{DateTime.Now.ToShortTimeString()}] [{logType.ToString().ToUpper()}]\n{line}\n\n";
			//File.AppendAllText("./output-log.txt", text);
		}
	}
}
#endif