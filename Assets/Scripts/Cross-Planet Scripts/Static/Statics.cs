using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Statics {
	private static int numberOfScenes = 9;
	public static int currentScene = 0;
	private static List<MonoBehaviour> controlDisablers = new List<MonoBehaviour>();
	private static List<MonoBehaviour> cameraDisablers = new List<MonoBehaviour>();

	//note: edge case - two scripts try to modify the player control at once
	public static bool PlayerHasControl{
		get{
			return controlDisablers.Count <= 0;
		}
	}

	public static void SetPlayerControl(bool enabled, MonoBehaviour source){
		if (enabled){
			if (!controlDisablers.Contains(source))
				return;
			controlDisablers.Remove(source);
		} else{
			if (controlDisablers.Contains(source))
				return;
			controlDisablers.Add(source);
		}
	}

	public static bool GameplayCamera{
		get{
			return cameraDisablers.Count <= 0;
		}
	}

	public static void SetCameraControl(bool enabled, MonoBehaviour source){
		if (enabled){
			if (!cameraDisablers.Contains(source))
				return;
			cameraDisablers.Remove(source);
		} else{
			if (cameraDisablers.Contains(source))
				return;
			cameraDisablers.Add(source);
		}
	}

	public static void ClearControl(){
		controlDisablers.Clear();
		cameraDisablers.Clear();
	}

	public static bool TryLoadNextScene(){
		if(currentScene >= SceneManager.sceneCountInBuildSettings){
			return false;
		}
		ClearControl();
		SceneManager.LoadSceneAsync(++currentScene);
		return true;
	}

	public static void ReloadCurrentScene(){
		SceneManager.LoadSceneAsync(currentScene);
	}

	public static bool TryLoadPreviousScene(){
		if(currentScene <= 0){
			return false;
		}
		ClearControl();
		SceneManager.LoadSceneAsync(--currentScene);
		return true;
	}

	public static bool LoadScene(int sceneIndex){
		if (sceneIndex < 0)
			return false;
		if (sceneIndex > SceneManager.sceneCountInBuildSettings)
			return false;
		ClearControl();
		SceneManager.LoadSceneAsync(currentScene = sceneIndex);
		return true;
	}
		
}
