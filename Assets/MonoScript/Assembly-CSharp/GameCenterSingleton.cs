using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SocialPlatforms;
//using UnityEngine.SocialPlatforms.GameCenter;

public class GameCenterSingleton
{
	[CompilerGenerated]
	private sealed class _003CReportAchievementProgress_003Ec__AnonStorey1D
	{
		internal bool success;

		internal GameCenterSingleton _003C_003Ef__this;

		internal void _003C_003Em__E(bool result)
		{
			if (result)
			{
				success = true;
				_003C_003Ef__this.LoadAchievements();
				Debug.Log("Successfully reported progress");
			}
			else
			{
				success = false;
				Debug.Log("Failed to report progress");
			}
		}
	}

	private static GameCenterSingleton instance;

	private static string _leaderboardID = ((!GlobalGameController.isFullVersion) ? "zombieslayerslite" : "zombieslayers");

	public string bestScore = "0";

	private IAchievement[] achievements;

	[CompilerGenerated]
	private static Action<bool> _003C_003Ef__am_0024cache4;

	public static GameCenterSingleton Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameCenterSingleton();
				instance.Initialize();
			}
			return instance;
		}
	}

	public void Initialize()
	{
		if (!IsUserAuthenticated())
		{
			Social.localUser.Authenticate(ProcessAuthentication);
		}
	}

	public bool IsUserAuthenticated()
	{
		if (Social.localUser.authenticated)
		{
			return true;
		}
		Debug.Log("User not Authenticated");
		return false;
	}

	public void ShowAchievementUI()
	{
		if (IsUserAuthenticated())
		{
			Social.ShowAchievementsUI();
		}
	}

	public void ShowLeaderboardUI()
	{
		if (IsUserAuthenticated())
		{
			Social.ShowLeaderboardUI();
		}
	}

	public bool AddAchievementProgress(string achievementID, float percentageToAdd)
	{
		IAchievement achievement = GetAchievement(achievementID);
		if (achievement != null)
		{
			return ReportAchievementProgress(achievementID, (float)achievement.percentCompleted + percentageToAdd);
		}
		return ReportAchievementProgress(achievementID, percentageToAdd);
	}

	public void ReportScore(long score)
	{
		Debug.Log("Reporting score " + score + " on leaderboard " + _leaderboardID);
		string leaderboardID = _leaderboardID;
		if (_003C_003Ef__am_0024cache4 == null)
		{
			_003C_003Ef__am_0024cache4 = _003CReportScore_003Em__C;
		}
		Social.ReportScore(score, leaderboardID, _003C_003Ef__am_0024cache4);
	}

	public void GetScore()
	{
		Social.LoadScores(_leaderboardID, _003CGetScore_003Em__D);
	}

	public bool ReportAchievementProgress(string achievementID, float progressCompleted)
	{
		if (Social.localUser.authenticated)
		{
			if (!IsAchievementComplete(achievementID))
			{
				_003CReportAchievementProgress_003Ec__AnonStorey1D _003CReportAchievementProgress_003Ec__AnonStorey1D = new _003CReportAchievementProgress_003Ec__AnonStorey1D();
				_003CReportAchievementProgress_003Ec__AnonStorey1D._003C_003Ef__this = this;
				_003CReportAchievementProgress_003Ec__AnonStorey1D.success = false;
				Social.ReportProgress(achievementID, progressCompleted, _003CReportAchievementProgress_003Ec__AnonStorey1D._003C_003Em__E);
				return _003CReportAchievementProgress_003Ec__AnonStorey1D.success;
			}
			return true;
		}
		Debug.Log("ERROR: GameCenter user not authenticated");
		return false;
	}

	public void ResetAchievements()
	{
		//GameCenterPlatform.ResetAllAchievements(ResetAchievementsHandler);
	}

	private void LoadAchievements()
	{
		Social.LoadAchievements(ProcessLoadedAchievements);
	}

	private void ProcessAuthentication(bool success)
	{
		if (success)
		{
			Debug.Log("Authenticated, checking achievements");
			GetScore();
		}
		else
		{
			Debug.Log("Failed to authenticate");
		}
	}

	private void ProcessLoadedAchievements(IAchievement[] achievements)
	{
		if (this.achievements != null)
		{
			this.achievements = null;
		}
		if (achievements.Length == 0)
		{
			Debug.Log("Error: no achievements found");
			return;
		}
		Debug.Log("Got " + achievements.Length + " achievements");
		this.achievements = achievements;
	}

	private bool IsAchievementComplete(string achievementID)
	{
		if (achievements != null)
		{
			IAchievement[] array = achievements;
			foreach (IAchievement achievement in array)
			{
				if (achievement.id == achievementID && achievement.completed)
				{
					return true;
				}
			}
		}
		return false;
	}

	private IAchievement GetAchievement(string achievementID)
	{
		if (achievements != null)
		{
			IAchievement[] array = achievements;
			foreach (IAchievement achievement in array)
			{
				if (achievement.id == achievementID)
				{
					return achievement;
				}
			}
		}
		return null;
	}

	private void ResetAchievementsHandler(bool status)
	{
		if (status)
		{
			if (achievements != null)
			{
				achievements = null;
			}
			LoadAchievements();
			Debug.Log("Achievements successfully resetted!");
		}
		else
		{
			Debug.Log("Achievements reset failure!");
		}
	}

	public void updateGameCenter()
	{
		instance = new GameCenterSingleton();
		instance.Initialize();
	}

	[CompilerGenerated]
	private static void _003CReportScore_003Em__C(bool success)
	{
		Debug.Log((!success) ? "Failed to report score" : "Reported score successfully");
	}

	[CompilerGenerated]
	private void _003CGetScore_003Em__D(IScore[] scores)
	{
		if (scores.Length > 0)
		{
			Debug.Log("Got " + scores.Length + " scores");
			if (scores.Length > 0)
			{
				bestScore = scores[0].formattedValue;
				if (bestScore == null || bestScore.Equals(string.Empty))
				{
					bestScore = "0";
				}
			}
		}
		else
		{
			Debug.Log("No scores loaded");
		}
		bestScore = "0";
	}
}
