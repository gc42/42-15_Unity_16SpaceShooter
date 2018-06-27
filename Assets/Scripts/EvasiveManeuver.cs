using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour
{
	public float dodge; // esquiver
	public float smooting;
	public float tilt;

	public Vector2 startWait;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;
	public Boundary boundary;

	private float currentSpeed_z;
	private float targetManeuver;
	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		//currentSpeed_z = rb.velocity.z;
		//Debug.Log("currentSpeed: " + currentSpeed_z);
		StartCoroutine(Evade());
	}

	IEnumerator Evade()
	{
		yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

		while (true)
		{
			targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
			yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
			targetManeuver = 0;
			yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
		}
	}

	private void FixedUpdate()
	{
		
		float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smooting);
		rb.velocity = new Vector3(newManeuver, 0.0f, rb.velocity.z);
		rb.position = new Vector3 
		(
			  Mathf.Clamp(rb.position.x, boundary.MinX, boundary.MaxX),
			  0.0f,
			  Mathf.Clamp(rb.position.z, boundary.MinZ, boundary.MaxZ)
		);

		rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
	}


}
