using System.Text.RegularExpressions;
using UnityEngine;

public class FilterBadWorld : MonoBehaviour
{
	private const string CensoredText = "***";

	private const string PatternTemplate = "\\b({0})(s?)\\b";

	private const RegexOptions Options = RegexOptions.IgnoreCase;

	private string[] badWords = new string[]
	{
		"dashcat", "nigger", "nigga", "niga", "igga", "igger", "iga", "iger", "nigea", "cunt", "bitch", "fag", "faggot", "retard", "retarded"
	};

	public string FilterString(string inputStr)
	{
		string[] array = new string[19]
		{
			".", ",", "%", "!", "@", "#", "$", "*", "&", ";",
			":", "?", "/", "<", ">", "|", "/", "-", "_"
		};
		string text = inputStr;
		string text2 = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			text = text.Replace(array[i], " ");
		}
		text = text.ToLower();
		int num = 0;
		int num2 = text.IndexOf(" ", num);
		while (num2 != -1)
		{
			text2 = ((!scanMatInWold(text.Substring(num, num2 - num))) ? (text2 + inputStr.Substring(num, num2 - num + 1)) : (text2 + "***" + inputStr.Substring(num2, 1)));
			num = num2 + 1;
			if (num < text.Length - 1)
			{
				num2 = text.IndexOf(" ", num);
				continue;
			}
			text2 += inputStr.Substring(text.Length - 1, 1);
			num2 = -1;
		}
		if (num < text.Length)
		{
			text2 = ((!scanMatInWold(text.Substring(num, text.Length - num))) ? (text2 + inputStr.Substring(num, text.Length - num)) : (text2 + "***"));
		}
		return text2;
	}

	private bool scanMatInWold(string str)
	{
		if (str.Length < 3)
		{
			return false;
		}
		string[] array = badWords;
		foreach (string text in array)
		{
			if (text.Equals(str))
			{
				Achievements.Give("censored");
				if (text.Trim().ToLower() == "dashcat")
					Achievements.Give("dashcat");
				return true;
			}
		}
		return false;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
