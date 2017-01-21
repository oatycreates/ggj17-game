using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

/// <summary>
/// Measures the Number of Active Sand in the Scene Against the amount of 'Destroyed' Sand in the Scene
/// </summary>

public class Game_ScoreSand : MonoBehaviour 
{
	private string percentage;

	private Game_FindRestingSand activeSandCountScript = null;

	public Text scoreText;
	public UI_Scale uiScale;

	//Use these floats to detect changes in percentage
	private float currentPercentage = 0;
	private float lastPercentage = 0;
	//If a high enough percentage is triggered, then play the special effect.
	private float effectThreshold = 5;

	void Start()
	{
		activeSandCountScript = FindObjectOfType<Game_FindRestingSand>();
	}

	void Update()
	{
		//Check the last percentage to see if there are any changes
		currentPercentage = ToPercent(activeSandCountScript.GetCastleDestroyPercent());
		float difference = (Mathf.Abs(lastPercentage - currentPercentage));

		if (difference > effectThreshold)
		{
			//Trigger Effect
			TriggerUIEffect();
		}

		//Calculate Percentage for Text string
		percentage = (ToPercent( activeSandCountScript.GetCastleDestroyPercent() ).ToString() + "%");

		if (scoreText != null)
		{
			scoreText.text = percentage;
		}

		//Set the last percentage
		lastPercentage = currentPercentage;
	}

	float ToPercent(float a_rawRatio)
	{
		float percent = 0;

		percent = ( a_rawRatio ) * 100;

		percent = Mathf.RoundToInt(percent);

		return percent;
	}

	void TriggerUIEffect()
	{
		if (uiScale != null)
		{
			uiScale.EffectTime(0.5f, true);
		}
	}

	/*
	void OnGUI()
	{
		GUI.TextField (new Rect( 35, 30, 50, 20), percentage);
	}*/

	/*
	public static void SandDestroyed()
	{
		//GetComponent<Game_ScoreSand>().ReduceSand();	
	}

	public void ReduceSand()
	{
		activeSand -= 1;
		return;
	}*/
}
