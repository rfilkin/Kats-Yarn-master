using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starLogic : MonoBehaviour {

	public Transform lightTarget;
    public GameObject sourceMirror;
    SpriteRenderer myRenderer;
    public bool active = false;

    // Use this for initialization
    void Start () {
        myRenderer = GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
        if (sourceMirror.GetComponent<mirrorLogic>().mirrorState == 2)
        {
            myRenderer.color = Color.white;
            active = true;
        }
        else
        {
            myRenderer.color = Color.gray;
            active = false;
        }
    }
}
