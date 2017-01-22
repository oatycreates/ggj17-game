using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


//Allows user to exit game/ reset game on PC/Mac
public class Game_Shortcuts : MonoBehaviour 
{
	private bool m_isWaitingForReset = false;
	private float m_gameResetTimer = -1.0f;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Minus))
		{
			Debug.Log( "Quit Game");
			Application.Quit();
		}

		if (Input.GetKeyDown(KeyCode.Backspace))
		{
			ResetScene();
		}

		// Allow the user to reset the scene by pressing two fingers down for 5 seconds
		if (m_isWaitingForReset) {
			if (Input.touchCount == 2) {
				m_gameResetTimer -= Time.deltaTime;
				if (m_gameResetTimer <= 0) {
					// Reset the game
					m_isWaitingForReset = false;
					ResetScene();
				}
			} else {
				// Touch stopped, cancel the wait
				m_isWaitingForReset = false;
				m_gameResetTimer = 0.0f;
			}
		} else if (Input.touchCount == 2) {
			m_isWaitingForReset = true;
			m_gameResetTimer = 5.0f;
		}
	}

	private void ResetScene()
	{
		Debug.Log ("Reset Scene");
		SceneManager.LoadScene ( SceneManager.GetActiveScene().name );
	}
}
