using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CenterTracker))]
public class CombinationSystem : MonoBehaviour {

	static float tolerance = 5f;

	[SerializeField] ComboLayer baseLayer;

	CenterTracker center;

	// Use this for initialization
	void Start () {
		center = GetComponent<CenterTracker>();
	}

	//returns the vector from the center to the given node
	Vector2 VectorToNode(Vector2 nodePosition){
		return nodePosition - center.GetGlobalCenter();
	}

	//returns the angle between the two nodes using the center as a zero point
	float AngleBetweenNodes(Vector2 node1, Vector2 node2){
		return Vector2.Angle(VectorToNode(node1), VectorToNode(node2));
	}

	bool NodesInLine(GameObject upNode, PowerLink downNode){
		return AngleBetweenNodes(upNode.transform.position, downNode.transform.position) <= tolerance;
	}

	public void UpdatePuzzle(){
		baseLayer.GetRecieverNodes()[0].SetPower(true);
		if (IsComplete(baseLayer)){
			//you win!
		}
	}

	//checks if the puzzle is complete and updates each layer in the process
	bool IsComplete(ComboLayer currentLayer){
		List<PowerLink> poweredNodes = new List<PowerLink>();

		//find all of the powered downNodes in this layer
		foreach(PowerLink node in currentLayer.GetRecieverNodes()){
			if (node.IsPowered()){
				poweredNodes.Add(node);
			}
		}


		ComboLayer nextLayer = currentLayer.GetNextLayer();

		//this part implies that the final layer needs only one node to be powered, or that the layer has only one node
		//this section could be modified so that a set of nodes needs to be powered instead.
		if (poweredNodes.Count > 0 && nextLayer == null)
			return true;

		if (nextLayer == null)
			return false;

		//turn off all the downNodes in the next layer
		foreach(PowerLink node in nextLayer.GetRecieverNodes()){
			node.SetPower(false);
		}

		//if there are no powered nodes here, then this cannot be the solution
		if (poweredNodes.Count == 0){
			IsComplete(nextLayer);	//this has to be done in order to turn off all the nodes on the layers above
			return false;
		}
			

		//for each powered upNode in this layer, find and turn on each corresponding downNode in the next layer
		foreach (PowerLink node in poweredNodes){
			foreach(GameObject upNode in node.GetAttachedOutNodes()){
				foreach(PowerLink downNode in nextLayer.GetRecieverNodes()){
					if (NodesInLine(upNode, downNode))
						downNode.SetPower(true);
				}
			}
		}

		//do recursion onto the next layer
		return IsComplete(nextLayer);
	}

	public void PuzzleOff(){
		TurnOffLayer(baseLayer.GetNextLayer());
	}

	void TurnOffLayer(ComboLayer currentLayer){
		if (currentLayer == null)
			return;

		//find all of the powered downNodes in this layer
		foreach(PowerLink node in currentLayer.GetRecieverNodes()){
			node.SetPower(false);
		}

		TurnOffLayer(currentLayer.GetNextLayer());
	}

}
