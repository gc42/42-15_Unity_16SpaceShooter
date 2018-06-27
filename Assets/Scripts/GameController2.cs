using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController2 : MonoBehaviour
{
	public Transform[] spawnObjects;
	public GameObject cube;
	public float speed = 10;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			foreach (var spawnObject in spawnObjects)
			{
				GameObject toto = Instantiate(cube, spawnObject.position, spawnObject.rotation);

				// Add velocity to the bullet
				toto.GetComponent<Rigidbody>().velocity = toto.transform.forward * speed;

				// Destroy the bullet after 2 seconds
				Destroy(toto, 2.0f);
			}
		}
	}
}



