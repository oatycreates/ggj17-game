using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Move Rigidbody while key is held down
/// </summary>
public class MoveWaveWithForce : MonoBehaviour 
{
	private Rigidbody myRigid;

	public float moveForce = 1000.0f;

	void Awake()
	{
		myRigid = gameObject.GetComponent<Rigidbody>() as Rigidbody;
	}

	void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			myRigid.AddForce( Vector3.right * moveForce, ForceMode.Acceleration );
		}
		else
		{
			myRigid.AddForce( Vector3.right * -(moveForce/10), ForceMode.Force);
		}
	}
}
