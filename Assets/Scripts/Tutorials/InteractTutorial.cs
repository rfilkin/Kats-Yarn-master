using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTutorial : MonoBehaviour {
    // the thought bubbles that will appear for the tutorial
    // these are not prefabs; they are instanced objects that are initally hidden
    [Header("Thought Bubble Objects")]
    [SerializeField] GameObject interactThoughtBubble;

    [Header("Scene References")]
    [SerializeField] Transform thoughtBubblePoint;

    [Header("drop Tutorial Settings")]
    [SerializeField] float interactTutorialDelay = 1f;

    //interact tutorial variables
    bool interactTutorialFinished = false;
    bool interactBubbleSpawned = false;
    float interactTutorialTimer = 0f;

    // Use this for initialization
    void Start () {
        interactThoughtBubble.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (!interactTutorialFinished)
            ManageInteractTutorial();
    }

    void ManageInteractTutorial()
    {
        //wait for drop tutorial delay
        if (interactTutorialTimer < interactTutorialDelay)
        {
            interactTutorialTimer += Time.deltaTime;
            return;
        }
        //after tutorial delay, spawn the bubble only once
        if (!interactBubbleSpawned)
        {
            TutorialSharedMethods.SpawnBubble(interactThoughtBubble, thoughtBubblePoint);
            interactBubbleSpawned = true;
        }
        //see if the player has interacted enough
        if (!PlayerHasInteractedEnough())
        {
            return;
        }
        //when they have jumped enough, end the tutorial
        TutorialSharedMethods.DestroyBubble(interactThoughtBubble);
        interactTutorialFinished = true;
    }

    bool PlayerHasInteractedEnough()
    {
        //return interactScript.GetJumpCount() > 0;   //for now just an interact count
        if (Input.GetKeyDown(KeyCode.Q))
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
}
