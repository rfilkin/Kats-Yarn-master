using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerRotate : MonoBehaviour, SwitchEffect {

	[SerializeField] float rotationDegrees = 90;
	[SerializeField] float rotationTime = 2;
	[SerializeField] Transform planetLayer;
    [SerializeField] Transform Player;

	CombinationSystem puzzleSystem;
	ComboSystemWithSymbol newSystem;

	Quaternion previousRotation;
	float timer = 0f;
	bool isRotating = false;

	void Start(){
		puzzleSystem = FindObjectOfType<CombinationSystem>();
		newSystem = FindObjectOfType<ComboSystemWithSymbol>();
	}

	public void TriggerEffect(){
		previousRotation = planetLayer.rotation;
		isRotating = true;
		DisablePuzzle();	//disables the puzzle when the rotation starts
	}

	public bool CanTrigger(){
		return !isRotating;
	}

	void Update(){
		if (!isRotating){
			timer = 0f;
			return;
		}
		Rotate();
	}

	void Rotate(){
        Player.parent = planetLayer;
		timer += Time.deltaTime;
		timer = Mathf.Clamp(timer, 0, rotationTime);
		float currentAngle = Mathf.Lerp(0, rotationDegrees, timer / rotationTime);

		planetLayer.rotation = previousRotation * Quaternion.AngleAxis(currentAngle, Vector3.forward);
		if (timer == rotationTime)
			RotationFinished();
        Player.parent = null;
	}

	void RotationFinished(){
		isRotating = false;
		UpdatePuzzle();	//updates the solution when the rotation ends
	}

	void DisablePuzzle(){
		if (puzzleSystem != null)
			puzzleSystem.PuzzleOff();
		else
			newSystem.PuzzleOff();
	}

	void UpdatePuzzle(){
		if (puzzleSystem != null)
			puzzleSystem.UpdatePuzzle();
		else
			newSystem.UpdatePuzzle();
	}
}
