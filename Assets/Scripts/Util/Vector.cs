using UnityEngine;

namespace Util
{
	public class VelocityVector
	{
		public  static Vector3 create (Transform transform, Vector2 horVerVariation, float speed)
		{
			return create (transform, horVerVariation.x, horVerVariation.y, speed);
		}

		public  static UnityEngine.Vector3 create (Transform transform, float xVariation, 
		                                           float zVariation, float speed)
		{
			Vector3 xMovement = transform.right * xVariation;
			Vector3 zMovement = transform.forward * zVariation;

			return (xMovement + zMovement).normalized * speed;
		}

		//-----------------------------------------------------------------------------
		// Constructors
		//-----------------------------------------------------------------------------

		private VelocityVector ()
		{
		}
	}
}
