using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Boundary limits
/// </summary>
[System.Serializable]
public class Boundary
{
	public float MinX, MaxX, MinZ, MaxZ;
}

public class MovingPlayerScript : MonoBehaviour {

	public Boundary    boundary;
	public float       speed = 10.0f;
	public float       tilt = 5.0f;
	public float       mouseSmoothTime = 0.2f;

	private Rigidbody  rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		// Keyboard inputs
		float inputX = Input.GetAxis("Horizontal");
		float inputZ = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(inputX, 0.0f, inputZ);
		rb.velocity = movement * speed;

		// If the mouse is used, mouse inputs
		float mouseX = Input.GetAxis("Mouse X");
		float mouseZ = Input.GetAxis("Mouse Y");
		transform.Translate(mouseX, 0.0f, mouseZ);

		// Player position stay inside the boundaries
		rb.position = new Vector3(
			Mathf.Clamp(rb.position.x, boundary.MinX, boundary.MaxX),
			0.0f,
			Mathf.Clamp(rb.position.z, boundary.MinZ, boundary.MaxZ)
			);

		// Player tilt on z axe when moving on the x axe
		rb.rotation = Quaternion.Euler(0.0f, rb.velocity.x * tilt / 8, rb.velocity.x * -tilt);
	}
}
