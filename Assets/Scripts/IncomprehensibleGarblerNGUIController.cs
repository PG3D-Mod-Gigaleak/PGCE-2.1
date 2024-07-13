#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomprehensibleGarblerNGUIController : MonoBehaviour {
	public UIButton generate;
	public UIInput input;
	public void GenerateFromInput() {
		string t = input.value;
		string outp = "";
		foreach (char c in t) {
			int offset = -999;
			for (int i = -9999; i < 9999; i++) {
				if (IncomprehensibleGarbler.Create2(i, "").ToLower() == c.ToString().ToLower()) {
					offset = i;
					break;
				}
			}
			outp += offset.ToString() + " ";
		}
		Debug.Log(outp.TrimEnd());
		Debug.Log("Test output: " + IncomprehensibleGarbler.Call("OngpuPerngr", outp.TrimEnd()));
		Debug.Log(GenerateAlgo(outp, t, "2"));
	}
	public string GenerateAlgo(string theWholeThing, string originalText, string suffix = "") {
		// make the Create/Create2 calls
		string finalCall = "IncomprehensibleGarbler.Call(\"Ernqncg\", [REPLACE], [TRAINOFTRUTH])";
		string generatedCall = "";
		string[] theWholeThingButSplit = theWholeThing.TrimEnd().Split(' ');
		int lastIndex = theWholeThingButSplit.Length-1;
		for (int i = 0; i < theWholeThingButSplit.Length; i++) {
			if (i < lastIndex) {
				if (generatedCall == string.Empty) {
					generatedCall += "IncomprehensibleGarbler.Create" + suffix + "(" + theWholeThingButSplit[i] + ", [REPLACE])";
				} else {
					generatedCall = generatedCall.Replace("[REPLACE]", "IncomprehensibleGarbler.Create" + suffix + "(" + theWholeThingButSplit[i] + ", [REPLACE])");
				}
			}else{
				if (generatedCall == string.Empty) {
					generatedCall += "IncomprehensibleGarbler.Create" + suffix + "(" + theWholeThingButSplit[i] + ", \"\")";
				} else {
					generatedCall = generatedCall.Replace("[REPLACE]", "IncomprehensibleGarbler.Create" + suffix + "(" + theWholeThingButSplit[i] + ", \"\")");
				}
			}
		}
		// call "readapt" to make things lowercase
		string trainOfTruth = "";
		foreach (char ch in originalText) {
			if (ch.ToString().Equals(ch.ToString().ToLower(), System.StringComparison.Ordinal)) {
				trainOfTruth += "false, ";
			} else {
				trainOfTruth += "true, ";
			}
		}
		trainOfTruth = trainOfTruth.Substring(0, trainOfTruth.Length-2).Trim();
		finalCall = finalCall.Replace("[REPLACE]", generatedCall);
		finalCall = finalCall.Replace("[TRAINOFTRUTH]", trainOfTruth);
		// it's done!
		return finalCall;
	}
	public void GenerateType1FromInput() {
		string t = input.value;
		string outp = "";
		foreach (char c in t) {
			int offset = -999;
			for (int i = -9999; i < 9999; i++) {
				if (IncomprehensibleGarbler.Create(i, "").ToLower() == c.ToString().ToLower()) {
					offset = i;
					break;
				}
			}
			outp += offset.ToString() + " ";
		}
		Debug.Log(outp.TrimEnd());
		Debug.Log("Test output: " + IncomprehensibleGarbler.Call("OngpuPerngrOhgGlcrBar", outp.TrimEnd()));
		Debug.Log(GenerateAlgo(outp, t));
	}
	public void GenerateType3FromInput() {
		string t = input.value;
		string outp = "";
		foreach (char c in t) {
			int offset = -999;
			for (int i = -9999; i < 9999; i++) {
				if (IncomprehensibleGarbler.Create3(i, "").ToLower() == c.ToString().ToLower()) {
					offset = i;
					break;
				}
			}
			outp += offset.ToString() + " ";
		}
		Debug.Log(outp.TrimEnd());
		Debug.Log("Test output: " + IncomprehensibleGarbler.Call("OngpuPerngrOhgGlcrGuerr", outp.TrimEnd()));
		Debug.Log(GenerateAlgo(outp, t, "3"));
	}
}
#endif