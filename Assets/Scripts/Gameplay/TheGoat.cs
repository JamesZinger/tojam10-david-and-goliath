using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[SelectionBase]
public class TheGoat: MonoBehaviour
{
	private const float hitNormalThreshold = -0.5f;

	
	public bool FUCKTHEGOAT;
	public bool IsDeathCoroutineRunning;
	public float MoveSpeed = 1;
	public AudioSource DeathSound;
	public AudioSource MoveSound;
	public AudioSource StartSound;

	
	private Animator canvasAnimator;
	private Cube cube;
	private Node.Direction currentDirection;
	private Vector3 currentDirectionVector;
	private bool hasBSCoroutineFinished;
	private bool hasReachedEnd;
	private int layerMask;
	private int pathLayerMask;
	private Collider prevCenterHit;
	private IEnumerator updateHandle;

	
	public bool IsRewinding { get; private set; }
	public Vector3 StartPosition { get; private set; }
	public Quaternion StartRotation { get; private set; }


	#region Init

	void Awake()
	{
		//animator = GetComponentInChildren<Animator>();
		layerMask = LayerMask.GetMask( "Quads" );
		pathLayerMask = LayerMask.GetMask( "Paths" );
		hasBSCoroutineFinished = true;
	}


	public void Configure( Cube realCube )
	{
		Transform t = transform.FindChild( "Icon" ).FindChild( "Canvas" );
		if ( t != null )
		{
			canvasAnimator = t.GetComponent<Animator>();
		}

		// Determine start node
		cube = realCube;

		var startNode = cube.Graph.Nodes.Cast<Node>()
			.Single( node => node.Type == NodeTypeEnum.Start );

		transform.position = startNode.Quad.transform.position + -startNode.Quad.transform.forward * 0.01f;

		var meshCenter = GetComponentInChildren<MeshRenderer>().bounds.center;
		var ray = new Ray( meshCenter, -meshCenter.normalized );
		Debug.DrawRay( ray.origin, ray.direction, Color.red, 1000 );
		var hits = Physics.RaycastAll( ray, 1.5f, layerMask )
			.Where( h => h.collider.gameObject.GetComponent<Quad>() != null )
			.Where( h => h.normal.x > hitNormalThreshold )
			.Where( h => h.normal.y > hitNormalThreshold )
			.Where( h => h.normal.z > hitNormalThreshold )
			.ToArray();

		if ( hits.Length == 0 )
		{
			Debug.LogError( "TheGoat is not on a Quad. Dafuq" );
			gameObject.SetActive( false );
			return;
		}

		var hit = hits.First();

		var quad = hit.collider.gameObject.GetComponent<Quad>();

		currentDirection = quad.Node.MoveableDirections.First();

		StartPosition = transform.position;

		Vector3 directionVector = Vector3.zero;
		switch ( currentDirection )
		{
			case Node.Direction.up: directionVector = quad.transform.up; break;
			case Node.Direction.down: directionVector = -quad.transform.up; break;
			case Node.Direction.right: directionVector = quad.transform.right; break;
			case Node.Direction.left: directionVector = -quad.transform.right; break;
		}

		StartRotation = transform.rotation = Quaternion.LookRotation( directionVector, hit.normal );
	}

	#endregion


	#region Enable/Disable

	void OnEnable()
	{
		updateHandle = UpdateCoroutine();
		StartCoroutine( updateHandle );
	}

	void OnDisable()
	{
		StopCoroutine( updateHandle );
	}

	#endregion


	#region Helpers and Coroutines

	IEnumerator BSCoroutine()
	{
		hasBSCoroutineFinished = false;
		yield return null;
		yield return null;
		prevCenterHit = null;
		hasBSCoroutineFinished = true;
	}


	IEnumerator Die()
	{
		IsDeathCoroutineRunning = true;
 		Debug.Log( "Goat is dead" );
		DeathSound.Play();
		MoveSound.Stop();
		yield return StartCoroutine( RewindWorld() );
		IsDeathCoroutineRunning = false;
	}


