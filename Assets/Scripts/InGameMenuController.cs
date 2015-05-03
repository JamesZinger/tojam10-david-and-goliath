using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGameMenuController : MonoBehaviour
{

	public float TransitionSpeed = 1;
	public RectTransform[] children;
	public GameObject LevelSelection;
	public GameObject CreditScreen;
	
	private Vector2[] targetPositions;
	private Vector2[] startingPositions;
	private Button selfButton;
	private bool isExpanded;
	private bool isCoroutineRunning;
	private int selectedIndex = 0;
	public bool isInChildScreen;

	private void Awake()
	{
		targetPositions = new Vector2[children.Length];

		for ( int i = 0; i < children.Length; i++ )
		{
			targetPositions[ i ] = children[ i ].anchoredPosition;
			children[ i ].anchoredPosition = ( (RectTransform) transform ).anchoredPosition;
			if ( i != 0 )
				children[ i ].gameObject.SetActive( false );
		}
		children[ 0 ].anchoredPosition = targetPositions[ 1 ];

		startingPositions = children.Select( t => t.anchoredPosition ).ToArray();
	}

	// Use this for initialization
	private void Start()
	{
		selfButton = children[ 0 ].GetComponent<Button>();
		selfButton.onClick.AddListener( Toggle );
	}

	public void Toggle()
	{
		if ( isCoroutineRunning ) return;
		StartCoroutine( ButtonMover() );
	}

	private IEnumerator ButtonMover()
	{
		float progress = 0f;
		if ( !isExpanded )
		{
			Time.timeScale = 0;
			isCoroutineRunning = true;

			while ( progress < 1f )
			{
				children[ 0 ].anchoredPosition = Vector2.Lerp( startingPositions[ 0 ], targetPositions[ 0 ], progress );
				children[ 1 ].anchoredPosition = children[ 0 ].anchoredPosition;
				children[ 2 ].anchoredPosition = children[ 0 ].anchoredPosition;

				progress += Time.unscaledDeltaTime * TransitionSpeed;
				yield return null;
			}

			for ( int i = 0; i < children.Length; i++ )
			{
				children[ i ].anchoredPosition = targetPositions[ 0 ];
			}

			progress = 0f;

			Array.ForEach( children, r => {
				r.gameObject.SetActive( true );
			} );

			while ( progress < 1f )
			{
				for ( int i = 1; i < children.Length; i++ )
				{
					children[ i ].anchoredPosition = Vector2.Lerp( children[ 0 ].anchoredPosition, targetPositions[ i ], progress );
				}
				progress += Time.unscaledDeltaTime * TransitionSpeed;
				yield return null;
			}

			for ( int i = 0; i < children.Length; i++ )
			{
				children[ i ].anchoredPosition = targetPositions[ i ];
			}

			EventSystem.current.SetSelectedGameObject( children[ 0 ].gameObject, new BaseEventData( EventSystem.current ) );
			isExpanded = true;
		}
		else
		{
			while ( progress < 1f )
			{
				for ( int i = 1; i < children.Length; i++ )
				{
					children[ i ].anchoredPosition = Vector2.Lerp( targetPositions[ i ], children[ 0 ].anchoredPosition, progress );
				}
				progress += Time.unscaledDeltaTime * TransitionSpeed;
				yield return null;
			}

			for ( int i = 0; i < children.Length; i++ )
			{
				if ( i != 0 )
					children[ i ].gameObject.SetActive( false );

				children[ i ].anchoredPosition = children[ 0 ].anchoredPosition;
			}

			progress = 0;
			while ( progress < 1f )
			{
				children[ 0 ].anchoredPosition = Vector2.Lerp( targetPositions[ 0 ], startingPositions[ 0 ], progress );
				children[ 1 ].anchoredPosition = children[ 0 ].anchoredPosition;
				children[ 2 ].anchoredPosition = children[ 0 ].anchoredPosition;


				progress += Time.unscaledDeltaTime * TransitionSpeed;
				yield return null;
			}

			Time.timeScale = 1f;
			isExpanded = false;
		}
		isCoroutineRunning = false;
	}

	// Update is called once per frame
	private void Update()
	{
		if ( isCoroutineRunning ) return;

		if ( isExpanded && !isInChildScreen )
		{
			if ( Xbox360GamepadState.Instance.AxisJustPastThreshold( Xbox.Axis.LAnalogY, -0.5f ) || Input.GetKeyDown( KeyCode.S ) )
			{
				selectedIndex++;
				if ( selectedIndex == children.Length ) selectedIndex = 0;

				EventSystem.current.SetSelectedGameObject( children[ selectedIndex ].gameObject,
					new BaseEventData( EventSystem.current ) );


			}
			// This statement does the same as above but for upward action
			if ( Xbox360GamepadState.Instance.AxisJustPastThreshold( Xbox.Axis.LAnalogY, 0.5f ) || Input.GetKeyDown( KeyCode.W ) )
			{
				selectedIndex--;
				if ( selectedIndex == -1 ) selectedIndex = children.Length - 1;

				EventSystem.current.SetSelectedGameObject( children[ selectedIndex ].gameObject,
					new BaseEventData( EventSystem.current ) );
			}

			if ( Xbox360GamepadState.Instance.IsButtonDown( Xbox.Button.Start ) || Input.GetKeyDown( KeyCode.M ) )
			{
				Toggle();
			}

			if ( Xbox360GamepadState.Instance.IsButtonDown( Xbox.Button.A ) || Input.GetKeyDown( KeyCode.A ) )
			{
				if ( selectedIndex == 0 )
				{
					// Spawn level selection screen
					Toggle();
				}
				else if ( selectedIndex == 1 )
				{
					isInChildScreen = true;
					LevelSelection.SetActive( true );
				}
				else if ( selectedIndex == 2 )
				{
					isInChildScreen = true;
					CreditScreen.SetActive( true );
				}
			}
		}
		else if ( isExpanded && isInChildScreen )
		{
			if ( Xbox360GamepadState.Instance.IsButtonDown( Xbox.Button.B ) || Input.GetKeyDown( KeyCode.Space ) )
			{
				switch ( selectedIndex )
				{
					case 1:

						break;
					case 2:
						CreditScreen.SetActive( false );
						break;
				}
			}
		}
		else if ( !isExpanded )
		{
			if ( Xbox360GamepadState.Instance.IsButtonDown( Xbox.Button.Start ) || Input.GetKeyDown( KeyCode.M ) )
			{
				Toggle();
			}
		}
	}
}