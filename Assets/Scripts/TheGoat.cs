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

	private Animator animator;

	private IEnumerator updateHandle;
	private Cube cube;
	private int layerMask;
	private const float HitNormalThreshold = -0.5f;
	private bool isDeathComplete;

	private AudioSource deathSound;
	
	[NonSerialized]
	public AudioSource MoveSound;

	[NonSerialized]
	public AudioSource StartSound;

	void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		layerMask = ~LayerMask.GetMask( "Rotaters", "Goat Ignored" );
	}

	void Start()
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
		cube = FindObjectOfType<Cube>();

		var startNode = cube.Graph.Nodes.Cast<Node>()
			.Single( node => node.Type == NodeTypeEnum.Start );

		transform.position = startNode.Quad.transform.position + startNode.Quad.transform.up * 0.1f;

		var meshCenter = GetComponentInChildren<MeshRenderer>().bounds.center;
		var ray = new Ray( meshCenter, -meshCenter.normalized );
		Debug.DrawRay( ray.origin, ray.direction, Color.red, 1000 );
		var hits = Physics.RaycastAll( ray, 1.5f, layerMask )
			.Where( hit => hit.collider.gameObject.GetComponent<Quad>() != null )
			.Where( hit => hit.normal.x > HitNormalThreshold )
			.Where( hit => hit.normal.y > HitNormalThreshold )
			.Where( hit => hit.normal.z > HitNormalThreshold )
			.ToArray();

		if ( hits.Length == 0 )
		{
			Debug.LogError( "TheGoat is not on a Quad. Dafuq" );
			gameObject.SetActive( false );
			return;
		}
		StartPosition = transform.position;
		StartRotation = transform.rotation = Quaternion.LookRotation( transform.forward, hits[ 0 ].normal );
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
 		Debug.Log( "Goat is dead" );
		deathSound.Play();
		MoveSound.Stop();
		animator.SetBool( "isDead", true );
		cube.Reset();
		Reset();
		yield return null;
		// DDDOOO EETTTt

		isDeathComplete = true;
		animator.SetBool( "isDead", false );
	}

	public void Reset()
	{
		transform.position = StartPosition;
		transform.rotation = StartRotation;
	}


	void NormalMovingBehaviour( RaycastHit rayHitInfo )
	{
		// If there is a quad underneath the goat than ground it.
		transform.position = rayHitInfo.point + transform.up * 0.05f;
		transform.rotation = Quaternion.LookRotation( transform.forward, rayHitInfo.normal );


		if ( !cube.HasStarted ) return;
		// for now just go forward.
		transform.position += transform.forward * Time.deltaTime * MoveSpeed;
	}

	public RaycastHit[] RaycastCube()
	{
		// Check that a quad is under the goat
		var ray = new Ray( transform.position, -transform.up );
		
		// Check if there is a quad underneath the goat
		var hits = Physics.RaycastAll( ray, 1f, layerMask );

		return hits;
	}
}
