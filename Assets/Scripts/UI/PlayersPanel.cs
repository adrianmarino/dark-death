using Fps;
using UnityEngine;

public class PlayersPanel : MonoBehaviour {

	void OnGUI()
	{
		Debug.Log("WRITE PLAYERS!!!" + GameManager.Players());
		GuiUtils.PlayersWindow(GameManager.Players(), BoxSize());
	}

	private static GameManager GameManager
	{
		get { return GameManager.Instance; }
	}

	private Rect BoxSize()
	{
		return new Rect(5, Screen.height + yOffset, width, height);
	}

	[SerializeField] private float x = 5;

	[SerializeField] private float yOffset = - 160;

	[SerializeField] private float width = 88;
	
	[SerializeField] private float height = 120;
}
