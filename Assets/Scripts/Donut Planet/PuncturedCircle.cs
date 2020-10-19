using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuncturedCircle : MonoBehaviour {

	[SerializeField] Collider2D puncturedCollider = null;
	[SerializeField] string circleGroup = "";

	// Use this for initialization
	void Start () {
		if (puncturedCollider == null)
			throw new MissingReferenceException("No collider to puncture");
		foreach (CirclePuncture cp in GameObject.FindObjectsOfType<CirclePuncture>()){
			if (cp.circleGroup == circleGroup)
				cp.SetOwner(puncturedCollider);
		}
	}
}
