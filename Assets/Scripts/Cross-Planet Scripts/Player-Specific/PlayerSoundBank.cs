using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSoundBank : MonoBehaviour {

	[SerializeField] AudioClip jump;
	[SerializeField] AudioClip land;
    [SerializeField] AudioClip push;

	AudioSource audioPlayer;

	void Start(){
		audioPlayer = GetComponent<AudioSource>();
	}

	public void OnJump(){
		audioPlayer.PlayOneShot(jump);
	}

	public void OnLand(){
		audioPlayer.PlayOneShot(land);
	}

}
