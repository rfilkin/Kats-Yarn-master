using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrorLogic : MonoBehaviour {

    static KeyCode interactButton = PlayerControlMap.interact;

	[SerializeField] Transform leftBulbPosition;
	[SerializeField] Transform middleBulbPosition;
	[SerializeField] Transform rightBulbPosition;

	[SerializeField] barrierLogic leftTarget;
	[SerializeField] starLogic middleTarget;
	[SerializeField] barrierLogic rightTarget;

	[SerializeField] Transform attachedLight;

    public int mirrorState; //0 is left, 1 is right, 2 is neutral
    float rightOffset;
    public bool playerOnMirror = false;
    //float timer = 0f;

    // Use this for initialization
    void Start () {
		setPosition();
	}

    // Update is called once per frame
    void Update()
    {
        if (playerOnMirror && Input.GetKeyDown(interactButton))
        {
            mirrorState = (mirrorState + 1) % 3;
            setPosition();
            GetComponent<AudioSource>().Play();

            //when button is pressed, cycle through 3 positions for the mirror: left (0), right (1), and neutral(2)(towards objective).
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        //print("called");
        if (col.gameObject.tag == "Player")
        {
            playerOnMirror = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerOnMirror = false;
        }
    }

    void setPosition()
    {
		switch (mirrorState){
			case 0:	//left
				adjustLight(leftBulbPosition.position, leftTarget.lightTarget.position);
				break;
			case 1:	//right
				adjustLight(rightBulbPosition.position, rightTarget.lightTarget.position);
				break;
			case 2:	//middle
				adjustLight(middleBulbPosition.position, middleTarget.lightTarget.position);
				break;
			default:
				break;
		}
    }

	void adjustLight(Vector3 lightSourcePosition, Vector3 lightTarget){
		//attachedLight

		float zDisplacement = attachedLight.position.z;

		lightSourcePosition = Vector3.ProjectOnPlane(lightSourcePosition, Vector3.forward);
		lightTarget = Vector3.ProjectOnPlane(lightTarget, Vector3.forward);

		Vector3 lookDirection = lightTarget - lightSourcePosition;

		//this will only work if the light's parent is oriented such that its 'up' is along the global z-axis
		//and its 'forward' is coplanar with the spotlight's own 'forward'
		attachedLight.rotation = Quaternion.LookRotation(lookDirection, Vector3.forward);
		attachedLight.position = new Vector3(lightSourcePosition.x, lightSourcePosition.y, zDisplacement);
	}

}
