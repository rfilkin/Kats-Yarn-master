using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushDetector : MonoBehaviour {

	public bool isPushing;
	
	void OnTriggerStay2D(Collider2D col){
		if(col.gameObject.tag == "Box"){
			isPushing = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D col){
		isPushing = false;
	}
}
