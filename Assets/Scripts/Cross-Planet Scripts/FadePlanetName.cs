using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class FadePlanetName : MonoBehaviour {

	public Text planetName;

	IEnumerator Start(){
		
		//Start the text as invisible
		planetName.canvasRenderer.SetAlpha(0.0f);
		
		yield return new WaitForSeconds(2.0f);
		
		fadeIn();
		yield return new WaitForSeconds(6.0f);
		
		fadeOut();
	}
	
	void fadeIn(){
		planetName.CrossFadeAlpha(1.0f, 1.5f, false);
	}
	
	void fadeOut(){
		planetName.CrossFadeAlpha(0.0f, 1.5f, false);
	}
	
}
