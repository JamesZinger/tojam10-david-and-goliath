﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GameUIController : MonoBehaviour {

	public InGameMenuController menuOP;

	public Button[] Options; // Make an array of button options
	public Button[] levelOp;
	public Button credReturn;

	public Image credits;
	public Image levelSelect;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
