using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CenterTracker))]
public class CentripetalCounter : MonoBehaviour {

	CenterTracker center;
	Rigidbody2D body;

	void Start () {
		center = GetComponent<CenterTracker>();
		body = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate () {
		ApplyCentripetalForce();
	}

	void ApplyCentripetalForce(){
		Vector2 offset = (Vector2)transform.position - center.GetGlobalCenter();	//the vector from the center of the planet to the player
		//Quaternion rotationOfOffset = Quaternion.FromToRotation(Vector3.up, offset);	//rotation of the offset vector from vertical
		//Vector2 tangent = rotationOfOffset * Vector3.right;		//the vector perpendicular to the offset vector in the xy plane
		//Vector2 projVelocity = Vector3.Project(body.velocity, tangent);	//the player's tangential velocity

		Vector2 projVelocity = Radium.TangentVelocity(transform.position, body, center.GetGlobalCenter());

		//print("Player velocity vector:" + projVelocity + " speed:" + projVelocity.magnitude);

		if (projVelocity.magnitude > Mathf.Epsilon){
			Vector2 force = -offset.normalized * Time.fixedDeltaTime * projVelocity.sqrMagnitude * body.mass / offset.magnitude;
			body.AddForce(force, ForceMode2D.Impulse);
		}
	}

}
