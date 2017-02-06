using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Find all the sand blocks and compare against sand blocks in water.
/// This script goes on the Water Trigger Zone
/// Most of this is modified from Patrick's Game_FindRestingSand script.
/// </summary>
public class Game_SandInWater : MonoBehaviour 
{
	/// <summary>
    /// Minimum castle destruction percentage before the game will start ending.
    /// </summary>
	public float gameEndMinDestroyPercent = 0.95f;

	/// <summary>
    /// Time to wait before ending the game.
    /// </summary>
	public float gameEndWaitTime = 3.0f;

	private GameObject[] sand;
	public List<GameObject> submergedSand = new List<GameObject>();

	private ShowPanels m_menuShowPanels = null;
	private Coroutine m_gameEndTimer = null;

	private bool m_isGameOver = false;
	private GameObject waterObject;

	void Start()
	{
		waterObject = GameObject.FindGameObjectWithTag("Water");

		m_isGameOver = false;
		m_menuShowPanels = FindObjectOfType<ShowPanels>();

		if (m_menuShowPanels == null)
		{
			Debug.LogWarning("Could not find ShowPanels instance in the scene! This may be because you directly started the scene instead of using the MainMenu.");
		}

		//sand = GameObject.FindObjectsOfType<SandPhysicsResting>();
		sand = GameObject.FindGameObjectsWithTag("Sand") as GameObject[];

		if (sand.Length == 0) {
			Debug.LogError("Couldn't find any Sand objects in the scene!");
		}

		if (gameObject.activeInHierarchy)
		{
			StartCoroutine(CheckSandStatus());
		}

	}

	void Update()
	{
		if (gameObject.activeInHierarchy)
		{
			if (!m_isGameOver)
			{
				if (GetCastleSubmergedPercent() >= gameEndMinDestroyPercent)
				{
					// Castle has been destroyed, start the timer to end the game
					m_gameEndTimer = StartCoroutine(StartGameEnd());
				}
				else if (m_gameEndTimer != null)
				{
					// Cancel game end timer
					StopCoroutine(m_gameEndTimer);
				}
			}
		}
		else
		{
			//No need to use this if the gameObject isn't active
			StopCoroutine(CheckSandStatus());
		}
	}

	IEnumerator CheckSandStatus()
	{
		if (gameObject.activeInHierarchy)
		{
			// Check with an interval to reduce the impact of lag.
			yield return new WaitForSeconds(1.5f);

			GetSubmergedSandCount();
		
			// Schedule the next sand check

			StartCoroutine(CheckSandStatus());
		}
	}

	IEnumerator StartGameEnd()
	{
		// Wait for some time to confirm that the castle is destroyed and stable.
		yield return new WaitForSeconds(gameEndWaitTime);

		if (!m_isGameOver)
		{
			Debug.Log("Game Over!");

			m_isGameOver = true;
			if (m_menuShowPanels)
			{
				m_menuShowPanels.ShowGameEndPanel();
			}
		}
	}

	private int GetSubmergedSandCount()
	{
		submergedSand.Clear();

		//Get the water object's highest point
		float waterLevel = GetWaterLevel(waterObject);

		//Add sand to the list ONLY if it's active in hierarchy.
		for (int i = 0; i < sand.Length; i++)
		{
			//If the instance is below the Water Object's Highest Point, then add to list
			if (sand[i].gameObject.transform.position.y <= waterLevel)
			{
				submergedSand.Add( sand[i] );
			}
		}

		print ("There are now " + submergedSand.Count + " submerged sand pieces. The castle submerged percent is: " + (GetCastleSubmergedPercent() * 100.0f));
		return submergedSand.Count;
	}

	public float GetCastleSubmergedPercent()
	{
		return (float)submergedSand.Count / sand.Length;
	}

	private float GetWaterLevel( GameObject water )
	{
		return (float) water.transform.position.y + ((water.GetComponent<Collider>().bounds.size.y)/2);
	}
}
