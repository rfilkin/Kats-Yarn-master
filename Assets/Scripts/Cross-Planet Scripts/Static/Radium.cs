using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Radium {

	public static Vector2 TangentVelocity(Vector2 objPos, Rigidbody2D body, Vector2 center){
		Vector2 offset = objPos - center;
		Quaternion rotationOfOffset = Quaternion.FromToRotation(Vector3.up, offset);
		Vector2 tangent = rotationOfOffset * Vector3.right;

		return (Vector2)Vector3.Project(body.velocity, tangent);
	}

}
