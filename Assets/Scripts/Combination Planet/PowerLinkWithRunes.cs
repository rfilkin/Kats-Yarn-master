using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerLinkWithRunes : MonoBehaviour {

	[SerializeField] List<GameObject> linkedUpnodes;
	[SerializeField] bool isPowered = false;

	[SerializeField] List<PowerSymbol> powerSymbols = new List<PowerSymbol>();
	[SerializeField] UnityEvent onPowered;

	public List<GameObject> GetAttachedOutNodes(){
		return linkedUpnodes;
	}

	public void SetPower(bool powerState){
		isPowered = powerState;
		if (isPowered)
			onPowered.Invoke();
		UpdateLines();
	}

	public bool IsPowered(){
		return isPowered;
	}

	void UpdateLines(){
		if (isPowered)
			SetPowerLines(true);
		else
			SetPowerLines(false);
	}

	void SetPowerLines(bool on){
		foreach (PowerSymbol line in powerSymbols)
			line.SetPowered(on);
	}
}
