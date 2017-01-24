using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Android and iOS platforms don't need quit buttons, but computers do.
//Use this simple script to make the gameObject inactive if the game is run on a mobile platform

public class UI_TurnOffOnPhone : MonoBehaviour 
{
	void Awake()
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			gameObject.SetActive(false);
		}
	}

	void OnEnabled()
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			gameObject.SetActive(false);
		}
	}
}
