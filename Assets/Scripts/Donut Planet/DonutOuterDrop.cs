using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attaching this to a collider will make it so that the player can penetrate the
/// collider while standing in
/// one of the serialized 'Drop Through Areas', which are other colliders.
/// </summary>
[RequireComponent(typeof(CircleCollider2D))]
public class DonutOuterDrop : MonoBehaviour {

	static string ignorePlayerLayer = "Ignore Player Collision";
	static string defaultLayer = "Planet";

	[SerializeField] Collider2D[] dropThroughAreas;
	[SerializeField] Collider2D player;

	CircleCollider2D thisCollider;

	void Start () {
		gameObject.layer = LayerMask.NameToLayer(defaultLayer);
		thisCollider = GetComponent<CircleCollider2D>();
	}

	// Update is called once per frame
	void Update () {
		if(gameObject.layer == LayerMask.NameToLayer(ignorePlayerLayer) && !IsPlayerInside()){
			gameObject.layer = LayerMask.NameToLayer(defaultLayer);
		}
		if (IsPlayerInDropArea()){
			gameObject.layer = LayerMask.NameToLayer(ignorePlayerLayer);
		}
	}

	bool IsPlayerInside(){
		Collider2D[] colliders = Physics2D.OverlapCircleAll(thisCollider.bounds.center, thisCollider.radius);

		foreach (Collider2D col in colliders){
			if (col.tag == "Player")
				return true;
		}

		return false;

	}

	bool IsPlayerInDropArea(){
		Collider2D[] colliders = Physics2D.OverlapPointAll(player.bounds.center);

		foreach (Collider2D col in colliders){
			foreach(Collider2D dropZone in dropThroughAreas){
				if (col == dropZone)
					return true;
			}
		}

		return false;
	}
}
