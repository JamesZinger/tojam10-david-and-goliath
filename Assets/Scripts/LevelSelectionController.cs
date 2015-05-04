using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class LevelSelectionController : MonoBehaviour
{
	public GameObject[] levels;
	public AudioSource onClick;
	private int lvlSel = 0;

	// Use this for initialization
	void OnEnable ()
	{
		lvlSel = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if ( Xbox360GamepadState.Instance.AxisJustPastThreshold( Xbox.Axis.LAnalogX, 0.5f ) || Input.GetKeyDown( KeyCode.D ) )
		{
			lvlSel += 1;
			if ( levels.Length == lvlSel )
			{
				lvlSel = 0;
			}
			EventSystem.current.SetSelectedGameObject( levels[ lvlSel ].gameObject, new BaseEventData( EventSystem.current ) );
		}
		else if ( Xbox360GamepadState.Instance.AxisJustPastThreshold( Xbox.Axis.LAnalogX, -0.5f ) || Input.GetKeyDown( KeyCode.A ) )
		{
			lvlSel -= 1;
			if ( lvlSel < 0 )
			{
				lvlSel = levels.Length - 1;
			}
			EventSystem.current.SetSelectedGameObject( levels[ lvlSel ].gameObject, new BaseEventData( EventSystem.current ) );
		}
		if ( ( lvlSel == 0 && Xbox360GamepadState.Instance.IsButtonDown( Xbox.Button.A ) ) || ( lvlSel == 0 && Input.GetKeyDown( KeyCode.KeypadEnter ) ) )
		{
			onClick.Play();
			MainMenu.numLvl = 1;
			Cube.LevelString = "Level 1";
			Application.LoadLevel( 1 );
		}
		if ( ( lvlSel == 1 && Xbox360GamepadState.Instance.IsButtonDown( Xbox.Button.A ) ) || ( lvlSel == 1 && Input.GetKeyDown( KeyCode.KeypadEnter ) ) )
		{
			onClick.Play();
			MainMenu.numLvl = 2;
			Cube.LevelString = "Level 2";
			Application.LoadLevel( 1 );
		}
		if ( ( lvlSel == 2 && Xbox360GamepadState.Instance.IsButtonDown( Xbox.Button.A ) ) || ( lvlSel == 2 && Input.GetKeyDown( KeyCode.KeypadEnter ) ) )
		{
			onClick.Play();
			MainMenu.numLvl = 3;
			Cube.LevelString = "Level 3";
			Application.LoadLevel( 1 );
		}
		if ( ( lvlSel == 3 && Xbox360GamepadState.Instance.IsButtonDown( Xbox.Button.A ) ) || ( lvlSel == 3 && Input.GetKeyDown( KeyCode.KeypadEnter ) ) )
		{
			onClick.Play();
			MainMenu.numLvl = 4;
			Cube.LevelString = "Level 4";
			Application.LoadLevel( 1 );
		}
		if ( ( lvlSel == 4 && Xbox360GamepadState.Instance.IsButtonDown( Xbox.Button.A ) ) || ( lvlSel == 4 && Input.GetKeyDown( KeyCode.KeypadEnter ) ) )
		{
			onClick.Play();
			MainMenu.numLvl = 5;
			Cube.LevelString = "Level 5";
			Application.LoadLevel( 1 );
		}
		if ( ( lvlSel == 5 && Xbox360GamepadState.Instance.IsButtonDown( Xbox.Button.A ) ) || ( lvlSel == 5 && Input.GetKeyDown( KeyCode.KeypadEnter ) ) )
		{
			onClick.Play();
			MainMenu.numLvl = 6;
			Cube.LevelString = "Level 6";
			Application.LoadLevel( 1 );
		}
		if ( ( lvlSel == 6 && Xbox360GamepadState.Instance.IsButtonDown( Xbox.Button.A ) ) || ( lvlSel == 6 && Input.GetKeyDown( KeyCode.KeypadEnter ) ) )
		{
			onClick.Play();
			FindObjectOfType<InGameMenuController>().TransitioningBack();
			gameObject.SetActive( false );
		}
	}
}
