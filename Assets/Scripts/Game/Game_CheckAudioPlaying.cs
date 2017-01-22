using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Because the Start Scene is also menu, make sure Audio Doesn't start every time this scene is called.
/// </summary>

public class Game_CheckAudioPlaying : MonoBehaviour 
{
	public AudioSource myAudio;

	void Awake()
	{
		myAudio = GameObject.FindObjectOfType<AudioSource>() as AudioSource;

		if (!myAudio.isPlaying)
		{
			myAudio.Play();
		}
	}

}
