using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mk two cat move.
/// 
/// The movement script for the player.
/// 
/// This script fails to account for the angular velocty of the planet, so the player moves extra fast one way and extra
/// slow the other way. If we want the planet to rotate, this will have to be calculated in
/// </summary>

[RequireComponent(typeof(CenterTracker))]
[RequireComponent(typeof(Rigidbody2D))]
public class MkTwoCatMove : MonoBehaviour {

	KeyCode left;
	KeyCode right;

	[SerializeField] float moveForce = 10f;
	[SerializeField] float maxTangentialSpeed = 6f;

	CenterTracker center;
	Rigidbody2D body;
	int willGoRight = 0;

	// Use this for initialization
	void Start () {
		left = PlayerControlMap.left;
		right = PlayerControlMap.right;
		center = GetComponent<CenterTracker>();
		body = GetComponent<Rigidbody2D>();
	}

	void Update(){
		CheckKeyConflict();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!Statics.PlayerHasControl)
			return;
		InputMovement();
		//LateralDamping();
		//InputJump(); // on new script
	}

	//makes it so that when both buttons are pushed down, the one pushed down last takes precidence
	void CheckKeyConflict(){
		if (Input.GetKeyDown(left))
			willGoRight = -1;
		if (Input.GetKeyDown(right))
			willGoRight = 1;
	}

	//reads the player's input and converts it to lateral movement
	void InputMovement(){
		int direction = (Input.GetKey(left) ? -1 : 0) + (Input.GetKey(right) ? 1 : 0);
		if (Input.GetKey(left) && Input.GetKey(right)){
			direction = willGoRight;
		}
		Vector2 inputForce = direction * moveForce * transform.right * Time.fixedDeltaTime;
		inputForce = SpeedLimitedForce(inputForce);

		body.AddForce(inputForce, ForceMode2D.Impulse);
	}

	//takes the force that the player would like to exert (for movement) and limits it so the player doesn't go too fast
	Vector2 SpeedLimitedForce(Vector2 rawForce){
		Vector2 projVelocity = Radium.TangentVelocity(transform.position, body, center.GetGlobalCenter());
		float dot = Vector2.Dot(rawForce, projVelocity);

		if (dot < 0)
			return rawForce;	//if your applied force is against the current velocity, you get full force
		if (projVelocity.magnitude >= maxTangentialSpeed)
			return Vector2.zero;	//if you are already going full speed, you get no force

		//otherwise, you get enough force to get you up to full speed or full force, whichever is less
		float maxAllowedForce = Mathf.Min(maxTangentialSpeed - projVelocity.magnitude, rawForce.magnitude);
		return rawForce.normalized * maxAllowedForce;
	}

	//causes the player to drop through a platform effector
	void DropThroughPlatform(){
		RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 1, -transform.up, 2);
		foreach (RaycastHit2D hit in hits){
			DropPlatform p = hit.collider.gameObject.GetComponent<DropPlatform>();
			if (p != null)
				p.DisableCollision(1.5f);
		}
	}

	public int GetWillGoRight(){
		return willGoRight;
	}
}
