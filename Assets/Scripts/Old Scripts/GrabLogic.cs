using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabLogic : MonoBehaviour {

    static KeyCode grabButton = KeyCode.Q;

    // Use this for initialization
    void Start () {
		
	}

    /*void OnTriggerMoveWithPlayer(Collider2D col)
    {
        print("called");
        if (col.gameObject.tag == "Player" && Input.GetKeyDown(grabButton))
        {
            print("success");
            //InteractEffect();
            //bind the player and grabbed object together, such that they move as one
            //disable the player's sprite flip script
            //disable the player's ability to jump

            //when the grab button is released: unbind the objects, re-enable the sprite flip script, re-enable player's jump
        }
    }*/

    Transform pullableItem;
    GameObject itemObject;
    Rigidbody itemBody;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //when the player collides with a box, and presses the grab button, store it as a pullable object
        if (hit.transform.name == "Box" && Input.GetKeyDown(grabButton))
        {
            pullableItem = hit.transform;
            itemObject = hit.gameObject;
            itemBody = hit.rigidbody;
        }
            
    }


    // Update is called once per frame
    void Update () {

        if (pullableItem != null)
        {
            Vector3 playerToItem = transform.position - pullableItem.position;
            float distance = playerToItem.magnitude;
            Vector3 pullDirection = playerToItem.normalized;
            float pullForce = 10;
            itemBody.velocity += pullDirection * (pullForce * Time.deltaTime);

        }
		
	}
}
