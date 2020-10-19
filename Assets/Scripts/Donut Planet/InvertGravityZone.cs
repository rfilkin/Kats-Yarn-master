using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertGravityZone : MonoBehaviour {

	LayerMask playerLayer;
	LayerMask ignoreLayer;
	LayerMask boxLayer;

	void Start(){
		playerLayer = LayerMask.NameToLayer("Player");
		ignoreLayer = LayerMask.NameToLayer("Ignore Player Collision");
		boxLayer = LayerMask.NameToLayer("Box");
	}

	void OnTriggerEnter2D(Collider2D col){

		if (col.gameObject.tag != "Player")
			return;

		int objLayer = col.gameObject.layer;

		if (objLayer == playerLayer || objLayer == boxLayer || objLayer == ignoreLayer){
			CentralGravity cg = col.GetComponent<CentralGravity>();
			//print(col.gameObject.name);
			cg.UpsideDown();
		}
	}

}
