using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectDisplay : MonoBehaviour {

    public GameObject interactBubble;
    public Transform thoughtBubblePoint;
    bool bubbleSpawned = false;

	// Use this for initialization
	void Start () {
        interactBubble.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            TutorialSharedMethods.SpawnBubble(interactBubble, thoughtBubblePoint);
            bubbleSpawned = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            TutorialSharedMethods.DestroyBubble(interactBubble);
            bubbleSpawned = false;
        } 
    }

    // Update is called once per frame
    void Update () {
		
	}
}
