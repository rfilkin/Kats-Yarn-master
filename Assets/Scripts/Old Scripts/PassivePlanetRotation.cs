using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassivePlanetRotation : MonoBehaviour {

	[SerializeField] float speed;

	Rigidbody2D body;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Rotate();
	}

	void Rotate(){
		body.angularVelocity = speed;
	}
}
