using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GroundDetector : MonoBehaviour {

	bool isGrounded = false;

	Collider2D col;

	// Use this for initialization
	void Start () {
		col = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(){

	}
}
