﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScrolling : MonoBehaviour
{

	private Vector3 startPosition;
	public float scrollSpeed;
	public float tileSizeZ;

	private void Start()
	{
		startPosition = transform.position;
	}

	void Update ()
	{
		// Scroll background by time, and repeat with tileSize step on Z axe.
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
		transform.position = startPosition + Vector3.forward * newPosition;
	}
}
