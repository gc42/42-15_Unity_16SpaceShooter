using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosionAsteroid;
	public GameObject explosionPlayer;

	public int pointsToAdd = 10;

	private GameController gameController;

	private void Start()
	{
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
		if (other.tag == "Boundary")
		{
			return;
		}
		Instantiate(explosionAsteroid, transform.position, transform.rotation);
		if (other.tag == "Player")
		{
			Instantiate(explosionPlayer, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		}
		gameController.AddScore(pointsToAdd);
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
