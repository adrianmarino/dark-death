using UnityEngine;
using System.Collections;

namespace Util.Component.UI
{
	public class FPSDisplay : TextPanel
	{
		//-----------------------------------------------------------------------------
		// Event Methods
		//-----------------------------------------------------------------------------

		void Reset ()
		{
			Value = "xxx fps";
		}

		void Start ()
		{
			StartCoroutine (FPS ());
		}
			
		//-----------------------------------------------------------------------------
		// Private Methods
		//-----------------------------------------------------------------------------

		IEnumerator FPS ()
		{
			while (true) {
				// Capture frame-per-second
				int lastFrameCount = Time.frameCount;
				float lastTime = Time.realtimeSinceStartup;

				yield return new WaitForSeconds (frequency);

				float timeSpan = Time.realtimeSinceStartup - lastTime;
				int frameCount = Time.frameCount - lastFrameCount;

				// Display it
				FramesPerSec = Mathf.RoundToInt (frameCount / timeSpan);
				Value = FramesPerSec.ToString () + " fps";
			}
		}

		//-----------------------------------------------------------------------------
		// Properties
		//-----------------------------------------------------------------------------

		public int FramesPerSec { get; protected set; }

		//-----------------------------------------------------------------------------
		// Attributes
		//-----------------------------------------------------------------------------

		[SerializeField]
		private float frequency = 0.5f;
	}
}