	void NormalMovingBehaviour( RaycastHit rayHitInfo )
	{
		// If there is a quad underneath the goat than ground it.
		transform.position = rayHitInfo.point + transform.up * 0.01f;
		transform.rotation = Quaternion.LookRotation( transform.forward, rayHitInfo.normal );

		if ( !cube.HasStarted ) return;
		
		// Determine the move direction
		// get the quad object underneath the GOAT
		//var quad = rayHitInfo.collider.GetComponent<Quad>();

		var ray = new Ray( transform.position + ( transform.up * 0.05f ), -transform.up );
		Debug.DrawRay( ray.origin, ray.direction * 100, Color.blue );
		var hits = Physics.RaycastAll( ray, 0.1f, pathLayerMask )
			.Where( h => h.normal.x > hitNormalThreshold )
			.Where( h => h.normal.y > hitNormalThreshold )
			.Where( h => h.normal.z > hitNormalThreshold )
			.Where( hit => Mathf.Abs( Vector3.Dot( hit.normal, ray.direction ) ) > 0.25 )
			.ToArray();

		var sphereHits = hits
			.Where( h => h.collider is SphereCollider )
			.Where( h => h.collider != prevCenterHit )
			.ToArray();

		if ( sphereHits.Length == 1 && hits.Length == 3 && prevCenterHit == null )
		{
			var sphereHit = sphereHits.First();

			var quad = rayHitInfo.collider.GetComponent<Quad>();

			var dotpPairs = quad.Node.MoveableDirections
				.Select( d =>
				{
					Vector3 dirV = Vector3.zero;
					switch ( d )
					{
						case Node.Direction.up:
							dirV = quad.transform.up * 90;
							break;
						case Node.Direction.down:
							dirV = -quad.transform.up * 90;
							break;
						case Node.Direction.right:
							dirV = quad.transform.right * 90;
							break;
						case Node.Direction.left:
							dirV = -quad.transform.right * 90;
							break;
					}

					var quart = Quaternion.LookRotation( dirV, -quad.transform.forward );
					return new KeyValuePair<float, Quaternion>( Quaternion.Dot( transform.rotation, quart ), quart );
				} )
				.Where( pair => Math.Abs( pair.Key ) > 0.1f  )
				.ToArray();

			var turns = dotpPairs
				.Where( dotPair => Math.Abs( dotPair.Key ) > 0.25f )
				.Where( dotPair => Math.Abs( dotPair.Key ) < 0.75f )
				.ToArray();
			
			if ( turns.Length > 0 )
			{
				Debug.Log( "Goat is turning" );
				transform.rotation = turns.First().Value;
				prevCenterHit = sphereHit.collider;
			}

			else
			{
				var straightLines = dotpPairs
					.ToArray();

				if ( straightLines.Length > 0 )
				{
					Debug.Log( "Goat is going straight" );
					prevCenterHit = sphereHit.collider;
				}
			}
		}
		else if ( sphereHits.Length == 1 && hits.Length == 2 )
		{
			
			
			transform.position += transform.forward * Time.fixedDeltaTime * MoveSpeed;	
		}
		else if ( hits.Length == 0 )
		{

			var magnitude = ( rayHitInfo.collider.bounds.center - transform.position ).magnitude;
			Debug.Log( "hit no paths" );
			if ( magnitude < 0.1f )
			{
				var quad = rayHitInfo.collider.GetComponent<Quad>();
				Debug.Log( "IS IT THE END" );
				switch ( quad.Node.Type )
				{
					case NodeTypeEnum.End:
						Debug.Log( "YUP" );
						cube.GoatReachedEnd();
						hasReachedEnd = true;
						return;
					case NodeTypeEnum.Start:
						StartCoroutine( Die() );
						return;
				}

				transform.position = rayHitInfo.collider.transform.position + transform.up * 0.01f;
			}
			else
			{
				var ray2 = new Ray( transform.position + ( transform.up * 0.05f ) + ( -transform.forward * 0.05f ), -transform.up );
				Debug.DrawRay( ray2.origin, ray2.direction * 100, Color.red );
				if ( Physics.Raycast( ray2, 0.5f, pathLayerMask ) )
				{
					transform.position += transform.forward * Time.fixedDeltaTime * MoveSpeed;
					return;
				}
				StartCoroutine( Die() );
				return;
			}
		}
		else if ( !IsRewinding )
		{

			// for now just go forward.
			transform.position += transform.forward * Time.fixedDeltaTime * MoveSpeed;
		}

		if ( sphereHits.Length == 0 && hasBSCoroutineFinished && prevCenterHit != null )
		{
			StartCoroutine( BSCoroutine() );
		}
	}

	
	public RaycastHit[] RaycastCube()
	{
		// Check that a quad is under the goat
		var ray = new Ray( transform.position, -transform.up );
		
		// Check if there is a quad underneath the goat
		var hits = Physics.RaycastAll( ray, 0.5f, layerMask );

		return hits;
	}


