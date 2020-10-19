using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	public Transform camTransform;
	
	public float shakeDuration = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 0.7f;
	public float shakeDecreaseFactor = 0.1f;
	
	public bool startShake = false;
	
	Vector3 originalPos;
	
	void Awake(){
		
		if (camTransform == null)
			camTransform = GetComponent(typeof(Transform)) as Transform;
	}
	
	void OnEnable(){
		
		originalPos = camTransform.localPosition;
	}
	
	IEnumerator Start(){
		
		yield return new WaitForSeconds(10.0f);
		startShake = true;
	}

	void Update(){
		
		if(startShake){
		
			if (shakeDuration > 0)
			{
				camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
				shakeDuration -= Time.deltaTime * decreaseFactor;
				if(shakeAmount > 0){
					shakeAmount -= shakeAmount * shakeDecreaseFactor;
				}
			}
			else
			{
				shakeDuration = 0f;
				camTransform.localPosition = originalPos;
			}
		}
	}
}
