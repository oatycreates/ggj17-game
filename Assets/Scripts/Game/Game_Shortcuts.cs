using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


//Allows user to exit game/ reset game on PC/Mac
public class Game_Shortcuts : MonoBehaviour 
{
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Debug.Log( "Quit Game");
			Application.Quit();
		}

		if (Input.GetKeyDown(KeyCode.Backspace))
		{
			Debug.Log ("Reset Scene");
			SceneManager.LoadScene ( SceneManager.GetActiveScene().name );
		}
	}
}
