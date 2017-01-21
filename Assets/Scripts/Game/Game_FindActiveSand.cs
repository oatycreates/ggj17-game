using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Find all the active sand blocks in the scene.
/// </summary>
public class Game_FindActiveSand : MonoBehaviour 
{
	public GameObject[] sand;
	//public List<GameObject> activeSand = new List<GameObject>();

	public static int totalSandBlocks;

	void Awake()
	{
		sand = GameObject.FindGameObjectsWithTag("Sand");

		//print ("There are " + sand.Length + " block of sand in the scene.");

		totalSandBlocks = sand.Length;
	}

	/*
	void Start()
	{
		//Add sand to the list ONLY if it's active in heirarchy.
		for (int i = 0; i < sand.Length; i++)
		{
			if (!sand[i].activeInHierarchy)
			{
				return;	
			}
			else
			{
				activeSand.Add( sand[i] );
			}
		}

		print ("However, only " + activeSand.Count + " are Active In Heirarchy.");

		totalSandBlocks = activeSand.Count;
	}*/

}
