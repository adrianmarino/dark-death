using UnityEngine;
using System.Collections;
using Fps;

namespace Fps
{
	public class WeaponFactory : MonoBehaviour
	{
		public static Weapon InstantiateOnHolder (GameObject weapon, Transform holder)
		{
			GameObject instance = Instantiate (
				                      weapon, 
				                      holder.position, 
				                      holder.rotation
			                      );
			instance.transform.SetParent (holder);

			Weapon weaponComponent = instance.GetComponent<Weapon> ();
			if (weapon == null)
				Debug.Log ("Not found weapon component in weapon prefab!");
			return weaponComponent;
		}
	}
}

