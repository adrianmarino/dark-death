using UnityEngine;

namespace Fps.Weapon
{
	public class WeaponFactory : MonoBehaviour
	{
		public IWeapon InstantiateOnHolder (GameObject weapon, Transform holder)
		{
			GameObject instance = Instantiate (
				                      weapon, 
				                      holder.position, 
				                      holder.rotation
			                      );
			instance.transform.SetParent (holder);

			IWeapon weaponComponent = instance.GetComponent<IWeapon> ();
			if (weapon == null)
				Debug.Log ("Not found weapon component in weapon prefab!");
			return weaponComponent;
		}
	}
}

