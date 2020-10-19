using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour {

	public Image splashImage;

	IEnumerator Start(){
		
		//Start the image as invisible
		splashImage.canvasRenderer.SetAlpha(0.0f);
		
		yield return new WaitForSeconds(1.0f);
		
		fadeIn();
		yield return new WaitForSeconds(3.0f);
		
		fadeOut();
		yield return new WaitForSeconds(2.0f);

		Statics.TryLoadNextScene();
		
	}
	
	void fadeIn(){
		//Fades in the splashImage to full (1.0) over 1.1 seconds
		splashImage.CrossFadeAlpha(1.0f, 1.5f, false);
	}
	
	void fadeOut(){
		//Fades out the splashImage over 1.1 seconds.
		splashImage.CrossFadeAlpha(0.0f, 1.5f, false);
	}
	
}
