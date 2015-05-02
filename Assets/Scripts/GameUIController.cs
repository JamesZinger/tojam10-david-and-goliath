using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

	public Image menuOP;
	public Image credits;
	public Image howTo;
	public Image options;
	public Image levelSelect;
	// Use this for initialization
	void Start () {
	
		menuOP.gameObject.SetActive(false);
		credits.gameObject.SetActive(false);
		howTo.gameObject.SetActive(false);
		options.gameObject.SetActive(false);
		levelSelect.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
