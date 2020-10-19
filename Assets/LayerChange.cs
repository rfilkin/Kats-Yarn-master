using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject target; // the child of the gameobject that will be affected by this script

    public void ChangeOrderInLayer(int orderInLayer)
    {
        target.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
    }

    public void ChangeSortingLayer()
    {
        target.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
    }
}
