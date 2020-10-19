using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlanetRotation : MonoBehaviour {

	public float degreesPerSec;

	Rigidbody2D body;

	// Use this for initialization
	void Start() {
		degreesPerSec = 1f;
		body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		
		if(Input.GetKey(KeyCode.D))
			degreesPerSec = 2f;
		if(Input.GetKey(KeyCode.A))
			degreesPerSec = 0f;
		if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
			degreesPerSec = 1f;
		
		body.angularVelocity = degreesPerSec;
	}
}
