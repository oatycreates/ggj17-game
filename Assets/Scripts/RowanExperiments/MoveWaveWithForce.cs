using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Move Rigidbody while key is held down
/// </summary>
public class MoveWaveWithForce : MonoBehaviour 
{
	private Rigidbody m_rigidbody;

	public float moveForce = 1000.0f;

	void Awake()
	{
		m_rigidbody = gameObject.GetComponent<Rigidbody>() as Rigidbody;
		if (m_rigidbody == null) {
			Debug.LogError("Couldn't find RigidBody component!");
		}
	}

	void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			m_rigidbody.AddForce( Vector3.right * moveForce, ForceMode.Acceleration );
		}
		else
		{
			m_rigidbody.AddForce( Vector3.right * -(moveForce/10), ForceMode.Acceleration);
		}
	}
}
