using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoryNGUI : MonoBehaviour
{
    public GameObject weaponButton;

    private List<WeaponNGUIButton> cachedButtons = new List<WeaponNGUIButton>();

    public UIGrid[] categoryGrids;

    public ArmoryCategoryButton[] categoryButtons;

    private GameObject selectedWeapon;

    public Transform characterWeaponInstantiationPoint;

    public GameObject pers;

    public UIScrollView scrollView;

    public static ArmoryNGUI instance;

    void Start()
    {
        foreach (GameObject sounds in WeaponManager.WeaponPrefabs)
        {
            CreateButton(sounds.GetComponent<WeaponSounds>());
        }

        foreach (UIGrid categoryGrid in categoryGrids)
        {
            categoryGrid.Reposition();
        }

        Primary();

        instance = this;
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        Check();
    }

    public void Check()
    {
        bool active = true;
        if (!Application.loadedLevelName.StartsWith("Menu_"))
        {
            if (Globals.PlayerMove != null)
            {
                if (!Globals.PlayerMove.actuallyOpenIt)
                {
                    active = false;
                }
            }
            else
            {
                active = false;
            }
        }
        gameObject.SetActive(active);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
    }

    private void CreateButton(WeaponSounds sounds)
    {
        WeaponNGUIButton weaponBtn = Instantiate(weaponButton, categoryGrids[(int)sounds.category].transform).GetComponent<WeaponNGUIButton>();

        weaponBtn.nameLabel.text = sounds.weaponName.Replace("newline", System.Environment.NewLine);

        GameObject weapon = Instantiate(sounds.bonusPrefab, weaponBtn.instantiationPoint);
        weapon.gameObject.layer = 10;
		foreach (Transform trf in weapon.gameObject.GetComponentsInChildren<Transform>())
		{
			trf.gameObject.layer = 10;
		}

        weapon.transform.localPosition = sounds.armoryPosition;
        weapon.transform.localRotation = Quaternion.Euler(sounds.armoryRotation);
        weapon.transform.localScale = sounds.armoryScale;

        if (weapon.GetComponent<SkinnedMeshRenderer>())
		{
			GameObject newModel = new GameObject("NewModel_" + weapon.name);
			newModel.AddComponent<MeshFilter>().sharedMesh = weapon.GetComponent<SkinnedMeshRenderer>().sharedMesh;
			newModel.AddComponent<MeshRenderer>().materials = weapon.GetComponent<SkinnedMeshRenderer>().materials;
			newModel.transform.parent = weapon.transform;
			newModel.transform.localScale = Vector3.one;
			newModel.transform.localPosition = Vector3.zero;
			newModel.transform.localRotation = Quaternion.Euler(sounds.armoryRotation);
            newModel.layer = 10;
		}

        EventDelegate.Add(weaponBtn.GetComponent<UIButton>().onClick, ()=> { SelectWeapon(sounds); } );
    }

    private void SelectWeapon(WeaponSounds sounds)
    {
        if (selectedWeapon != null)
        {
            Destroy(selectedWeapon);
        }

        selectedWeapon = Instantiate(sounds, characterWeaponInstantiationPoint).gameObject;
        selectedWeapon.transform.localPosition = Vector3.zero;

        Animation anim = sounds.animationObject.GetComponent<Animation>();
        if (anim["Profile"] != null)
        {
            anim.Play("Profile");
        }
        
        selectedWeapon.layer = 10;
        foreach (Transform trf in selectedWeapon.gameObject.GetComponentsInChildren<Transform>())
        {
            trf.gameObject.layer = 10;
        }

        Texture texture = SkinsManager.currentMultiplayerSkin();
		texture.filterMode = FilterMode.Point;
		Player_move_c.SetTextureRecursivelyFrom(pers, texture, new GameObject[1] { selectedWeapon.GetComponent<WeaponSounds>().bonusPrefab });

        if (!Application.loadedLevelName.StartsWith("Menu_"))
        {
            Globals.PlayerMove._weaponManager.ResetCategory("cat" + ((int)sounds.category + 1), sounds.name);
            Globals.PlayerMove.ChangeWeaponFull(sounds.category);
        }
        else
        {
            prefs.SetString("cat" + ((int)sounds.category + 1), sounds.name);
        }
    }

    public void Primary() { SetCategory(0); }

    public void Secondary() { SetCategory(1); }

    public void Melee() { SetCategory(2); }

    public void Special() { SetCategory(3); }

    public void Heavy() { SetCategory(4); }

    public void Back(bool gameOver = false)
    {
        if (Application.loadedLevelName.StartsWith("Menu_"))
        {
            MenuGUI.instance.gameObject.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
            if (!gameOver)
            {
                Globals.PlayerMove.ReEnableCams();
                Globals.PlayerMove.actuallyOpenIt = false;
            }
        }
        gameObject.SetActive(false);
    }

    public void SetCategory(int index)
    {
        scrollView.ResetPosition();

        for (int i = 0; i < categoryGrids.Length; i++)
        {
            categoryGrids[i].gameObject.SetActive(i == index);
            categoryButtons[i].pressed = i == index;
        }

        if (selectedWeapon != null)
        {
            Destroy(selectedWeapon);
        }

        WeaponSounds sounds = null;

        foreach (GameObject weapon in WeaponManager.WeaponPrefabs)
        {
            if (weapon.name == prefs.GetString("cat" + (index + 1)))
            {
                sounds = weapon.GetComponent<WeaponSounds>();
                break;
            }
        }

        selectedWeapon = Instantiate(sounds, characterWeaponInstantiationPoint).gameObject;
        selectedWeapon.transform.localPosition = Vector3.zero;

        Animation anim = sounds.animationObject.GetComponent<Animation>();
        if (anim["Profile"] != null)
        {
            anim.Play("Profile");
        }

        selectedWeapon.layer = 10;
        foreach (Transform trf in selectedWeapon.gameObject.GetComponentsInChildren<Transform>())
        {
            trf.gameObject.layer = 10;
        }

        Texture texture = SkinsManager.currentMultiplayerSkin();
		texture.filterMode = FilterMode.Point;
		Player_move_c.SetTextureRecursivelyFrom(pers, texture, new GameObject[1] { selectedWeapon.GetComponent<WeaponSounds>().bonusPrefab });
    }
}
