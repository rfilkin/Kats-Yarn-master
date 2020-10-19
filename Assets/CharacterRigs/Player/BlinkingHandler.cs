using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingHandler : MonoBehaviour {
	
	public Animator animator;
	public int blinkNumber;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//Blinking
		blinkNumber = Random.Range(1,80);
		if(blinkNumber == 8){
			animator.SetBool("Blinking", true);
		}
		else{
			animator.SetBool("Blinking", false);
		}
	
		//Facing Direction
		if((Input.GetAxis("Horizontal") > 0) && Statics.PlayerHasControl){
			transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		}
		else if ((Input.GetAxis("Horizontal") < 0) && Statics.PlayerHasControl){
			transform.localScale = new Vector3(-1.0f,1.0f,1.0f);
		}
	}
}
