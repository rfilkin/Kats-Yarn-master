using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLayerControl : MonoBehaviour {

	[SerializeField] Transform npcTopParent;

	public void ChangeNPCLayer(string layerName){
		RecursiveChange(npcTopParent, layerName, 0);
	}

	public void ChangeNPCSorting(int adjustPriority){
		RecursiveChange(npcTopParent, "", adjustPriority);
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
