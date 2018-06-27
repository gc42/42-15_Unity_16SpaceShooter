using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingScript2 : MonoBehaviour
{
	public float speed = 10.0f;

	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		if (rb != null)
		{
			Vector3 movement = Vector3.forward;
			rb.velocity = movement * speed;
		}
	}

}
