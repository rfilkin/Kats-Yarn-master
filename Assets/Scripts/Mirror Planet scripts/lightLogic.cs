using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightLogic : MonoBehaviour {

    public string myLightDirection;
    public GameObject sourceMirror;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (sourceMirror.GetComponent<mirrorLogic>().mirrorState == 0 && myLightDirection == "left")
        {
            GetComponent<Renderer>().enabled = true;
        }
        else if (sourceMirror.GetComponent<mirrorLogic>().mirrorState == 1 && myLightDirection == "right")
        {
            GetComponent<Renderer>().enabled = true;
        }
        else if (sourceMirror.GetComponent<mirrorLogic>().mirrorState == 2 && myLightDirection == "center")
        {
            GetComponent<Renderer>().enabled = true;
        }
        else
        {
            GetComponent<Renderer>().enabled = false;
        }

    }
}
