using System.Collections.Generic;

public class VirtualCurrencyHelper
{
	public static int[] coinInappsQuantity;

	public static Dictionary<string, int> prices;

	static VirtualCurrencyHelper()
	{
		coinInappsQuantity = new int[7] { 15, 30, 45, 80, 165, 330, 800 };
		prices = new Dictionary<string, int>();
		prices.Add("crystalsword", 75);
		prices.Add("Fullhealth", 15);
		prices.Add("bigammopack", 15);
		prices.Add("MinerWeapon", 30);
		prices.Add(StoreKitEventListener.elixirID, 15);
		prices.Add(StoreKitEventListener.combatrifle, 75);
		prices.Add(StoreKitEventListener.magicbow, 150);
		prices.Add(StoreKitEventListener.goldeneagle, 30);
		prices.Add(StoreKitEventListener.chief, 40);
		prices.Add(StoreKitEventListener.nanosoldier, 40);
		prices.Add(StoreKitEventListener.endmanskin, 40);
		prices.Add(StoreKitEventListener.spaceengineer, 40);
		prices.Add(StoreKitEventListener.steelman, 40);
		prices.Add(StoreKitEventListener.CaptainSkin, 40);
		prices.Add(StoreKitEventListener.HawkSkin, 40);
		prices.Add(StoreKitEventListener.TunderGodSkin, 40);
		prices.Add(StoreKitEventListener.GreenGuySkin, 40);
		prices.Add(StoreKitEventListener.GordonSkin, 40);
		prices.Add(StoreKitEventListener.axe, 45);
		prices.Add(StoreKitEventListener.spas, 60);
		prices.Add(StoreKitEventListener.armor, 15);
		prices.Add(StoreKitEventListener.chainsaw, 75);
		prices.Add(StoreKitEventListener.famas, -99999);
		prices.Add(StoreKitEventListener.glock, 45);
		prices.Add(StoreKitEventListener.scythe, 60);
		prices.Add(StoreKitEventListener.shovel, 30);
		prices.Add(StoreKitEventListener.hammer, 100);
		prices.Add(StoreKitEventListener.sword_2, 150);
		prices.Add(StoreKitEventListener.staff, 180);
		prices.Add(StoreKitEventListener.laser, 180);
		prices.Add(StoreKitEventListener.lightSword, 120);
		prices.Add(StoreKitEventListener.beretta, 90);
		prices.Add(StoreKitEventListener.magicGirl, 40);
		prices.Add(StoreKitEventListener.braveGirl, 40);
		prices.Add(StoreKitEventListener.glamDoll, 40);
		prices.Add(StoreKitEventListener.kittyGirl, 40);
		prices.Add(StoreKitEventListener.famosBoy, 40);
		prices.Add(StoreKitEventListener.mace, 100);
		prices.Add(StoreKitEventListener.crossbow, 150);
		prices.Add(StoreKitEventListener.minigun, 200);
	}
}
