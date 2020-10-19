using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class DropPlatform : MonoBehaviour {

	static string ignorePlayerLayer = "Ignore Player Collision";
	static string defaultLayer = "Planet";

	float timer = 0;
	
	// Update is called once per frame
	void Update () {
		RunTimer();
	}

	public void DisableCollision(float duration){
		timer = duration;
		gameObject.layer = LayerMask.NameToLayer(ignorePlayerLayer);
	}

	void EnableCollision(){
		gameObject.layer = LayerMask.NameToLayer(defaultLayer);
	}

	void RunTimer(){
		timer -= Time.deltaTime;
		if (timer <= 0){
			timer = 0;
			EnableCollision();
		}
	}
}
