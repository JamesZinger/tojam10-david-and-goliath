using UnityEngine;
using System.Collections;

public class UIControls : MonoBehaviour 
{
	public Cube Cube;
	public bool IsControlBlocked;
	

	void Update() 
	{
		Xbox360GamepadState.Instance.UpdateState();
		
		if ( IsControlBlocked )
		{
			return;
		}

		// Handle gamepad cube controls.
		if ( Xbox360GamepadState.Instance.IsButtonDown( Xbox.Button.X ) )
		{
			RotateX();
		}
		else if ( Xbox360GamepadState.Instance.IsButtonDown( Xbox.Button.A ) )
		{
			RotateY();
		}
		else if ( Xbox360GamepadState.Instance.IsButtonDown( Xbox.Button.B ) )
		{
			RotateZ();
		}
		else if ( Xbox360GamepadState.Instance.IsButtonDown( Xbox.Button.Y ) )
		{
			StartMoving();
		}
	}


	public void ResetCube()
	{
		Cube.MasterReset();
	}


	public void RotateX()
	{
		Cube.RotateX();
	}


	public void RotateY()
	{
		Cube.RotateY();
	}


	public void RotateZ()
	{
		Cube.RotateZ();
	}


	public void StartMoving()
	{
		Cube.StartMovingGoat();
	}
}
