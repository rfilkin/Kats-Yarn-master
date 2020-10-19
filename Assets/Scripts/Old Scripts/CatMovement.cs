using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CatMovement : MonoBehaviour {

	[SerializeField] KeyCode left = KeyCode.A;
	[SerializeField] KeyCode right = KeyCode.D;
	[SerializeField] KeyCode up = KeyCode.W;
	[SerializeField] KeyCode down = KeyCode.S;

	[SerializeField] float force = 1;

	public float jumpForce;
	
	Rigidbody2D body;
	int willGoRight = 0;

	void Start () {
		jumpForce = 700f;
		body = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		CheckKeyConflict();
		//InputMovement();
		LateralDamping();
		direction();
		jump();

	}

	//makes it so that when both buttons are pushed down, the one pushed down last takes precidence
	void CheckKeyConflict(){
		if (Input.GetKeyDown(left))
			willGoRight = -1;
		if (Input.GetKeyDown(right))
			willGoRight = 1;
	}
	
	//void InputMovement(){
	//	int direction = (Input.GetKey(left) ? -1 : 0) + (Input.GetKey(right) ? 1 : 0);
	//	if (Input.GetKey(left) && Input.GetKey(right)){
	//		direction = willGoRight;
	//	}
	//	body.AddRelativeForce(direction * force * Vector2.right, ForceMode2D.Impulse);
	//}

	void LateralDamping(){
		//body.velocity;
	}
	
	void direction(){
		if(Input.GetKey(left))
			transform.localScale = new Vector3(-1,1,1);
		else if(Input.GetKey(right))
			transform.localScale = new Vector3(1,1,1);
	}
	
	void jump(){
		if(Input.GetKeyDown(KeyCode.W))
			body.AddForce(transform.up * jumpForce);	
	}
}
