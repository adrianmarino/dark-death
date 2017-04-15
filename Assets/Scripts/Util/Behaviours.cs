using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Util
{
	public class Behaviours
	{
		public static void DisableAll (List<Behaviour> components)
		{
			SetEnable (components, false);
		}

		public static void Enable (List<Behaviour> components)
		{
			SetEnable (components, true);
		}

		public static void SetEnable (List<Behaviour> components, bool value)
		{
			components
				.Where (it => it != null) 
				.ToList <Behaviour> ()
				.ForEach (it => it.enabled = value);
		}
	}
}

