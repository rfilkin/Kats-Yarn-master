using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayerControl : MonoBehaviour {

	[SerializeField] Transform playerTopParent;

	public void ChangePlayerLayer(string layerName){
		RecursiveChange(playerTopParent, layerName, 0);
	}

	public void ChangePlayerSorting(int adjustPriority){
		RecursiveChange(playerTopParent, "", adjustPriority);
	}

	void RecursiveChange(Transform obj, string layerName, int priorityAdjust){
		SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
		if(sr != null){
			if (layerName != "")
				sr.sortingLayerName = layerName;
			else
				sr.sortingOrder += priorityAdjust;
		}

		for (int i = 0; i < obj.childCount; ++i){
			RecursiveChange(obj.GetChild(i), layerName, priorityAdjust);
		}
	}
}
