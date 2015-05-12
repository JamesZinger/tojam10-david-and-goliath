using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UILevelSelect : MonoBehaviour
{
	public Button[] LevelButtons;
	public AudioSource onClick;
	public AudioSource OnBack;


	void Update()
	{
		// TODO Go "Back" when any CANCEL button is pushed.
	}


	public void Back()
	{
		UIPause pauseMenu = FindObjectOfType<UIPause>();
		if ( pauseMenu )
		{
			pauseMenu.TransitioningBack();
		}
		OnBack.Play();
		gameObject.SetActive( false );
	}


	public void LoadLevel( int levelNumber )
	{
		onClick.Play();
		Game.LoadLevel( levelNumber );
	}
}
