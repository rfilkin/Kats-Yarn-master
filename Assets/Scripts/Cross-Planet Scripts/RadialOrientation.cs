using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CenterTracker))]
public class RadialOrientation : MonoBehaviour {

	CenterTracker center;

	// Use this for initialization
	void Start () {
		center = GetComponent<CenterTracker>();
		SetupRigidBody();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		FixRotation();
	}

	void FixRotation(){
		Vector2 offset = (Vector2)transform.position - center.GetGlobalCenter();
		transform.rotation = Quaternion.LookRotation(Vector3.forward, offset);
	}

	void SetupRigidBody(){
		Rigidbody2D body = GetComponent<Rigidbody2D>();
		if (body == null)
			return;
		body.freezeRotation = true;
		//InitializeVelocity(body);
	}

	/*
	void InitializeVelocity(Rigidbody2D body){
		GameObject obj = center.GetCenterObject();
		if (obj == null)
			return;
		RotatingPlanet rotator = obj.GetComponent<RotatingPlanet>();
		rotator.ApplyStartingSpeedToMoon(body, center.ColliderDistanceToCenter());
	}
	*/
}
