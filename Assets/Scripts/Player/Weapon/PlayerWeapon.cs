using UnityEngine;

[System.Serializable]
public class PlayerWeapon
{
	public string name = "HeavyBlaster";

	public float damage = 25f;
	public float range = 100f;

	public float fireRate = 2f;

	public GameObject graphics;
}
