using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour {

	[SerializeField]
	Transform planet;
	
	[SerializeField]
	float magnitude;
	
	private Rigidbody2D rb;
	
	
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		rb.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Verticle")));
		Vector2 difference = planet.transform.position - this.transform.position;
		rb.AddForce((difference).normalized * magnitude);
		float angle = Mathf.Atan2(difference.x,difference.y);
		this.transform.rotation = Quaternion.AngleAxis(angle, transform.up);
		
	}
	
	//Credits to UnityHour
	//https://www.youtube.com/watch?v=3T2bB9LSB44
}
