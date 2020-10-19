using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class mirrorEndLogic : MonoBehaviour {

	public starLogic star1;
	public starLogic star2;
	public starLogic star3;
	public starLogic star4;
	public starLogic star5;
    public mirrorLogic mirror1;
    bool triggered = false;
    public Sprite openDoor;
    public Sprite closedDoor;
    public GameObject Success;
    bool prev_state = false;

	bool runningEvent = false;
	public GameObject ren;

	public UnityEvent onComplete;

    void OnTriggerEnter2D(Collider2D col)
    {
		if (triggered && col.tag == "Player" && !runningEvent)
			Complete();
    }

	void OnTriggerStay2D(Collider2D col){
		if (triggered && col.tag == "Player" && !runningEvent)
			Complete();
	}

	void Complete(){
		runningEvent = true;
		onComplete.Invoke();
	}

	void NextScene(){
		Statics.TryLoadNextScene();
	}

    // Use this for initialization
    void Start () {
        ren.GetComponent<SpriteRenderer>().sprite = closedDoor;
    }
	
	// Update is called once per frame
	void Update () {
        if (star1.active
            && star2.active
            && star3.active
            && star4.active
            && star5.active)
        {
            ren.GetComponent<SpriteRenderer>().sprite = openDoor; //open the door
            print("opened door");
            triggered = true;
            if (prev_state == false && mirror1.playerOnMirror)
            {
                Success.GetComponent<AudioSource>().Play();
                prev_state = true;
            }
        }
        else
        {
            ren.GetComponent<SpriteRenderer>().sprite = closedDoor; //close the door
            print("closed door");
            triggered = false;
            if (prev_state == true)
            {
                prev_state = false;
            }
        }
    }
}
