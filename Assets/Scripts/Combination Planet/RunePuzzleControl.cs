using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ComboSystemWithSymbol))]
public class RunePuzzleControl : MonoBehaviour {

	ComboSystemWithSymbol system;

	// Use this for initialization
	void Start () {
		system = GetComponent<ComboSystemWithSymbol>();
	}

	void Update(){
		system.UpdatePuzzle();
	}

}
