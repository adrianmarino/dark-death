using UnityEngine;

namespace Util
{
	public class ObjectElement
	{
		public static bool Grounded (MonoBehaviour behaviour, float distance)
		{
			return Physics.RaycastAll (
				behaviour.transform.position, 
				Vector2.down,
				distance
			).Length > 0;
		}
	}
}
