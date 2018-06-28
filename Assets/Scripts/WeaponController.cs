using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Weapon controller for the enemies ships
/// </summary>
public class WeaponController : MonoBehaviour
{
	public GameObject shot;
	public Transform shotSpawn;

	public float fireRate;
	public float delay;

	private AudioSource audioSource;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();

		// Invoque 'Fire' method after delay, then every fireRate seconds.
		InvokeRepeating("Fire", delay, fireRate);
	}

	void Fire()
	{
		Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		audioSource.Play();
	}

}
