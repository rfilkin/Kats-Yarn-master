using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component assumes that it is attached to the object that will be playing the animation
/// </summary>
public class PreemptCutscene : MonoBehaviour {

	[SerializeField] float tolerance = 0.001f;
	[SerializeField] string animationName;
	[SerializeField] float moveRate = 1f;
	[SerializeField] Transform cutsceneStartPoint;
	[SerializeField] Transform playerReference;	//the actual player object
	[SerializeField] Transform animationObject;
	[SerializeField] GameObject[] hideObjects;


	//Animation returns null
	//AnimationHandler returns Kat
	//Animator returns PlayerRig
	Animator anim;
	Camera cutsceneCamera;
	bool isMoving = false;

	public void MovePlayer(){
		AssumeControl();
		isMoving = true;
	}

	public void JustStart(){
		BeginCutscene();
	}

	void RelinquishControl(){
		Statics.SetPlayerControl(true, this);
		Statics.SetCameraControl(true, this);
	}

	void Start(){
		anim = animationObject.GetComponent<Animator>();
		cutsceneCamera = animationObject.GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isMoving)
			return;
		bool playerAtStart = false;
		bool camAtStart = true;

		MovePlayerTowardsCutsceneStart();
		playerAtStart = IsObjectAtStartPoint(playerReference.position, cutsceneStartPoint.position);

		if (cutsceneCamera != null){
			MoveCameraTowardsCutsceneStart();
			camAtStart = IsObjectAtStartPoint(Camera.main.transform.position, cutsceneCamera.transform.position);
		}


		if (playerAtStart && camAtStart)
			BeginCutscene();
	}

	void AssumeControl(){
		Statics.SetPlayerControl(false, this);
		if(cutsceneCamera != null)
			Statics.SetCameraControl(false, this);
	}

	void MovePlayerTowardsCutsceneStart(){
		playerReference.position = Vector3.MoveTowards(playerReference.position, cutsceneStartPoint.position, moveRate * Time.deltaTime);
	}

	void MoveCameraTowardsCutsceneStart(){
		Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position,
			cutsceneCamera.transform.position,
			moveRate * Time.deltaTime);
		Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation,
			cutsceneCamera.transform.rotation,
			moveRate * Time.deltaTime * 10);
	}

	void BeginCutscene(){
		isMoving = false;
		animationObject.gameObject.SetActive(true);
		playerReference.gameObject.SetActive(false);
		if (cutsceneCamera != null)
			Camera.main.enabled = false;
		RelinquishControl();
		foreach(GameObject g in hideObjects){
			g.SetActive(false);
		}
		anim.Play(animationName);
	}

	bool IsObjectAtStartPoint(Vector3 objPos, Vector3 startPoint){
		Vector3 bottom = Vector3.Min(objPos, startPoint + (Vector3.kEpsilon + tolerance) * Vector3.one);
		Vector3 top = Vector3.Max(objPos, startPoint - (Vector3.kEpsilon + tolerance) * Vector3.one);

		return bottom == top;
	}

}
