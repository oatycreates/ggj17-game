using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandPhysicsResting : MonoBehaviour {
	private bool m_hasJointBroken = false;

	private Rigidbody m_rigidbody = null;
	private FixedJoint m_fixedJoint = null;

	/// <summary>
    /// Use this for initialization.
    /// </summary>
	void Start () {
		m_rigidbody = FindObjectOfType<Rigidbody>();
		if (m_rigidbody == null) {
			Debug.LogError("Couldn't find RigidBody component!");
		}
		m_fixedJoint = FindObjectOfType<FixedJoint>();
		if (m_fixedJoint == null) {
			Debug.LogError("Couldn't find FixedJoint component!");
		}
		
		m_hasJointBroken = true;
	}
	
	/// <summary>
    /// Update is called once per frame.
    /// </summary>
	void Update () {
		
	}

	/// <summary>
	/// Called when a joint attached to the same game object broke.
	/// </summary>
	/// <param name="breakForce">The force applied to the joint.</param>
	void OnJointBreak(float breakForce)
	{
		m_hasJointBroken = true;
	}

	public bool IsSandResting()
	{
		return m_rigidbody.IsSleeping();
	}

	public bool HasSandBeenTriggered()
	{
		return m_hasJointBroken;
	}
}
