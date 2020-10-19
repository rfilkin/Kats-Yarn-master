using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTrigger : MonoBehaviour {

	[SerializeField] Animator blackscreenAnimator;

	public void FadeToBlack(){
		blackscreenAnimator.Play("Fade In");
	}

	public void FadeFromBlack(){
		blackscreenAnimator.Play("Fade Out");
	}

}
