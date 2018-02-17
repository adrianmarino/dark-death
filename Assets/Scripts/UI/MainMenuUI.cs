using Fps;
using Fps.UI;
using UnityEngine;

public class MainMenuUI : MonoBehaviour {

	private void Update()
	{
		if (!Input.GetKeyDown(KeyCode.Escape)) return;

		yesNoModal.Setup()
			.Question("Do you want to quit the game?")
			.OnYes(modal => GameManager.Instance.Quit())
			.Show();
	}

	#region Attributes

	[SerializeField] private YesNoModal yesNoModal;

	#endregion
}
