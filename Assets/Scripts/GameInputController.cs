using UnityEngine;

public class GameInputController : MonoBehaviour
{
	Cube cube;

	void Start()
	{
		cube = FindObjectOfType<Cube> ();
	}

	void Update()
	{
		if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A)) {
			cube.RotateX();
		}
		if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.B)) {
			cube.RotateY();
		}
		if (Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.X)) {
			cube.RotateZ();
		}
	}
}
