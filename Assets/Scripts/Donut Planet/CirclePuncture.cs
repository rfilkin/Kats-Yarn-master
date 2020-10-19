using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CirclePuncture : MonoBehaviour {

	public string circleGroup = "";

	Collider2D circleOwner = null;

	public void SetOwner(Collider2D owner){
		circleOwner = owner;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (ShouldChangeCollision(other))
			Physics2D.IgnoreCollision(other, circleOwner);
	}

	void OnTriggerExit2D(Collider2D other){
		if (ShouldChangeCollision(other))
			Physics2D.IgnoreCollision(other, circleOwner, false);
	}

	bool ShouldChangeCollision(Collider2D other){
		if (circleOwner == null)
			return false;
		if (other.GetComponent<DropThroughPlatform>() == null)
			return false;
		return true;
	}
}
