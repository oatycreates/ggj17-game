using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Adds a fixed joint to any rigidbody it collides with
/// </summary>
public class DynamicFixedJoints : MonoBehaviour 
{
	private Rigidbody myRigid;
	public float breakForce = 1000.0f;

	void Awake()
	{
		myRigid = gameObject.GetComponent<Rigidbody>() as Rigidbody;
	}

	void OnCollisionEnter(Collision a_other)
	{
		if (a_other.gameObject.GetComponent<Rigidbody>() != null && a_other.gameObject.GetComponent<FixedJoint>() == null)
		{
			//Has a rigidbody but no fixed joint...
			FixedJoint joint = a_other.gameObject.AddComponent<FixedJoint>() as FixedJoint;

			joint.connectedBody = myRigid;
			joint.breakForce = breakForce;

		}
	}
}
