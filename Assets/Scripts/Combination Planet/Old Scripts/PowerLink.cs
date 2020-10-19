using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerLink : MonoBehaviour {

	[SerializeField] GameObject linePrefab;
	[SerializeField] List<GameObject> linkedUpnodes;
	[SerializeField] bool isPowered = false;

	List<Powerline> powerLines = new List<Powerline>();

	void Start(){
		CreatePowerLines();
	}

	public List<GameObject> GetAttachedOutNodes(){
		return linkedUpnodes;
	}

	public void SetPower(bool powerState){
		isPowered = powerState;
		UpdateLines();
	}

	public bool IsPowered(){
		return isPowered;
	}

	void CreatePowerLines(){
		foreach (GameObject upNode in linkedUpnodes){
			powerLines.Add(CreateLine(upNode));
		}
		UpdateLines();
	}

	Powerline CreateLine(GameObject target){
		GameObject lineObj = GameObject.Instantiate(linePrefab);
		lineObj.transform.SetParent(transform);
		lineObj.transform.position = transform.position;
		LineRenderer line = lineObj.GetComponent<LineRenderer>();
		line.SetPosition(1, target.transform.position - transform.position);
		return lineObj.GetComponent<Powerline>();
	}

	void UpdateLines(){
		if (isPowered)
			PowerLines();
		else
			UnpowerLines();
	}

	void PowerLines(){
		foreach (Powerline line in powerLines)
			line.SetPowered(true);
	}

	void UnpowerLines(){
		foreach (Powerline line in powerLines)
			line.SetPowered(false);
	}
}
