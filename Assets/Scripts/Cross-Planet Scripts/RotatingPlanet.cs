using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlanet : MonoBehaviour {

	[SerializeField] float speed;
	//[SerializeField] List<Rigidbody2D> moons;

	Rigidbody2D body;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
		//StartMoons();
	}

	void FixedUpdate(){
		body.angularVelocity = speed;
	}

	/*
	void StartMoons(){
		foreach (Rigidbody2D moon in moons){
			float dist = Vector2.Distance(moon.worldCenterOfMass, body.worldCenterOfMass);
			ApplyStartingSpeedToMoon(moon, dist);
		}
	}
		
	public void ApplyStartingSpeedToMoon(Rigidbody2D moonBody, float distance){
		//adds a velocity vector to the passed-in rigid body so that
		//its movement is already at the same speed at the planet's
		//rotation at the start. This should eliminate game-start
		//sliding

		float magnitude = speed * distance * Mathf.Deg2Rad;	//do I need to convert the angular velocity's units?
		//print(magnitude);

		//figure out the tangent to this objects rotational velocity
		Vector2 offset = moonBody.centerOfMass - (Vector2)transform.position;
		Quaternion rotationOfOffset = Quaternion.FromToRotation(Vector3.up, offset);
		Vector2 tangent = rotationOfOffset * Vector2.up;

		moonBody.velocity += -tangent * magnitude * moonBody.mass;

		//apply the calculated velocity to the object
		//moonBody.AddForce(-tangent * magnitude * moonBody.mass, ForceMode2D.Impulse);
	}
	*/

}
