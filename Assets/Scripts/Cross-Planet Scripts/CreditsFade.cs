using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsFade : MonoBehaviour {

    public Text studioName;
    public Text namesLeft;
    public Text namesRight;
    public Text thankYou;

	[SerializeField] Animator blackscreenAnimator;

	[SerializeField] float timeUntilFadeOut;
	[SerializeField] float delayReturnToTitle;

    IEnumerator Start()
    {

        //Start the text as invisible
        studioName.canvasRenderer.SetAlpha(0.0f);
        namesLeft.canvasRenderer.SetAlpha(0.0f);
        namesRight.canvasRenderer.SetAlpha(0.0f);
        thankYou.canvasRenderer.SetAlpha(0.0f);

        yield return new WaitForSeconds(2.0f);

        fadeIn();

		yield return new WaitForSeconds(timeUntilFadeOut);

		blackscreenAnimator.Play("Fade In");

		yield return new WaitForSeconds(delayReturnToTitle);

		Statics.LoadScene(1);
    }

    void fadeIn()
    {
        studioName.CrossFadeAlpha(1.0f, 1.5f, false);
        namesLeft.CrossFadeAlpha(1.0f, 1.5f, false);
        namesRight.CrossFadeAlpha(1.0f, 1.5f, false);
        thankYou.CrossFadeAlpha(1.0f, 1.5f, false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
