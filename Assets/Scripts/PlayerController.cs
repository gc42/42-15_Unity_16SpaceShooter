using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Transform bullet;
	public Transform[] gunSpawns;

	public float  fireRate = 0.5f;
	public Vector2 randomRate;
	private bool boostMode = false;
	private float boost = 1.0f;
	private float nextFire;

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
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			FireAll();
		}
	}

	private void FireAll()
	{
		if (boostMode == false)
		{
			Fire(gunSpawns[0]);
		}

		if (boostMode == true)
		{
			foreach (var gunSpawn in gunSpawns)
			{
				Fire(gunSpawn);
			}
		}
	}

	private void Fire(Transform gunSpawn)
	{
		Random.InitState((int) Time.time);
		float random = Random.Range(randomRate.x, randomRate.y);
		nextFire = Time.time + (fireRate * boost) + random;

		Transform shot = Instantiate(bullet, gunSpawn.position, gunSpawn.rotation);
		float speed = shot.gameObject.GetComponent<MoveScript>().speed;
		shot.gameObject.GetComponent<Rigidbody>().velocity = shot.forward * speed;
		Destroy(shot.gameObject, 2.0f);

		if (gunSpawn.name == "GunF")
		{
			audioShoot.Play();
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
