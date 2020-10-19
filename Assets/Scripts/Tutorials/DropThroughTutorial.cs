using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropThroughTutorial : MonoBehaviour {

	// the thought bubbles that will appear for the tutorial
	// these are not prefabs; they are instanced objects that are initally hidden
	[Header("Thought Bubble Objects")]
	[SerializeField] GameObject dropThoughtBubble;

	[Header("Scene References")]
	[SerializeField] Transform thoughtBubblePoint;

	[Header("drop Tutorial Settings")]
	[SerializeField]
	float dropTutorialDelay = 1f;
    //[SerializeField] float dropTimeThreshold = 3f;  //how long the player has to move for in order to finish the tutorial

    //push tutorial variables
    bool dropTutorialFinished = false;
	bool dropBubbleSpawned = false;
    float playerDropTime = 0f;
    float dropTutorialTimer = 0f;

    //other tutorial collider
    public GameObject otherTutorial;

	// Use this for initialization
	void Start () {
		dropThoughtBubble.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(!dropTutorialFinished)
			ManageDropTutorial();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!dropTutorialFinished && collision.gameObject.tag == "Player")
        {
             TutorialSharedMethods.SpawnBubble(dropThoughtBubble, thoughtBubblePoint);
             dropBubbleSpawned = true;
             ManageDropTutorial();
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!dropTutorialFinished && collision.gameObject.tag == "Player")
        {
            TutorialSharedMethods.DestroyBubble(dropThoughtBubble);
            dropBubbleSpawned = false;
        }
    }

    void ManageDropTutorial()
	{
		//wait for drop tutorial delay
		/*if (dropTutorialTimer < dropTutorialDelay)
		{
			dropTutorialTimer += Time.deltaTime;
			return;
		}
		//after tutorial delay, spawn the bubble only once
		if (!dropBubbleSpawned)
		{
			TutorialSharedMethods.SpawnBubble(dropThoughtBubble, thoughtBubblePoint);
			dropBubbleSpawned = true;
		}*/
		//see if the player has interacted enough
		if (!PlayerHasDroppedEnough())
		{
			return;
		}
		//when they have jumped enough, end the tutorial
		TutorialSharedMethods.DestroyBubble(dropThoughtBubble);
		dropTutorialFinished = true;
        otherTutorial.GetComponent<DropThroughTutorial>().dropTutorialFinished = true;
	}

	bool PlayerHasDroppedEnough()
	{
        return Input.GetKeyDown(PlayerControlMap.drop);
        //return interactScript.GetJumpCount() > 0;   //for now just an interact count
        /*if (playerDropTime < dropTimeThreshold && (Input.GetKey(PlayerControlMap.drop)))
            playerDropTime += Time.deltaTime;
        return playerDropTime >= dropTimeThreshold;
        */
        //return true;
	}
}
