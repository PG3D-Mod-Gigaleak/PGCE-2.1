using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnMenu : MonoBehaviour
{
	public List<Category> objectCategories = new List<Category>();

    public List<Category> photonObjectCategories = new List<Category>();

    [System.Serializable]
    public class Category
    {
        public string key;

        public List<GameObject> value = new List<GameObject>();
    }

    public string[] resourceFolders;

    public List<Category> LoadAllCategories()
    {
        List<Category> categories = new List<Category>();
        foreach (string resourceFolder in resourceFolders)
        {
            categories.Add(new Category { key = resourceFolder, value = new List<GameObject>(Resources.LoadAll<GameObject>(resourceFolder)) });
        }
        return categories;
    }

    public List<Category> GroupPhotonViewObjects(List<Category> categories)
    {
        List<Category> newCategories = new List<Category>();
        for (int i = 0; i < categories.Count; i++)
        {
            newCategories.Add(new Category { key = categories[i].key, value = (from categoryObject in categories[i].value.ToArray() where categoryObject.GetComponent<PhotonView>() != null select categoryObject).ToList() } );
        }
        return newCategories;
    }

    void Start()
    {
        objectCategories = LoadAllCategories();
        photonObjectCategories = GroupPhotonViewObjects(objectCategories);
    }
}
