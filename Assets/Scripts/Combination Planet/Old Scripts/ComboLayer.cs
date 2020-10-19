using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This represents one layer of the combination lock puzzle. Each layer has some number of nodes that recieve power
/// from the layer below if aligned correctly, and some number of nodes that pass the power upwards.
/// 
/// This class holds a list of all of the downward-facing nodes, which in turn each have a list of upward-facing nodes
/// that they pass power to.
/// </summary>

public class ComboLayer : MonoBehaviour {

	//the list of nodes that can recieve power from the layer below when aligned correctly
	//the base layer will only have one such node
	[SerializeField] List<PowerLink> downNodes;

	//the layer of the combination puzzle above this one. If there isn't one, this layer is considered the final layer.
	[SerializeField] ComboLayer layerAbove = null;

	public List<PowerLink> GetRecieverNodes(){
		return downNodes;
	}

	public ComboLayer GetNextLayer(){
		return layerAbove;
	}

}
