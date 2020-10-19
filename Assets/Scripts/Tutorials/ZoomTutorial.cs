using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomTutorial : MonoBehaviour {
    // the thought bubbles that will appear for the tutorial
    // these are not prefabs; they are instanced objects that are initally hidden
    [Header("Thought Bubble Objects")]
    [SerializeField]
    GameObject zoomThoughtBubble;

    [Header("Scene References")]
    [SerializeField]
    Transform thoughtBubblePoint;

    [Header("drop Tutorial Settings")]
    [SerializeField]
    float zoomTutorialDelay = 1f;

    //interact tutorial variables
    bool zoomTutorialFinished = false;
    bool zoomBubbleSpawned = false;
    float zoomTutorialTimer = 0f;
    // Use this for initialization
    void Start () {
        zoomThoughtBubble.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (!zoomTutorialFinished)
            ManageZoomTutorial();
    }

    void ManageZoomTutorial()
    {
        //wait for drop tutorial delay
        if (zoomTutorialTimer < zoomTutorialDelay)
        {
            zoomTutorialTimer += Time.deltaTime;
            return;
        }
        //after tutorial delay, spawn the bubble only once
        if (!zoomBubbleSpawned)
        {
            TutorialSharedMethods.SpawnBubble(zoomThoughtBubble, thoughtBubblePoint);
            zoomBubbleSpawned = true;
        }
        //see if the player has interacted enough
        if (!PlayerHasZoomedEnough())
        {
            return;
        }
        //when they have jumped enough, end the tutorial
        TutorialSharedMethods.DestroyBubble(zoomThoughtBubble);
        zoomTutorialFinished = true;
    }

    bool PlayerHasZoomedEnough()
    {
        //return interactScript.GetJumpCount() > 0;   //for now just an interact count
        if (Input.GetKeyDown(PlayerControlMap.zoom))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
