using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenLogic : MonoBehaviour {

    public Button PlayButton;
    public Button QuitButton;
	public Text titleName;
	public Text play;
	public Text quit;
	
	public Animator animator;
	public Animator celest;
	
	public Image blackScreen;

	bool loadingNextScene = false;
	bool playPressed = false;

	// Use this for initialization
	void Start () {
		blackScreen.canvasRenderer.SetAlpha(0.0f);
	}
	
	void Update () {
		
		if(playPressed == true){
			StartCoroutine(Cutscene());
		}
	}
	
	IEnumerator Cutscene(){
		
		FadeUI();
		yield return new WaitForSeconds(1.7f);
		celest.SetBool("Chase", true);
		
		yield return new WaitForSeconds(2.5f);
		animator.SetBool("Chase", true);
		
		yield return new WaitForSeconds(4.0f);
		
		blackScreen.CrossFadeAlpha(1.0f, 2.0f, false);
		
		yield return new WaitForSeconds(1.5f);
		
		if(!loadingNextScene)
			loadingNextScene = Statics.TryLoadNextScene();
		
	}
	

	public void FadeUI(){
		PlayButton.GetComponent<Image>().CrossFadeAlpha(0.0f, 0.5f, false);
		QuitButton.GetComponent<Image>().CrossFadeAlpha(0.0f, 0.5f, false);
		titleName.CrossFadeAlpha(0.0f, 0.5f, false);
		PlayButton.GetComponent<Button>().enabled = false;
		QuitButton.GetComponent<Button>().enabled = false;
		play.CrossFadeAlpha(0.0f, 0.5f, false);
		quit.CrossFadeAlpha(0.0f, 0.5f, false);
	}

    public void PlayPressed()
	{
		playPressed = true;
    }
	
    public void QuitPressed()
    {
        Application.Quit();
    }

}
