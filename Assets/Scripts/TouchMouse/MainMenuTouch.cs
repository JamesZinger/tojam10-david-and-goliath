using UnityEngine;
using System.Collections;

public class MainMenuTouch : MonoBehaviour {
	
	private GameUIController UIcontrols;
	public AudioSource onClick;
	public AudioSource onBack;
	
	// Use this for initialization
	void Start () {
		UIcontrols = FindObjectOfType<GameUIController>();
	}

	// **** MAIN MENU SCREEN ****
	public void Begin()
	{
		onClick.Play ();
		MainMenu.numLvl = 1;
		Cube.LevelString = "Level 1";
		Application.LoadLevel (1);
		UIcontrols.menuOP.gameObject.SetActive (false);
	}

	public void LevelSelectionScreen()
	{
		onClick.Play ();
		UIcontrols.menuOP.gameObject.SetActive (false);
		UIcontrols.levelSelect.gameObject.SetActive(true);
	}

	public void CreditsScreen()
	{
		onClick.Play ();
		UIcontrols.menuOP.gameObject.SetActive (false);
		UIcontrols.credits.gameObject.SetActive(true);
	}

	public void Exit()
	{
		onClick.Play ();
		Application.Quit();
	}

	// **** LEVEL SELECT SCREEN ****
	public void Level1()
	{
		onClick.Play ();
		MainMenu.numLvl = 1;
		Cube.LevelString = "Level 1";
		Application.LoadLevel (1);
	}
	public void Level2()
	{
		onClick.Play ();
		MainMenu.numLvl = 2;
		Cube.LevelString = "Level 2";
		Application.LoadLevel (1);
	}
	public void Level3()
	{
		onClick.Play ();
		MainMenu.numLvl = 3;
		Cube.LevelString = "Level 3";
		Application.LoadLevel (1);
	}
	public void Level4()
	{
		onClick.Play ();
		MainMenu.numLvl = 4;
		Cube.LevelString = "Level 4";
		Application.LoadLevel (1);
	}
	public void Level5()
	{
		onClick.Play ();
		MainMenu.numLvl = 5;
		Cube.LevelString = "Level 5";
		Application.LoadLevel (1);
	}
	public void Level6()
	{
		onClick.Play ();
		MainMenu.numLvl = 6;
		Cube.LevelString = "Level 6";
		Application.LoadLevel (1);
	}

	// **** CREDITS SCREEN ****
	public void Credits()
	{
		onClick.Play ();
		UIcontrols.credits.gameObject.SetActive (true);
		UIcontrols.menuOP.gameObject.SetActive(false);
	}

	// **** RETURNING TO MAIN MENU ****
	public void MainMenuScreen()
	{
		onBack.Play ();
		UIcontrols.menuOP.gameObject.SetActive (true);
		UIcontrols.levelSelect.gameObject.SetActive(false);
		UIcontrols.credits.gameObject.SetActive(false);
	}
}
