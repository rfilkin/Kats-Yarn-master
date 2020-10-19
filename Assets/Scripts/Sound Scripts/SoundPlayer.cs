using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {

	[SerializeField] AudioSource soundPlayer;

	public void Initialize(AudioClip sound, bool repeat, GameObject source){
		LinkToSource(source);
	}

	void Update(){
		CheckStop();
	}

	//makes this game object a child of the object that is supposed to emit the noise, then moves it to the parent's position
	void LinkToSource(GameObject source){
		transform.parent = source.transform;
		transform.localPosition = Vector3.zero;
	}

	void PlaySound(AudioClip sound, bool repeat){
		soundPlayer.clip = sound;
		soundPlayer.Play();
		soundPlayer.loop = repeat;
	}

	void CheckStop(){
		if (!soundPlayer.isPlaying){
			Stop();
		}
	}

	public void Stop(){
		Destroy(gameObject);
	}

}
