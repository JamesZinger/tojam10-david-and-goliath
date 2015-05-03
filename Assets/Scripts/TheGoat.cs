using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[SelectionBase]
public class TheGoat: MonoBehaviour
{
	public float MoveSpeed = 1;
	
	public Vector3 StartPosition { get; private set; }

	public Quaternion StartRotation { get; private set; }

	private Animator animator;

	private IEnumerator updateHandle;
	private Cube cube;
	private int layerMask;
	private int pathLayerMask;
	private const float HitNormalThreshold = -0.5f;
	private bool isDeathComplete;
	private bool isDeathCoroutineRunning;
	private Node.Direction currentDirection;
	private Vector3 currentDirectionVector;
	private Collider prevCenterHit;
	private bool hasReachedEnd;
	private bool hasBSCoroutineFinished;

	private AudioSource deathSound;
	
	[NonSerialized]
	public AudioSource MoveSound;

	[NonSerialized]
	public AudioSource StartSound;

	void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		layerMask = ~LayerMask.GetMask( "Rotaters", "Goat Ignored" );
		pathLayerMask = LayerMask.GetMask( "Paths" );
		hasBSCoroutineFinished = true;
	}

	public void Configure( Cube realCube )
	{
		Transform t = transform.FindChild( "DeathSound" );
		if ( t != null )
		{
			deathSound = t.audio;
		}		
		t = transform.FindChild( "MoveSound" );
		if ( t != null )
		{
			MoveSound = t.audio;
		}
		t = transform.FindChild( "StartSound" );
		if ( t != null )
		{
			StartSound = t.audio;
		}
		// Determine start node
		cube = realCube;

		var startNode = cube.Graph.Nodes.Cast<Node>()
			.Single( node => node.Type == NodeTypeEnum.Start );

		transform.position = startNode.Quad.transform.position + startNode.Quad.transform.up * 0.1f;

		var meshCenter = GetComponentInChildren<MeshRenderer>().bounds.center;
		var ray = new Ray( meshCenter, -meshCenter.normalized );
		Debug.DrawRay( ray.origin, ray.direction, Color.red, 1000 );
		var hits = Physics.RaycastAll( ray, 1.5f, layerMask )
			.Where( h => h.collider.gameObject.GetComponent<Quad>() != null )
			.Where( h => h.normal.x > HitNormalThreshold )
			.Where( h => h.normal.y > HitNormalThreshold )
			.Where( h => h.normal.z > HitNormalThreshold )
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
		yield return null;
		yield return null;

		if ( cube == null )
			cube = FindObjectOfType<Cube>();

		while ( true )
		{
			if ( cube.IsRotating )
			{
				yield return null;
				continue;
			}
			if ( isDeathCoroutineRunning )
			{
				yield return null;
				continue;
			}
			if ( hasReachedEnd )
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
				.Where( hit => hit.normal.x > HitNormalThreshold )
				.Where( hit => hit.normal.y > HitNormalThreshold )
				.Where( hit => hit.normal.z > HitNormalThreshold )
				.ToArray();

			if ( hits.Length > 0 )
			{
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
				StartCoroutine( Die() );
				while ( !isDeathComplete )
					yield return null;
				isDeathComplete = false;
				continue;
			}
		
			var downVector = -transform.up;

			// Move and rotate to align with the normal of the quad.
			transform.position += downVector * 0.07f;
			transform.rotation = Quaternion.LookRotation( downVector, hitInfo.normal );
			yield return null;
		}
	}

	IEnumerator Die()
	{
		isDeathCoroutineRunning = true;
 		Debug.Log( "Goat is dead" );
		deathSound.Play();
		MoveSound.Stop();
		animator.SetBool( "isDead", true );
		cube.Reset();
		Reset();
		yield return null;
		isDeathComplete = true;
		animator.SetBool( "isDead", false );
		isDeathCoroutineRunning = false;
	}

	public void Reset()
	{
		transform.position = StartPosition;
		transform.rotation = StartRotation;
		prevCenterHit = null;
		hasReachedEnd = false;
	}


	void NormalMovingBehaviour( RaycastHit rayHitInfo )
	{
		// If there is a quad underneath the goat than ground it.
		transform.position = rayHitInfo.point + transform.up * 0.05f;
		transform.rotation = Quaternion.LookRotation( transform.forward, rayHitInfo.normal );

		if ( !cube.HasStarted ) return;
		
		// Determine the move direction
		// get the quad object underneath the GOAT
		//var quad = rayHitInfo.collider.GetComponent<Quad>();

		var ray = new Ray( transform.position + ( transform.up * 0.05f ), -transform.up );
		var hits = Physics.RaycastAll( ray, 0.1f, pathLayerMask )
			.Where( h => h.normal.x > HitNormalThreshold )
			.Where( h => h.normal.y > HitNormalThreshold )
			.Where( h => h.normal.z > HitNormalThreshold )
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
				.ToArray();

			var turns = dotpPairs
				.Where( dotPair => dotPair.Key > 0.25f )
				.Where( dotPair => dotPair.Key < 0.75f )
				.ToArray();

			if ( turns.Length > 0 )
			{
				Debug.Log( "Goat is turning" );
				Debug.Log( turns.First().Value );
				transform.rotation = turns.First().Value;
				prevCenterHit = sphereHit.collider;
			}

			else
			{
				var straightLines = dotpPairs
					.Where( dotpPair => dotpPair.Key >= 0.75f )
					.ToArray();
				if ( straightLines.Length > 0 )
				{
					Debug.Log( "Goat is going straight" );
					transform.rotation = straightLines.First().Value;
					prevCenterHit = sphereHit.collider;
				}
			}
		}
		else if ( sphereHits.Length == 1 && hits.Length == 2 )
		{
			
			
			transform.position += transform.forward * Time.deltaTime * MoveSpeed;	
		}
		else if ( hits.Length == 0 )
		{

			var magnitude = ( rayHitInfo.collider.bounds.center - transform.position ).magnitude;

			if ( magnitude < 0.1f )
			{
				var quad = rayHitInfo.collider.GetComponent<Quad>();
				Debug.Log( "IS IT THE END" );
				if ( quad.Node.Type == NodeTypeEnum.End )
				{
					Debug.Log( "YUP" );
					cube.GoatReachedEnd();
					hasReachedEnd = true;
					return;
				}
			}
			else
			{
				isDeathComplete = false;
				StartCoroutine( Die() );
				return;
			}
		}
		{
			// for now just go forward.
			transform.position += transform.forward * Time.deltaTime * MoveSpeed;
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
		var hits = Physics.RaycastAll( ray, 1f, layerMask );

		return hits;
	}

	IEnumerator BSCoroutine()
	{
		hasBSCoroutineFinished = false;
		yield return null;
		yield return null;
		prevCenterHit = null;
		hasBSCoroutineFinished = true;
	}
}
