using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualSceneControl : MonoBehaviour {
	
	void Update () {
		if (Input.GetKeyDown(PlayerControlMap.nextScene))
			Statics.TryLoadNextScene();
		else if (Input.GetKeyDown(PlayerControlMap.previousScene))
			Statics.TryLoadPreviousScene();
		else if (Input.GetKeyDown(PlayerControlMap.reloadScene))
			Statics.ReloadCurrentScene();
	}
}
