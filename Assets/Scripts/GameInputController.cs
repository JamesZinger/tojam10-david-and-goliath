using UnityEngine.UI;
using UnityEngine.EventSystems; // This "using" method is mandatory for selecting buttons without mouse input
using UnityEngine;

public class GameInputController : MonoBehaviour
{
	public EventSystem es; // This is what allows for the events of selection options to take place
	private int opSel = 0; // Variable for selecting through array
	private int lvlSel = 0;
	Cube cube;
	private GameUIController UIcontrols;

	void Start()
	{
		cube = FindObjectOfType<Cube>();
		UIcontrols = FindObjectOfType<GameUIController>();
		opSel = 0;
		es.SetSelectedGameObject(UIcontrols.Options[opSel].gameObject, new BaseEventData(es));
		lvlSel = 0;
	}

	void Update()
	{
		// The following "if" statement allows for selecting menue buttons through controls
		Xbox360GamepadState.Instance.UpdateState ();
		if (UIcontrols.menuOP.gameObject.activeSelf == true) {
			// When menu is active the following is allowed
			if (Xbox360GamepadState.Instance.AxisJustPastThreshold(Xbox.Axis.LAnalogY, -0.5f) || Input.GetKeyDown(KeyCode.S)) {
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
			if (Xbox360GamepadState.Instance.AxisJustPastThreshold(Xbox.Axis.LAnalogY, 0.5f) || Input.GetKeyDown(KeyCode.W)) {
				opSel -=1;
				if (opSel < 0)
				{
					opSel = 3;
				}
				es.SetSelectedGameObject(UIcontrols.Options[opSel].gameObject, new BaseEventData(es));
			}
			if ((opSel == 0 && Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A))|| (opSel == 0 && Input.GetKeyDown(KeyCode.A))) {
				UIcontrols.menuOP.gameObject.SetActive (false);
				UIcontrols.howTo.gameObject.SetActive(true);
			}
			else if ((opSel == 1 && Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A))|| (opSel == 1 && Input.GetKeyDown(KeyCode.A))) {
				UIcontrols.menuOP.gameObject.SetActive (false);
				UIcontrols.levelSelect.gameObject.SetActive(true);
				return;
			}
			else if ((opSel == 2 && Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A))|| (opSel == 2 && Input.GetKeyDown(KeyCode.A))) {
				UIcontrols.menuOP.gameObject.SetActive (false);
				UIcontrols.credits.gameObject.SetActive(true);
			}
			else if ((opSel == 3 && Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A))|| (opSel == 3 && Input.GetKeyDown(KeyCode.A))) {
				Application.Quit();
			}
			if(Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.Back) || Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.Start)|| Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.B)|| Input.GetKeyDown(KeyCode.Space)) {
				UIcontrols.menuOP.gameObject.SetActive(false);
				opSel = 0;
				es.SetSelectedGameObject(UIcontrols.Options[opSel].gameObject, new BaseEventData(es));
				Time.timeScale = 1;
			}
			return;
		}
		if (UIcontrols.menuOP.gameObject.activeSelf == false) {


			
		if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.Start) || Input.GetKeyDown(KeyCode.M)) {

				UIcontrols.menuOP.gameObject.SetActive(true);
				Time.timeScale = 0;
			}
			//cube.RotateY();
		}

		if (UIcontrols.howTo.gameObject.activeSelf == true) {
			if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.B) || Input.GetKeyDown (KeyCode.Space)) {
				UIcontrols.howTo.gameObject.SetActive (false);
				UIcontrols.menuOP.gameObject.SetActive(true);
				opSel = 0;
				es.SetSelectedGameObject(UIcontrols.Options[opSel].gameObject, new BaseEventData(es));
			}
		}

		if (UIcontrols.levelSelect.gameObject.activeSelf == true) {
			es.SetSelectedGameObject(UIcontrols.levelOp[lvlSel].gameObject, new BaseEventData(es));
			if (Xbox360GamepadState.Instance.AxisJustPastThreshold(Xbox.Axis.LAnalogY, -0.5f) || Input.GetKeyDown(KeyCode.S)) {
				lvlSel += 1;
				if (UIcontrols.levelOp.Length == lvlSel)
				{
					lvlSel = 0;
				}
				es.SetSelectedGameObject(UIcontrols.levelOp[lvlSel].gameObject, new BaseEventData(es));
			}
			if (Xbox360GamepadState.Instance.AxisJustPastThreshold(Xbox.Axis.LAnalogY, 0.5f) || Input.GetKeyDown(KeyCode.W)) {
				lvlSel -= 1;
				if (lvlSel < 0)
				{
					lvlSel = 3;
				}
				es.SetSelectedGameObject(UIcontrols.levelOp[lvlSel].gameObject, new BaseEventData(es));
			}
			if ((lvlSel == 0 && Xbox360GamepadState.Instance.IsButtonDown(Xbox.Button.A)) || (lvlSel == 0 && Input.GetKeyDown(KeyCode.A))) {
				Application.LoadLevel(1);
			}
			/*
			else if (lvlSel == 1 && Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A)|| lvlSel == 1 && Input.GetKeyDown(KeyCode.A)) {
				Application.LoadLevel(2);
			}
			else if (lvlSel == 2 && Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A)|| lvlSel == 2 && Input.GetKeyDown(KeyCode.A)) {
				Application.LoadLevel(3);
			}
			else if (lvlSel == 3 && Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A)|| lvlSel == 3 && Input.GetKeyDown(KeyCode.A)) {
				Application.LoadLevel(4);
			}*/
			if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.B) || Input.GetKeyDown (KeyCode.Space)) {
				UIcontrols.levelSelect.gameObject.SetActive (false);
				UIcontrols.menuOP.gameObject.SetActive(true);
				opSel = 1;
				lvlSel = 0;
				es.SetSelectedGameObject(UIcontrols.Options[opSel].gameObject, new BaseEventData(es));
			}
		}

		if (UIcontrols.credits.gameObject.activeSelf == true) {
			if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.B) || Input.GetKeyDown (KeyCode.Space)) {
				UIcontrols.credits.gameObject.SetActive (false);
				UIcontrols.menuOP.gameObject.SetActive(true);
				opSel = 2;
				es.SetSelectedGameObject(UIcontrols.Options[opSel].gameObject, new BaseEventData(es));
			}
		}
		if (UIcontrols.menuOP.gameObject.activeSelf == false) {
						if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.X)) {
								cube.RotateX ();
						}
						if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A)) {
								cube.RotateY ();
						}

						if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.B)) {
								cube.RotateZ ();
						}
						
						if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.Y)) {
								cube.StartMovingGoat ();
			}
			return;
		}
	}
}