using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CHANGE: The connected Rigidbody's Mass Increases as it's distance from the Spring's Anchor Increases (not Height per se');
/// </summary>

public class Experiment_WeightWithHeight : MonoBehaviour 
{
	private Rigidbody myRigid;
	private SpringJoint mySpring;
	private float myMass;

	private float distanceFromAnchor;
	public float weightMod = 2;

	void Awake()
	{
		myRigid = gameObject.GetComponent<Rigidbody>() as Rigidbody;
		mySpring = gameObject.GetComponent<SpringJoint>() as SpringJoint;
		myMass = myRigid.mass;
	}

	void FixedUpdate()
	{
		distanceFromAnchor = Vector3.Distance( myRigid.transform.position, mySpring.anchor);

		//print (distanceFromAnchor + " dist");

		myMass = distanceFromAnchor;
		myMass = Mathf.Clamp(myMass, 1, myMass);

		//Set the Rigidbody's custom Mass
		myRigid.mass = myMass * weightMod;

	}
}
