using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterTracker : MonoBehaviour {

	[SerializeField] Transform centerPoint = null;
	[SerializeField] Collider2D centerCollider = null;

	Vector2 center;

	void Start () {
		if (centerCollider != null)
			center = centerCollider.bounds.center;
		else if (centerPoint != null)
			center = centerPoint.position;
		else
			throw new System.NullReferenceException("center not set");
	}

	public Vector2 GetGlobalCenter(){
		return center;
	}

	public Quaternion RotationAroundCenter(){
		Transform centerTransform;
		if (centerPoint != null)
			centerTransform = centerPoint;
		else
			centerTransform = centerCollider.transform;
		Quaternion objectGlobalRotation = Quaternion.LookRotation((Vector2)transform.position - center, Vector3.forward);
		return Quaternion.Inverse(objectGlobalRotation) * centerTransform.rotation;
	}

	public Quaternion RotationAroundCenter(Transform obj){
		Transform centerTransform;
		if (centerPoint != null)
			centerTransform = centerPoint;
		else
			centerTransform = centerCollider.transform;
		Quaternion objectGlobalRotation = Quaternion.LookRotation((Vector2)obj.position - center, Vector3.forward);
		return Quaternion.Inverse(objectGlobalRotation) * centerTransform.rotation;
	}

	public static Quaternion RotationAroundObject(Transform obj, Transform center){
		return Quaternion.LookRotation((Vector2)obj.position - (Vector2)center.position, -Vector3.forward);
	}

	public float DistanceToCenter(){
		if (centerPoint != null)
			return TransformDistanceToCenter();
		else
			return ColliderDistanceToCenter();
	}

	public float TransformDistanceToCenter(){
		return Vector2.Distance(transform.position, center);
	}

	public float ColliderDistanceToCenter(){
		Collider2D col = GetComponent<Collider2D>();
		if (col == null)
			throw new MissingComponentException("Object has no collider to check");
		float distance = Vector2.Distance(col.bounds.center, center);
		print(distance);
		return distance;
	}

	public GameObject GetCenterObject(){
		if (centerCollider != null)
			return centerCollider.gameObject;
		else
			return centerPoint.gameObject;
	}
}
