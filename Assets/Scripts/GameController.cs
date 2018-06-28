using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public GameObject[] 	hazards;
	public Vector3 			spawnValues;

	public int 				hazardCount  = 10;
	public float 			startWait    = 1.0f;
	public float 			spawnWaitMax = 1.0f;
	public float 			waveWaitMax  = 4.0f;

	private int 			score;
	public Text 			scoreText;
	public Text 			restartText;
	public Text 			gameOverText;
	public float 			timeScatter  = 1.0f;

	private bool 			gameOver;
	private bool 			restart;


	private void Start()
	{
		int width = 600; // or something else
		int height = 900; // or something else
		bool isFullScreen = false; // should be windowed to run in arbitrary resolution
		int desiredFPS = 30; // or something else

		Screen.SetResolution(width, height, isFullScreen, desiredFPS);



		score             = 0;
		gameOver          = false;
		restart           = false;

		restartText.text  = "";
		gameOverText.text = "";
		UpdateScore();

		Cursor.lockState = CursorLockMode.Locked;

		StartCoroutine (SpawnWaves());
	}

	private void Update()
	{
		if (restart == true)
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				int scene = SceneManager.GetActiveScene().buildIndex;
				SceneManager.LoadScene(scene);
			}

			if (Input.GetKeyDown(KeyCode.Q))
			{
				Cursor.lockState = CursorLockMode.None;
				Application.Quit();
			}
		}

		Time.timeScale = timeScatter;
	}

	/// <summary>
	/// Spawns waves of enemies, with waiting times.
	/// </summary>
	/// <returns>The waves.</returns>
	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startWait);

		while (true)
		{
			// Spawn enemies
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards[Random.Range(0, hazards.Length)];
				if (hazard == null)
				{
					break;
				}
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y + Random.Range(-0.1f, 0.1f), spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;

				Instantiate(hazard, spawnPosition, spawnRotation);

				// Random time before spawn next enemy
				yield return new WaitForSeconds(Random.Range(0.3f, spawnWaitMax));
			}

			// Random time before next wave
			yield return new WaitForSeconds(Random.Range(1.0f, waveWaitMax));

			if (gameOver == true)
			{
				restartText.text = "Press 'R' key to restart.\nPress 'Q' key to quit.";
				restart = true;
				break;
			}
		}
	}

	/// <summary>
	/// Adds pointsToAdd to the current score.
	/// </summary>
	/// <param name="pointsToAdd">Points to add.</param>
	public void AddScore(int pointsToAdd)
	{
		score += pointsToAdd;
		UpdateScore();
	}

	/// <summary>
	/// Updates the score display text.
	/// </summary>
	public void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	/// <summary>
	/// When game is over, display game over text.
	/// </summary>
	public void GameOver()
	{
		gameOverText.text = "Game Over !!";
		gameOver = true;
	}
}





