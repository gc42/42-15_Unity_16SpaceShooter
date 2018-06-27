using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject explosionPlayer;

	public int pointsToAdd = 10;

	private GameController gameController;
	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();

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
			GameObject exp =  Instantiate(explosion, transform.position, transform.rotation);
			exp.GetComponent<Rigidbody>().velocity = rb.velocity;
		}

		if (other.tag == "Player")
		{
			GameObject exp = Instantiate(explosionPlayer, other.transform.position, other.transform.rotation);
			exp.GetComponent<Rigidbody>().velocity = other.gameObject.GetComponent<Rigidbody>().velocity;
			gameController.GameOver();
		}
		gameController.AddScore(pointsToAdd);
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
