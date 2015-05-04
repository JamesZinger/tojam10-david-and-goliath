using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InGameInput : MonoBehaviour
{
	public Image winMenu;
	public Button[] winOp;

	void Start () {
		winMenu.gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if (Cube.HasFinished) {
						winMenu.gameObject.SetActive (true);
				} else 
						winMenu.gameObject.SetActive (false);
	}
}