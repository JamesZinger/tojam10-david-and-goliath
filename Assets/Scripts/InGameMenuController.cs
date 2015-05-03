using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameMenuController : MonoBehaviour {

	public RectTransform [] children;
	private Vector2 [] targetPositions;
	private Button selfButton;

	void Awake () {
		targetPositions = new Vector2[children.Length];

		for (int i =0; i < children.Length; i++) {
			targetPositions[i] = children[i].anchoredPosition;
			children[i].anchoredPosition = ((RectTransform)transform).anchoredPosition;
			if(i != 2)
				children[i].gameObject.SetActive(false);
			Debug.Log ("position : " + targetPositions[i]);
				}
		children[2].anchoredPosition = targetPositions [0];
	}
	// Use this for initialization
	void Start () {
		selfButton = GetComponent<Button> ();
		selfButton.onClick.AddListener (onClick);
	}

	void onClick () {
		StartCoroutine (buttonMover ());
	}

	IEnumerator buttonMover () {
		while (true) {
			for(int i = 0; i < children.Length; i++) {

			}
			yield return null;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
