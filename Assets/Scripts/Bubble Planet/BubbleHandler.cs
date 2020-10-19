using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleHandler : MonoBehaviour {
	
	//public GameObject playerobject;
	public Animator animator;
	public BubbleProgress puzzlemanager;
	
	public int bubblecounter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		bubblecounter = puzzlemanager.LockedBoxCount();
		
		//Debug.Log(bubblecounter);
		
		if (bubblecounter == 0){
			animator.SetInteger("Counter", 0);
		}
		else if (bubblecounter == 1){
			animator.SetInteger("Counter", 1);
		}
		else if (bubblecounter == 2){
			animator.SetInteger("Counter", 2);
		}
		else if (bubblecounter == 3){
			animator.SetInteger("Counter", 3);
		}
		else if (bubblecounter == 4){
			animator.SetInteger("Counter", 4);
		}
		
	}
}
