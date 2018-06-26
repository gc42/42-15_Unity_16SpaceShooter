using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Transform bullet;
	public Transform gunSpawnL;
	public Transform gunSpawnR;

	public float  fireRate = 0.5f;
	public float randomRate = 0.1f;
	private bool boostMode = false;
	private float boost = 1.0f;
	private float nextFireL;
	private float nextFireR;

	private AudioSource audioShoot;

	// Use this for initialization
	void Start ()
	{
		audioShoot = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift))
			boostMode = true;
		if (Input.GetKeyUp(KeyCode.LeftShift))
			boostMode = false;

		CheckBoostMode();
		if (Input.GetButton("Fire1") && Time.time > nextFireL)
		{
			float random = Random.Range(0.0f, randomRate);
			nextFireL = Time.time + (fireRate * boost) + random;
			Transform shotL = Instantiate(bullet, gunSpawnL.position, gunSpawnL.rotation);
			Destroy(shotL.gameObject, 2.0f);
			audioShoot.Play();
		}		

		if (Input.GetButton("Fire1") && Time.time > nextFireR)
		{
			float random = Random.Range(0.0f, randomRate);
			nextFireR = Time.time + (fireRate * boost) + random;
			Transform shotR = Instantiate(bullet, gunSpawnR.position, gunSpawnR.rotation);
			Destroy(shotR.gameObject, 2.0f);
		}
	}

	private void CheckBoostMode()
	{
		if (boostMode == false)
			boost = 1.0f;
		if (boostMode == true)
			boost = 0.25f;

	}
}
