using System.Collections.Generic;
using System.Linq;

namespace Util
{
	public class Behaviour
	{
		public static void DisableAll (List<UnityEngine.Behaviour> components)
		{
			SetEnable (components, false);
		}

		public static void Enable (List<UnityEngine.Behaviour> components)
		{
			SetEnable (components, true);
		}

		public static void SetEnable (List<UnityEngine.Behaviour> components, bool value)
		{
			components.Where (it => it != null)
				.ToList<UnityEngine.Behaviour> ()
				.ForEach (it => it.enabled = value);
		}
	}
}

