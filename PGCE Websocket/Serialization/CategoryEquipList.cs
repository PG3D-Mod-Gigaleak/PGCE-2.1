using System;

namespace PG3D.Serialization
{
	public struct CategoryEquipList
	{
		public string cat1 = "Weapon1";
		public string cat2 = "Weapon2";
		public string cat3 = "Weapon9";
		public string cat4 = "";
		public string cat5 = "";

		public CategoryEquipList()
		{
			this.cat1 = "Weapon1";
			this.cat2 = "Weapon2";
			this.cat3 = "Weapon9";
			this.cat4 = "";
			this.cat5 = "";
		}
		public void SetWeaponForCat(CategoryType type, string name)
		{
			if (type == CategoryType.Primary)
				this.cat1 = name;
			if (type == CategoryType.Backup)
				this.cat2 = name;
			if (type == CategoryType.Melee)
				this.cat3 = name;
			if (type == CategoryType.Special)
				this.cat4 = name;
			if (type == CategoryType.Heavy)
				this.cat5 = name;
		}
	}
}