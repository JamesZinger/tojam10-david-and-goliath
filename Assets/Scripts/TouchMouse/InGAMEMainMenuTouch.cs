using UnityEngine;
using System.Collections;

public class InGAMEMainMenuTouch : MonoBehaviour {

	public GameObject LevelSelection;
	public GameObject CreditScreen;
	public GameObject WinScreen;
	public GameObject InGameMenu;
	public AudioSource onClick;

	InGameMenuController MenuBar;

	// ****** FUNCTIONS FOR INGAME SCREEN MENU *******
	// **** InGame SelectScreen ****
	public void InGameSelectScreen()
	{
		// TODO: Disable the MenuBar in the InGame Screen
		LevelSelection.SetActive( true );
	}
	
	// **** InGame Credits Screen ****
	public void InGameCredits()
	{
		// TODO: Disable the MenuBar in the InGame Screen
		CreditScreen.SetActive( true );
	}

	// **** Return to InGame Screen ****
	public void InGameScreen()
	{
		// TODO: Enable the MenuBar in the InGame Screen I believe??
		LevelSelection.SetActive (false);
		CreditScreen.SetActive (false);
	}

	// **** Win Screen ****
	public void NextLevel()
	{
		Time.timeScale = 0;
		MainMenu.numLvl++;
		onClick.Play();
		WinScreen.SetActive(false);
		Cube.HasFinished = false;
		Cube.DeathCount = 0;
		var nextLevelNum = MainMenu.numLvl + 1;
		if ( nextLevelNum == Graph.levelSelectionDictionary.Count )
		{
			Application.LoadLevel( 0 );
			return;
		}
		Cube.LevelString = "Level " + ( MainMenu.numLvl + 1 );
		Debug.Log( "Cube Level Name: " + Cube.LevelString );
		Application.LoadLevel( 1 );
	}

	// **** Return to Main Screen ****
	public void MainMainScreen()
	{
		Time.timeScale = 0;
		WinScreen.SetActive (false);
		Cube.HasFinished = false;
		Cube.DeathCount = 0;
		onClick.Play ();
		Application.LoadLevel (0);
	}
}
