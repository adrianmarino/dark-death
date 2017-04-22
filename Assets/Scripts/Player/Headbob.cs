using UnityEngine;
using System;
using UnityEngine.Networking;

public class Headbob : NetworkBehaviour
{
	//-----------------------------------------------------------------------------
	// Event Methods
	//-----------------------------------------------------------------------------

	void Update ()
	{
		if (!IsGrounded ())
			return;

		if (Util.Input.IsMoving ()) {
			// Use the timer value to set the position
			timer += bobSpeed * Time.deltaTime;

			// Abs val of y for a parabolic path
			Position = NextHeadBobMovementPostion (timer);
		} else {
			timer = TIMER_INITIAL_VALUE;

			// Transition smoothly from walking to stopping.
			Position = NextStopTransationPosition ();
		}

		// Completed a full cycle on the unit circle. 
		// Reset to 0 to avoid bloated values.
		if (timer > Mathf.PI * 2)
			timer = 0;
	}

	//-----------------------------------------------------------------------------
	// Private Methods
	//-----------------------------------------------------------------------------

	bool IsGrounded ()
	{
		return Util.ObjectElement.IsGrounded (this, floorDistance);
	}

	Vector3 NextHeadBobMovementPostion (float _timer)
	{
		return new Vector3 (
			Mathf.Cos (_timer) * bobAmount, 
			restPosition.y + Mathf.Abs ((Mathf.Sin (_timer) * bobAmount)), 
			restPosition.z
		);
	}

	Vector3 NextStopTransationPosition ()
	{
		return new Vector3 (
			Mathf.Lerp (Position.x, restPosition.x, transitionSpeed * Time.deltaTime), 
			Mathf.Lerp (Position.y, restPosition.y, transitionSpeed * Time.deltaTime), 
			Mathf.Lerp (Position.z, restPosition.z, transitionSpeed * Time.deltaTime)
		);
	}

	//-----------------------------------------------------------------------------
	// Properties
	//-----------------------------------------------------------------------------

	Vector3 Position {
		get { return transform.localPosition; }
		set { transform.localPosition = value; }
	}

	//-----------------------------------------------------------------------------
	// Constants
	//-----------------------------------------------------------------------------

	private const float TIMER_INITIAL_VALUE = Mathf.PI / 2;

	//-----------------------------------------------------------------------------
	// Attributes
	//-----------------------------------------------------------------------------

	[SerializeField]
	private Vector3 restPosition;

	// Local position where your camera would rest when it's not bobbing.
	[SerializeField]
	private float transitionSpeed = 20f;

	// Smooths out the transition from moving to not moving.
	[SerializeField]
	private float bobSpeed = 4.8f;

	// How quickly the player's head bobs.
	[SerializeField]
	private float bobAmount = 0.05f;

	[SerializeField]
	private float floorDistance = 1.8f;

	// How dramatic the bob is.
	// Increasing this in conjunction with bobSpeed gives a nice effect for sprinting.
	private float timer = TIMER_INITIAL_VALUE;
}