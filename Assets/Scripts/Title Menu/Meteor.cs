using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Meteor : MonoBehaviour {

	float remainingLife = 0f;
	Vector2 velocity;
	float rotationSpeed;

	RectTransform rTrans;

	public void Init(float lifeSpan, Vector2 newVelocity, float newRotationSpeed, float x, float y){
		remainingLife = lifeSpan;
		velocity = newVelocity;
		rotationSpeed = newRotationSpeed;
		rTrans = GetComponent<RectTransform>();
		rTrans.position = new Vector3(x, y, rTrans.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		remainingLife -= Time.deltaTime;
		if (remainingLife <= 0)
			Destroy(gameObject);
		transform.position += (Vector3)(velocity * Time.deltaTime);
		transform.rotation *= Quaternion.Euler(0, 0, rotationSpeed*Time.deltaTime);
	}
}
