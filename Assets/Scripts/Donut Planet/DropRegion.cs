using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DropRegion : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		DropThroughPlatform dropController;
		if (dropController = col.gameObject.GetComponent<DropThroughPlatform>()){
			dropController.AllowDrop(this);
		}
	}

	void OnTriggerExit2D(Collider2D col){
		DropThroughPlatform dropController;
		if (dropController = col.gameObject.GetComponent<DropThroughPlatform>()){
			dropController.BlockDrop(this);
		}
	}
}
