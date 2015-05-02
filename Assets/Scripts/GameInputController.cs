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
<<<<<<< HEAD
		if (UIcontrols.menuOP == true) {
			if(Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A) || Input.GetKeyDown(KeyCode.M)) {
				UIcontrols.menuOP = false;
			}
		}
=======
		if (UIcontrols.menuOP.gameObject.activeSelf == true) {
			if(Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A) || Input.GetKeyDown(KeyCode.Space)) {
				UIcontrols.menuOP.gameObject.SetActive(false);
			}
		}
		if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A)) {
			cube.RotateX();
		}
		if (UIcontrols.menuOP.gameObject.activeSelf == false) {
>>>>>>> 46aa59d3da632591578c53c14a0d42185ea7ddd3
		if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.B) || Input.GetKeyDown(KeyCode.M)) {
				UIcontrols.menuOP.gameObject.SetActive(true);
			}
			//cube.RotateY();
		}
		if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.X)) {
			cube.RotateX();
		}

		if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.X)) {
			cube.RotateZ();
		}
	}
}
