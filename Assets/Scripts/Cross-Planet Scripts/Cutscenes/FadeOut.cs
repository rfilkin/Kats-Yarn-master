using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour {

	public Image blackScreen;
	
	public float fadeInTime;

	IEnumerator Start(){
		
		//Start the image as invisible
		blackScreen.canvasRenderer.SetAlpha(0.0f);
		
		yield return new WaitForSeconds(11.0f);
		
		fadeIn(); //Turns the screen black (Fades IN the black screen)
		
		yield return new WaitForSeconds(fadeInTime + 0.5f);
		
		Statics.TryLoadNextScene();
		
	}
	
	void fadeIn(){
		//Fades in the blackscreen to full (1.0) over fadeInTime seconds
		blackScreen.CrossFadeAlpha(1.0f, fadeInTime, false);
	}
	
}