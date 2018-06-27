using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public Vector3 spawnValues;

	public int hazardCount = 10;
	public float startWait = 1.0f;
	public float spawnWaitMax = 1.0f;
	public float waveWaitMax = 4.0f;

	private int score;
	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	public float timeScatter = 1.0f;

	private bool gameOver;
	private bool restart;


	private void Start()
	{
		score = 0;
		//scoreText    = GameObject.Find("ScoreText").GetComponent<Text>();
		//restartText  = GameObject.Find("RestartText").GetComponent<Text>();
		//gameOverText = GameObject.Find("GameOverText").GetComponent<Text>();

		restartText.text  = "";
		gameOverText.text = "";

		gameOver = false;
		restart = false;

		UpdateScore();

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
		}

		Time.timeScale = timeScatter;
	}

	IEnumerator SpawnWaves()
	{
		

		yield return new WaitForSeconds(startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards[Random.Range(0, hazards.Length)];
				if (hazard == null)
				{
					break;
				}
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(Random.Range(0.1f, spawnWaitMax));
			}
			yield return new WaitForSeconds(Random.Range(1.0f, waveWaitMax));

			if (gameOver == true)
			{
				restartText.text = "Press 'R' key to restart";
				restart = true;
				break;
			}

		}
	}

	public void AddScore(int pointsToAdd)
	{
		score += pointsToAdd;
		UpdateScore();
	}

	public void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	public void GameOver()
	{
		gameOverText.text = "Game Over !!";
		gameOver = true;
	}
}
