using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CatPhysics : MonoBehaviour {

	[SerializeField] float gravity = 9.8f;
	[SerializeField] Transform gravityPoint;
	[SerializeField] float velocityRotationCap = 10f;

	Rigidbody2D body;
	Vector2 localCenterOfRotation;

	Quaternion lastRotation;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
		Collider2D col = GetComponent<Collider2D>();
		localCenterOfRotation = col.bounds.center;
		LogRotation();
		print(localCenterOfRotation);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		FixRotation();
		//AdaptVelocity();
		LogRotation();
		ApplyGravity();
	}

	void FixRotation(){
		Vector2 offset = transform.position - gravityPoint.position;
		//transform.rotation = Quaternion.FromToRotation(Vector2.up, offset);
		transform.rotation = Quaternion.LookRotation(Vector3.forward, offset);
	}

	void ApplyGravity(){
		if(!Input.GetKey(KeyCode.Space))
			body.AddRelativeForce(-gravity * Vector2.up * Time.deltaTime, ForceMode2D.Impulse);
	}

	void AdaptVelocity(){
		
	}

	void LogRotation(){
		lastRotation = transform.rotation;
	}
}
