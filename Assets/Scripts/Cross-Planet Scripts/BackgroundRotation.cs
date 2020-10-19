using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRotation : MonoBehaviour {

	public float degreesPerSec;

	Rigidbody2D body;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
		degreesPerSec = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
		body.angularVelocity = degreesPerSec;
	}
}
