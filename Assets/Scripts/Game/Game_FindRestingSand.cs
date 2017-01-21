using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Find all the disturbed and resting sand blocks in the scene.
/// </summary>
public class Game_FindRestingSand : MonoBehaviour 
{
	private SandPhysicsResting[] sand;
	public List<SandPhysicsResting> restingSand = new List<SandPhysicsResting>();

	void Awake()
	{
		sand = GameObject.FindObjectsOfType<SandPhysicsResting>();

		print ("There are " + sand.Length + " block of sand in the scene.");
	}

	void Start()
	{
		StartCoroutine(CheckSandStatus());
	}

	IEnumerator CheckSandStatus()
	{
		// Check with an interval to reduce the impact of lag.
		yield return new WaitForSeconds(1.5f);

		GetrestingSandCount();
	
		// Schedule the next sand check
		StartCoroutine(CheckSandStatus());
	}

	private int GetrestingSandCount()
	{
		restingSand.Clear();

		//Add sand to the list ONLY if it's active in hierarchy.
		for (int i = 0; i < sand.Length; i++)
		{
			if (sand[i].HasSandBeenTriggered() && sand[i].IsSandResting())
			{
				restingSand.Add( sand[i] );
			}
		}

		print ("There are now " + restingSand.Count + " resting disturbed sand pieces. The castle destroy percent is: " + GetCastleDestroyPercent());
		return restingSand.Count;
	}

	public float GetCastleDestroyPercent()
	{
		return restingSand.Count / sand.Length;
	}
}
