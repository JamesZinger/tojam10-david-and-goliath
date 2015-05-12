using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPause : MonoBehaviour
{
	public float TransitionSpeed = 1;
	public AnimationCurve Easing = new AnimationCurve();
	public RectTransform ModalPanel;
	public RectTransform ModalPanelText;
	public RectTransform ModalPanelTextBackground;
	public RectTransform MenuButton;
	public RectTransform PlayButton;
	public RectTransform[] Children;
	public GameObject LevelSelectionScreen;
	public GameObject CreditScreen;
	public GameObject[] GameUI;
	
	private bool isCoroutineRunning;
	private Vector2[] targetPositions;
	private Vector2[] startingPositions;
	
	public bool IsExpanded { get; private set; }

	void Start()
	{
		Time.timeScale = 1;
		targetPositions = new Vector2[ Children.Length + 1 ];
		startingPositions = new Vector2[ Children.Length + 1 ];

		targetPositions[ 0 ] = MenuButton.position;
		MenuButton.position = ( (RectTransform) transform ).position;
		PlayButton.position = ( (RectTransform) transform ).position;
		startingPositions[ 0 ] = MenuButton.position;

		var tempColour = PlayButton.GetComponent<Image>().color;
		tempColour.a = 1f;
		MenuButton.GetComponent<Image>().color = tempColour;
		tempColour.a = 0f;
		PlayButton.GetComponent<Image>().color = tempColour;
		tempColour.a *= 0.2f;
		ModalPanel.GetComponent<Image>().color = tempColour;

		tempColour = ModalPanelText.GetComponent<Text>().color;
		tempColour.a = 0f;
		ModalPanelText.GetComponent<Text>().color = tempColour;

		tempColour = ModalPanelTextBackground.GetComponent<Image>().color;
		tempColour.a = 0f;
		ModalPanelTextBackground.GetComponent<Image>().color = tempColour;

		for ( int i = 0; i < Children.Length; i++ )
		{
			targetPositions[ i + 1 ] = Children[ i ].position;
			Children[ i ].position = MenuButton.position;
			startingPositions[ i + 1 ] = MenuButton.position;
			
			tempColour = Children[ i ].GetComponent<Image>().color;
			tempColour.a = 0f;
			Children[ i ].GetComponent<Image>().color = tempColour;
		}

		Array.ForEach( Children, o => o.gameObject.SetActive( false ) );
		ModalPanel.gameObject.SetActive( false );
		MenuButton.gameObject.SetActive( true );
		PlayButton.gameObject.SetActive( false );
	}

	void Update()
	{
		if ( isCoroutineRunning )
		{
			return;
		}
		
		if ( !IsExpanded )
		{
			if ( Xbox360GamepadState.Instance.IsButtonDown( Xbox.Button.Start ) || Input.GetKeyDown( KeyCode.Space ) )
			{
				Toggle();
			}
		}
		else 
		{
			if ( !( LevelSelectionScreen.activeSelf || CreditScreen.activeSelf ) )
			{
				if ( Xbox360GamepadState.Instance.IsButtonDown( Xbox.Button.Start ) || Input.GetKeyDown( KeyCode.Escape ) )
				{
					Toggle();
				}
			}
		}
	}

	public void Toggle()
	{
		if ( isCoroutineRunning ) return;
		StartCoroutine( ToggleCoroutine() );
	}

	private IEnumerator ToggleCoroutine()
	{
		isCoroutineRunning = true;

		if ( !IsExpanded )
		{
			Time.timeScale = 0;
			Array.ForEach( Children, o => o.gameObject.SetActive( true ) );
			ModalPanel.gameObject.SetActive( true );
			MenuButton.gameObject.SetActive( false );
			PlayButton.gameObject.SetActive( true );
			
			for ( float progress = 0f; progress < 1f; progress += Time.unscaledDeltaTime * TransitionSpeed )
			{
				MenuButton.position = SampleEasing( startingPositions[ 0 ], targetPositions[ 0 ], progress );
				PlayButton.position = MenuButton.position;
				
				var sample = SampleEasing( 0f, 1f, progress );
				var tempColour = PlayButton.GetComponent<Image>().color;
				tempColour.a = 1f - sample;
				MenuButton.GetComponent<Image>().color = tempColour;
				tempColour.a = sample;
				PlayButton.GetComponent<Image>().color = tempColour;
				tempColour.a *= 0.2f;
				ModalPanel.GetComponent<Image>().color = tempColour;

				tempColour = ModalPanelText.GetComponent<Text>().color;
				tempColour.a = sample;
				ModalPanelText.GetComponent<Text>().color = tempColour;

				tempColour = ModalPanelTextBackground.GetComponent<Image>().color;
				tempColour.a = 0.6f * sample;
				ModalPanelTextBackground.GetComponent<Image>().color = tempColour;

				for ( int i = 0; i < Children.Length; i++ )
				{
					Children[ i ].position = SampleEasing( startingPositions[ i + 1 ], targetPositions[ i + 1 ], progress );
					
					tempColour = Children[ i ].GetComponent<Image>().color;
					tempColour.a = sample;
					Children[ i ].GetComponent<Image>().color = tempColour;
				}

				yield return null;
			}

			for ( int i = 0; i < Children.Length; i++ )
			{
				Children[ i ].position = targetPositions[ i + 1 ];
			}

			EventSystem.current.SetSelectedGameObject( MenuButton.gameObject, new BaseEventData( EventSystem.current ) );
			IsExpanded = true;
		}
		else
		{ 
			for ( float progress = 0f; progress < 1f; progress += Time.unscaledDeltaTime * TransitionSpeed )
			{
				MenuButton.position = SampleEasing( targetPositions[ 0 ], startingPositions[ 0 ], progress );
				PlayButton.position = MenuButton.position;
				
				var sample = SampleEasing( 1f, 0f, progress );
				var tempColour = PlayButton.GetComponent<Image>().color;
				tempColour.a = 1f - sample;
				MenuButton.GetComponent<Image>().color = tempColour;
				tempColour.a = sample;
				PlayButton.GetComponent<Image>().color = tempColour;
				tempColour.a *= 0.2f;
				ModalPanel.GetComponent<Image>().color = tempColour;

				tempColour = ModalPanelText.GetComponent<Text>().color;
				tempColour.a = sample;
				ModalPanelText.GetComponent<Text>().color = tempColour;

				tempColour = ModalPanelTextBackground.GetComponent<Image>().color;
				tempColour.a = 0.6f * sample;
				ModalPanelTextBackground.GetComponent<Image>().color = tempColour;

				for ( int i = 0; i < Children.Length; i++ )
				{
					Children[ i ].position = SampleEasing( targetPositions[ i + 1 ], startingPositions[ i + 1 ], progress );
					
					tempColour = Children[ i ].GetComponent<Image>().material.color;
					tempColour.a = sample;
					Children[ i ].GetComponent<Image>().color = tempColour;
				}
				
				yield return null;
			}

			for ( int i = 0; i < Children.Length; i++ )
			{
				Children[ i ].position = startingPositions[ i + 1 ];
			}

			EventSystem.current.SetSelectedGameObject( null, new BaseEventData( EventSystem.current ) );
			IsExpanded = false;

			Time.timeScale = 1f;
			Array.ForEach( Children, o => o.gameObject.SetActive( false ) );
			ModalPanel.gameObject.SetActive( false );
			MenuButton.gameObject.SetActive( true );
			PlayButton.gameObject.SetActive( false );
		}

		isCoroutineRunning = false;
	}
	
	private float SampleEasing( float start, float target, float progress )
	{
		return Easing.Evaluate( progress ) * ( target - start ) + start;
	}

	private Vector2 SampleEasing( Vector2 start, Vector2 target, float progress )
	{
		return Easing.Evaluate( progress ) * ( target - start ) + start;
	}

	public void TransitioningBack()
	{
		Array.ForEach( Children, child => child.gameObject.SetActive( true ) );
	}

	public void BackToMainMenu()
	{
		Game.ReturnToMainMenu();
	}

	public void OpenCredits()
	{
		Array.ForEach( Children, child => child.gameObject.SetActive( false ) );
		CreditScreen.SetActive( true );
	}

	public void OpenLevelSelect()
	{
		Array.ForEach( Children, child => child.gameObject.SetActive( false ) );
		LevelSelectionScreen.SetActive( true );
	}

}