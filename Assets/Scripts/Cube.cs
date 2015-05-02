using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cube : MonoBehaviour
{
	public int deathCount;
	public Collider GoatCollider;
	public Collider StartPointCollider;
	public Collider EndPointCollider;
	public RotationCollider XRotationCollider;
	public RotationCollider YRotationCollider;
	public RotationCollider ZRotationCollider;
	public Transform CenterTransform;
	public AnimationCurve RotationCurve;
	public float SpinSpeed;
	public Graph graph;

	public Quad[] QuadArray;

	public bool IsRotating { get; private set; }

	private enum RotationAxis { X, Y, Z }

	void Awake()
	{
		graph = new Graph( 4, 8 );
	}

	void Start()
	{
		QuadArray = GetComponentsInChildren<Quad>();
		int x = 0, y = 0;
		for ( int i = 0; i < QuadArray.Length; i++ )
		{
			var childQuad = QuadArray[ i ];

			childQuad.Node = graph.Nodes[ x, y ];
			y++;
			var width = graph.Nodes.GetLength( 1 );
			if ( y == width )
			{
				x++;
				y = 0;
			}
		}
		
	}

	void Update()
	{

	}

	public void RotateX()
	{
		StartCoroutine( Rotate( RotationAxis.X ) );
	}

	public void RotateY()
	{
		StartCoroutine( Rotate( RotationAxis.Y ) );
	}

	public void RotateZ()
	{
		StartCoroutine( Rotate( RotationAxis.Z ) );
	}

	IEnumerator Rotate( RotationAxis axis )
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
		switch ( axis )
		{
			case RotationAxis.X: rotationAxis = new Vector3( x: 1, y: 0, z: 0 ); break;
			case RotationAxis.Y: rotationAxis = new Vector3( x: 0, y: 1, z: 0 ); break;
			case RotationAxis.Z: rotationAxis = new Vector3( x: 0, y: 0, z: 1 ); break;
		}

		if ( rotationAxis == null )
		{
			Debug.Log( "WWWHHAATTT" );
			yield break;
		}

		
		IsRotating = true;
		CenterTransform.rotation = Quaternion.identity;
		for ( var i = 0; i < transformArray.Length; i++ )
		{
			transformArray[ i ].parent = CenterTransform;
		}

		IsRotating = true;
		var progress = 0f;

		while ( progress < 1f )
		{
			var angle = RotationCurve.Evaluate( progress );
			CenterTransform.rotation = Quaternion.Euler( angle * rotationAxis.Value );
			progress += Time.deltaTime * SpinSpeed;
	
			yield return null;
		}

		CenterTransform.rotation = Quaternion.Euler( 90 * rotationAxis.Value ); 

		for ( var i = 0; i < transformArray.Length; i++ )
		{
			transformArray[ i ].parent = transformParentArray[ i ];
		}

		CenterTransform.rotation = Quaternion.identity;

		IsRotating = false;
		yield return null;
	}
	
	public void Reset()
	{
		deathCount++;
	}
}
