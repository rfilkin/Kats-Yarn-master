using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

	[SerializeField] Vector2 scrollRate;

	RectTransform rt;
	RectTransform titleCanvas;

	BoxCollider2D col;
	BoxCollider2D parentCol;

	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform>();
		if (rt != null)
			titleCanvas = (RectTransform)rt.parent;
		else{
			col = GetComponent<BoxCollider2D>();
			parentCol = GetComponentInParent<BoxCollider2D>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		Scroll();
		Wrap();
	}

	void Scroll(){
		if (rt != null)
			rt.Translate(scrollRate.x * Time.deltaTime, scrollRate.y * Time.deltaTime, 0);
		else
			transform.Translate(scrollRate.x * Time.deltaTime, scrollRate.y * Time.deltaTime, 0);
		
	}

	void Wrap(){
		if (rt != null){
			if (rt.rect.xMax + rt.localPosition.x < titleCanvas.rect.xMin){
				rt.localPosition = rt.localPosition + Vector3.right * rt.rect.width * 2;
				/*
			if (Mathf.Abs(scrollRate.x) > Mathf.Abs(scrollRate.y)){
				rt.position = rt.position + Vector3.right * rt.rect.width * 2;
			} else{
				rt.position = rt.position + Vector3.up * rt.rect.height * 2;
			}
			*/
			}
		} else{
			if(col.bounds.max.x + transform.localPosition.x < parentCol.bounds.min.x){
				transform.localPosition += Vector3.right * col.bounds.max.x * 2;
			}
		}

	}
}