	public void Reset()
	{
		transform.position = StartPosition;
		transform.rotation = StartRotation;
		prevCenterHit = null;
		hasReachedEnd = false;
	}


	public IEnumerator RewindWorld()
	{
		IsRewinding = true;
		canvasAnimator.SetBool( "isDead", true );
		yield return new WaitForSeconds( 1f );
		yield return StartCoroutine( cube.ResetCoroutine() );
		transform.position = StartPosition;
		Reset();
		canvasAnimator.SetBool( "isDead", false );
		while ( true )
		{
			var aniState = canvasAnimator.GetCurrentAnimatorStateInfo( 0 );
			if ( aniState.IsName( "GoatDead" ) || aniState.IsName( "GoatMove" ) )
				break;
			yield return null;
		}
		IsRewinding = false;
	}


	IEnumerator UpdateCoroutine()
	{
		yield return null;
		yield return null;

		if ( cube == null )
			cube = FindObjectOfType<Cube>();

		while ( true )
		{
			if ( FUCKTHEGOAT )
			{
				yield return new WaitForFixedUpdate();
				continue;
			}
			if ( cube.IsRotating )
			{
				yield return new WaitForFixedUpdate();
				continue;
			}
			if ( IsDeathCoroutineRunning )
			{
				yield return new WaitForFixedUpdate();
				continue;
			}
			if ( hasReachedEnd )
			{
				yield return new WaitForFixedUpdate();
				continue;
			}


			// Check that a quad is under the goat
			var ray = new Ray( transform.position + ( transform.up * 0.2f ), -transform.up );
			Debug.DrawRay( ray.origin, ray.direction, Color.green );
			var originalHits = RaycastCube();
			var hits = originalHits
				.Where( hit => hit.collider.gameObject.GetComponent<Quad>() != null )
				.Where( hit => hit.normal.x > hitNormalThreshold )
				.Where( hit => hit.normal.y > hitNormalThreshold )
				.Where( hit => hit.normal.z > hitNormalThreshold )
				.Where( hit => Mathf.Abs( Vector3.Dot( hit.normal, ray.direction ) ) > 0.25 )
				.ToArray();

			if ( hits.Length > 0 )
			{
				// then continue moving forward
				NormalMovingBehaviour( hits [ 0 ] );
				yield return new WaitForFixedUpdate();
				continue;
			}

			//Debug.DrawRay( ray.origin, ray.direction * 100, Color.blue, 1000 );

			// move the ray origin to father down the old ray to prepare for the sampling of each direction
			ray.origin += transform.up * -0.5f;
			ray.origin += transform.forward * 0.02f;
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
				Debug.DrawRay( ray.origin, ray.direction, Color.red );
				didHit = Physics.Raycast( ray, out hitInfo, 1f, layerMask );

				if ( didHit )
				{
					break;
				}
			}
			if ( !didHit )
			{ 
				Debug.Log( "Death due to no quads" );
				yield return StartCoroutine( Die() );
				continue;
			}

			Debug.Log( "Edge case" );
		
			var downVector = -transform.up;

				// Move and rotate to align with the normal of the quad.
			transform.position += downVector * 0.07f;
			transform.rotation = Quaternion.LookRotation( downVector, hitInfo.normal );//-hitInfo.collider.GetComponent<Quad>().transform.up );
			
			yield return new WaitForFixedUpdate();
		}
	}

	#endregion
}
