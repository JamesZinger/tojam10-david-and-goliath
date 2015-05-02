using UnityEngine;

public class GameInputController : MonoBehaviour
{
	Cube cube;
	GameUIController UIcontrols;

	void Start()
	{
		cube = FindObjectOfType<Cube> ();
		UIcontrols = FindObjectOfType<GameUIController> ();
	}

	void Update()
	{
		if (UIcontrols.menuOP == true) {
			if(Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A) || Input.GetKeyDown(KeyCode.Space)) {
				UIcontrols.menuOP = false;
			}
		}
		if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A)) {
			cube.RotateX();
		}
		if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.B) || Input.GetKeyDown(KeyCode.M)) {
			UIcontrols.menuOP = true;
			//cube.RotateY();
		}
		if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.X)) {
			cube.RotateZ();
		}
	}
}
