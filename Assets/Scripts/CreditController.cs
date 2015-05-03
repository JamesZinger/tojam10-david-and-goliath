using UnityEngine;
using System.Collections;

public class CreditController : MonoBehaviour
{
	public AudioSource onClick;
	
	// Update is called once per frame
	void Update () {
		if ( ( Xbox360GamepadState.Instance.IsButtonDown( Xbox.Button.B ) ) || ( Input.GetKeyDown( KeyCode.Return ) ) )
		{
			FindObjectOfType<InGameMenuController>().TransitioningBack();
			onClick.Play();
			gameObject.SetActive( false );
		}
	}
}
