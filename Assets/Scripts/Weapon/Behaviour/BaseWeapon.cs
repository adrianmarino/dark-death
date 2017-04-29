using UnityEngine;
using Fps.Weapon.State;

namespace Fps.Weapon
{
	public abstract class BaseWeapon : MonoBehaviour, IWeapon
	{
		//-----------------------------------------------------------------------------
		// Public Methods
		//-----------------------------------------------------------------------------

		public virtual bool Shoot (Transform origin, out RaycastHit target, LayerMask targetMask)
		{
			return State.Shoot (origin, out target, targetMask);
		}

		public virtual void HitTarget (Vector3 position, Vector3 normal)
		{
			State.HitTarget (position, normal);
		}

		public virtual void PlayShootEffect ()
		{
			State.PlayShootEffect ();
		}

		public virtual void Reload ()
		{
			State.Reload ();
		}

		public override string ToString ()
		{
			return Name;
		}

		public void Hide ()
		{
			model.SetActive (false);
		}

		public void Show ()
		{
			model.SetActive (true);
		}

		public bool ShootAction (Transform origin, out RaycastHit target, LayerMask targetMask)
		{
			return Physics.Raycast (
				origin.position, 
				origin.forward, 
				out target,
				targetMask
			);
		}

		public abstract void HitTargetAction (Vector3 position, Vector3 normal);

		public abstract void PlayShootEffectAction ();

		//-----------------------------------------------------------------------------
		// Protected Methods
		//-----------------------------------------------------------------------------

		protected abstract WeaponState InitState ();

		//-----------------------------------------------------------------------------
		// Properties
		//-----------------------------------------------------------------------------

		public string Name {
			get { return _name; }
		}

		protected WeaponState State {
			get {
				if (currentState == null)
					currentState = InitState ();
				return currentState;
			}
			set { currentState = value; }
		}

		public float Damage {
			get { return damage; }
		}

		public float Range {
			get { return range; }
		}

		public float FireRate {
			get { return fireRate; }
		}

		public GameObject GameObject {
			get { return this.gameObject; }
		}

		//-----------------------------------------------------------------------------
		// Properties
		//-----------------------------------------------------------------------------

		public int RemainAmmo {
			get { return State.RemainAmmo; }
		}

		//-----------------------------------------------------------------------------
		// Attributes
		//-----------------------------------------------------------------------------

		[SerializeField]
		private string _name;

		[SerializeField]
		private GameObject model;

		[SerializeField]
		private float damage = 25f;

		[SerializeField]
		private float range = 100f;

		[SerializeField]
		private float fireRate = 2f;

		private WeaponState currentState;

		//-----------------------------------------------------------------------------
		// Constructors
		//-----------------------------------------------------------------------------

		protected BaseWeapon ()
		{
			_name = this.GetType ().Name;
		}
	}
}

