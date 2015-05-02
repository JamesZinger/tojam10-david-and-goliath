using System.Collections;
using UnityEngine;

[SelectionBase]
public class TheGoat: MonoBehaviour
{
	public float MoveSpeed = 1;

	void Start()
	{
		StartCoroutine( UpdateCoroutine() );
	}

	IEnumerator UpdateCoroutine()
	{
		while ( true )
		{
			// Check that a quad is under the goat
			var ray = new Ray( transform.position, -transform.up );
			var layerMask = ~LayerMask.GetMask( "Rotaters" );
			// Debug.DrawRay( ray.origin, ray.direction, Color.blue );
			
			RaycastHit rayHitInfo;
			var didHit = Physics.Raycast( ray, out rayHitInfo, 1f, layerMask );
			if ( didHit )
			{
				transform.position = rayHitInfo.point + transform.up * 0.05f;
				
				// then continue moving forward
				ContinueMoving();
				yield return null;
				continue;
			}

			ray.origin += transform.up * -0.5f;
			Vector3[] directions = {
				transform.right,
				-transform.right,
				transform.forward,
				-transform.forward
			};

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
			if ( go.tag != "Active Quad" )
			{
				Kill();
				yield return null;
				continue;
			}

			var downVector = -transform.up;

			// rotate to align with the normal of the quad.
			transform.position += downVector * 0.05f;
			transform.rotation = Quaternion.LookRotation( downVector, rayHitInfo.normal );
			yield return null;
		}
	}

	void Kill()
	{

	}

	void ContinueMoving()
	{
		transform.position += transform.forward * Time.deltaTime * MoveSpeed;
	}
}
