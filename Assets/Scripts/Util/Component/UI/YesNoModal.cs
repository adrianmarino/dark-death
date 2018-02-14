using UnityEngine;
using UnityEngine.UI;

namespace Fps.UI
{
	public class YesNoModal : MonoBehaviour
	{	
		public delegate void OnYesDelegate();

		public delegate void OnNoDelegate();

		#region Events

		private void Start()
		{
			gameObject.SetActive(false);
		}

		public void OnYes()
		{
			onYesDelegate();
			gameObject.SetActive(false);
		}

		public void OnNo()
		{
			onNoDelegate();
			gameObject.SetActive(false);
		}

		#endregion

		public void Show(string question, OnYesDelegate onYesDelegate, OnNoDelegate onNoDelegate)
		{
			this.onYesDelegate = onYesDelegate;
			this.onNoDelegate = onNoDelegate;
			this.question.text = question;
			gameObject.SetActive(true);
		}

		private OnYesDelegate onYesDelegate;
		
		private OnNoDelegate onNoDelegate;

		[SerializeField] private Text question;
	}
}