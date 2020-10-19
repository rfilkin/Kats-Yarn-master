using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutsceneEffects : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NextScene(){
		Statics.TryLoadNextScene();
	}

	public void ReturnToTitle(){
		Statics.LoadScene(1);
	}

	public IEnumerator DelayReturnToTitle(float delay){
		yield return new WaitForSeconds(delay);
		ReturnToTitle();
	}

	public IEnumerator DelayNextScene(float delay){
		yield return new WaitForSeconds(delay);
		NextScene();
	}

}
