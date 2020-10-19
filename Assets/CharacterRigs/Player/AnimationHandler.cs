using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour {
	
	public Animator animator;
	public GameObject rig;
	public PlayerJump playerJump;
	public PushDetector pushDetector;

	Vector3 rigDefaultScale;

	// Use this for initialization
	void Start () {
		playerJump = GetComponentInParent<PlayerJump>();
		rigDefaultScale = rig.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		
		//Idle and Walking
		if((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) && Statics.PlayerHasControl){
			animator.SetBool("Walking", true);
			if(Input.GetKey(KeyCode.D)){
				rig.transform.localScale = rigDefaultScale;
			}
			else if (Input.GetKey(KeyCode.A)){
				rig.transform.localScale = new Vector3(-rigDefaultScale.x,rigDefaultScale.y,rigDefaultScale.z);
			}
		}
		else{
			animator.SetBool("Walking", false);
		}
		
		//Grounded and Jumping
		if(playerJump.isGrounded){
			animator.SetBool("Grounded", true);
		}
		if(!playerJump.isGrounded){
			animator.SetBool("Grounded", false);
		}
		
		if(pushDetector.isPushing && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))){
			animator.SetBool("Pushing", true);
		}
		if(!pushDetector.isPushing){
			animator.SetBool("Pushing", false);
		}
		
	}

}
