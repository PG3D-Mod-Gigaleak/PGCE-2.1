#if USES_WEBSOCKET
using System;
using System.Collections;
using System.Collections.Generic;
using WebSocketSharp;
using Newtonsoft.Json;
using UnityEngine;
using handler.data;
using System.Text;
using System.Threading;

// ACTUALLY GET WEBSOCKET FIRST????
namespace handler.networking
{
	public class WebsocketHandler : MonoBehaviour
	{
		public delegate void ActionAnswered(string data);
		private const string WS = "ws://127.0.0.1:8083/action";
		public static bool WSIsAlive = false;
		private static WebsocketHandler Instance;
		private static WebSocket Listener;
		public static void Init()
		{
			GameObject websocket = new GameObject("WebsocketHandler");
			Instance = websocket.AddComponent<WebsocketHandler>();
			DontDestroyOnLoad(websocket);
			Instance.StartCoroutine(RealInit());
		}
		private static IEnumerator RealInit()
		{
			CallAction("ensure_ws_alive", (string data) => {
				Dictionary<string, object> resultDictionary = Decrypt(JsonConvert.DeserializeObject<Dictionary<string, object>>(data));
				if ((string)resultDictionary["response"] == "success")
				{
					WSIsAlive = true;
				}
			}, default);
			yield return new WaitUntil(() => WSIsAlive);
			if (WSIsAlive)
			{
				Listener = new WebSocket(WS);
				Listener.OnMessage += (sender, e) => {
					try
					{
						MainThreadDispatcher.Instance.QueueOnMainThread(() =>
						{
							Instance.HandleRecv(e.Data);
						});
					}
					catch (Exception e22)
					{
					}
				};
				Listener.Connect();
				UserController.Init();
			}
			yield break;
		}
		private void HandleRecv(string data)
		{
			Dictionary<string, object> resultDictionary = Decrypt(JsonConvert.DeserializeObject<Dictionary<string, object>>(data));
			if ((string)resultDictionary["response"] == "success")
			{
				if (resultDictionary.ContainsKey("type") && (string)resultDictionary["type"] == "send")
				{
					if (resultDictionary.ContainsKey("action"))
					{
						if ((string)resultDictionary["action"] == "alert-ban" && resultDictionary.ContainsKey("bannedID") && (string)resultDictionary["bannedID"] == Convert.ToString(UserController.Instance.ID))
						{
							AlertNGUI.Show("You have been banned!", 8f);
						}
						if ((string)resultDictionary["action"] == "alert-downtime")
						{
							AlertNGUI.Show("The servers are down for maintenance!", 8f);
						}
						if ((string)resultDictionary["action"] == "recv-forceupdate-user" && resultDictionary.ContainsKey("recvid") && (string)resultDictionary["recvid"] == Convert.ToString(UserController.Instance.ID))
						{
							UserController.Instance.StartCoroutine(UserController.Instance.GetPlayerInfo());
						}
					}
				}
			}
		}
		private void OnApplicationQuit()
		{
			Listener.Close();
			CallAction("close_session", null, default);
		}
		#region Websocket Actions
		private static void HandleAnswered(WebSocket connection, ActionAnswered callback, string data)
		{
			Dictionary<string, object> resultDictionary = Decrypt(JsonConvert.DeserializeObject<Dictionary<string, object>>(data));
			try
			{
				if (callback != null)
				{
					if (resultDictionary.ContainsKey("type") && (string)resultDictionary["type"] == "send")
						return;
					callback(data);
				}
			}
			catch (Exception e)
			{
				connection.Close();
			}
		}
		// this code looks so fucking bad please i can't
		public static void CallAction(string action, ActionAnswered callback, Dictionary<string, object> args)
		{
			if (args == null)
				args = new Dictionary<string, object>();
			if (!WSIsAlive && action != "ensure_ws_alive")
			{
				return;
			}
			args["action"] = action;
			WebSocket CurrentConnection = new WebSocket(WS);
			CurrentConnection.Connect();
			CurrentConnection.OnMessage += (sender, e) => {
				try
				{
					MainThreadDispatcher.Instance.QueueOnMainThread(() =>
					{
						HandleAnswered(CurrentConnection, callback, e.Data);
					});
					Thread.Sleep(2000);
					CurrentConnection.Close();
				}
				catch (Exception e22)
				{
					CurrentConnection.Close();
				}
			};
			string sargs = JsonConvert.SerializeObject(Encrypt(args));
			CurrentConnection.Send(sargs);
		}
		#endregion
		private static readonly byte[] EncryptionKey = new byte[] { 0x8d, 0xa9, 0x1b, 0xb9, 0x2d, 0x1d, 0x95, 0x0c, 0x98, 0x08, 0x8d, 0xb9, 0x14, 0xd5, 0x87, 0x33, 0xf4, 0x93, 0x2c, 0xe9, 0x41, 0x7c, 0x45, 0xf7, 0xb2, 0x2d, 0xe8, 0xcf, 0x0f, 0xd0, 0x8a, 0x4d, 0x2c, 0xa3, 0x92, 0x14, 0x75, 0x89, 0x86, 0x42, 0x9e, 0x14, 0x25, 0x35, 0x42, 0x1a, 0x80, 0xe7, 0x51, 0xaf, 0xe9, 0x5e, 0xca, 0x1c, 0xaf, 0x9a, 0xc3, 0x54, 0xe3, 0x04, 0x82, 0xc0, 0xaf, 0xa6, 0xba, 0x7c, 0xbd, 0x42, 0xfd, 0x4f, 0x94, 0xdd, 0x66, 0x30, 0x1b, 0x93, 0x80, 0x6c, 0x45, 0x97, 0xcc, 0x66, 0xac, 0xb2, 0x75, 0x7a, 0x48, 0xf8, 0xd3, 0x3f, 0x28, 0x9c, 0x42, 0xbe, 0xf0, 0x75, 0x5c, 0x2f, 0x99, 0xeb, 0xfe, 0x7d, 0x24, 0x42, 0x4f, 0x64, 0x8f, 0xb7, 0x01, 0x4f, 0xa6, 0x96, 0x66, 0x90, 0xff, 0x19, 0x62, 0xe0, 0xa2, 0x09, 0xfa, 0x19, 0xc2, 0x7a, 0xda, 0x88, 0x28, 0x8f, 0x92, 0x45, 0x0d, 0x7a, 0xe7, 0x0f, 0x6a, 0x98, 0x35, 0x24, 0x64, 0xdc, 0xd4, 0xcb, 0x65, 0x2c, 0x02, 0xd7, 0xe6, 0x2c, 0x4a, 0x6e, 0x8c, 0x19, 0x55, 0x36, 0x32, 0xd6, 0x61, 0xb1, 0x28, 0x6a, 0xff, 0xca, 0x91, 0x11, 0x00, 0x2c, 0x78, 0x04, 0x78, 0xf6, 0x67, 0x06, 0x5b, 0xb6, 0x84, 0x56, 0x46, 0x65, 0x07, 0x29, 0x3f, 0x7b, 0xb6, 0xfb, 0xb9, 0x1e, 0x7b, 0xad, 0x9d, 0x3e, 0xd2, 0xbc, 0x5a, 0x1f, 0x20, 0xa0, 0xe8, 0xfb, 0xf0, 0xe7, 0x8c, 0x51, 0x96, 0x69, 0x75, 0xaf, 0x39, 0x0c, 0x8d, 0x1d, 0x82, 0x02, 0x16, 0xf9, 0x9a, 0xc7, 0xcf, 0x47, 0x9c, 0x36, 0x89, 0xe6, 0xe5, 0xb1, 0xe1, 0x7c, 0x4d, 0xdc, 0xb3, 0x90, 0xdc, 0xf8, 0x6f, 0x05, 0x89, 0x59, 0xbd, 0x29, 0x2c, 0x50, 0xb7, 0x36, 0x0b, 0xde, 0xbb, 0x31, 0x40, 0xb9, 0xb2, 0x47, 0x01, 0x92, 0xe8, 0x0b, 0x53, 0x12, 0xce, 0x33, 0x03, 0xc2, 0x79, 0xc7, 0x34, 0x0b, 0x6b, 0xba, 0xd7, 0xa2, 0x7b, 0x54, 0xf5, 0xc1, 0xd4, 0x1e, 0x1a, 0xca, 0x91, 0xfe, 0x4c, 0x26, 0x90, 0x71, 0xac, 0xa2, 0x12, 0x1c, 0xdc, 0x3b, 0xb1, 0x32, 0xf8, 0x43, 0xd2, 0x2f, 0xde, 0x83, 0x55, 0x08, 0x6f, 0xe4, 0xd1, 0x8d, 0xbd, 0x61, 0x6d, 0x15, 0x6b, 0x6f, 0x87, 0xc8, 0x65, 0xa4, 0x44, 0x91, 0x17, 0x64, 0x19, 0x43, 0xd7, 0x22, 0x96, 0xb6, 0xf5, 0x06, 0xaf, 0x20, 0x2d, 0x09, 0x6d, 0x3a, 0x12, 0xfa, 0xc5, 0x5c, 0xfc, 0xcc, 0x6c, 0x89, 0x93, 0xef, 0xea, 0x8c, 0x3b, 0xf1, 0xe8, 0x62, 0x48, 0x16 };
		public static long ReturnFunnyLittleThing(float value, float istart, float istop, float ostart, float ostop)
		{
			return (long)(ostart + (value - istart) * (ostop - ostart) / (istop - istart));
		}
		public static Dictionary<string, object> Encrypt(Dictionary<string, object> input)
		{
			var encryptedDictionary = new Dictionary<string, object>();

			foreach (var kvp in input)
			{
				encryptedDictionary.Add(EncryptValue(kvp.Key.ToString()), EncryptValue(kvp.Value.ToString()));
			}

			return encryptedDictionary;
		}

		public static Dictionary<string, object> Decrypt(Dictionary<string, object> encryptedInput)
		{
			var decryptedDictionary = new Dictionary<string, object>();

			foreach (var kvp in encryptedInput)
			{
				decryptedDictionary.Add(DecryptValue(kvp.Key.ToString()), DecryptValue(kvp.Value.ToString()));
			}

			return decryptedDictionary;
		}

		private static string EncryptValue(string value)
		{
			List<long> ThankYou = new List<long>();
			ThankYou.Append(ReturnFunnyLittleThing(3735931646, 0, 1036409945714741630, 29456, 9223372036854775807)-29511450961);
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			for (int i = 0; i < bytes.Length; i++)
			{
				bytes[i] ^= EncryptionKey[i % EncryptionKey.Length];
			}
			return Convert.ToBase64String(bytes);
		}

		private static string DecryptValue(string encryptedValue)
		{
			byte[] bytes = Convert.FromBase64String(encryptedValue);
			for (int i = 0; i < bytes.Length; i++)
			{
				bytes[i] ^= EncryptionKey[i % EncryptionKey.Length];
			}
			return Encoding.UTF8.GetString(bytes);
		}
	}
}
#endif