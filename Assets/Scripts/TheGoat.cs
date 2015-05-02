using System.Collections;
using UnityEngine;

[SelectionBase]
public class TheGoat: MonoBehaviour
{
	private Cube Cube;
	public float MoveSpeed = 1;

	private IEnumerator updateHandle;

	void Start()
	{
		Cube = FindObjectOfType<Cube>();
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
		if ( Cube == null )
			yield return null;
		while ( true )
		{
			// Check that a quad is under the goat
			var ray = new Ray( transform.position, -transform.up );
			
			// Setup mask to ignore the Rotaters Physics layer
			var layerMask = ~LayerMask.GetMask( "Rotaters", "Goat Ignored" );
			// Debug.DrawRay( ray.origin, ray.direction, Color.blue );
			
			RaycastHit rayHitInfo;
			// Check if there is a quad underneath the goat
			var didHit = Physics.Raycast( ray, out rayHitInfo, 1f, layerMask );
			if ( didHit )
			{
				// If there is a quad underneath the goat than ground it.
				transform.position = rayHitInfo.point + transform.up * 0.05f;
				transform.rotation = Quaternion.LookRotation( transform.forward, rayHitInfo.normal );
				
				// then continue moving forward
				ContinueMoving();
				yield return null;
				continue;
			}

			Debug.Log( "Goat is off of the edge" );

			// move the ray origin to father down the old ray to prepare for the sampling of each direction
			ray.origin += transform.up * -0.5f;
			Vector3[] directions = {
				transform.right,
				-transform.right,
				transform.forward,
				-transform.forward
			};

			// Sample each direction from the point where it went off to check if there is a plane there.
			for ( var i = 0; i < directions.Length; i++ )
			{
				ray.direction = directions[ i ];
				// Debug.DrawRay( ray.origin, ray.direction, Color.red );
				didHit = Physics.Raycast( ray, out rayHitInfo, 1f, layerMask );
				if ( didHit )
				{
					break;
				}
			}

			if ( !didHit )
			{
				// Something weird happened but prolly should just kill the goat.
				Kill();
				yield return null;
				continue;
			}

			// check if the plane that was collided with is a valid plane to move to.
			var go = rayHitInfo.collider.gameObject;
			//if ( go.tag != "Active Quad" )
			//{
			//	// than the goat has run off of the edge of the map
			//	// therefore it must die because the plane closest to it is not an active quad
			//	Kill();
			//	yield return null;
			//	continue;
			//}

			var downVector = -transform.up;

			// Move and rotate to align with the normal of the quad.
			transform.position += downVector * 0.07f;
			transform.rotation = Quaternion.LookRotation( downVector, rayHitInfo.normal );
			yield return null;
		}
	}

	void Kill()
	{
		Cube.Reset();
	}

	void ContinueMoving()
	{
		if ( Cube.IsRotating ) return;
		// for now just go forward.
		transform.position += transform.forward * Time.deltaTime * MoveSpeed;
	}
}
