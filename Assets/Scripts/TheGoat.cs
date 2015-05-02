using System;
using System.Collections;
using System.Linq;
using UnityEngine;

[SelectionBase]
public class TheGoat: MonoBehaviour
{
	public float MoveSpeed = 1;

	public Vector3 StartPosition { get; private set; }

	public Quaternion StartRotation { get; private set; }

	private IEnumerator updateHandle;
	private Cube cube;
	private int layerMask;

	void Awake()
	{
		layerMask = ~LayerMask.GetMask( "Rotaters", "Goat Ignored" );
	}

	void Start()
	{
		var hits = RaycastCube()
			.Where( hit => hit.collider.gameObject.GetComponent<Quad>() != null )
			.Where( hit => hit.normal.x > -0.5f )
			.Where( hit => hit.normal.y > -0.5f )
			.Where( hit => hit.normal.z > -0.5f )
			.ToArray();

		if ( hits.Length == 0 )
		{
			Debug.LogError( "TheGoat is not on a Quad. Dafuq" );
			gameObject.SetActive( false );
			return;
		}

		StartPosition = transform.position;
		StartRotation = transform.rotation = Quaternion.LookRotation( transform.forward, hits[ 0 ].normal );
		cube = FindObjectOfType<Cube>();
	}

	void OnEnable()
	{
		updateHandle = UpdateCoroutine();
		StartCoroutine( updateHandle );
	}

	void OnDisable()
	{
		StopCoroutine( updateHandle );
	}

	IEnumerator UpdateCoroutine()
	{
		if ( cube == null )
			yield return null;

		while ( true )
		{
			if ( cube.IsRotating )
			{
				yield return null;
				continue;
			}
			// Check that a quad is under the goat
			var ray = new Ray( transform.position + ( transform.up * 0.2f ), -transform.up );
			Debug.DrawRay( ray.origin, ray.direction, Color.green );
			var originalHits = RaycastCube();
			var hits = originalHits
				.Where( hit => hit.collider.gameObject.GetComponent<Quad>() != null )
				.Where( hit => hit.normal.x > -0.5f )
				.Where( hit => hit.normal.y > -0.5f )
				.Where( hit => hit.normal.z > -0.5f )
				.ToArray();

			if ( hits.Length > 0 )
			{
				var shouldLive = IsOnValidSurface( hits[ 0 ] );

				if ( !shouldLive  )
				{
					Kill();
					yield return null;
					continue;
				}
				
				// then continue moving forward
				NormalMovingBehaviour( hits [ 0 ] );
				yield return null;
				continue;
			}

			//Debug.DrawRay( ray.origin, ray.direction * 100, Color.blue, 1000 );

			// move the ray origin to father down the old ray to prepare for the sampling of each direction
			ray.origin += transform.up * -0.5f;
			Vector3[] directions = {
				transform.right,
				-transform.right,
				transform.forward,
				-transform.forward
			};

			var didHit = false;

			RaycastHit hitInfo = new RaycastHit();

			// Sample each direction from the point where it went off to check if there is a plane there.
			for ( var i = 0; i < directions.Length; i++ )
			{
				ray.direction = directions[ i ];
				// Debug.DrawRay( ray.origin, ray.direction, Color.red );
				didHit = Physics.Raycast( ray, out hitInfo, 1f, layerMask );

				if ( didHit )
				{
					break;
				}
			}

			if ( !didHit )
			{
				for ( var i = 0; i < directions.Length; i++ )
				{
					Debug.DrawRay( ray.origin, directions[ i ] * 2, Color.red, 1000 );
				}
				for ( var i = 0; i < originalHits.Length; i++ )
				{
					Debug.Log( "Hit: " + originalHits [ i ] );
				}
				// Something weird happened but prolly should just kill the goat.
				Kill();
				yield return null;
				continue;
			}
		
			var downVector = -transform.up;

			// Move and rotate to align with the normal of the quad.
			transform.position += downVector * 0.07f;
			transform.rotation = Quaternion.LookRotation( downVector, hitInfo.normal );
			yield return null;
		}
	}

	void Kill()
	{
 		Debug.Log( "Goat is dead" );
		transform.position = StartPosition;
		transform.rotation = StartRotation;
		cube.Reset();
	}

	void NormalMovingBehaviour( RaycastHit rayHitInfo )
	{
		// If there is a quad underneath the goat than ground it.
		transform.position = rayHitInfo.point + transform.up * 0.05f;
		transform.rotation = Quaternion.LookRotation( transform.forward, rayHitInfo.normal );
		
		// for now just go forward.
		transform.position += transform.forward * Time.deltaTime * MoveSpeed;
	}

	public RaycastHit[] RaycastCube()
	{
		// Check that a quad is under the goat
		var ray = new Ray( transform.position, -transform.up );

		RaycastHit rayHitInfo;
		// Check if there is a quad underneath the goat
		var hits = Physics.RaycastAll( ray, 1f, layerMask );

		return hits;
	}

	bool IsOnValidSurface( RaycastHit rayHitInfo )
	{
		var nonZeroNormalNumber = 0f;
		if ( Math.Abs( rayHitInfo.normal.x )      > .5f ) nonZeroNormalNumber = rayHitInfo.normal.x;
		else if ( Math.Abs( rayHitInfo.normal.y ) > .5f ) nonZeroNormalNumber = rayHitInfo.normal.y;
		else if ( Math.Abs( rayHitInfo.normal.z ) > .5f ) nonZeroNormalNumber = rayHitInfo.normal.z;

		if ( Math.Abs( nonZeroNormalNumber ) < float.Epsilon )
		{
			Debug.Log( "Something weird happened" );
			return false;
		}

		return !( nonZeroNormalNumber < 0 );
	}
}
