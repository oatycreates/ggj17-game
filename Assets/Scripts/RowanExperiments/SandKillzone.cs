using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Destroys the Sand as it enters the trigger volume. Make that sand Pay!
/// </summary>
public class SandKillzone : MonoBehaviour 
{
	public Game_ScoreSand scoreScript;

	void OnTriggerEnter(Collider a_other)
	{
		if (a_other.gameObject.tag == "Sand")
		{
			//scoreScript.ReduceSand();
			a_other.gameObject.SetActive(false);
		}
	}
}
