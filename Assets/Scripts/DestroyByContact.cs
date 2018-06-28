using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
	public GameObject 		explosion;
	public GameObject 		explosionPlayer;

	public int 				pointsToAdd = 10;

	private GameController 	gameController;
	private Rigidbody 		rb;
	private MoveScript 		moveScript;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		moveScript = GetComponent<MoveScript>();

		// Set points to add depending from enemy scale
		if (System.Math.Abs(moveScript.scale) > Mathf.Epsilon)
		{
			pointsToAdd = (int)(pointsToAdd / moveScript.scale);
		}
		else
		{
			pointsToAdd = 100;
		}

		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		else
		{
			Debug.LogError("No GameController script could be found in scene.");
		}
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
		{
			return;
		}

		if (explosion != null)
		{
			GameObject explose =  Instantiate(explosion, transform.position, transform.rotation);
			explose.GetComponent<Rigidbody>().velocity = rb.velocity;
		}

		if (other.tag == "Player")
		{
			GameObject explose = Instantiate(explosionPlayer, other.transform.position, other.transform.rotation);

			Vector3 otherVelocity = other.gameObject.GetComponent<Rigidbody>().velocity;
			explose.GetComponent<Rigidbody>().velocity = otherVelocity;

			gameController.GameOver();
		}

		gameController.AddScore(pointsToAdd);

		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
