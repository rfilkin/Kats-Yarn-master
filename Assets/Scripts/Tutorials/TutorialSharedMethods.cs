using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TutorialSharedMethods {

	// creates a thought bubble
	public static void SpawnBubble(GameObject bubble, Transform thoughtBubblePoint){
		bubble.SetActive(true);
		bubble.transform.position = thoughtBubblePoint.position;
		bubble.transform.rotation = thoughtBubblePoint.rotation;
		bubble.transform.SetParent(thoughtBubblePoint);
	}

	// makes the bubble grow towards its full size
	public static void GrowBubble(GameObject bubble){

	}

	//vanishes the bubble
	public static void DestroyBubble(GameObject bubble){
		bubble.SetActive(false);
	}

}
