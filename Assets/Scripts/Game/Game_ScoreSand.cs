using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Measures the Number of Active Sand in the Scene Against the amount of 'Destroyed' Sand in the Scene
/// </summary>

public class Game_ScoreSand : MonoBehaviour 
{
	public int activeSand;
	public int startingSand;

	private string percentage;

	void Start()
	{
		activeSand = Game_FindActiveSand.totalSandBlocks;

		startingSand = activeSand;
	}

	void Update()
	{
		percentage = (ToPercent( activeSand, startingSand).ToString() + "%");
	}

	float ToPercent(float current, float total)
	{
		float percent = 0;

		percent = ( current / total ) * 100;

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
