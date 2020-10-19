using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneTrigger : MonoBehaviour {
    public bool canConverse = false;
    public bool currentlyConversing = false;
    KeyCode proceedKey;
    public Transform ThoughtBubblePoint;
    public GameObject interactBubble;
    public GameObject[] bubbles;
    public Transform [] attachPoints;
    public bool bubbleSpawned = false;

	public UnityEvent onConversationEnd;

    // Use this for initialization
    void Start () {
		proceedKey = PlayerControlMap.interact;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(proceedKey) && canConverse && !currentlyConversing)
        {
            currentlyConversing = true;
            TutorialSharedMethods.DestroyBubble(interactBubble);
            bubbleSpawned = false;
            StartCoroutine(CutsceneCoroutine());
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            canConverse = true;
            TutorialSharedMethods.SpawnBubble(interactBubble, ThoughtBubblePoint);
            bubbleSpawned = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            canConverse = false;
            if (bubbleSpawned)
            {
                TutorialSharedMethods.DestroyBubble(interactBubble);
                bubbleSpawned = false;
            }
        }
    }

    IEnumerator CutsceneCoroutine()
    {
        Debug.Log("player has started talking");
        Statics.SetPlayerControl(false, this); // revoke control from player

        yield return null; //wait until player releases Q

        for (int i = 0; i < bubbles.Length; ++i)
        {
            bubbles[i].transform.position = attachPoints[i].position;
            bubbles[i].transform.rotation = attachPoints[i].rotation;
			bubbles[i].transform.parent = attachPoints[i];
            bubbles[i].GetComponent<Renderer>().enabled = true; // makes bubble visible
            while (!Input.GetKeyDown(proceedKey)) //wait until user proceeds
            {
                yield return null;
            }

            yield return null;

            bubbles[i].GetComponent<Renderer>().enabled = false; // makes bubble invisible
        }

        Debug.Log("player has finished talking");
        Statics.SetPlayerControl(true, this); // return control to player
        currentlyConversing = false;
        TutorialSharedMethods.SpawnBubble(interactBubble, ThoughtBubblePoint);
        bubbleSpawned = true;
		onConversationEnd.Invoke();
    }

	void OnDisable(){
		if(bubbleSpawned)
			TutorialSharedMethods.DestroyBubble(interactBubble);
	}
}
