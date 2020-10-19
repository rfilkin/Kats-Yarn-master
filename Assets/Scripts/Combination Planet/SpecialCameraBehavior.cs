using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCameraBehavior : MonoBehaviour {

	[SerializeField] bool useDefaultCameraSize = true;
	[SerializeField] Transform target;	//player target; default
	[SerializeField] Transform planetCenter;	//zoom-out point
	[SerializeField] float zoomOutScale = 25f;
	[SerializeField] float zoomTime = 1f;

	[SerializeField] bool lockToPlanetRotation;

	[SerializeField] GameObject[] boxes;
	[SerializeField] float boxProximityBegin;	//how far from the box does this functionality kick in?
	[SerializeField] float boxProximityEnd;		//how far from the box does the functionality reach its maximum?
	[SerializeField] float boxVerticalProximityOffset;	//moves the elliptical area up by this amount
	[SerializeField] float boxAreaVerticalSquish;	//squishes the eliptical area in the vertical direction; 0-1
	[SerializeField] float edgeOfPlanet;	//0 is focused on player, 1 is focused on planet center
	[SerializeField] float nearBoxCamScale;

	//will need variables for how close the player needs to be to the block to engage zoom out
	//as well as for how much the camera zooms, the change in radial offset

	KeyCode zoomKey;
	Camera cam;
	float zoomInScale;
	float timer = 0f;
	float boxLerpValue = 0;

	// Use this for initialization
	void Start () {
		zoomKey = PlayerControlMap.zoom;
		cam = GetComponent<Camera>();
		if (useDefaultCameraSize)
			zoomInScale = CameraBehavior.baseCameraSize;
		else
			zoomInScale = cam.orthographicSize;
	}

	// Update is called once per frame
	void Update () {
		if (!Statics.GameplayCamera)
			return;
		ZoomControl();
		FixBoxLerpValue();
		AltLockOn();
		MirrorRotation();
		SizeControl();
		PlayerControls();
	}

	void AltLockOn(){
		Vector3 characterCamPosition = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
		Vector3 zoomedOutCamPosition = new Vector3(planetCenter.transform.position.x, planetCenter.transform.position.y, transform.position.z);

		Vector3 characterOffset = target.position - planetCenter.position;
		Vector2 halfOfPlanetPosition = (characterOffset.normalized * edgeOfPlanet/2) + planetCenter.position;
		Vector3 halfOfPlanetPosCamZ = new Vector3(halfOfPlanetPosition.x, halfOfPlanetPosition.y, transform.position.z);

		Vector3 boxTranslatePosition = Vector3.Lerp(characterCamPosition, halfOfPlanetPosCamZ, boxLerpValue);
		transform.position = Vector3.Lerp(boxTranslatePosition, zoomedOutCamPosition, timer / zoomTime);
	}

	void FixBoxLerpValue(){
		GameObject closestBox = null;
		float distance = Mathf.Infinity;
		foreach (GameObject box in boxes){
			float boxDistance = Vector2.Distance(box.transform.position, planetCenter.position);
			float playerDistance = Vector2.Distance(target.position, planetCenter.position);
			float radialDistance = playerDistance - boxDistance - boxVerticalProximityOffset;


			Quaternion boxRotation = CenterTracker.RotationAroundObject(box.transform, planetCenter);
			Quaternion playerRotation = CenterTracker.RotationAroundObject(target, planetCenter);
			float angleOffset = Quaternion.Angle(boxRotation, playerRotation);
			float arcOffset = angleOffset * Mathf.Deg2Rad * boxDistance;


			float thisDistance = Mathf.Sqrt(Mathf.Pow(radialDistance, 2) / Mathf.Pow((float)(1 - 0.5 * boxAreaVerticalSquish), 2)
				+ Mathf.Pow(arcOffset, 2) / Mathf.Pow((float)(1 + 0.5 * boxAreaVerticalSquish), 2));

			if (thisDistance < distance){
				distance = thisDistance;
				closestBox = box;
			}
		}

		boxLerpValue = Mathf.InverseLerp(boxProximityBegin, boxProximityEnd, distance);
		//print(boxLerpValue);
	}

	void LockOn(){	//the camera's center should be changed near a box
		Vector3 characterCamPosition = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
		Vector3 zoomedOutCamPosition = new Vector3(planetCenter.transform.position.x, planetCenter.transform.position.y, transform.position.z);
		transform.position = Vector3.Lerp(characterCamPosition, zoomedOutCamPosition, timer / zoomTime);
	}

	void MirrorRotation(){	//the rotation should be the same near a box; no change
		if (!lockToPlanetRotation){
			transform.rotation = target.transform.rotation;
			return;
		}
		Quaternion playerRotation = target.transform.rotation;
		Quaternion zoomOutRotation = planetCenter.transform.rotation;
		transform.rotation = Quaternion.Lerp(playerRotation, zoomOutRotation, timer / zoomTime);
	}

	void SizeControl(){	//will need to zoom out near a box
		float boxScale = Mathf.Lerp(zoomInScale, nearBoxCamScale, boxLerpValue);
		cam.orthographicSize = Mathf.Lerp(boxScale, zoomOutScale, timer / zoomTime);
	}

	void ZoomControl(){
		if (Input.GetKey(zoomKey)){
			timer += Time.deltaTime;
		} else{
			timer -= Time.deltaTime;
		}
		timer = Mathf.Clamp(timer, 0, zoomTime);
		//print(timer);
	}

	void PlayerControls(){
		/*
		if (timer > 0){
			if (timer < zoomTime && !Input.GetKey(zoomKey))
				Statics.PlayerHasControl = true;
			Statics.PlayerHasControl = false;
		}
		else
			Statics.PlayerHasControl = true;
		*/

		if (Input.GetKey(zoomKey))
			Statics.SetPlayerControl(false, this);
		else
			Statics.SetPlayerControl(true, this);
	}
}
