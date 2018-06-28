using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Transform 		bullet_small;
	public Transform 		bullet_big;
	public Transform[] 		gunSpawns;

	public float  			fireRate = 0.5f;
	public Vector2 			randomRate;
	private bool 			boostMode = false;
	private bool 			specialMode = false;
	private float 			boost = 1.0f;
	private float 			nextFire;

	private AudioSource 	audioShoot;



	void Start ()
	{
		audioShoot = GetComponent<AudioSource>();
	}
	
	void Update ()
	{
		// Set boostMode active/inactive
		if (Input.GetKeyDown(KeyCode.LeftShift))
			boostMode = true;
		if (Input.GetKeyUp(KeyCode.LeftShift))
			boostMode = false;

		// Set specialMode active/inactive
		if (Input.GetKeyDown(KeyCode.LeftAlt))
			specialMode = true;
		if (Input.GetKeyUp(KeyCode.LeftAlt))
			specialMode = false;

		CheckBoostMode();

		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			Fire();
		}
	}

	private void Fire()
	{
		// according to boost mode state
		if (boostMode == false)
		{
			// Use front gun, big bullets
			Shoot(gunSpawns[0], 0);
		}

		if (boostMode == true)
		{
			foreach (var gunSpawn in gunSpawns)
			{
				// Use all guns, small bullets
				Shoot(gunSpawn, 1);
			}
		}

		// according to special mode state
		if (specialMode == true)
		{
			// Use side gun, small bullets bullets
			Shoot(gunSpawns[3], 1);
		}
	}

	private void Shoot(Transform gunSpawn, int gun)
	{
		Random.InitState((int) Time.time);
		float random = Random.Range(randomRate.x, randomRate.y);
		nextFire = Time.time + (fireRate * boost) + random;

		Transform shot = null;

		if (gun == 0)
		{
			shot = Instantiate(bullet_big, gunSpawn.position, gunSpawn.rotation);
		}
		else if (gun == 1)
		{
			shot = Instantiate(bullet_small, gunSpawn.position, gunSpawn.rotation);
		}


		if (shot != null)
		{
			float speed = shot.gameObject.GetComponent<MoveScript>().speed;
			shot.gameObject.GetComponent<Rigidbody>().velocity = shot.forward * speed;
			Destroy(shot.gameObject, 2.0f);
		}

		// Audio only on front gun
		if (gunSpawn.name == "GunF")
		{
			audioShoot.Play();
		}
	}

	/// <summary>
	/// Checks if boost mode in on/off.
	/// </summary>
	private void CheckBoostMode()
	{
		if (boostMode == false)
			boost = 1.0f;
		if (boostMode == true)
			boost = 0.25f;
	}
}
