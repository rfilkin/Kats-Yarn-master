using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yellowLineLogic : MonoBehaviour {

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject star4;
    public GameObject star5;
    SpriteRenderer myRenderer;

    // Use this for initialization
    void Start () {
        myRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		if(star1.GetComponent<starLogic>().active
           && star2.GetComponent<starLogic>().active
           && star3.GetComponent<starLogic>().active
           && star4.GetComponent<starLogic>().active
           && star5.GetComponent<starLogic>().active)
        {
            myRenderer.color = Color.white;
        }
        else
        {
            myRenderer.color = Color.gray;
        }
	}
}
