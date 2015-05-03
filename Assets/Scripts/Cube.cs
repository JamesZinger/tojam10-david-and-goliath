using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cube : MonoBehaviour
{
	public int DeathCount;

	public static string LevelString = "";
	public string DefaultLevelString = "";
	public Transform CenterTransform;
	public AnimationCurve RotationCurve;
	public float SpinSpeed;
	public float SpinSpeedReverse;
	public Graph Graph;
	public bool HasStarted;
	public bool HasFinished;
	public Quad[] QuadArray;
	public GameObject DirectionPointer;
	public GameObject QuadPointerCenter;
	public GameObject StartPointPrefab;
	public GameObject EndPointPrefab;

	private Vector3[] originalQuadPositions;
	private Quaternion[] originalQuadRotations;

	private bool isReversing;

	private AudioSource rotateSound;
	private AudioSource reverseSound;

	private Stack<RotationAxis> rotationHistory;

	public bool IsRotating { get; private set; }
	public bool IsGoatDyingRightAtThisSecond { get; set; }

	public Collider GoatCollider { get; private set; }

	public RotationCollider XRotationCollider { get; private set; }
	public RotationCollider YRotationCollider { get; private set; }
	public RotationCollider ZRotationCollider { get; private set; }

	public enum RotationAxis { X, Y, Z }

	void Awake()
	{
		var stringLevelName = DefaultLevelString;
		if ( LevelString.Length != 0 ) stringLevelName = LevelString;

		Graph = Graph.LoadLevel( stringLevelName );
		HasStarted = false;
	}

	void Start()
	{
		IsGoatDyingRightAtThisSecond = false;
		rotationHistory = new Stack<RotationAxis>();

		Transform t = transform.FindChild( "RotateSound" );
		if ( t != null )
		{
			rotateSound = t.audio;
		}
		t = transform.FindChild( "ReverseSound" );
		if ( t != null )
		{
			reverseSound = t.audio;
		}

		var Colliders = GetComponentsInChildren<RotationCollider>();
		
		XRotationCollider = Colliders.Single( c => c.Axis == RotationAxis.X );
		YRotationCollider = Colliders.Single( c => c.Axis == RotationAxis.Y );
		ZRotationCollider = Colliders.Single( c => c.Axis == RotationAxis.Z );

		var goat = FindObjectOfType<TheGoat>();

		GoatCollider = goat.GetComponent<BoxCollider>();

		QuadArray = GetComponentsInChildren<Quad>();
		originalQuadPositions = new Vector3[ QuadArray.Length ];
		originalQuadRotations = new Quaternion[ QuadArray.Length ];
		for ( var i = 0; i < QuadArray.Length; i++ )
		{
			originalQuadPositions[ i ] = QuadArray[ i ].transform.position;
			originalQuadRotations[ i ] = QuadArray[ i ].transform.rotation;
			var childQuad = QuadArray[ i ];

			childQuad.Node = Graph.GetNodeByName( childQuad.NodeName );
			QuadArray[ i ].Configure();
		}

		goat.Configure( this );
		
	}

	public void StartMovingGoat()
	{
		if (HasStarted)
			return;
	
		HasStarted = true;
		GoatCollider.GetComponent<TheGoat>().StartSound.Play();
		GoatCollider.GetComponent<TheGoat>().MoveSound.Play();
	}

	void Update()
	{

	}

	public void RotateX()
	{
		if ( !IsRotating && !HasFinished )
		{
			rotateSound.Play();
			rotationHistory.Push( RotationAxis.X );
			StartCoroutine( Rotate( RotationAxis.X, RotationEnum.Clockwise, SpinSpeed ) );
		}
	}

	public void RotateY()
	{
		if ( !IsRotating && !HasFinished )
		{
			rotateSound.Play();
			rotationHistory.Push( RotationAxis.Y );
			StartCoroutine( Rotate( RotationAxis.Y, RotationEnum.Clockwise, SpinSpeed ) );
		}
	}

	public void RotateZ()
	{
		if ( !IsRotating && !HasFinished )
		{
			rotateSound.Play();
			rotationHistory.Push( RotationAxis.Z );
			StartCoroutine( Rotate( RotationAxis.Z, RotationEnum.Clockwise, SpinSpeed ) );
		}
	}

	IEnumerator Rotate( RotationAxis axis, RotationEnum direction, float speed )
	{
		if ( IsRotating )
		{
			yield break;
		}

		if ( SpinSpeed < float.Epsilon )
		{
			Debug.LogError( "SpinSpeed is less than or equal to 0" );
			yield break;
		}

		RotationCollider activeCollider = null;
		switch ( axis )
		{
			case RotationAxis.X: activeCollider = XRotationCollider; break;
			case RotationAxis.Y: activeCollider = YRotationCollider; break;
			case RotationAxis.Z: activeCollider = ZRotationCollider; break;
		}

		if ( activeCollider == null )
		{
			Debug.Log( "WHHAATT" );
			yield break;
		}

		Transform[] transformArray;
		Transform[] transformParentArray;
		{
			var quadList = activeCollider.GetAllObjectsToMove();

			transformArray = quadList.Select( o => o.transform )
				.ToArray();

			transformParentArray = transformArray.Select( t => t.parent ).ToArray();
		}

		Vector3? rotationAxis = null;
		if ( direction == RotationEnum.Clockwise )
		{
			switch ( axis )
			{
				case RotationAxis.X: rotationAxis = new Vector3( x: 1, y: 0, z: 0 ); break;
				case RotationAxis.Y: rotationAxis = new Vector3( x: 0, y: 1, z: 0 ); break;
				case RotationAxis.Z: rotationAxis = new Vector3( x: 0, y: 0, z: 1 ); break;
			}
		}
		else
		{
			switch ( axis )
			{
				case RotationAxis.X: rotationAxis = new Vector3( x: -1, y: 0, z: 0 ); break;
				case RotationAxis.Y: rotationAxis = new Vector3( x: 0, y: -1, z: 0 ); break;
				case RotationAxis.Z: rotationAxis = new Vector3( x: 0, y: 0, z: -1 ); break;
			}
		}

		if ( rotationAxis == null )
		{
			Debug.Log( "WWWHHHHAATTT" );
			yield break;
		}

		//switch ( axis )
		//{
		//	case RotationAxis.X: Graph.RotateX( direction ); break;
		//	case RotationAxis.Y: Graph.RotateY( direction ); break;
		//	case RotationAxis.Z: Graph.RotateZ( direction ); break;
		//}
		
		IsRotating = true;
		CenterTransform.rotation = Quaternion.identity;
		for ( var i = 0; i < transformArray.Length; i++ )
		{
			transformArray[ i ].parent = CenterTransform;
		}

		var progress = 0f;

		while ( progress < 1f )
		{
			var angle = RotationCurve.Evaluate( progress );
			CenterTransform.rotation = Quaternion.Euler( angle * rotationAxis.Value );
			progress += Time.deltaTime * speed;
	
			yield return null;
		}

		CenterTransform.rotation = Quaternion.Euler( 90 * rotationAxis.Value ); 

		for ( var i = 0; i < transformArray.Length; i++ )
		{
			transformArray[ i ].parent = transformParentArray[ i ];
		}

		CenterTransform.rotation = Quaternion.identity;

		yield return new WaitForSeconds( .1f );
		IsRotating = false;
		yield return null;
		yield return null;
	}
	
	public void Reset()
	{
		StartCoroutine( ResetCoroutine() );
	}

	public void GoatReachedEnd()
	{
		Debug.Log( "You Win!!!" );
	}

	public IEnumerator ResetCoroutine()
	{
		DeathCount++;
		HasStarted = false;

		// Reverse through each of the rotations the user has made until they're all gone.
		reverseSound.Play();
		while ( rotationHistory.Count > 0 )
		{
			yield return StartCoroutine( Rotate( rotationHistory.Pop(), RotationEnum.Counterclockwise, SpinSpeedReverse ) );
		}
		reverseSound.Stop();

		// Just set positions and rotations back tro originals for good measure.
		for ( int i = 0; i < QuadArray.Length; i ++ )
		{
			var quad = QuadArray[ i ];
			quad.transform.position = originalQuadPositions[ i ];
			quad.transform.rotation = originalQuadRotations[ i ];
		}

		HasFinished = false;
	}

	public void MasterReset()
	{
		if ( !IsRotating )
		{
			var goat = GoatCollider.GetComponent<TheGoat>();
			StartCoroutine( goat.RewindWorld() );
		}
	}
}
