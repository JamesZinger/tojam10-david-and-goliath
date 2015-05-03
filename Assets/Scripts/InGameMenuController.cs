using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class InGameMenuController : MonoBehaviour
{

	public float TransitionSpeed = 1;
	public RectTransform[] children;
	private Vector2[] targetPositions;
	private Vector2[] startingPositions;
	private Button selfButton;
	private bool isExpanded;
	private bool isCoroutineRunning;


	void Awake ()
	{
		targetPositions = new Vector2[children.Length];

		for (int i =0; i < children.Length; i++)
		{
			targetPositions[ i ] = children[ i ].anchoredPosition;
			children[ i ].anchoredPosition = ( (RectTransform) transform ).anchoredPosition;
			if ( i != 2 )
				children[ i ].gameObject.SetActive( false );
		}
		children[ 2 ].anchoredPosition = targetPositions[ 0 ];

		startingPositions = children.Select( t => t.anchoredPosition ).ToArray();
	}
	// Use this for initialization
	void Start ()
	{
		selfButton = children[ 2 ].GetComponent<Button>();
		selfButton.onClick.AddListener( Toggle );
	}

	public void Toggle()
	{
		if ( isCoroutineRunning ) return;
		StartCoroutine( buttonMover() );
	}

	IEnumerator buttonMover ()
	{
		float progress = 0f;
		if ( !isExpanded )
		{
			isCoroutineRunning = true;
			
			while ( progress < 1f )
			{
				children[ 2 ].anchoredPosition = Vector2.Lerp( startingPositions[ 2 ], targetPositions[ 2 ], progress );
				children[ 1 ].anchoredPosition = children[ 2 ].anchoredPosition;
				children[ 0 ].anchoredPosition = children[ 2 ].anchoredPosition;

				progress += Time.deltaTime * TransitionSpeed;
				yield return null;
			}

			for ( int i = 0; i < children.Length; i++ )
			{
				children[ i ].anchoredPosition = targetPositions[ 2 ];
			}
			
			progress = 0f;

			Array.ForEach( children, r => {
				r.gameObject.SetActive( true );
			} );

			while ( progress < 1f )
			{
				for ( int i = children.Length - 2; i >= 0; i-- )
				{
					children[ i ].anchoredPosition = Vector2.Lerp( children[ 2 ].anchoredPosition, targetPositions[ i ], progress );
				}
				progress += Time.deltaTime * TransitionSpeed;
				yield return null;
			}

			for ( int i = 0; i < children.Length; i++ )
			{
				children[ i ].anchoredPosition = targetPositions[ i ];
			}


			isExpanded = true;
		}
		else
		{
			while ( progress < 1f )
			{
				for ( int i = children.Length - 2; i >= 0; i-- )
				{
					children[ i ].anchoredPosition = Vector2.Lerp( targetPositions[ i ], children[ 2 ].anchoredPosition, progress );
				}
				progress += Time.deltaTime * TransitionSpeed;
				yield return null;
			}

			for ( int i = 0; i < children.Length; i++ )
			{
				if ( i != 2 )
					children[ i ].gameObject.SetActive( false );

				children[ i ].anchoredPosition = children[ 2 ].anchoredPosition;
			}

			progress = 0;
			while ( progress < 1f )
			{
				children[ 2 ].anchoredPosition = Vector2.Lerp( targetPositions[ 2 ], startingPositions[ 2 ], progress );
				children[ 1 ].anchoredPosition = children[ 2 ].anchoredPosition;
				children[ 0 ].anchoredPosition = children[ 2 ].anchoredPosition;

				progress += Time.deltaTime * TransitionSpeed;
				yield return null;
			}

			isExpanded = false;
		}
		isCoroutineRunning = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
