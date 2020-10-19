using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class IgnoreCollisions : MonoBehaviour {

	[SerializeField] Collider2D[] others;

	Collider2D[] theseColliders;

	// Use this for initialization
	void Start () {
		theseColliders = GetComponents<Collider2D>();

		foreach(Collider2D col in others){
			foreach(Collider2D thisCol in theseColliders){
				Physics2D.IgnoreCollision(thisCol, col);
			}
		}
	}
}
