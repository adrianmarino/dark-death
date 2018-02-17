using Fps;
using Fps.Player;
using Fps.UI;
using UnityEngine;
using UnityEngine.UI;
using Util;
using Input = UnityEngine.Input;

[RequireComponent(typeof(LocalPayerAccessor))]
public class LevelUI : MonoBehaviour {

	void Start()
	{
		version.text = GameManager.Instance.Version();
	}

	private void Update()
	{
		if (!Input.GetKeyDown(KeyCode.Escape)) return;

		Player.Pause();

		yesNoModal.Setup()
			.Question("Do you want finish match?")
			.OnYes(modal => GameManager.Instance.CloseMatch())
			.OnNo(modal => {
				modal.Close();
				Player.Resume();
			})
			.NotClose()
			.Show();
	}

	PlayerPauseManager Player
	{
		get { return GetComponent<LocalPayerAccessor>().GetComponent<PlayerPauseManager>(); }
	}

	#region Attributes

	[SerializeField] private YesNoModal yesNoModal;

	[SerializeField] Text version;

	#endregion
}
