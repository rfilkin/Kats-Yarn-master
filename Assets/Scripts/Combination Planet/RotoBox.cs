using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CenterTracker))]
[RequireComponent(typeof(CentralGravity))]
[RequireComponent(typeof(RadialOrientation))]
public class RotoBox : MonoBehaviour {

	[SerializeField] List<GameObject> rotationLayers;
	[SerializeField] List<int> rotationRates;
	[SerializeField] int lockingNodes;	//the number of 'zones' around the circle that the box can be in
	//[SerializeField] int rotationRatio;	//the number of degrees the layer turns per degree the box is moved

	CenterTracker center;
	Quaternion startNodeRotation;
	Quaternion lastRotation;
	float nodeWidth;

	// Use this for initialization
	void Start () {
		center = GetComponent<CenterTracker>();
		nodeWidth = 360f / (float)lockingNodes;
	}
	
	// Update is called once per frame
	void Update () {
		RotateLayers();
		UpdateRotation();
	}

	void SetStartNode(){
		startNodeRotation = center.RotationAroundCenter();
		lastRotation = startNodeRotation;
	}

	void RotateLayers(){
		Quaternion currentRotation = center.RotationAroundCenter();
		Quaternion rotationChange = Quaternion.Inverse(lastRotation) * currentRotation;	//supposedly this gives the change in rotation
		int index = 0;
		foreach (GameObject go in rotationLayers){
			RotateLayer(go, rotationChange, rotationRates[index]);
			++index;
		}
	}

	void RotateLayer(GameObject toRotate, Quaternion amount, int multiplier){
		for (int i = 0; i < Mathf.Abs(multiplier); ++i)
			if (multiplier > 0){
				RotateOne(toRotate, amount);
			} else if (multiplier < 0){
				RotateMinusOne(toRotate, amount);
			}
	}

	void RotateOne(GameObject toRotate, Quaternion amount){
		toRotate.transform.rotation *= Quaternion.Inverse(amount);
	}

	void RotateMinusOne(GameObject toRotate, Quaternion amount){
		toRotate.transform.rotation *= amount;
	}

	void UpdateRotation(){
		lastRotation = center.RotationAroundCenter();
	}

	int NearestNode(){
		Quaternion currentRotation = center.RotationAroundCenter();
		Quaternion.Angle(startNodeRotation, currentRotation);

		//divide by the node width and take the floor to get the number of nodes away on the lesser side
		//then add one to get the number of nodes away on the greater side
		//or just round to the nearest node
		//Mathf.Round();

		return 0;
	}

}
