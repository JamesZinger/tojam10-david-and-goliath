using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
	public GameObject CreditScreen;
	public GameObject LevelSelectionScreen;
	public AudioSource OnBack;
	public AudioSource OnClick;


	public void ExitGame()
	{
		OnBack.Play();
		Application.Quit();
	}

	public void OpenCredits()
	{
		OnClick.Play();
		CreditScreen.SetActive( true );
	}

	public void OpenLevelSelect()
	{
		OnClick.Play();
		LevelSelectionScreen.SetActive( true );
	}

	public void StartGame()
	{
		OnClick.Play();
		Game.LoadFirstLevel();
	}
}