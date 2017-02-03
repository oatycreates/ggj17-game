using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Find all the disturbed and resting sand blocks in the scene.
/// </summary>
public class Game_FindRestingSand : MonoBehaviour 
{
	/// <summary>
    /// Minimum castle destruction percentage before the game will start ending.
    /// </summary>
	public float gameEndMinDestroyPercent = 0.95f;

	/// <summary>
    /// Time to wait before ending the game.
    /// </summary>
	public float gameEndWaitTime = 3.0f;

	private SandPhysicsResting[] sand;
	public List<SandPhysicsResting> restingSand = new List<SandPhysicsResting>();

	private ShowPanels m_menuShowPanels = null;
	private Coroutine m_gameEndTimer = null;

	private bool m_isGameOver = false;

	void Start()
	{
		m_isGameOver = false;
		m_menuShowPanels = FindObjectOfType<ShowPanels>();
		if (m_menuShowPanels == null)
		{
			Debug.LogWarning("Could not find ShowPanels instance in the scene! This may be because you directly started the scene instead of using the MainMenu.");
		}

		sand = GameObject.FindObjectsOfType<SandPhysicsResting>();
		Debug.Log("There are " + sand.Length + " block of sand in the scene.");

		if (sand.Length == 0) {
			Debug.LogError("Couldn't find any Sand objects in the scene!");
		}

			StartCoroutine(CheckSandStatus());

	}

	void Update()
	{
		if (!m_isGameOver)
		{
			if (GetCastleDestroyPercent() >= gameEndMinDestroyPercent)
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

	IEnumerator CheckSandStatus()
	{
		// Check with an interval to reduce the impact of lag.
		yield return new WaitForSeconds(1.5f);

		GetrestingSandCount();
	
		// Schedule the next sand check
		StartCoroutine(CheckSandStatus());
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

		print ("There are now " + restingSand.Count + " resting disturbed sand pieces. The castle destroy percent is: " + (GetCastleDestroyPercent() * 100.0f));
		return restingSand.Count;
	}

	public float GetCastleDestroyPercent()
	{
		return (float)restingSand.Count / sand.Length;
	}
}
