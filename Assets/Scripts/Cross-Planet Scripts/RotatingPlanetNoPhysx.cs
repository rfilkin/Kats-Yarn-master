using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlanetNoPhysx : MonoBehaviour {

	[SerializeField] float degreesPerSec;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Rotate();
	}

	void Rotate(){
		float degreesThisFrame = Time.deltaTime * degreesPerSec;
		RotateSelf(degreesThisFrame);
		RotateMoons();
	}

	void RotateSelf(float degrees){
		transform.localRotation *= Quaternion.AngleAxis(degrees, Vector3.forward);
	}

	void RotateMoons(){

	}
}
