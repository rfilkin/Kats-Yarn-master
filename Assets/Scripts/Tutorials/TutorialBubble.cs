using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBubble : MonoBehaviour {

	// the thought bubbles that will appear for the tutorial
	// these are not prefabs; they are instanced objects that are initally hidden
	[Header("Thought Bubble Objects")]
	[SerializeField] GameObject moveThoughtBubble;
	[SerializeField] GameObject pushThoughtBubble;
	[SerializeField] GameObject jumpThoughtBubble;
    //[SerializeField] GameObject interactThoughtBubble;
    //[SerializeField] GameObject zoomThoughtBubble;

    [Header("Scene References")]
	//[SerializeField] GameObject playerReference;	// used to spawn the thought bubbles attached to the player game object
	[SerializeField] Transform thoughtBubblePoint;
	[SerializeField] BubbleProgress puzzleManager;
	[SerializeField] PlayerJump jumpScript;

	[Header("General Settings")]
	//[SerializeField] float tutorialDelay = 1f;
	[SerializeField] float bubbleSpawnTime = 1f;	//how long it takes a bubble to animate into existence

	//general variables
	bool isRunning = true;	//used to wait until the intro cutscene is over. Maybe.
	bool isFinished = false;	//indicates whether the tutorial is over

	[Header("Move Tutorial Settings")]
	[SerializeField] float moveTutorialDelay = 1f;	//delay until the tutorial appears
	[SerializeField] float moveTimeThreshold = 3f;	//how long the player has to move for in order to finish the tutorial

	//move tutorial variables
	bool moveTutorialFinished = false;
	bool moveBubbleSpawned = false;
	float playerMoveTime = 0f;
	float moveTutorialTimer = 0f;


	[Header("Push Tutorial Settings")]
	[SerializeField] float pushTutorialDelay = 1f;

	//push tutorial variables
	bool pushTutorialFinished = false;
	bool pushBubbleSpawned = false;
	float pushTutorialTimer = 0f;


	[Header("Jump Tutorial Settings")]
	[SerializeField] float jumpTutorialDelay = 1f;

	//push tutorial variables
	bool jumpTutorialFinished = false;
	bool jumpBubbleSpawned = false;
	float jumpTutorialTimer = 0f;

    /*[Header("interact Tutorial Settings")]
    [SerializeField] float interactTutorialDelay = 1f;

    //push tutorial variables
    bool interactTutorialFinished = false;
    bool interactBubbleSpawned = false;
    float interactTutorialTimer = 0f;

    [Header("zoom Tutorial Settings")]
    [SerializeField]
    float zoomTutorialDelay = 1f;

    //push tutorial variables
    bool zoomTutorialFinished = false;
    bool zoomBubbleSpawned = false;
    float zoomTutorialTimer = 0f;*/

    //------------------------------------------------------------------------------
    // Control points
    //------------------------------------------------------------------------------

    // Use this for initialization
    void Start () {
		HideBubbles();
	}
	
	// Update is called once per frame
	void Update () {
		RunTutorials();
	}

	//------------------------------------------------------------------------------
	// Setup
	//------------------------------------------------------------------------------

	void HideBubbles(){
		moveThoughtBubble.SetActive(false);
		pushThoughtBubble.SetActive(false);
		jumpThoughtBubble.SetActive(false);
        //interactThoughtBubble.SetActive(false);
        //zoomThoughtBubble.SetActive(false);
    }

	//------------------------------------------------------------------------------
	// Methods for the MOVEMENT tutorial
	//------------------------------------------------------------------------------

	void ManageMoveTutorial(){
		//wait for move tutorial delay
		if (moveTutorialTimer < moveTutorialDelay){
			moveTutorialTimer += Time.deltaTime;	//this will take longer than the moveTutorialDelay because it isn't incremented first
			return;
		}
		//after tutorial delay, spawn the bubble only once
		if (!moveBubbleSpawned){
			TutorialSharedMethods.SpawnBubble(moveThoughtBubble, thoughtBubblePoint);
			moveBubbleSpawned = true;
		}
		//keep track of how much the player has moved
		if (!PlayerHasMovedEnough()){
			return;
		}
		//when they have moved enough, end the tutorial
		TutorialSharedMethods.DestroyBubble(moveThoughtBubble);
		moveTutorialFinished = true;
	}

	bool PlayerHasMovedEnough(){
		if (playerMoveTime < moveTimeThreshold && (Input.GetKey(PlayerControlMap.right) || Input.GetKey(PlayerControlMap.left)))
			playerMoveTime += Time.deltaTime;
		return playerMoveTime >= moveTimeThreshold;
	}

	//------------------------------------------------------------------------------
	// Methods for the PUSH tutorial
	//------------------------------------------------------------------------------

	void ManagePushTutorial(){
		//wait for push tutorial delay
		if (pushTutorialTimer < pushTutorialDelay){
			pushTutorialTimer += Time.deltaTime;	//this will take longer than the moveTutorialDelay because it isn't incremented first
			return;
		}
		//after tutorial delay, spawn the bubble only once
		if (!pushBubbleSpawned){
			TutorialSharedMethods.SpawnBubble(pushThoughtBubble, thoughtBubblePoint);
			pushBubbleSpawned = true;
		}
		//see if the player has pushed enough - enough is when one of the blocks is in a geyser
		if (!PlayerHasPushedEnough()){
			return;
		}
		//when they have pushed enough, end the tutorial
		TutorialSharedMethods.DestroyBubble(pushThoughtBubble);
		pushTutorialFinished = true;
	}

	bool PlayerHasPushedEnough(){
		return puzzleManager.LockedBoxCount() > 0;
	}

	//------------------------------------------------------------------------------
	// Methods for the JUMP tutorial
	//------------------------------------------------------------------------------

	void ManageJumpTutorial(){
		//wait for push tutorial delay
		if (jumpTutorialTimer < jumpTutorialDelay){
			jumpTutorialTimer += Time.deltaTime;	//this will take longer than the moveTutorialDelay because it isn't incremented first
			return;
		}
		//after tutorial delay, spawn the bubble only once
		if (!jumpBubbleSpawned){
			TutorialSharedMethods.SpawnBubble(jumpThoughtBubble, thoughtBubblePoint);
			jumpBubbleSpawned = true;
		}
		//see if the player has jumped enough
		if (!PlayerHasJumpedEnough()){
			return;
		}
		//when they have jumped enough, end the tutorial
		TutorialSharedMethods.DestroyBubble(jumpThoughtBubble);
		jumpTutorialFinished = true;
	}

	//maybe jumping enough is either two jumps or one jump and a time delay?
	bool PlayerHasJumpedEnough(){
		return jumpScript.GetJumpCount() > 0;	//for now just a jump count
	}

    /*//------------------------------------------------------------------------------
    // Methods for the INTERACT tutorial
    //------------------------------------------------------------------------------

    void ManageInteractTutorial()
    {
        //wait for interact tutorial delay
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
        return true;
    }

    //------------------------------------------------------------------------------
    // Methods for the ZOOM tutorial
    //------------------------------------------------------------------------------

    void ManageZoomTutorial()
    {
        //wait for zoom tutorial delay
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
        return true;
    }*/

    //------------------------------------------------------------------------------
    // Methods for all phases
    //------------------------------------------------------------------------------

    void RunTutorials(){
		if(!isRunning || isFinished){
			return;
		}
		//wait for the tutorial delay?
		//--
		if (!moveTutorialFinished){
			ManageMoveTutorial();
			return;
		}
		/*if(!pushTutorialFinished){
			ManagePushTutorial();
			return;
		}*/
		if(!jumpTutorialFinished){
			ManageJumpTutorial();
			return;
		}
		isFinished = true;
	}
}
