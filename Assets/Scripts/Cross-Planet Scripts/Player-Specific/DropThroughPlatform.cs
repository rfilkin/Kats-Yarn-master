using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropThroughPlatform : MonoBehaviour {

	KeyCode control;
	string dropLayer = "Player Drop Down";
	int defaultLayer;

	List<MonoBehaviour> dropAllowers = new List<MonoBehaviour>();

	// Use this for initialization
	void Start () {
		control = PlayerControlMap.drop;
		defaultLayer = gameObject.layer;
	}
	
	// Update is called once per frame
	void Update () {
		PlayerDropControl();
		AdjustPassage();
	}

	void PlayerDropControl(){
		if (Input.GetKeyDown(control))
			AllowDrop(this);
		else if (Input.GetKeyUp(control))
			BlockDrop(this);
	}

	/*
	void PassThroughPlatform(){
		print("allowing passage");
		gameObject.layer = LayerMask.NameToLayer(dropLayer);
	}

	void WalkOnPlatform(){
		print("blocking passage");
		gameObject.layer = defaultLayer;
	}
	*/

	void AdjustPassage(){
		if(dropAllowers.Count <= 0)
			gameObject.layer = defaultLayer;
		else
			gameObject.layer = LayerMask.NameToLayer(dropLayer);
	}

	public void AllowDrop(MonoBehaviour controller){
		if(!dropAllowers.Contains(controller))
			dropAllowers.Add(controller);
	}

	public void BlockDrop(MonoBehaviour controller){
		if(dropAllowers.Contains(controller))
			dropAllowers.Remove(controller);
	}
}
