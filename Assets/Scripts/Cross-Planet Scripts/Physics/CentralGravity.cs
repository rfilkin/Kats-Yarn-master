using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CenterTracker))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class CentralGravity : MonoBehaviour {

	enum ForceType{
		VelocityChange, Impulse, Force
	}

	static float gravity = 25f; //25f;
	public static float GetGravityAcceleration(){return gravity;}
	public bool reversed;

	[SerializeField] ForceType forceType = ForceType.Impulse;

	Vector3 baseScale;
	Vector3 flipScale;
	CenterTracker center;
	Rigidbody2D body;
	//Collider2D col;

	// Use this for initialization
	void Start () {
		baseScale = transform.localScale;
		flipScale = new Vector3(baseScale.x, -baseScale.y, baseScale.z);
		center = GetComponent<CenterTracker>();
		body = GetComponent<Rigidbody2D>();
		//col = GetComponent<Collider2D>();
		body.gravityScale = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Compensate();
	}

	void Compensate(){
		switch (forceType){
			case ForceType.Force:
				Force();
				break;
			case ForceType.Impulse:
				Impulse();
				break;
			case ForceType.VelocityChange:
				VelocityChange();
				break;
		}
	}

	/*
	 * Note: This may need to be modified so that the offset is calculated from the center of mass
	 * or 'collider.bounds.center'.
	 */

	void Impulse(){
		Vector2 offset = (Vector2)transform.position - center.GetGlobalCenter();
		float reversalValue = reversed ? -1 : 1;
		body.AddForce(-offset.normalized * reversalValue * gravity * Time.fixedDeltaTime * body.mass, ForceMode2D.Impulse);
	}

	void VelocityChange(){
		Vector2 offset = (Vector2)transform.position - center.GetGlobalCenter();
		body.velocity += -offset.normalized * gravity * Time.fixedDeltaTime;
	}

	//this one is weaaaaak
	void Force(){
		Vector2 offset = (Vector2)transform.position - center.GetGlobalCenter();
		body.AddForce(-offset.normalized * gravity * Time.fixedDeltaTime * body.mass, ForceMode2D.Force);
	}

	public void UpsideDown(){
		reversed = true;
		FlipVertical(true);
	}

	public void RightsideUp(){
		reversed = false;
		FlipVertical(false);
	}

	void FlipVertical(bool isFlipped){
		transform.localScale = isFlipped ? flipScale : baseScale;
	}
}
