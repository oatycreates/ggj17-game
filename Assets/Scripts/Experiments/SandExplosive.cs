using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SandPhysicsResting))]
public class SandExplosive : MonoBehaviour {
	public float explosionForce = 50.0f;
	public float explosionRadius = 25.0f;

	private bool m_hasExploded = false;

	private SandPhysicsResting m_sandPhysicsResting = null;
	private Rigidbody m_rigidbody = null;

	/// <summary>
    /// Use this for initialization.
    /// </summary>
	void Start () {
		m_sandPhysicsResting = GetComponent<SandPhysicsResting>();
		m_rigidbody = GetComponent<Rigidbody>();
	}
	
	/// <summary>
    /// Update is called once per frame.
    /// </summary>
	void Update () {
		if ((!m_hasExploded && m_sandPhysicsResting.HasSandBeenTriggered()) || Input.GetButtonDown("Fire2"))
		{
			TriggerExplosion();
			m_hasExploded = true;
		}
	}

    private void TriggerExplosion()
    {
		Debug.Log("Kaboom!");
		Rigidbody[] sceneBodies = FindObjectsOfType<Rigidbody>();
		foreach (Rigidbody rigidbody in sceneBodies)
		{
			rigidbody.AddExplosionForce(explosionForce, m_rigidbody.position, explosionRadius, 0.0f, ForceMode.Impulse);
		}
    }
}
