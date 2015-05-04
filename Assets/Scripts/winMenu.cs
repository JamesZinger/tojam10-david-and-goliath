using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class winMenu : MonoBehaviour {
	
	private int winSel = 0;
	public Image phrase1;
	public Image phrase2;
	public Image phrase3;
	public Text tries;
	public EventSystem es;
	public AudioSource onClick;
	private InGameInput UIcontrols;
	// Use this for initialization
	void Start () {
		UIcontrols = FindObjectOfType<InGameInput>();

		tries.text = " " + (Cube.DeathCount + 1);

		if (Cube.DeathCount < 3)
			phrase1.gameObject.SetActive (true);
		else if (Cube.DeathCount > 3 && Cube.DeathCount < 6)
			phrase2.gameObject.SetActive (true);
		else if (Cube.DeathCount > 6)
			phrase3.gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		Xbox360GamepadState.Instance.UpdateState ();
		if (UIcontrols.winMenu.gameObject.activeSelf == true)
		{
			Time.timeScale = 0;
			// When menu is active the following is allowed
			if (Xbox360GamepadState.Instance.AxisJustPastThreshold(Xbox.Axis.LAnalogX, 0.5f) || 
				Input.GetKeyDown(KeyCode.D)) 
			{
				winSel +=1; // Increment the array by 1 as you move down the list
				// This statement allows for returning back to top option once you reach the bottom of the list
				if (UIcontrols.winOp.Length == winSel)
				{
					// Sets the variable for the array back to 0 to continue moving down the list
					winSel = 0;
				}
			// This line is what allows for the selection of buttons through key input and so on
			es.SetSelectedGameObject(UIcontrols.winOp[winSel].gameObject, new BaseEventData(es));
			}
			// This statement does the same as above but for upward action
			if (Xbox360GamepadState.Instance.AxisJustPastThreshold(Xbox.Axis.LAnalogX, -0.5f) || 
			    Input.GetKeyDown(KeyCode.A)) 
			{
				winSel -=1;
				if (winSel < 0)
				{
					winSel = 1;
				}
				es.SetSelectedGameObject(UIcontrols.winOp[winSel].gameObject, new BaseEventData(es));
			}
			if((winSel == 0 && Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A)) ||
				(winSel == 0 && Input.GetKeyDown(KeyCode.KeypadEnter))) 
			{
				Time.timeScale = 0;
				UIcontrols.winMenu.gameObject.SetActive(false);
				Cube.HasFinished = false;
				Cube.DeathCount = 0;
				onClick.Play();
				Application.LoadLevel(0);
				return;
			}
			if((winSel == 1 && Xbox360GamepadState.Instance.IsButtonDown (Xbox.Button.A)) ||
				(winSel == 1 && Input.GetKeyDown(KeyCode.KeypadEnter))) 
			{
				Time.timeScale = 0;
				MainMenu.numLvl++;
				onClick.Play();
				UIcontrols.winMenu.gameObject.SetActive(false);
				Cube.HasFinished = false;
				Cube.DeathCount = 0;
				var nextLevelNum = MainMenu.numLvl + 1;
				if ( nextLevelNum == Graph.levelSelectionDictionary.Count )
				{
					Application.LoadLevel( 0 );
					return;
				}
				Cube.LevelString = "Level " + ( MainMenu.numLvl + 1 );
				Debug.Log( "Cube Level Name: " + Cube.LevelString );
				Application.LoadLevel( 1 );
			}
		}
	}
}
