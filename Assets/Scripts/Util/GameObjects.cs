using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Util
{
	public class GameObjects
	{
		public static void DisableAll (List<GameObject> components)
		{
			SetEnable (components, false);
		}

		public static void Enable (List<GameObject> components)
		{
			SetEnable (components, true);
		}

		public static void SetEnable (List<GameObject> components, bool value)
		{
			components.Where (it => it != null)
				.ToList<GameObject> ()
				.ForEach (it => it.SetActive (value));
		}

		public static void SetRenderedEnable (List<GameObject> components, bool value)
		{
			components.Where (it => it != null)
				.ToList<GameObject> ()
				.ForEach (it => SetRenderedEnable (it, value));
		}

		public static void SetRenderedEnable (GameObject component, bool value)
		{
			component.GetComponent<Renderer> ().enabled = value;
		}

		public static bool GetRenderedEnable (GameObject component)
		{
			return component.GetComponent<Renderer> ().enabled;
		}

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

		public static ELEMENT Instance<ELEMENT> (string name, GameObject from)
		{
			return Instances (name) [0].GetComponent <ELEMENT> ();
		}
	}

}