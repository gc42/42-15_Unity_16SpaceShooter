using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Evasive maneuver for the enemy ship
/// </summary>
public class EvasiveManeuver : MonoBehaviour
{
	public float 		dodge; // esquiver
	public float 		smoothTime;
	public float 		tilt;

	public Vector2 		startWait;
	public Vector2 		maneuverTime;
	public Vector2 		maneuverWait;
	public Boundary 	boundary;

	private float 		currentSpeed_z;
	private float 		targetManeuver;
	private Rigidbody 	rb;



	private void Start()
	{
		rb = GetComponent<Rigidbody>();

		StartCoroutine(Evade());
	}

	/// <summary>
	/// Evade maneuver for the enemy ship, as coroutine with pauses
	/// </summary>
	/// <returns>The target point for the evade maneuver</returns>
	IEnumerator Evade()
	{
		yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

		while (true)
		{
			// target anywhere on the opposite side of the game field, x axe
			targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
			yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));

			// target on x = 0
			targetManeuver = 0;
			yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
		}
	}

	private void FixedUpdate()
	{
		// Damped interpolation between x-position and x-target, during smooth time
		float xVelocity = 0.0F;
		float newManeuver = Mathf.SmoothDamp(rb.velocity.x, targetManeuver, ref xVelocity, smoothTime);

		// new velocity on x axe
		rb.velocity = new Vector3(newManeuver, 0.0f, rb.velocity.z);

		// clamp position inside the boundaries
		rb.position = new Vector3 
		(
			  Mathf.Clamp(rb.position.x, boundary.MinX, boundary.MaxX),
			  0.0f,
			  Mathf.Clamp(rb.position.z, boundary.MinZ, boundary.MaxZ)
		);

		// tilt the enemy ship on the z axe, when the ship have x axe velocity 
		rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
	}


}
