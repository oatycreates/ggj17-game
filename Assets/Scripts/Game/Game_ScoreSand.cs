using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Measures the Number of Active Sand in the Scene Against the amount of 'Destroyed' Sand in the Scene
/// </summary>

public class Game_ScoreSand : MonoBehaviour 
{
	private string percentage;

	private Game_FindRestingSand activeSandCountScript = null;

	void Start()
	{
		activeSandCountScript = FindObjectOfType<Game_FindRestingSand>();
	}

	void Update()
	{
		percentage = (ToPercent( activeSandCountScript.GetCastleDestroyPercent() ).ToString() + "%");
	}

	float ToPercent(float a_rawRatio)
	{
		float percent = 0;

		percent = ( a_rawRatio ) * 100;

		percent = Mathf.RoundToInt(percent);

		return percent;
	}

	void OnGUI()
	{
		GUI.TextField (new Rect( 20, 10, 100, 20), percentage);
	}

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
