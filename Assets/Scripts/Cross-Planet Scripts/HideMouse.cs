using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMouse : MonoBehaviour {

	[SerializeField] bool shouldBeHiddenThisScene = true;

	// Use this for initialization
	void Start () {
		Cursor.visible = !shouldBeHiddenThisScene;
	}
}
