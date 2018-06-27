using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{

	public float speed = 10.0f;
	public Vector2 randomSpeed = new Vector2(0.5f, 1.5f);
	private Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		
		rb.velocity = Vector3.forward * speed * Random.Range(randomSpeed.x, randomSpeed.y);
	}
}
