using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attaching this to a collider will make it so that the player can penetrate the
/// collider if they press the 'drop through platforms' button while standing in
/// one of the serialized 'Drop Through Areas', which are other colliders.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class DropCircle : MonoBehaviour {

	static string ignorePlayerLayer = "Ignore Player Collision";
	static string defaultLayer = "Planet";

	[SerializeField] Collider2D thisCollider;
	[SerializeField] bool isStaticHole = false;
	[SerializeField] Collider2D[] dropThroughAreas;
	[SerializeField] Collider2D player;

	[SerializeField] bool findCollidersAutomatically = false;
	[SerializeField] string tagName = "";

	KeyCode control;

	void Start () {
		control = PlayerControlMap.drop;
		gameObject.layer = LayerMask.NameToLayer(defaultLayer);
		if (thisCollider == null)
			thisCollider = GetComponent<Collider2D>();
		if (findCollidersAutomatically)
			FindColliders();
	}

	void FindColliders(){
		GameObject[] foundObjects = GameObject.FindGameObjectsWithTag(tagName);
		dropThroughAreas = new Collider2D[foundObjects.Length];

		int i = 0;
		foreach(GameObject g in foundObjects){
			Collider2D triggerArea = g.GetComponent<Collider2D>();
			if (triggerArea == null){
				throw new UnityException("No collider on drop zone!!!");
			}
			dropThroughAreas[i] = triggerArea;
			++i;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.layer == LayerMask.NameToLayer(ignorePlayerLayer) && !IsPlayerInside()){
			gameObject.layer = LayerMask.NameToLayer(defaultLayer);
		}
		if ((Input.GetKey(control) || isStaticHole) && IsPlayerInDropArea()){
			gameObject.layer = LayerMask.NameToLayer(ignorePlayerLayer);
		}
	}

	bool IsPlayerInside(){
		//Collider2D[] colliders = Physics2D.OverlapCircleAll(thisCollider.bounds.center, thisCollider.radius);
		Collider2D[] colliders = new Collider2D[100];
		int results = Physics2D.OverlapCollider(thisCollider, new ContactFilter2D(), colliders);

		for (int i = 0; i < results; ++i){
			if (colliders[i].tag == "Player")
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
