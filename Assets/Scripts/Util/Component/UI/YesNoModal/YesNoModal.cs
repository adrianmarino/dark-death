using UnityEngine;
using UnityEngine.UI;
using Util.Component.UI;

namespace Fps.UI
{
	public class YesNoModal : MonoBehaviour
	{
		public enum CloseOption
		{
			CloseAfterAnswer,
			NotClose
		}

		public delegate void OnYesDelegate(YesNoModal modal);

		public delegate void OnNoDelegate(YesNoModal modal);

		#region Events

		private void Start()
		{
			gameObject.SetActive(false);
		}

		public void OnYes()
		{
			gameObject.SetActive(closeOption == CloseOption.NotClose);
			onYesDelegate(this);
		}

		public void OnNo()
		{
			gameObject.SetActive(closeOption == CloseOption.NotClose);
			onNoDelegate(this);
		}

		#endregion

		public void Close()
		{
			gameObject.SetActive(false);
		}

		public YesNoModalBuilder Setup()
		{
			return new YesNoModalBuilder(this);
		}
		
		protected internal void Show(string question, CloseOption closeOption, OnYesDelegate onYesDelegate, OnNoDelegate onNoDelegate)
		{
			this.onYesDelegate = onYesDelegate;
			this.onNoDelegate = onNoDelegate;
			this.question.text = question;
			gameObject.SetActive(true);
			this.closeOption = closeOption;
		}

		private OnYesDelegate onYesDelegate;
		
		private OnNoDelegate onNoDelegate;

		private CloseOption closeOption;

		[SerializeField] private Text question;
	}
}