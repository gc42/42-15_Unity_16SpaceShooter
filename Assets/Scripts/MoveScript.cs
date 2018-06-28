using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Move script for asteroids and enemies
/// </summary>
public class MoveScript : MonoBehaviour
{

	public float 		speed = 10.0f;
	private Vector2 	randomSpeed = new Vector2(0.5f, 1.0f);
	private Vector2 	randomScale = new Vector2(0.5f, 1.0f);
	private Rigidbody 	rb;
	public float 		scale = 1;

	void Start ()
	{
		// Random velocity
		rb          = GetComponent<Rigidbody>();
		rb.velocity = Vector3.forward * speed * Random.Range(randomSpeed.x, randomSpeed.y);

		// Random scale
		scale       = Random.Range(randomScale.x, randomScale.y);
		rb.gameObject.transform.localScale = new Vector3(scale, scale, scale);
	}
}
