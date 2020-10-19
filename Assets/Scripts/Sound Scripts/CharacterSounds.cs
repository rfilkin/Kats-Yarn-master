using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSounds : MonoBehaviour {

	[SerializeField] AudioClip jumpSound;
	[SerializeField] AudioClip landingSound;
	[SerializeField] AudioClip walkingSound;
	[SerializeField] GameObject soundPrefab;	//the prefab that will actually play the sound
	[SerializeField] GameObject soundSource;	//the object that the sound is supposed to eminate from

	Dictionary<AudioClip, AudioSource> existingSounds = new Dictionary<AudioClip, AudioSource>();

	void Jump(){

	}

	void PlayOnce(AudioClip clip){
		
	}

	void PlayRepeat(AudioClip clip){

	}

	void StopSound(AudioClip clip){

	}

	void CreateSoundSystem(AudioClip clip, bool repeat, GameObject sourceObject){
		GameObject player = GameObject.Instantiate(soundPrefab);
		player.GetComponent<SoundPlayer>().Initialize(clip, repeat, sourceObject);
	}

}
