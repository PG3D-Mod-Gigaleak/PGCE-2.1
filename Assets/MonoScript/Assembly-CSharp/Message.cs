using UnityEngine;

public sealed class Message : MonoBehaviour
{
	public GUIStyle labelStyle;

	public Rect rect = Player_move_c.SuccessMessageRect();

	public string message = "Purchases restored";

	public int depth = -2;

	private void Start()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		Invoke("Remove", 2f);
	}

	[Beebyte.Obfuscator.SkipRename]
	private void Remove()
	{
		Object.Destroy(base.gameObject);
	}

	private void OnGUI()
	{
		rect = Player_move_c.SuccessMessageRect();
		GUI.depth = depth;
		labelStyle.fontSize = Player_move_c.FontSizeForMessages;
		GUI.Label(rect, message, labelStyle);
	}
}
