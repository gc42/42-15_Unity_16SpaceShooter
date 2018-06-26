using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {

	private Rigidbody rb;
	public float speed = 10.0f;

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
		Vector3 movement = Vector3.forward;
		rb.velocity = movement * speed;
	}
}
