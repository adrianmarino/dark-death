using UnityEngine;
using System.Collections;
using System;


namespace Util
{
	public class GameObjects
	{
		public static GameObject[] Instances (string name)
		{
			GameObject[] gameObjects = GameObject.FindGameObjectsWithTag (name);
			if (gameObjects.Length == 0)
				throw new Exception ("Cannot find any " + name + " object!");
			return gameObjects;
		}

		public static ELEMENT Instance<ELEMENT> (string name)
		{
			return Instances (name) [0].GetComponent <ELEMENT> ();
		}
	}

}