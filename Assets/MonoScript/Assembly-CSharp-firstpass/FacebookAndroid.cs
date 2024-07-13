using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Prime31;
using UnityEngine;

public class FacebookAndroid
{
	[CompilerGenerated]
	private sealed class _003CshowDialog_003Ec__AnonStorey1
	{
		internal string dialogType;

		internal Dictionary<string, string> parameters;

		internal void _003C_003Em__1()
		{
			_facebookPlugin.Call("showDialog", dialogType, parameters.toJson());
		}
	}

	[CompilerGenerated]
	private sealed class _003CgraphRequest_003Ec__AnonStorey2
	{
		internal string graphPath;

		internal string httpMethod;

		internal Dictionary<string, string> parameters;

		internal void _003C_003Em__2()
		{
			_facebookPlugin.Call("graphRequest", graphPath, httpMethod, parameters.toJson());
		}
	}

	private static AndroidJavaObject _facebookPlugin;

	[CompilerGenerated]
	private static Action _003C_003Ef__am_0024cache1;

	static FacebookAndroid()
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.prime31.FacebookPlugin"))
		//	{
		//		_facebookPlugin = androidJavaClass.CallStatic<AndroidJavaObject>("instance", new object[0]);
		//	}
		//	if (_003C_003Ef__am_0024cache1 == null)
		//	{
		//		_003C_003Ef__am_0024cache1 = _003CFacebookAndroid_003Em__0;
		//	}
		//	FacebookManager.preLoginSucceededEvent += _003C_003Ef__am_0024cache1;
		//}
	}

	internal static void babysitRequest(bool requiresPublishPermissions, Action afterAuthAction)
	{
		new FacebookAuthHelper(requiresPublishPermissions, afterAuthAction).start();
	}

	public static void init(bool printKeyHash = true)
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	_facebookPlugin.Call("init", printKeyHash);
		//}
	}

	public static void setSessionLoginBehavior(FacebookSessionLoginBehavior loginBehavior)
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	_facebookPlugin.Call("setSessionLoginBehavior", loginBehavior.ToString());
		//}
	}

	public static bool isSessionValid()
	{
		//if (Application.platform != RuntimePlatform.Android)
		//{
			return false;
		//}
		//return _facebookPlugin.Call<bool>("isSessionValid", new object[0]);
	}

	public static string getAccessToken()
	{
		//if (Application.platform != RuntimePlatform.Android)
		//{
			return string.Empty;
		//}
		//return _facebookPlugin.Call<string>("getAccessToken", new object[0]);
	}

	public static List<object> getSessionPermissions()
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	string json = _facebookPlugin.Call<string>("getSessionPermissions", new object[0]);
		//	return json.listFromJson();
		//}
		return new List<object>();
	}

	public static void login()
	{
		loginWithReadPermissions(new string[0]);
	}

	public static void loginWithReadPermissions(string[] permissions)
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	_facebookPlugin.Call("loginWithReadPermissions", new object[1] { permissions });
		//}
	}

	public static void loginWithPublishPermissions(string[] permissions)
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	_facebookPlugin.Call("loginWithPublishPermissions", new object[1] { permissions });
		//}
	}

	public static void reauthorizeWithReadPermissions(string[] permissions)
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	_facebookPlugin.Call("reauthorizeWithReadPermissions", permissions.toJson());
		//}
	}

	public static void reauthorizeWithPublishPermissions(string[] permissions, FacebookSessionDefaultAudience defaultAudience)
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	string text = null;
		//	text = ((defaultAudience != FacebookSessionDefaultAudience.OnlyMe) ? defaultAudience.ToString().ToUpper() : "ONLY_ME");
		//	_facebookPlugin.Call("reauthorizeWithPublishPermissions", permissions.toJson(), text);
		//}
	}

	public static void logout()
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	_facebookPlugin.Call("logout");
		//	Facebook.instance.accessToken = string.Empty;
		//}
	}

	public static void showFacebookShareDialog(Dictionary<string, object> parameters)
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	_facebookPlugin.Call("showFacebookShareDialog", parameters.toJson());
		//}
	}

	public static void showDialog(string dialogType, Dictionary<string, string> parameters)
	{
		_003CshowDialog_003Ec__AnonStorey1 _003CshowDialog_003Ec__AnonStorey = new _003CshowDialog_003Ec__AnonStorey1();
		_003CshowDialog_003Ec__AnonStorey.dialogType = dialogType;
		_003CshowDialog_003Ec__AnonStorey.parameters = parameters;
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	_003CshowDialog_003Ec__AnonStorey.parameters = _003CshowDialog_003Ec__AnonStorey.parameters ?? new Dictionary<string, string>();
		//	if (!isSessionValid())
		//	{
		//		babysitRequest(false, _003CshowDialog_003Ec__AnonStorey._003C_003Em__1);
		//		return;
		//	}
		//	_facebookPlugin.Call("showDialog", _003CshowDialog_003Ec__AnonStorey.dialogType, _003CshowDialog_003Ec__AnonStorey.parameters.toJson());
		//}
	}

	public static void graphRequest(string graphPath, string httpMethod, Dictionary<string, string> parameters)
	{
		_003CgraphRequest_003Ec__AnonStorey2 _003CgraphRequest_003Ec__AnonStorey = new _003CgraphRequest_003Ec__AnonStorey2();
		_003CgraphRequest_003Ec__AnonStorey.graphPath = graphPath;
		_003CgraphRequest_003Ec__AnonStorey.httpMethod = httpMethod;
		_003CgraphRequest_003Ec__AnonStorey.parameters = parameters;
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	_003CgraphRequest_003Ec__AnonStorey.parameters = _003CgraphRequest_003Ec__AnonStorey.parameters ?? new Dictionary<string, string>();
		//	if (!isSessionValid())
		//	{
		//		babysitRequest(true, _003CgraphRequest_003Ec__AnonStorey._003C_003Em__2);
		//		return;
		//	}
		//	_facebookPlugin.Call("graphRequest", _003CgraphRequest_003Ec__AnonStorey.graphPath, _003CgraphRequest_003Ec__AnonStorey.httpMethod, _003CgraphRequest_003Ec__AnonStorey.parameters.toJson());
		//}
	}

	[CompilerGenerated]
	private static void _003CFacebookAndroid_003Em__0()
	{
		Facebook.instance.accessToken = getAccessToken();
	}
}
