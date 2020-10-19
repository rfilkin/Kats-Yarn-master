using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutscenePlayerControls : MonoBehaviour {

	Camera cutsceneCamera;

	// Use this for initialization
	void Start () {
		cutsceneCamera = GetComponentInChildren<Camera>();
	}

	public void RelinquishControl(){
		Statics.SetPlayerControl(true, this);
		Statics.SetCameraControl(true, this);
	}

	public void AssumeControl(){
		Statics.SetPlayerControl(false, this);
		if(cutsceneCamera != null)
			Statics.SetCameraControl(false, this);
	}

	public IEnumerator DelayRelinquishControl(float delay){
		yield return new WaitForSeconds(delay);
		RelinquishControl();
	}

	void OnEnable()
	{
		//Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		//Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		RelinquishControl();
	}
}
