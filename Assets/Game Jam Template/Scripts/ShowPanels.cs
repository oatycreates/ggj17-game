using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Marlin Flip game- modifications made by Rowan Donaldson, 22/01/2017
/// </summary>

public class ShowPanels : MonoBehaviour {

	public GameObject optionsPanel;							//Store a reference to the Game Object OptionsPanel 
	public GameObject darkTint;								//Store a reference to the Game Object DarkTint 
	public GameObject creditsPanel;							//Store a reference to the Game Object CreditsPanel 
	public GameObject menuPanel;							//Store a reference to the Game Object MenuPanel 
	public GameObject gameMenuPanel;						//Store a reference to the Game Object GameMenuPanel 
	public GameObject splashPanel;							//Store a reference to the Game Object SplashPanel 
	public GameObject pausePanel;							//Store a reference to the Game Object PausePanel 
	public GameObject gameEndPanel;							//Store a reference to the Game Object GameEndPanel 

	public GameObject infoPanel;


	//Call this function to activate and display the Options panel during the main menu
	public void ShowOptionsPanel()
	{
		optionsPanel.SetActive(true);
		darkTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Options panel during the main menu
	public void HideOptionsPanel()
	{
		optionsPanel.SetActive(false);
		darkTint.SetActive(false);
	}

	//Call this function to activate and display the credits panel during the main menu
	public void ShowCreditsPanel()
	{
		creditsPanel.SetActive(true);
		darkTint.SetActive(true);
	}

	//Call this function to deactivate and hide the credits panel during the main menu
	public void HideCreditsPanel()
	{
		creditsPanel.SetActive(false);
		darkTint.SetActive(false);
	}

	//Call this function to activate and display the splash panel during the splash screen
	public void ShowSplash()
	{
		splashPanel.SetActive (true);
	}

	//Call this function to deactivate and hide the splash panel during the splash screen
	public void HideSplash()
	{
		splashPanel.SetActive (false);
	}

	//Call this function to activate and display the main menu panel during the main menu
	public void ShowMenu()
	{
		menuPanel.SetActive (true);
	}

	//Call this function to deactivate and hide the main menu panel during the main menu
	public void HideMenu()
	{
		menuPanel.SetActive (false);
	}

	//Call this function to activate and display the game menu panel during the game
	public void ShowGameMenu()
	{
		gameMenuPanel.SetActive (true);
	}

	//Call this function to deactivate and hide the game menu panel during the game
	public void HideGameMenu()
	{
		gameMenuPanel.SetActive (false);
	}
	
	//Call this function to activate and display the Pause panel during game play
	public void ShowPausePanel()
	{
		pausePanel.SetActive (true);
		darkTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Pause panel during game play
	public void HidePausePanel()
	{
		pausePanel.SetActive (false);
		darkTint.SetActive(false);
	}
	
	//Call this function to activate and display the GameEnd panel during game play
	public void ShowGameEndPanel()
	{
		gameEndPanel.SetActive (true);
		darkTint.SetActive(true);
	}

	//Call this function to deactivate and hide the GameEnd panel during game play
	public void HideGameEndPanel()
	{
		gameEndPanel.SetActive (false);
		darkTint.SetActive(false);
	}


	//Rowan Custom Panel
	public void ShowInfoPanel()
	{
		infoPanel.SetActive(true);
		darkTint.SetActive(true);
	}

	public void HideInfoPanel()
	{
		infoPanel.SetActive(false);
		darkTint.SetActive(false);
	}

	public void RestartGame()
	{
		//Was having a lot of trouble here - couldn't figure out why game wouldn't reset properly
		//Turns Out, I forgot to resume game time with Time.timeScale
		//Do this in the Inspector, with the Pause Script and this button

		Debug.Log ("Restart Game");
		HideGameEndPanel();
		HideGameMenu();
		HidePausePanel();
		//gameMenuPanel.SetActive (false);


		// If we just restart the game, then this gameObject ("UI") will be loaded again, 
		//making duplicates of a 'Don't Destroy On Load' object

		//To avoid this, we'll destroy THIS instance of the "UI" gameObject
		DestroyMyself();

		SceneManager.LoadScene ( "MainMenu" );
	}

	void DestroyMyself()
	{
		//Debug.LogWarning("Destroyed One Instance of Me");
		GameObject.Destroy( gameObject );
		Debug.Log("Destroyed old instance of UI. Allow for a new one to be created in 'Main Menu' scene.");
	}
}
