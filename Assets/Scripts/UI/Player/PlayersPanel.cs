using Fps;
using UnityEngine;

public class PlayersPanel : MonoBehaviour {

	void OnGUI()
	{
		if (Event.current.type == EventType.Repaint)
			GuiUtils.PlayersWindow(GameManager.Players(), BoxSize());
	}

	private static GameManager GameManager
	{
		get { return GameManager.Instance; }
	}

	private Rect BoxSize()
	{
		return new Rect(x, Screen.height + yOffset, width, height);
	}

	[SerializeField] private float x = 5;

	[SerializeField] private float yOffset = - 160;

	[SerializeField] private float width = 88;
	
	[SerializeField] private float height = 120;
}
