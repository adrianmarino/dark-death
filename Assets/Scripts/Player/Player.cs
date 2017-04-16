using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

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
			LoadActiveStates ();
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

		void SaveActiveStates ()
		{
			SaveBehaviourActiveStates ();
			SaveGameObjectActiveStates ();
		}

		void SaveBehaviourActiveStates ()
		{
			behaviourActiveStates.Clear ();
			disableBehaviourOnDeath.ForEach (it => behaviourActiveStates.Add (it, it.enabled));
		}

		void SaveGameObjectActiveStates ()
		{
			gameObjectActiveStates.Clear ();
			disableGameObjectsOnDeath.ForEach (it => gameObjectActiveStates.Add (it, it.activeSelf));
		}

		void LoadActiveStates ()
		{
			LoadBehaviourActiveStates ();
			LoadGameObjectActiveStates ();
		}

		void LoadBehaviourActiveStates ()
		{
			behaviourActiveStates.ToList ().ForEach (entry => entry.Key.enabled = entry.Value);
		}

		void LoadGameObjectActiveStates ()
		{
			gameObjectActiveStates.ToList ().ForEach (it => it.Key.SetActive (it.Value));
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

		void DisableAllBehaviours ()
		{
			Util.Behaviours.DisableAll (disableBehaviourOnDeath);
		}

		void DisableAllGameObjects ()
		{
			Util.GameObjects.DisableAll (disableGameObjectsOnDeath);
		}

		void Die ()
		{
			Dead = true;
			SaveActiveStates ();
			DisableAllBehaviours ();
			DisableAllGameObjects ();
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
		public List<Behaviour> disableBehaviourOnDeath;

		[SerializeField]
		public List<GameObject> disableGameObjectsOnDeath;

		[SerializeField]
		private GameObject deadEffect;

		[SyncVar]
		private float currentHealth;

		[SyncVar]
		private bool dead;

		private Dictionary<Behaviour, bool> behaviourActiveStates = new Dictionary<Behaviour, bool> ();

		private Dictionary<GameObject, bool> gameObjectActiveStates = new Dictionary<GameObject, bool> ();

	}
}