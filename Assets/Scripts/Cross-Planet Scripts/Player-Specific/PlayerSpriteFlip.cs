using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteFlip : MonoBehaviour {

	[SerializeField] MkTwoCatMove moveControls;
	//also need to read grip controls

	float rightOffset;
	SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer>();
		rightOffset = transform.localPosition.x;
	}
	
	// Update is called once per frame
	void Update () {
		SetFacing();
	}

	//assume that right-facing is default
	void SetFacing(){
		int right = moveControls.GetWillGoRight();

		if (right == 1){
			FaceRight();
		} else if (right == -1){
			FaceLeft();
		}
	}

	void FaceRight(){
		transform.localPosition = new Vector2(rightOffset, transform.localPosition.y);
		sprite.flipX = false;
	}

	void FaceLeft(){
		transform.localPosition = new Vector2(-rightOffset, transform.localPosition.y);
		sprite.flipX = true;
	}
}
