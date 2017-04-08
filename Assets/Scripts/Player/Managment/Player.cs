using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Collections;

namespace Fps
{
	public class Player : NetworkBehaviour
	{
		//-----------------------------------------------------------------------------
		// Engine Methods
		//-----------------------------------------------------------------------------

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
			SetEnableCollider (true);
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
			Collider colliderComponent = GetComponent<Collider> ();
			if (colliderComponent != null)
				colliderComponent.enabled = value;
		}

		void Die ()
		{
			Dead = true;
			SaveComponentsEnableState ();
			Util.Behaviour.DisableAll (disableOnDeath);
			SetEnableCollider (false);
			Debug.Log (this.name + " is dead!");
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

		[SyncVar]
		private float currentHealth;

		[SyncVar]
		private bool dead;

		private Dictionary<Behaviour, bool> componentEnableStates = new Dictionary<Behaviour, bool> ();
	}
}