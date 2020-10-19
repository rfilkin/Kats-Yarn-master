using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolutionChecker : MonoBehaviour {
	
	//Bubble Planet
	List<bool> GeysersActive = new List<bool>();
	
	//Donut Planet
	[SerializeField] Collider2D donutBarrier;
	public bool checkKey;
	//GameObject dpBarrierOne;
	
	
	// Use this for initialization
	void Start () {
		
		//Bubble Planet
        for (int i = 0; i < 4; i++)
        {
            GeysersActive.Add(false);
        }
		
		//Donut Planet
		//dpBarrierOne = GameObject.Find("BarrierOne");
		
    }

	// Update is called once per frame
	void Update () {
		
	}
	
	//Bubble Planet
	public void ActivateGeyser(int index)
    {
        this.GeysersActive[index] = true;
    }
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "donutKey"){
			checkKey = true;
			other.gameObject.SetActive(false);
		}
	}
	
	//Donut Planet
	void OnCollisionEnter2D(Collision2D other){
		if(checkKey && other.gameObject.name == "BarrierOne"){
			other.gameObject.SetActive(false);
		}
	}
	
}
