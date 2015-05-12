using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICredits : MonoBehaviour
{
	public Button BackButton;
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
}
