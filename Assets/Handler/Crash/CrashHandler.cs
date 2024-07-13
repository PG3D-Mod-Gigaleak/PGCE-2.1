#if USES_WEBSOCKET
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using handler.logger;
using UnityEngine;

namespace handler.crash
{
	public class CrashHandler
	{
		public static void Init()
		{
			AppDomain.CurrentDomain.UnhandledException += HandleCrash;
		}
		// thank you Pixel Gun World
		private static void HandleCrash(object sender, UnhandledExceptionEventArgs e)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(((Exception)e.ExceptionObject).Message);
			stringBuilder.AppendLine();
			int num = 0;
			StackFrame[] frames = new StackTrace().GetFrames();
			StackFrame[] array = frames;
			foreach (StackFrame stackFrame in array)
			{
				stringBuilder.AppendFormat("{0}: {1}[{2}]: {3}\n", num, stackFrame.GetFileName(), stackFrame.GetFileLineNumber(), stackFrame.GetMethod().Name);
				num++;
			}
			string string_ = stringBuilder.ToString();
			Log.AddLine($"[CrashHandler::HandleCrash] {string_}");
		}
		public static void Crash()
		{
			throw new ApplicationException("Game was force crashed!");
		}
	}
}
#endif