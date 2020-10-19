using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SpinningParticles : MonoBehaviour {

	[SerializeField] float emissionRatePerSource = 10;
	[SerializeField] float speed;
	[SerializeField] starLogic[] references;

	ParticleSystem.EmissionModule part;

	// Use this for initialization
	void Start () {
		part = GetComponent<ParticleSystem>().emission;
	}
	
	// Update is called once per frame
	void Update () {
		float modifier = 0;

		foreach (starLogic sl in references)
			if (sl.active)
				++modifier;

		part.rateOverTime = new ParticleSystem.MinMaxCurve(emissionRatePerSource * modifier);

		transform.localRotation *= Quaternion.AngleAxis(speed * Time.deltaTime * modifier, Vector3.forward);
	}
}
