using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float MinX, MaxX, MinZ, MaxZ;
}

public class MovingPlayerScript : MonoBehaviour {

	public Boundary boundary;
	private Rigidbody rb;
	public float speed = 10.0f;
	public float tilt = 5.0f;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	private void FixedUpdate()
	{
		float inputX = Input.GetAxis("Horizontal");
		float inputZ = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(inputX, 0.0f, inputZ);
		rb.velocity = movement * speed;

		rb.position = new Vector3(
			Mathf.Clamp(rb.position.x, boundary.MinX, boundary.MaxX),
			0.0f,
			Mathf.Clamp(rb.position.z, boundary.MinZ, boundary.MaxZ)
			);

		rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
