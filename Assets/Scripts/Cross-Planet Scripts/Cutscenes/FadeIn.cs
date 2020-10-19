using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour {

	public Image blackScreen;
	public float fadeOutTime;

	IEnumerator Start(){
		
		//Start the image as invisible
		blackScreen.canvasRenderer.SetAlpha(1.0f);
		
		yield return new WaitForSeconds(0.5f);
		
		fadeOut(); //Fades in the scene (Fades OUT the black screen)
		
		
	}
	
	void fadeOut(){
		//Fades in the blackscreen to full (0.0) over fadeOutTime seconds
		blackScreen.CrossFadeAlpha(0.0f, fadeOutTime, false);
	}
}
