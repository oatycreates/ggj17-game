using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

/// <summary>
/// Plays a Noise on Pointer Over and Pointer Down
/// </summary>

public class UI_ButtonNoise : MonoBehaviour , IPointerEnterHandler , IPointerDownHandler 
{
	public AudioSource sfxEnter;
	public AudioSource sfxDown;

	public void OnPointerEnter( PointerEventData ped )
	{
		//sfxEnter.Play();
	}

	public void OnPointerDown( PointerEventData ped )
	{
		sfxDown.Play();
	}
}
