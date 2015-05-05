using UnityEngine.UI;
using UnityEngine.EventSystems; // This "using" method is mandatory for selecting buttons without mouse input
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public static int numLvl;

	public EventSystem es; // This is what allows for the events of selection options to take place
	private int opSel = 0; // Variable for selecting through array
	private int lvlSel = 0;
	private GameUIController UIcontrols;
	
	public AudioSource onClick;
	public AudioSource onBack;

	// Use this for initialization
	void Start () {

		UIcontrols = FindObjectOfType<GameUIController>();

		
	}
	
	// Update is called once per frame
	void Update () {
		Xbox360GamepadState.Instance.UpdateState();

		// The following "if" statement allows for selecting menue buttons through controls
		if (UIcontrols.menuOP.gameObject.activeSelf == true) 
		{
			es.SetSelectedGameObject(UIcontrols.Options[opSel].gameObject, new BaseEventData(es));
			UIcontrols.levelSelect.gameObject.SetActive(false);
			UIcontrols.credits.gameObject.SetActive(false);
			// When menu is active the following is allowed
			if (Xbox360GamepadState.Instance.AxisJustPastThreshold(Xbox.Axis.LAnalogX, 0.5f) || 
			    Input.GetKeyDown(KeyCode.D))
			{
				opSel +=1; // Increment the array by 1 as you move down the list
				// This statement allows for returning back to top option once you reach the bottom of the list
				if (UIcontrols.Options.Length == opSel)
				{
					// Sets the variable for the array back to 0 to continue moving down the list
					opSel = 0;
				}
				// This line is what allows for the selection of buttons through key input and so on
				es.SetSelectedGameObject(UIcontrols.Options[opSel].gameObject, new BaseEventData(es));
			}
			// This statement does the same as above but for upward action
			if (Xbox360GamepadState.Instance.AxisJustPastThreshold(Xbox.Axis.LAnalogX, -0.5f) || 
			    Input.GetKeyDown(KeyCode.A)) {
				opSel -=1;
				if (opSel < 0)
				{
					opSel = 3;
				}
				es.SetSelectedGameObject(UIcontrols.Options[opSel].gameObject, new BaseEventData(es));
			}
			if ((opSel == 0 && Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A)) || 
			    (opSel == 0 && Input.GetKeyDown(KeyCode.KeypadEnter)))
			{
				onClick.Play();
				numLvl = 1;
				Cube.LevelString = "Level 1";
				Application.LoadLevel (1);
				UIcontrols.menuOP.gameObject.SetActive (false);
			}
			if ((opSel == 1 && Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A)) ||
			    (opSel == 1 && Input.GetKeyDown(KeyCode.KeypadEnter)))
			{
				onClick.Play();
				UIcontrols.menuOP.gameObject.SetActive (false);
				UIcontrols.levelSelect.gameObject.SetActive(true);
				return;
			}
			if ((opSel == 2 && Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A)) ||
			    (opSel == 2 && Input.GetKeyDown(KeyCode.KeypadEnter)))
			{
				onClick.Play();
				UIcontrols.menuOP.gameObject.SetActive (false);
				UIcontrols.credits.gameObject.SetActive(true);
				return;
			}
			if ((opSel == 3 && Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A)) ||
			    (opSel == 3 && Input.GetKeyDown(KeyCode.KeypadEnter))) 
			{
				onClick.Play();
				Application.Quit();
			}
		}

		if (UIcontrols.levelSelect.gameObject.activeSelf == true) {
			es.SetSelectedGameObject(UIcontrols.levelOp[lvlSel].gameObject, new BaseEventData(es));
			if (Xbox360GamepadState.Instance.AxisJustPastThreshold(Xbox.Axis.LAnalogX, 0.5f) || Input.GetKeyDown(KeyCode.D)) {
				lvlSel += 1;
				if (UIcontrols.levelOp.Length == lvlSel)
				{
					lvlSel = 0;
				}
				es.SetSelectedGameObject(UIcontrols.levelOp[lvlSel].gameObject, new BaseEventData(es));
			}
			if (Xbox360GamepadState.Instance.AxisJustPastThreshold(Xbox.Axis.LAnalogX, -0.5f) || Input.GetKeyDown(KeyCode.A)) {
				lvlSel -= 1;
				if (lvlSel < 0)
				{
					lvlSel = 6;
				}
				es.SetSelectedGameObject(UIcontrols.levelOp[lvlSel].gameObject, new BaseEventData(es));
			}
			if ((lvlSel == 0 && Xbox360GamepadState.Instance.IsButtonDown(Xbox.Button.A)) || (lvlSel == 0 && Input.GetKeyDown(KeyCode.KeypadEnter))) {
				onClick.Play();
				numLvl = 1;
				Cube.LevelString = "Level 1";
				Application.LoadLevel (1);
			}
			if ((lvlSel == 1 && Xbox360GamepadState.Instance.IsButtonDown(Xbox.Button.A)) || (lvlSel == 1 && Input.GetKeyDown(KeyCode.KeypadEnter))) {
				onClick.Play();
				numLvl = 2;
				Cube.LevelString = "Level 2";
				Application.LoadLevel (1);
			}
			if ((lvlSel == 2 && Xbox360GamepadState.Instance.IsButtonDown(Xbox.Button.A)) || (lvlSel == 2 && Input.GetKeyDown(KeyCode.KeypadEnter))) {
				onClick.Play();
				numLvl = 3;
				Cube.LevelString = "Level 3";
				Application.LoadLevel (1);
			}
			if ((lvlSel == 3 && Xbox360GamepadState.Instance.IsButtonDown(Xbox.Button.A)) || (lvlSel == 3 && Input.GetKeyDown(KeyCode.KeypadEnter))) {
				onClick.Play();
				numLvl = 4;
				Cube.LevelString = "Level 4";
				Application.LoadLevel (1);
			}
			if ((lvlSel == 4 && Xbox360GamepadState.Instance.IsButtonDown(Xbox.Button.A)) || (lvlSel == 4 && Input.GetKeyDown(KeyCode.KeypadEnter))) {
				onClick.Play();
				numLvl = 5;
				Cube.LevelString = "Level 5";
				Application.LoadLevel (1);
			}
			if ((lvlSel == 5 && Xbox360GamepadState.Instance.IsButtonDown(Xbox.Button.A)) || (lvlSel == 5 && Input.GetKeyDown(KeyCode.KeypadEnter))) {
				onClick.Play();
				numLvl = 6;
				Cube.LevelString = "Level 6";
				Application.LoadLevel (1);
			}
			if ((lvlSel == 6 && Xbox360GamepadState.Instance.IsButtonDown(Xbox.Button.A)) || (lvlSel == 6 && Input.GetKeyDown(KeyCode.KeypadEnter))) {
				onBack.Play();
				UIcontrols.levelSelect.gameObject.SetActive (false);
				UIcontrols.menuOP.gameObject.SetActive(true);
				opSel = 1;
			}
			if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A) || Input.GetKeyDown (KeyCode.KeypadEnter)) {
				onBack.Play();
				UIcontrols.levelSelect.gameObject.SetActive (false);
				UIcontrols.menuOP.gameObject.SetActive(true);
				opSel = 1;
				lvlSel = 0;
				es.SetSelectedGameObject(UIcontrols.Options[opSel].gameObject, new BaseEventData(es));
			}
		}

		if (UIcontrols.credits.gameObject.activeSelf == true) 
		{
			if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A) || Input.GetKeyDown (KeyCode.KeypadEnter)) {
				onClick.Play();
				UIcontrols.credits.gameObject.SetActive (false);
				UIcontrols.menuOP.gameObject.SetActive(true);
				opSel = 2;
				es.SetSelectedGameObject(UIcontrols.Options[opSel].gameObject, new BaseEventData(es));
			}
		}
	}
}
