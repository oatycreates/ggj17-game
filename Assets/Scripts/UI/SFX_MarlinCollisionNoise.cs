using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Play an SFX noise on collision with a certain object
/// </summary>

public class SFX_MarlinCollisionNoise : MonoBehaviour 
{
	//Best to assign individual Audio Sources in the editor
	public AudioSource myAudio;
	public string tagToSearchFor;

	public float minPitch = 1.0f;
	public float maxPitch = 1.0f;

	void OnCollisionEnter( Collision a_other )
	{
		if (a_other.gameObject.tag == tagToSearchFor)
		{
			if (!myAudio.isPlaying)
			{
				myAudio.pitch = RandomizePitch( minPitch, maxPitch);
				myAudio.Play();
			}
		}
	}

	void OnTriggerEnter( Collider a_other )
	{
		if (a_other.gameObject.tag == tagToSearchFor)
		{
			if (!myAudio.isPlaying)
			{
				myAudio.pitch = RandomizePitch( minPitch, maxPitch);
				myAudio.Play();
			}
		}
	}

	float RandomizePitch( float a_min, float a_max)
	{
		float rand = 0;

		rand = Random.Range( a_min, a_max);

		return rand;
	}
}
