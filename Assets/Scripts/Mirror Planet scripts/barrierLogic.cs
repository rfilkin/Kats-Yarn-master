using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrierLogic : MonoBehaviour {

	public mirrorLogic leftMirror;
	public mirrorLogic rightMirror;

	public GameObject openGate;
	public GameObject closedGate;

	public Transform lightTarget;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(leftMirror.mirrorState == 1 || rightMirror.mirrorState == 0)
        {
            //if leftMirror faces right, or rightMirror faces left, 
            //turn off rendering and collision
			openGate.SetActive(true);
			closedGate.SetActive(false);
        }
        else
        {
            //else, 
            //turn on rendering and collision
            //GetComponent<Renderer>().enabled = true;
			openGate.SetActive(false);
			closedGate.SetActive(true);
        }
    }
}
