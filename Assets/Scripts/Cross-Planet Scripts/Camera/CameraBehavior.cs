using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

	public static readonly float baseCameraSize = 8f;

	[SerializeField] bool useDefaultCameraSize = true;
	[SerializeField] Transform target;	//player target; default
	[SerializeField] Transform planetCenter;	//zoom-out point
	[SerializeField] float zoomOutScale = 25f;
	[SerializeField] float zoomTime = 1f;

	[SerializeField] bool lockToPlanetRotation;

	KeyCode zoomKey;
	Camera cam;
	float zoomInScale;
	float timer = 0f;

	// Use this for initialization
	void Start () {
		zoomKey = PlayerControlMap.zoom;
		cam = GetComponent<Camera>();
		if (useDefaultCameraSize)
			zoomInScale = baseCameraSize;
		else
			zoomInScale = cam.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		if (!Statics.GameplayCamera)
			return;
		ZoomControl();
		LockOn();
		MirrorRotation();
		SizeControl();
		PlayerControls();
	}

	void LockOn(){
		Vector3 characterCamPosition = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
		Vector3 zoomedOutCamPosition = new Vector3(planetCenter.transform.position.x, planetCenter.transform.position.y, transform.position.z);
		transform.position = Vector3.Lerp(characterCamPosition, zoomedOutCamPosition, timer / zoomTime);
	}

	void MirrorRotation(){
		if (!lockToPlanetRotation){
			transform.rotation = target.transform.rotation;
			return;
		}
		Quaternion playerRotation = target.transform.rotation;
		Quaternion zoomOutRotation = planetCenter.transform.rotation;
		transform.rotation = Quaternion.Lerp(playerRotation, zoomOutRotation, timer / zoomTime);
	}

	void SizeControl(){
		cam.orthographicSize = Mathf.Lerp(zoomInScale, zoomOutScale, timer / zoomTime);
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
