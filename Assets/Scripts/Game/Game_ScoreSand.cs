using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public enum GameMode {RestingSand, SubmergedSand};

/// <summary>
/// Measures the Number of Active Sand in the Scene Against the amount of 'Destroyed' Sand in the Scene
/// </summary>

public class Game_ScoreSand : MonoBehaviour 
{
	private string percentage;

	///Determine the game mode
	public GameMode gameMode = GameMode.SubmergedSand;

	/// <summary>
	/// Option 1
	/// </summary>
	private Game_FindRestingSand activeSandCountScript = null;

	/// <summary>
	/// Option 2
	/// </summary>
	private Game_SandInWater submergedSandCountScript = null;

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
		submergedSandCountScript = FindObjectOfType<Game_SandInWater>();


		//To avoid the End Game event being called in different scripts with different conditions,
		//we'll TURN OFF the script we don't need at the Start of the Game, once we know which GameMode we are using.
		if (gameMode == GameMode.RestingSand)
		{
			submergedSandCountScript.enabled = false;
		}
		else
		if (gameMode == GameMode.SubmergedSand)
		{
			activeSandCountScript.enabled = false;
		}
	}

	void Update()
	{
		//Determine Game Mode

		if (gameMode == GameMode.RestingSand)
		{
			//Check the last percentage to see if there are any changes
			currentPercentage = ToPercent(activeSandCountScript.GetCastleDestroyPercent());
		}
		else
		if (gameMode == GameMode.SubmergedSand)
		{
			//Check the last percentage to see if there are any changes
			currentPercentage = ToPercent(submergedSandCountScript.GetCastleSubmergedPercent());
		}




		float difference = (Mathf.Abs(lastPercentage - currentPercentage));

		if (difference > effectThreshold)
		{
			//Trigger Effect
			TriggerUIEffect();
		}

		if (gameMode == GameMode.RestingSand)
		{
			//Calculate Percentage for Text string
			percentage = (ToPercent( activeSandCountScript.GetCastleDestroyPercent() ).ToString() + "%");
		}
		else
		if (gameMode == GameMode.SubmergedSand)
		{
				//Calculate Percentage for Text string
			percentage = (ToPercent( submergedSandCountScript.GetCastleSubmergedPercent() ).ToString() + "%");
		}

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
}
