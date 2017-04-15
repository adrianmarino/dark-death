using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

namespace Fps
{
	public class Player : NetworkBehaviour
	{
		//-----------------------------------------------------------------------------
		// Engine Methods
		//-----------------------------------------------------------------------------

		void Start ()
		{
			Setup ();
		}

		[ClientRpc]
		public void RpcTakeDamage (float damage)
		{
			if (Dead)
				return;

			DecreaseHealth (damage);

			if (currentHealth <= 0) {
				Die ();
				Restart ();
			}
		}

		void Update ()
		{
			if (!isLocalPlayer)
				return;
			ShowCurrentHealth ();

			if (Input.GetKeyDown (KeyCode.K))
				RpcTakeDamage (99999);
		}

		//-----------------------------------------------------------------------------
		// Public Methods
		//-----------------------------------------------------------------------------

		public void Setup ()
		{
			Dead = false;
			currentHealth = maxHealth;
			LoadComponentsEnableState ();
			// Rigidbody ().useGravity = true;
			// SetEnableCollider (true);
		}

		//-----------------------------------------------------------------------------
		// Private Methods
		//-----------------------------------------------------------------------------

		void ShowCurrentHealth ()
		{
			healthPanel.text = "Health: " + currentHealth;
		}

		void DecreaseHealth (float damage)
		{
			currentHealth -= damage;
			Debug.Log (this.name + " current health: " + currentHealth);
		}

		void SaveComponentsEnableState ()
		{
			componentEnableStates.Clear ();
			foreach (Behaviour component in disableOnDeath)
				componentEnableStates.Add (component, component.enabled);
		}

		void LoadComponentsEnableState ()
		{
			foreach (Behaviour component in componentEnableStates.Keys)
				component.enabled = componentEnableStates [component];
		}

		void SetEnableCollider (bool value)
		{
			Collider component = GetComponent<Collider> ();
			if (component != null)
				component.enabled = value;
		}

		Rigidbody Rigidbody ()
		{
			return GetComponent<Rigidbody> ();
		}

		void Die ()
		{
			Dead = true;
			SaveComponentsEnableState ();
			Util.Behaviour.DisableAll (disableOnDeath);
			// Rigidbody ().useGravity = false;
			// SetEnableCollider (false);
			Debug.Log (this.name + " is dead!");
			PerformDeadEffect ();
		}

		void PerformDeadEffect ()
		{
			Instantiate (deadEffect, transform.position, Quaternion.identity);
		}

		void MoveToStartPoint ()
		{
			Transform startPoint = NetworkManager.singleton.GetStartPosition ();
			transform.position = startPoint.position;
			transform.rotation = startPoint.rotation;
		}

		void Restart ()
		{
			StartCoroutine (Respawn (4f));
		}

		IEnumerator Respawn (float delay)
		{
			yield return new WaitForSeconds (delay);
			Setup ();
			MoveToStartPoint ();
		}

		//-----------------------------------------------------------------------------
		// Properties
		//-----------------------------------------------------------------------------
	
		public bool Dead {
			get { 
				return dead;
			}
			protected set { 
				dead = value;
			}
		}

		//-----------------------------------------------------------------------------
		// Attributes
		//-----------------------------------------------------------------------------

		[SerializeField]
		private float maxHealth = 100f;
	
		[SerializeField]
		public Text healthPanel;

		[SerializeField]
		public List<Behaviour> disableOnDeath;

		[SerializeField]
		private GameObject deadEffect;

		[SyncVar]
		private float currentHealth;

		[SyncVar]
		private bool dead;

		private Dictionary<Behaviour, bool> componentEnableStates = new Dictionary<Behaviour, bool> ();
	}
}