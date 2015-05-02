using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GameUIController : MonoBehaviour {

	public List <Button> Options;
	public Image menuOP;
	public Image credits;
	public Image howTo;
	public Image options;
	public Image levelSelect;
	// Use this for initialization
	void Start () {


		menuOP.gameObject.SetActive(true);
		credits.gameObject.SetActive(true);
		howTo.gameObject.SetActive(true);
		options.gameObject.SetActive(true);
		levelSelect.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
