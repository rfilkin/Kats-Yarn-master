using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TitleCutscene : MonoBehaviour {

	[SerializeField] UnityEvent onCutsceneFinished;

	[SerializeField] float cutsceneDelay;

	[SerializeField] Image exhaust;
	[SerializeField] GameObject spaceShip;
	Vector3 shipPosition;

	[SerializeField] float shipRumbleAmplitude;

	[SerializeField] float engineFlickerDuration;
	[SerializeField] float flickerSpeed;	//defines the period
	[SerializeField] float flickerDurationPercent;	//how long the fire should be out in each period
	[SerializeField] float flickerDecay;	//increases the duration and the speed

	[SerializeField] float fallPhaseDuration;
	float fallSpeed = 1;
	[SerializeField] float fallAcceleration;
	[SerializeField] float fallRotationFactor;

	public TitleScreenLogic titleScreen;

	float timer = -1f;
	bool finished = false;

	void Start(){
		shipPosition = spaceShip.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (finished)
			return;
		spaceShip.transform.position = shipPosition;
		if (timer >= 0){
			timer += Time.deltaTime;
			RunCutscene();
		} else{
			Idle();
		}
	}

	void Idle(){
		ShipRumble();
	}

	void ShipRumble(){
		Vector2 offset = Random.insideUnitCircle * shipRumbleAmplitude;
		spaceShip.transform.position = (Vector3)offset + shipPosition;
	}

	public void StartCutscene(){
		timer = 0f;
	}

	void RunCutscene(){
		titleScreen.FadeUI();
		if (timer < engineFlickerDuration){
			EngineFlicker();
			ShipRumble();
		} else if (timer < engineFlickerDuration + fallPhaseDuration){
			exhaust.enabled = false;
			Fall();
		} else{
			EndCutscene();
		}
	}

	void EngineFlicker(){
		float flickerTime = timer % flickerSpeed;
		bool isOn = (flickerTime / flickerSpeed >= flickerDurationPercent);
		flickerDurationPercent += flickerDecay * Time.deltaTime;
		exhaust.enabled = isOn;
	}

	void Fall(){
		spaceShip.transform.rotation *= Quaternion.AngleAxis(-fallSpeed * fallRotationFactor * Time.deltaTime, Vector3.forward);
		shipPosition -= Vector3.up * fallSpeed * Time.deltaTime;
		fallSpeed *= 1 + (fallAcceleration * Time.deltaTime);
		spaceShip.transform.position = shipPosition;
	}

	void EndCutscene(){
		finished = true;
		onCutsceneFinished.Invoke();
	}


}
