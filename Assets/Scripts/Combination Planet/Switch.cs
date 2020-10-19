using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Switch : MonoBehaviour {

	SwitchEffect effect;
	Collider2D trigger;

	// Use this for initialization
	void Start () {
		trigger = GetComponent<Collider2D>();
		effect = GetComponent<SwitchEffect>();
	}

	void OnTriggerStay2D(Collider2D col){
		//print("called");
		if (col.gameObject.tag == "Player" && Input.GetKeyDown(PlayerControlMap.interact)){
			print("player is interacting");
			InteractEffect();
		}
	}

	void InteractEffect(){
		if (effect.CanTrigger())
			effect.TriggerEffect();
	}
}
