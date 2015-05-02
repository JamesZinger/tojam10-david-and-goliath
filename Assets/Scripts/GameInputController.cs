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
		if (UIcontrols.menuOP.gameObject.activeSelf == true) {
			if(Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A) || Input.GetKeyDown(KeyCode.Space)) {
				UIcontrols.menuOP.gameObject.SetActive(false);
			}
		}
		if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A)) {
			cube.RotateX();
		}
		if (UIcontrols.menuOP.gameObject.activeSelf == false) {
		if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.B) || Input.GetKeyDown(KeyCode.M)) {
				UIcontrols.menuOP.gameObject.SetActive(true);
			}
			//cube.RotateY();
		}
		if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.X)) {
			cube.RotateZ();
		}
	}
}