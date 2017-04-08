using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

namespace Fps
{
	public class Player : NetworkBehaviour
	{
		//-----------------------------------------------------------------------------
		// Engine Methods
		//-----------------------------------------------------------------------------

		void Awake ()
		{
			SetDefaultState ();
		}

		void Update ()
		{
			ShowCurrentHealth ();
		}

		//-----------------------------------------------------------------------------
		// Public Methods
		//-----------------------------------------------------------------------------

		public void TakeDamage (float damage)
		{
			currentHealth -= damage;
			Debug.Log (this.name + " current health: " + currentHealth);
		}

		//-----------------------------------------------------------------------------
		// Private Methods
		//-----------------------------------------------------------------------------

		void ShowCurrentHealth ()
		{
			healthPanel.text = "Health: " + currentHealth;
		}

		void SetDefaultState ()
		{
			currentHealth = maxHealth;
		}

		//-----------------------------------------------------------------------------
		// Attributes
		//-----------------------------------------------------------------------------

		[SerializeField]
		private float maxHealth = 100f;

		[SyncVar]
		private float currentHealth;

		[SerializeField]
		public Text healthPanel;

	}

}