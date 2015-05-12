using UnityEngine;
using System.Collections;

public class UIWinDetection : MonoBehaviour 
{
	public UIWin WinScreen;

	void Update()
	{
		if ( Cube.HasFinished && !WinScreen.gameObject.activeSelf )
		{
			WinScreen.gameObject.SetActive( true );
		}
	}
}
