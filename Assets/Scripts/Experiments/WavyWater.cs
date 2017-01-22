using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class WavyWater : MonoBehaviour {
	[Range(0.0f, 2.0f)]
	public float waveSpeed = 1.0f;
	[Range(0.0f, 1.0f)]
	public float waveAmplitude = 1.0f;
	private Renderer m_renderer = null;

	/// <summary>
    /// Use this for initialization.
    /// </summary>
	void Start () {
		m_renderer = GetComponent<Renderer>();
	}
	
	/// <summary>
    /// Update is called once per frame.
    /// </summary>
	void Update () {
		float offset = Mathf.Sin(Time.time * waveSpeed) * waveAmplitude;
        m_renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
	}
}
