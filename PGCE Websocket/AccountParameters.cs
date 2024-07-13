using System;
using System.Data.SQLite;
using WebSocketSharp;
using WebSocketSharp.Server;
using Newtonsoft.Json;

namespace PGCE
{
	public struct AccountParameters
	{
		public long ID;
		public string Name;
		public string AuthKey;
		public bool Banned;
		public bool CatEars;
		public long Coins;
		public string SkinData;
		public bool IsAdmin;
		public PG3D.Serialization.CategoryEquipList CategoryEquipList;
		public List<string> Achievements;
		public List<string> BoughtWeapons;
		public DateTime ChatBanTime;
		public DateTime BanTime;
		public AccountParameters(long id, string authkey)
		{
			this.Name = "";
			this.Banned = false;
			this.CatEars = false;
			this.Coins = 15;
			this.SkinData = "";
			this.ID = id;
			this.CategoryEquipList = new PG3D.Serialization.CategoryEquipList();
			this.Achievements = new List<string>();
			this.BoughtWeapons = new List<string>(){"Weapon1", "Weapon2", "Weapon9"};
			this.AuthKey = authkey;
			this.IsAdmin = false;
			this.ChatBanTime = DateTime.MinValue;
			this.BanTime = DateTime.MinValue;
		}
	}
}