using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIWin : MonoBehaviour 
{
	public Text AttemptsText;
	public Image CommentImage1;
	public Image CommentImage2;
	public Image CommentImage3;
	public AudioSource OnBack;
	public AudioSource OnClick;
	public AudioSource OnWin;
	

	void OnEnable() 
	{
		OnWin.Play();

		AttemptsText.text = " " + ( Cube.DeathCount + 1 );

		if ( Cube.DeathCount < 3 )
		{
			CommentImage1.gameObject.SetActive( true );
			CommentImage2.gameObject.SetActive( false );
			CommentImage2.gameObject.SetActive( false );
		}
		else if ( Cube.DeathCount > 3 && Cube.DeathCount < 6 )
		{
			CommentImage1.gameObject.SetActive( false );
			CommentImage2.gameObject.SetActive( true );
			CommentImage2.gameObject.SetActive( false );
		}
		else if ( Cube.DeathCount > 6 )
		{
			CommentImage1.gameObject.SetActive( false );
			CommentImage2.gameObject.SetActive( false );
			CommentImage2.gameObject.SetActive( true );
		}
	}


	public void BackToMainMenu()
	{
		Time.timeScale = 0;
		Cube.HasFinished = false;
		Cube.DeathCount = 0;
		OnClick.Play();
		Game.ReturnToMainMenu();
	}


	public void LoadNextLevel()
	{
		Time.timeScale = 0;
		OnClick.Play();
		Cube.HasFinished = false;
		Cube.DeathCount = 0;
		Game.LoadNextLevel();
	}
}
