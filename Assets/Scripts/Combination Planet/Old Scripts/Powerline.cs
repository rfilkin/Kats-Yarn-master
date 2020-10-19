using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Powerline : MonoBehaviour {

	[SerializeField] Material onMaterial;
	[SerializeField] Material offMaterial;

	LineRenderer line;
	bool initialized = false;

	// Use this for initialization
	void Start () {
		Initialize();
	}

	public void Initialize(){
		if (initialized)
			return;
		line = GetComponent<LineRenderer>();
		initialized = true;
	}

	public void SetPowered(bool powered){
		Initialize();
		if (powered)
			line.material = Material.Instantiate(onMaterial);
		else
			line.material = Material.Instantiate(offMaterial);
	}
}
