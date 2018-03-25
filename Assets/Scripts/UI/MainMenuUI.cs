using Fps;
using Fps.UI;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {

	void Start()
	{
		version.text = GameManager.Version();
	}

	void Update()
	{
		if (!Input.GetKeyDown(KeyCode.Escape)) return;

		yesNoModal.Setup()
			.Question("Do you want to quit the game?")
			.OnYes(modal => GameManager.Quit())
			.Show();
	}

	#region Attributes

	[SerializeField] YesNoModal yesNoModal;

	[SerializeField] Text version;

	#endregion
}
