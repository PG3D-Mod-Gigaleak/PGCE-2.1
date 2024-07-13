//lets forget I ever made this

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class MapInfoMigrate : MonoBehaviour
//{
//	void Start()
//	{
//		MigrateConnectScene(connectScene);
//	}
//
//	public ConnectGUI connectScene;
//
//	public void MigrateConnectScene(ConnectGUI connectGUI)
//	{
//		DoTheStupid();
//		for (int i = 0; i < connectGUI.masMap.Length; i++)
//		{
//			string sceneName = connectGUI.masMapName[i];
//			Texture mapIcon = connectGUI.masLoading[i];
//			string mapName = "";
//			mapNamesForUser.TryGetValue(sceneName, out mapName);
//			int bgmNum = 0;
//			levelNumsForMusicInMult.TryGetValue(sceneName, out bgmNum);
//			bool hasVarY = levelsWithVarY.Contains(bgmNum);
//			MapInfoPosAndRot posRot = GetMapInfoPosAndRot(sceneName);
//			GetComponent<MapInfo>().deathmatchMaps.Add(new MapInfo.Map { sceneName = sceneName, icon = mapIcon, mapName = mapName, backgroundMusic = bgmNum, hasVarY = hasVarY, position = posRot.pos, rotation = posRot.rot });
//		}
//		for (int i = 0; i < connectGUI.masMapCOOP.Length; i++)
//		{
//			string sceneName = connectGUI.masMapNameCOOP[i];
//			Texture mapIcon = connectGUI.masLoadingCOOP[i];
//			string mapName = "";
//			mapNamesForUser.TryGetValue(sceneName, out mapName);
//			int bgmNum = 0;
//			levelNumsForMusicInMult.TryGetValue(sceneName, out bgmNum);
//			bool hasVarY = levelsWithVarY.Contains(bgmNum);
//			MapInfoPosAndRot posRot = GetMapInfoPosAndRot(sceneName);
//			GetComponent<MapInfo>().coopMaps.Add(new MapInfo.Map { sceneName = sceneName, icon = mapIcon, mapName = mapName, backgroundMusic = bgmNum, hasVarY = hasVarY, position = posRot.pos, rotation = posRot.rot });
//		}
//	}
//
//	public void DoTheStupid()
//	{
//		mapNamesForUser.Add("Maze", "Temple");
//		mapNamesForUser.Add("Cementery", "Graveyard");
//		mapNamesForUser.Add("City", "Dead City");
//		mapNamesForUser.Add("Gluk", "END World");
//		mapNamesForUser.Add("Jail", "Prison");
//		mapNamesForUser.Add("Hospital", "Hospital");
//		mapNamesForUser.Add("Pool", "Pool");
//		mapNamesForUser.Add("Slender", "Forest");
//		mapNamesForUser.Add("Castle", "Hell");
//		mapNamesForUser.Add("Ranch", "Shooting Range");
//		mapNamesForUser.Add("Arena_MP", "Deadly Arena");
//		mapNamesForUser.Add("Sky_islands", "Sky Islands");
//		mapNamesForUser.Add("Dust", "Arabian City");
//		mapNamesForUser.Add("Bridge", "Bridge");
//		mapNamesForUser.Add("Farm", "Farm");
//		mapNamesForUser.Add("Utopia", "Utopia");
//		mapNamesForUser.Add("Arena", "Arena");
//		mapNamesForUser.Add("Assault", "Warehouse");
//		mapNamesForUser.Add("Town", "Town");
//		mapNamesForUser.Add("Abandoned_Pool", "Abandoned Pool");
//		mapNamesForUser.Add("campaignsuperreal", "Campaign Full Super Real");
//		mapNamesForUser.Add("Winter", "Winter");
//		mapNamesForUser.Add("School", "School");
//		mapNamesForUser.Add("Hills", "Hills");
//		mapNamesForUser.Add("desert_temple", "Desert Temple");
//		mapNamesForUser.Add("minecraft_church", "cathedralllll");
//		mapNamesForUser.Add("EndersHouse", "An Enderman's House");
//		mapNamesForUser.Add("Upside_down_castle", "Upside Down Hell Castle");
//		mapNamesForUser.Add("weird", "Weird Dimension");
//		mapNamesForUser.Add("main_menu_but_how", "Main Menu");
//		mapNamesForUser.Add("ants_life_warehouse", "Ants Life: Warehouse");
//	    mapNamesForUser.Add("PiratIsland", "Pirate Island");
//		mapNamesForUser.Add("buntu", "Buntu");
//		mapNamesForUser.Add("DripDust", "Drip Dust");
//		mapNamesForUser.Add("Slender_but_bad", "256p Slender Forest");
//		mapNamesForUser.Add("Training_11.2.4", "Training from 11.2.4");
//		mapNamesForUser.Add("rupture_of_time", "you teleported to 2014 and uh something is wrong here");
//		mapNamesForUser.Add("northpolebut", "North Pole map but");
//		mapNamesForUser.Add("Stupid52", "Area 52 but very unfunny");
//		mapNamesForUser.Add("COMZ_older_village", "COMZ unused village");
//		mapNamesForUser.Add("COMZ_unused_cemetery", "COMZ unused church");
//		mapNamesForUser.Add("UniBuilder_Map2", "UniVoxelBuilder Map 2");
//		mapNamesForUser.Add("COMZ_unused_hospital", "COMZ unused hospital");
//		mapNamesForUser.Add("COMZ_test", "COMZ test map");
//		mapNamesForUser.Add("COMZ_unused_parkingspace", "COMZ unused parking lot");
//		mapNamesForUser.Add("SandboxSmall", "Endi Tank Small Sandbox");
//		mapNamesForUser.Add("GardenSmall", "Endi Tank Small Garden");
//		mapNamesForUser.Add("noobite_map", "Noobite working on video-");
//		mapNamesForUser.Add("Utopia Castle", "hell castle in utopia");
//		mapNamesForUser.Add("Hell Utopia", "utopia in hell castle");
//		mapNamesForUser.Add("wholeblockworldmaphelpme", "the block world");
//		mapNamesForUser.Add("CRScene2", "a christmas prison wowowo");
//		mapNamesForUser.Add("SWF_Level17", "Star Warfare Warehouse");
//		mapNamesForUser.Add("COMZ_unused_church", "cool church");
//		mapNamesForUser.Add("the truth", "find out the truth about pg3d");
//		mapNamesForUser.Add("Zombie3D_Desert3", "the pixelated");
//		mapNamesForUser.Add("Supermarket", "Blockmart");
//		mapNamesForUser.Add("Paradise_Night", "Terrifying Resort");
//		mapNamesForUser.Add("PSky_islands", "vjg pgy wrfcvg hqt rkzgn iwp ewtugf gfkvkqp yknn rtqdcdna tgngcug kp ncvg 2023 k fqpv mpqy");
//		mapNamesForUser.Add("UtopiaWavy", "utopia but I gave you drugs");
//		mapNamesForUser.Add("FajitaSky_islands", "i hate kuro");
//		mapNamesForUser.Add("Abandoned_Pool_old", "welcome back to the old days of abandoned pool");
//		levelNumsForMusicInMult.Add("Maze", 2);
//		levelNumsForMusicInMult.Add("Cementery", 1);
//		levelNumsForMusicInMult.Add("City", 3);
//		levelNumsForMusicInMult.Add("Gluk", 6);
//		levelNumsForMusicInMult.Add("Jail", 5);
//		levelNumsForMusicInMult.Add("Hospital", 4);
//		levelNumsForMusicInMult.Add("Pool", 1001);
//		levelNumsForMusicInMult.Add("Slender", 9);
//		levelNumsForMusicInMult.Add("Castle", 1002);
//		levelNumsForMusicInMult.Add("Ranch", 1003);
//		levelNumsForMusicInMult.Add("Arena_MP", 1004);
//		levelNumsForMusicInMult.Add("Sky_islands", 1005);
//		levelNumsForMusicInMult.Add("Dust", 1006);
//		levelNumsForMusicInMult.Add("Bridge", 1007);
//		levelNumsForMusicInMult.Add("Assault", 1008);
//		levelNumsForMusicInMult.Add("Town", 4007);
//		levelNumsForMusicInMult.Add("Abandoned_Pool", 4006);
//		levelNumsForMusicInMult.Add("Farm", 4001);
//		levelNumsForMusicInMult.Add("Utopia", 4002);
//		levelNumsForMusicInMult.Add("Arena", 7);
//		levelNumsForMusicInMult.Add("Winter", 4003);
//		levelNumsForMusicInMult.Add("campaignsuperreal", 4005);
//		levelNumsForMusicInMult.Add("School", 1009);
//		levelNumsForMusicInMult.Add("Hills", 4008);
//		levelNumsForMusicInMult.Add("desert_temple", 4009);
//		levelNumsForMusicInMult.Add("minecraft_church", 4010);
//		levelNumsForMusicInMult.Add("EndersHouse", 4011);
//		levelNumsForMusicInMult.Add("Upside_down_castle", 500);
//		levelNumsForMusicInMult.Add("weird", 501);
//		levelNumsForMusicInMult.Add("main_menu_but_how", 502);
//		levelNumsForMusicInMult.Add("ants_life_warehouse", 503);
//		levelNumsForMusicInMult.Add("PiratIsland", 504);
//		levelNumsForMusicInMult.Add("buntu", 505);
//		levelNumsForMusicInMult.Add("DripDust", 506);
//		levelNumsForMusicInMult.Add("Slender_but_bad", 507);
//		levelNumsForMusicInMult.Add("Training_11.2.4", 508);
//		levelNumsForMusicInMult.Add("rupture_of_time", 509);
//		levelNumsForMusicInMult.Add("northpolebut", 510);
//		levelNumsForMusicInMult.Add("Stupid52", 511);
//		levelNumsForMusicInMult.Add("COMZ_older_village", 512);
//		levelNumsForMusicInMult.Add("COMZ_unused_cemetery", 4013);
//		levelNumsForMusicInMult.Add("UniBuilder_Map2", 4015);
//		levelNumsForMusicInMult.Add("COMZ_unused_hospital", 513);
//		levelNumsForMusicInMult.Add("COMZ_test", 514);
//		levelNumsForMusicInMult.Add("COMZ_unused_parkingspace", 515);
//		levelNumsForMusicInMult.Add("SandboxSmall", 516);
//		levelNumsForMusicInMult.Add("noobite_map", 517);
//		levelNumsForMusicInMult.Add("GardenSmall", 518);
//		levelNumsForMusicInMult.Add("Utopia Castle", 519);
//		levelNumsForMusicInMult.Add("Hell Utopia", 520);
//		levelNumsForMusicInMult.Add("wholeblockworldmaphelpme", 521);
//		levelNumsForMusicInMult.Add("CRScene2", 522);
//		levelNumsForMusicInMult.Add("SWF_Level17", 4012);
//		levelNumsForMusicInMult.Add("COMZ_unused_church", 4013);
//		levelNumsForMusicInMult.Add("the truth", 523);
//		levelNumsForMusicInMult.Add("Zombie3D_Desert3", 524);
//		levelNumsForMusicInMult.Add("PSky_islands", 4016);
//		levelNumsForMusicInMult.Add("UtopiaWavy", 4017);
//		levelNumsForMusicInMult.Add("FajitaSky_islands", 6969);
//		levelNumsForMusicInMult.Add("Abandoned_Pool_old", 1001);
//		levelNumsForMusicInMult.Add("echo_room", 4018);
//		levelNumsForMusicInMult.Add("Supermarket", 4019);
//		levelNumsForMusicInMult.Add("Paradise_Night", 4020);
//		levelsWithVarY.Add(8);
//		levelsWithVarY.Add(10);
//		levelsWithVarY.Add(1005);
//		levelsWithVarY.Add(1006);
//		levelsWithVarY.Add(1007);
//		levelsWithVarY.Add(1008);
//		levelsWithVarY.Add(1009);
//		levelsWithVarY.Add(4001);
//		levelsWithVarY.Add(4002);
//		levelsWithVarY.Add(4003);
//	}
//
//	public Dictionary<string, string> mapNamesForUser = new Dictionary<string, string>();
//
//	public Dictionary<string, int> levelNumsForMusicInMult = new Dictionary<string, int>();
//
//	public List<int> levelsWithVarY = new List<int>();
//
//	//too lazy to find an actual way to do this but it was hardcoded before so
//	public MapInfoPosAndRot GetMapInfoPosAndRot(string mapName)
//	{
//		Vector3 position = new Vector3(17f, 11f, 17f);
//		Quaternion rotation = Quaternion.Euler(new Vector3(39f, 226f, 0f));
//		if (mapName.Equals("Maze"))
//		{
//			position = new Vector3(23f, 5.25f, -20.5f);
//			rotation = Quaternion.Euler(new Vector3(33f, -50f, 0f));
//		}
//		if (mapName.Equals("Cementery"))
//		{
//			position = new Vector3(17f, 11f, 17f);
//			rotation = Quaternion.Euler(new Vector3(39f, 226f, 0f));
//		}
//		if (mapName.Equals("Hospital"))
//		{
//			position = new Vector3(9.5f, 3.2f, 9.5f);
//			rotation = Quaternion.Euler(new Vector3(25f, -140f, 0f));
//		}
//		if (mapName.Equals("City"))
//		{
//			position = new Vector3(17f, 11f, 17f);
//			rotation = Quaternion.Euler(new Vector3(39f, 226f, 0f));
//		}
//		if (mapName.Equals("Jail"))
//		{
//			position = new Vector3(13.5f, 2.9f, 3.1f);
//			rotation = Quaternion.Euler(new Vector3(11f, -66f, 0f));
//		}
//		if (mapName.Equals("Gluk"))
//		{
//			position = new Vector3(17f, 11f, 17f);
//			rotation = Quaternion.Euler(new Vector3(39f, 226f, 0f));
//		}
//		if (mapName.Equals("Pool"))
//		{
//			position = new Vector3(-17.36495f, 5.448204f, -5.605346f);
//			rotation = Quaternion.Euler(new Vector3(31.34471f, 31.34471f, 0.2499542f));
//		}
//		if (mapName.Equals("Slender"))
//		{
//			position = new Vector3(31.82355f, 5.959687f, 37.378f);
//			rotation = Quaternion.Euler(new Vector3(36.08264f, -110.1159f, 2.307983f));
//		}
//		if (mapName.Equals("Castle"))
//		{
//			position = new Vector3(-12.3107f, 4.9f, 0.2716838f);
//			rotation = Quaternion.Euler(new Vector3(26.89935f, 89.99986f, 0f));
//		}
//		if (mapName.Equals("Bridge"))
//		{
//			position = new Vector3(-14.22702f, 14.6011f, -74.93485f);
//			rotation = Quaternion.Euler(new Vector3(24.68127f, -151.4293f, 0.2789154f));
//		}
//		if (mapName.Equals("Farm"))
//		{
//			position = new Vector3(22.4933f, 16.03175f, -35.17904f);
//			rotation = Quaternion.Euler(new Vector3(29.99995f, -28.62347f, 0f));
//		}
//		if (mapName.Equals("School"))
//		{
//			position = new Vector3(-19.52079f, 2.868755f, -19.50274f);
//			rotation = Quaternion.Euler(new Vector3(14.96701f, 40.79106f, 1.266037f));
//		}
//		if (mapName.Equals("Sky_islands"))
//		{
//			position = new Vector3(-3.111776f, 21.94557f, 25.31594f);
//			rotation = Quaternion.Euler(new Vector3(41.94537f, -143.1731f, 6.383652f));
//		}
//		if (mapName.Equals("Dust"))
//		{
//			position = new Vector3(-12.67253f, 6.92115f, 28.89415f);
//			rotation = Quaternion.Euler(new Vector3(28.46265f, 147.2818f, 0.2389221f));
//		}
//		if (mapName.Equals("Utopia"))
//		{
//			position = new Vector3(-10.62854f, 10.01794f, -51.20456f);
//			rotation = Quaternion.Euler(new Vector3(13.26845f, 16.31204f, 1.440735f));
//		}
//		if (mapName.Equals("Assault"))
//		{
//			position = new Vector3(19.36158f, 19.61019f, -24.24763f);
//			rotation = Quaternion.Euler(new Vector3(35.9299f, -11.80757f, -1.581451f));
//		}
//		if (mapName.Equals("Town"))
//		{
//			position = new Vector3(-52.1f, 14f, -146.5f);
//			rotation = Quaternion.Euler(new Vector3(4.136f, 10.833f, 0f));
//		}
//		if (mapName.Equals("Abandoned_Pool"))
//		{
//			position = new Vector3(19.36158f, 19.61019f, -24.24763f);
//			rotation = Quaternion.Euler(new Vector3(35.9299f, -11.80757f, -1.581451f));
//		}
//		if (mapName.Equals("campaignsuperreal"))
//		{
//			position = new Vector3(19.36158f, 19.61019f, -24.24763f);
//			rotation = Quaternion.Euler(new Vector3(35.9299f, -11.80757f, -1.581451f));
//		}
//		if (mapName.Equals("Hills"))
//		{
//			position = new Vector3(30.15f, 24.47f, -34.01f);
//			rotation = Quaternion.Euler(new Vector3(19.248f, -39.168f, 22.026f));
//		}
//		if (mapName.Equals("desert temple"))
//		{
//			position = new Vector3(30.15f, 24.47f, -34.01f);
//			rotation = Quaternion.Euler(new Vector3(19.248f, -39.168f, 22.026f));
//		}
//		if (mapName.Equals("EndersHouse"))
//		{
//			position = new Vector3(8.49f, 1.14f, 0.05f);
//			rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
//		}
//		if (mapName.Equals("minecraft_church"))
//		{
//			position = new Vector3(30.15f, 24.47f, -34.01f);
//			rotation = Quaternion.Euler(new Vector3(19.248f, -39.168f, 22.026f));
//		}
//		if (mapName.Equals("weird"))
//		{
//			position = new Vector3(30.15f, 24.47f, -34.01f);
//			rotation = Quaternion.Euler(new Vector3(19.248f, -39.168f, 22.026f));
//		}
//		if (mapName.Equals("main_menu_but_how"))
//		{
//			position = new Vector3(30.15f, 24.47f, -34.01f);
//			rotation = Quaternion.Euler(new Vector3(19.248f, -39.168f, 22.026f));
//		}
//		if (mapName.Equals("ants_life_warehouse"))
//		{
//			position = new Vector3(30.15f, 24.47f, -34.01f);
//			rotation = Quaternion.Euler(new Vector3(19.248f, -39.168f, 22.026f));
//		}
//		if (mapName.Equals("PiratIsland"))
//		{
//			position = new Vector3(30.15f, 24.47f, -34.01f);
//			rotation = Quaternion.Euler(new Vector3(19.248f, -39.168f, 22.026f));
//		}
//		if (mapName.Equals("buntu"))
//		{
//			position = new Vector3(30.15f, 24.47f, -34.01f);
//			rotation = Quaternion.Euler(new Vector3(19.248f, -39.168f, 22.026f));
//		}
//		if (mapName.Equals("DripDust"))
//		{
//			position = new Vector3(30.15f, 24.47f, -34.01f);
//			rotation = Quaternion.Euler(new Vector3(19.248f, -39.168f, 22.026f));
//		}
//		if (mapName.Equals("Slender_but_bad"))
//		{
//			position = new Vector3(30.15f, 24.47f, -34.01f);
//			rotation = Quaternion.Euler(new Vector3(19.248f, -39.168f, 22.026f));
//		}
//		if (mapName.Equals("Training_11.2.4"))
//		{
//			position = new Vector3(30.15f, 24.47f, -34.01f);
//			rotation = Quaternion.Euler(new Vector3(19.248f, -39.168f, 22.026f));
//		}
//		if (mapName.Equals("rupture_of_time"))
//		{
//			position = new Vector3(30.15f, 24.47f, -34.01f);
//			rotation = Quaternion.Euler(new Vector3(19.248f, -39.168f, 22.026f));
//		}
//		if (mapName.Equals("northpolebut"))
//		{
//			position = new Vector3(30.15f, 24.47f, -34.01f);
//			rotation = Quaternion.Euler(new Vector3(19.248f, -39.168f, 22.026f));
//		}
//		if (mapName.Equals("Stupid52"))
//		{
//			position = new Vector3(30.15f, 24.47f, -34.01f);
//			rotation = Quaternion.Euler(new Vector3(19.248f, -39.168f, 22.026f));
//		}
//		if (mapName.Equals("COMZ_older_village"))
//		{
//			position = new Vector3(30.15f, 24.47f, -34.01f);
//			rotation = Quaternion.Euler(new Vector3(19.248f, -39.168f, 22.026f));
//		}
//		if (mapName.Equals("COMZ_unused_hospital"))
//		{
//			position = new Vector3(11.2f, 2.71f, 9.9f);
//			rotation = Quaternion.Euler(new Vector3(0f, -45.053f, 0f));
//		}
//		if (mapName.Equals("COMZ_test"))
//		{
//			position = new Vector3(10.78f, 2.6f, -0.8f);
//			rotation = Quaternion.Euler(new Vector3(8.966001f, -99.898f, -1.191f));
//		}
//		if (mapName.Equals("COMZ_unused_parkingspace"))
//		{
//			position = new Vector3(3f, 2.18f, -2.15f);
//			rotation = Quaternion.Euler(new Vector3(5.661f, -131.569f, -1.738f));
//		}
//		if (mapName.Equals("SandboxSmall"))
//		{
//			position = new Vector3(-20.2f, 10f, -28.4f);
//			rotation = Quaternion.Euler(new Vector3(12.826f, 51.444f, 3.004f));
//		}
//		if (mapName.Equals("noobite_map"))
//		{
//			position = new Vector3(-20.2f, 10f, -28.4f);
//			rotation = Quaternion.Euler(new Vector3(12.826f, 51.444f, 3.004f));
//		}
//		if (mapName.Equals("GardenSmall"))
//		{
//			position = new Vector3(-25.14f, 13.22f, 19.2f);
//			rotation = Quaternion.Euler(new Vector3(17.454f, -219.103f, 1.253f));
//		}
//		if (mapName.Equals("Utopia Castle"))
//		{
//			position = new Vector3(-25.14f, 13.22f, 19.2f);
//			rotation = Quaternion.Euler(new Vector3(17.454f, -219.103f, 1.253f));
//		}
//		if (mapName.Equals("Hell Utopia"))
//		{
//			position = new Vector3(-25.14f, 13.22f, 19.2f);
//			rotation = Quaternion.Euler(new Vector3(17.454f, -219.103f, 1.253f));
//		}
//		if (mapName.Equals("wholeblockmaphelpme"))
//		{
//			position = new Vector3(-25.14f, 13.22f, 19.2f);
//			rotation = Quaternion.Euler(new Vector3(17.454f, -219.103f, 1.253f));
//		}
//		if (mapName.Equals("CRScene2"))
//		{
//			position = new Vector3(-25.14f, 13.22f, 19.2f);
//			rotation = Quaternion.Euler(new Vector3(17.454f, -219.103f, 1.253f));
//		}
//		if (mapName.Equals("SWF_Level17"))
//		{
//			position = new Vector3(-25.14f, 13.22f, 19.2f);
//			rotation = Quaternion.Euler(new Vector3(17.454f, -219.103f, 1.253f));
//		}
//		if (mapName.Equals("COMZ_unused_church"))
//		{
//			position = new Vector3(-26.1f, 10.7f, 36f);
//			rotation = Quaternion.Euler(new Vector3(16.286f, 129.54f, -0.033f));
//		}
//		if (mapName.Equals("the truth"))
//		{
//			position = new Vector3(-25.14f, 13.22f, 19.2f);
//			rotation = Quaternion.Euler(new Vector3(17.454f, -219.103f, 1.253f));
//		}
//		if (mapName.Equals("Zombie3D_Desert3"))
//		{
//			position = new Vector3(-25.14f, 13.22f, 19.2f);
//			rotation = Quaternion.Euler(new Vector3(17.454f, -219.103f, 1.253f));
//		}
//		if (mapName.Equals("UniBuilder_Map2"))
//		{
//			position = new Vector3(-25.14f, 13.22f, 19.2f);
//			rotation = Quaternion.Euler(new Vector3(17.454f, -219.103f, 1.253f));
//		}
//		if (mapName.Equals("PSky_islands"))
//		{
//			position = new Vector3(-25.14f, 13.22f, 19.2f);
//			rotation = Quaternion.Euler(new Vector3(17.454f, -219.103f, 1.253f));
//		}
//		if (mapName.Equals("UtopiaWavy"))
//		{
//			position = new Vector3(-25.14f, 13.22f, 19.2f);
//			rotation = Quaternion.Euler(new Vector3(17.454f, -219.103f, 1.253f));
//		}
//		if (mapName.Equals("FajitaSky_islands"))
//		{
//			position = new Vector3(-25.14f, 13.22f, 19.2f);
//			rotation = Quaternion.Euler(new Vector3(17.454f, -219.103f, 1.253f));
//		}
//		if (mapName.Equals("Abandoned_Pool_old"))
//		{
//			position = new Vector3(19.36158f, 19.61019f, -24.24763f);
//			rotation = Quaternion.Euler(new Vector3(35.9299f, -11.80757f, -1.581451f));
//		}
//		return new MapInfoPosAndRot { rot = rotation, pos = position };
//	}
//
//	public struct MapInfoPosAndRot
//	{
//		public Quaternion rot;
//
//		public Vector3 pos;
//	}
//}
//