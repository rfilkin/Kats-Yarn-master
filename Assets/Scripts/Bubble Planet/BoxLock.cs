using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UNTESTED
/// </summary>

[RequireComponent(typeof(Collider2D))]
public class BoxLock : MonoBehaviour {

	[SerializeField] Transform finalPosition = null;
	[SerializeField] float slidingRate = 0.2f;
	[SerializeField] float detecionTolerance = 1;
	[SerializeField] float lockingTolerance = 0.3f;
	[SerializeField] float animationSpeed = 0.2f;
	[SerializeField] ParticleSystem particleSystem;

	Collider2D triggerArea;
	Collider2D lockingObject = null;
	bool finished = false;
	bool repositioned = false;
	BubbleProgress puzzleManager;

	//-------------------------------------------------------------------------------
	// Use this for initialization
	//-------------------------------------------------------------------------------
	void Start () {
		triggerArea = GetComponent<Collider2D>();
	}

	//-------------------------------------------------------------------------------
	// Update is called once per frame
	//-------------------------------------------------------------------------------
	void Update () {
		if (lockingObject == null)
			return;
		if (!finished){
			LerpToCenter();
			CheckFinished();
		}
		if (finished && !repositioned){
			MoveToFinalPosition();
		}
	}

	//-------------------------------------------------------------------------------
	// Checks if a "Box" is overlapping the detection area
	//-------------------------------------------------------------------------------
	void OnTriggerStay2D(Collider2D col){
		if (lockingObject == null && col.gameObject.tag == "Box" && IsBoxCloseEnough(col)){
			SetLockingObject(col);
		}
	}

	//-------------------------------------------------------------------------------
	// Returns true if the box is close enough to the solution point to qualify as solved
	//-------------------------------------------------------------------------------
	bool IsBoxCloseEnough(Collider2D col){
		return Vector2.Distance(col.bounds.center, triggerArea.bounds.center) <= detecionTolerance;
	}

	//-------------------------------------------------------------------------------
	// saves a reference to the object
	//-------------------------------------------------------------------------------
	void SetLockingObject(Collider2D col){
		lockingObject = col;
	}

	//-------------------------------------------------------------------------------
	//this doesn't actually lerp; its moves the box with a force into the locking point
	//-------------------------------------------------------------------------------
	void LerpToCenter(){
		Vector2 offset = (lockingObject.transform.position - transform.position).normalized;
		float speedTowardsGoal = Vector2.Dot(lockingObject.attachedRigidbody.velocity, offset);
		float maxForce = slidingRate - Mathf.Clamp(speedTowardsGoal, 0, slidingRate);

		lockingObject.attachedRigidbody.AddForce(-offset * maxForce * lockingObject.attachedRigidbody.mass, ForceMode2D.Impulse);
	}

	//-------------------------------------------------------------------------------
	//checks if the box has reached the locking point
	//-------------------------------------------------------------------------------
	void CheckFinished(){
		if (Vector2.Distance(lockingObject.bounds.center, triggerArea.bounds.center) <= lockingTolerance){
			Lock();
		}
	}

	//-------------------------------------------------------------------------------
	//sets the box to locking mode and informs the puzzle manager that this section is complete
	//-------------------------------------------------------------------------------
	void Lock(){
		finished = true;
		lockingObject.GetComponent<Box>().Disable();
		particleSystem.Stop();
		Complete();
	}

	//-------------------------------------------------------------------------------
	//this handles the animation of moving the box from the locking point to its final resting place.
	//-------------------------------------------------------------------------------
	void MoveToFinalPosition(){
		if (finalPosition == null)
			repositioned = true;

		Vector2 offset = finalPosition.position - lockingObject.transform.position;
		offset.Normalize();

		float magnitude = Mathf.Min(Vector2.Distance(lockingObject.transform.position, finalPosition.position), animationSpeed);
		offset *= magnitude;

		lockingObject.transform.position += new Vector3(offset.x, offset.y, 0);

		if (Vector2.Distance(lockingObject.transform.position, finalPosition.position) <= Mathf.Epsilon){
			repositioned = true;
		}
	}

	//-------------------------------------------------------------------------------
	//tells the puzzle manager that this piece of the puzzle is complete
	//-------------------------------------------------------------------------------
	void Complete(){
		puzzleManager.CheckSolution();
	}

	//-------------------------------------------------------------------------------
	//gets a reference to the puzzle manager
	//-------------------------------------------------------------------------------
	public void InitPuzzleManager(BubbleProgress manager){
		puzzleManager = manager;
	}

	//-------------------------------------------------------------------------------
	//returns true if this part of the puzzle is complete
	//-------------------------------------------------------------------------------
	public bool GetComplete(){
		return finished;
	}

}
