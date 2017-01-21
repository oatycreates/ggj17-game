using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandPhysicsResting : MonoBehaviour {
	/// <summary>
    /// Maximum velocity value for the sand to be considered as 'resting'.
    /// </summary>
	public float restVelocityThreshold = 0.1f;

	/// <summary>
    /// Minimum distance the sand must move to be considered 'triggered'.
    /// </summary>
	public float minTriggerMoveDist = 0.1f;

	private Vector3 m_sandStartPos = Vector3.zero;

	private Rigidbody m_rigidbody = null;

	/// <summary>
    /// Use this for initialization.
    /// </summary>
	void Start () {
		m_rigidbody = GetComponent<Rigidbody>();
		if (m_rigidbody == null) {
			Debug.LogError("Couldn't find RigidBody component!");
		}
		
		m_sandStartPos = m_rigidbody.position;
	}
	
	/// <summary>
    /// Update is called once per frame.
    /// </summary>
	void Update () {
		
	}

	public bool IsSandResting()
	{
		return m_rigidbody.velocity.magnitude <= restVelocityThreshold;
	}

	public bool HasSandBeenTriggered()
	{
		return Vector3.Distance(m_sandStartPos, m_rigidbody.position) >= minTriggerMoveDist;
	}
}
