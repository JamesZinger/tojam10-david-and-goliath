using System.Collections;
using System.IO.IsolatedStorage;
using UnityEngine;

public class Cube : MonoBehaviour
{
	public int deathCount;
	public RotationCollider XRotationCollider;
	public RotationCollider YRotationCollider;
	public RotationCollider ZRotationCollider;
	public Transform CenterTransform;
	public AnimationCurve RotationCurve;
	public float SpinSpeed;

	private bool isRotating = false;

	private enum RotationAxis { X, Y, Z }

	void Start()
	{
		
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
		if ( isRotating )
		{
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

		var quadList = activeCollider.collisionQuads;
		var parent = quadList[ 0 ].transform.parent;

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

		isRotating = true;
		CenterTransform.rotation = Quaternion.identity;
		for ( var i = 0; i < quadList.Count; i++ )
		{
			quadList[ i ].transform.parent = CenterTransform;
		}
		
		isRotating = true;
		var progress = 0f;

		while ( progress < 1f )
		{
			var angle = RotationCurve.Evaluate( progress );
			CenterTransform.rotation = Quaternion.Euler( angle * rotationAxis.Value );
			progress += Time.deltaTime * SpinSpeed;
	
			yield return null;
		}

		CenterTransform.rotation = Quaternion.Euler( 90 * rotationAxis.Value ); 

		Debug.Log( "Got HERE!" );
		for ( var i = 0; i < quadList.Count; i++ )
		{
			quadList[ i ].transform.parent = parent;
		deathCount++;

		}

		CenterTransform.rotation = Quaternion.identity;

		isRotating = false;
		
		yield return null;
	}
	
	public void Reset()
	{
		deathCount++;
	}
}
