using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutSuccess : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
        AudioSource noise = GameObject.Find("Success Trigger").GetComponent<AudioSource>();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource noise = GameObject.Find("Success Trigger").GetComponent<AudioSource>();
        noise.Play();
        Destroy(this);
    }

    
}
