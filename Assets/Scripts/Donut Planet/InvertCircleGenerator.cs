using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(EdgeCollider2D))]
public class InvertCircleGenerator : MonoBehaviour {

	public CircleCollider2D referenceCircle;
	public EdgeCollider2D target;

	public float edgesPerArcLength;

	// Use this for initialization
	void Start () {
		float radius = referenceCircle.radius;
		int totalEdges = (int)(radius * edgesPerArcLength);

		Vector2[] points = new Vector2[totalEdges];

		for (int i = 0; i < totalEdges; i++)
		{
			float angle = 2 * Mathf.PI * i / totalEdges;
			float x = radius * Mathf.Cos(angle);
			float y = radius * Mathf.Sin(angle);

			points[i] = new Vector2(x, y);
		}
		target.points = points;

		referenceCircle.enabled = false;
	}
}
