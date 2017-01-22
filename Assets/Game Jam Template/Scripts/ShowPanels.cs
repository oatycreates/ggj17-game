using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ShowPanels : MonoBehaviour {

	public GameObject optionsPanel;							//Store a reference to the Game Object OptionsPanel 
	public GameObject darkTint;								//Store a reference to the Game Object DarkTint 
	public GameObject creditsPanel;							//Store a reference to the Game Object CreditsPanel 
	public GameObject menuPanel;							//Store a reference to the Game Object MenuPanel 
	public GameObject gameMenuPanel;						//Store a reference to the Game Object GameMenuPanel 
	public GameObject pausePanel;							//Store a reference to the Game Object PausePanel 
	public GameObject gameEndPanel;							//Store a reference to the Game Object GameEndPanel 


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

	public void RestartGame()
	{
		Debug.Log ("Restart Game");
		HideGameEndPanel();
		gameMenuPanel.SetActive (false);
		SceneManager.LoadScene ( "MainMenu" );
	}
}
