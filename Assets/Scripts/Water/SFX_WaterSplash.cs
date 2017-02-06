using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays a splash SFX whenever a sand object enters the water
/// </summary>
public class SFX_WaterSplash : MonoBehaviour 
{
	public string tagToLookFor = "Sand";

	public AudioSource myAudio;

	public float minPitch = 1.0f;
	public float maxPitch = 1.0f;

	void OnTriggerEnter( Collider a_other )
	{
		if (a_other.gameObject.tag == tagToLookFor)
		{
			//if (!myAudio.isPlaying)
			{
				myAudio.pitch = RandomizePitch(minPitch, maxPitch);
				myAudio.Play();
			}
		}
	}

	float RandomizePitch(float a_min, float a_max)
	{
		float rand = 0;

		rand = Random.Range(a_min, a_max);

		return rand;
	}
	
}
