using System;

public class NewArmory
{
    public static string GetCatStringFromCategoryType(CategoryType category)
	{
		switch (category)
		{
			case CategoryType.Backup:
			    return "cat2";
			case CategoryType.Melee:
			    return "cat3";
			case CategoryType.Special:
			    return "cat4";
			case CategoryType.Heavy:
			    return "cat5";
			default:
			    return "cat1";
		}
	}
}