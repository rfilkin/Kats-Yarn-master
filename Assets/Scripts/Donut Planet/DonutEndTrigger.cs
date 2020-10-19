using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class DonutEndTrigger : MonoBehaviour {

	bool triggered = false;

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player" && !triggered){
			triggered = true;
			if (!Statics.TryLoadNextScene())
				return;
		}
	}

}
