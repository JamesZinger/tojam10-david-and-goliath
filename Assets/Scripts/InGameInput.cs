﻿using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class InGameInput : MonoBehaviour {

	public EventSystem es;
	private int opSel = 0;
	//Cube cube;
	private InGameUI UIcontrols;
	// Use this for initialization
	void Start () {
		//cube = FindObjectOfType<Cube>();
		UIcontrols = FindObjectOfType<InGameUI>();
		opSel = 0;
		es.SetSelectedGameObject(UIcontrols.gameOptions[opSel].gameObject, new BaseEventData(es));
	}
	
	// Update is called once per frame
	void Update () {
		Xbox360GamepadState.Instance.UpdateState ();
		if (UIcontrols.pauseMenu.gameObject.activeSelf == true) {
			// When menu is active the following is allowed
			if (Xbox360GamepadState.Instance.AxisJustPastThreshold(Xbox.Axis.LAnalogY, -0.5f) || Input.GetKeyDown(KeyCode.S)) {
				opSel +=1; // Increment the array by 1 as you move down the list
				// This statement allows for returning back to top option once you reach the bottom of the list
				if (UIcontrols.gameOptions.Length == opSel)
				{
					// Sets the variable for the array back to 0 to continue moving down the list
					opSel = 0;
				}
				// This line is what allows for the selection of buttons through key input and so on
				es.SetSelectedGameObject(UIcontrols.gameOptions[opSel].gameObject, new BaseEventData(es));
			}
			// This statement does the same as above but for upward action
			if (Xbox360GamepadState.Instance.AxisJustPastThreshold(Xbox.Axis.LAnalogY, 0.5f) || Input.GetKeyDown(KeyCode.W)) {
				opSel -=1;
				if (opSel < 0)
				{
					opSel = 1;
				}
				es.SetSelectedGameObject(UIcontrols.gameOptions[opSel].gameObject, new BaseEventData(es));
			}
			if ((opSel == 0 && Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A))|| (opSel == 0 && Input.GetKeyDown(KeyCode.A))) {
				UIcontrols.pauseMenu.gameObject.SetActive (false);
				UIcontrols.inGameHowTo.gameObject.SetActive(true);
			}
			else if ((opSel == 1 && Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A))|| (opSel == 1 && Input.GetKeyDown(KeyCode.A))) {
				Application.LoadLevel(0);
			}
			if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.Start) || Input.GetKeyDown(KeyCode.M)) {
				UIcontrols.pauseMenu.gameObject.SetActive(true);
				Time.timeScale = 0;
			}
			if(Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.Back) || Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.Start)|| Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.B)|| Input.GetKeyDown(KeyCode.Space)) {
				UIcontrols.pauseMenu.gameObject.SetActive(false);
				opSel = 0;
				es.SetSelectedGameObject(UIcontrols.gameOptions[opSel].gameObject, new BaseEventData(es));
				Time.timeScale = 1;
			}
			return;
		}
		if (UIcontrols.pauseMenu.gameObject.activeSelf == false) {
			if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.Start) || Input.GetKeyDown(KeyCode.M)) {
				UIcontrols.pauseMenu.gameObject.SetActive(true);
				Time.timeScale = 0;
			}
			//cube.RotateY();
		}
		if (UIcontrols.inGameHowTo.gameObject.activeSelf == true) {
			if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.B) || Input.GetKeyDown (KeyCode.Space)) {
				UIcontrols.inGameHowTo.gameObject.SetActive (false);
				UIcontrols.pauseMenu.gameObject.SetActive(true);
				opSel = 0;
				es.SetSelectedGameObject(UIcontrols.gameOptions[opSel].gameObject, new BaseEventData(es));
			}
		}
	}
}
