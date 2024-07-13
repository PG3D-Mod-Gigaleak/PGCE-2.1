using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class AchievementNGUI : MonoBehaviour
{
	public static AchievementNGUI instance;

	public UILabel achievementName;

	public UITexture achievementIcon;

	public AudioClip achievementSound;

	public GameObject achievementPopup;

	private List<string> queuedAchievements = new List<string>();
	
	private Achievements achievements;

	void Awake()
	{
		instance = this;
		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		achievements = Resources.Load<Achievements>("Achievements");
	}

	void Update()
	{
		if (queuedAchievements.Count > 0)
		{
			if (!achievementPopup.GetComponent<Animation>().isPlaying)
			{
				Achievements.Achievement achievement = achievements.GetAchievement(queuedAchievements[0]);
				achievementIcon.mainTexture = achievement.icon;
				achievementName.text = achievement.name;

				achievementPopup.GetComponent<Animation>().Play("AchievementGet");
				achievementPopup.GetComponent<AudioSource>().PlayOneShot(achievementSound);
				queuedAchievements.RemoveAt(0);
			}
		}
	}

	public void GiveAchievement(string achievement)
	{
		#if USES_WEBSOCKET
		if (Storager.hasKey(achievement + "-ACHIEVEMENT"))
			return;
		// since the code is bad, we use this as a "helper function" instead
		handler.data.UserController.Instance.StartCoroutine(handler.data.UserController.Instance.GetPlayerInfo());
		if (handler.data.UserController.Instance.Achievements.Contains(achievement))
			return;
		handler.networking.WebsocketHandler.CallAction("give_achievement", (string data) => {
			Dictionary<string, object> resultDictionary = handler.networking.WebsocketHandler.Decrypt(JsonConvert.DeserializeObject<Dictionary<string,object>>(data));
		}, new Dictionary<string, object>(){
			{"uid", handler.data.UserController.Instance.ID},
			{"ak", handler.data.UserController.Instance.AuthKey},
			{"achievement", achievement},
		});
		// the normal code is still ran like usual
		if (Storager.hasKey(achievement + "-ACHIEVEMENT"))
			return;
		Storager.setInt(achievement + "-ACHIEVEMENT", 1, false);
		queuedAchievements.Add(achievement);
		#else
		if (Storager.hasKey(achievement + "-ACHIEVEMENT"))
			return;

		Storager.setInt(achievement + "-ACHIEVEMENT", 1, false);
		queuedAchievements.Add(achievement);
		#endif
	}
}
