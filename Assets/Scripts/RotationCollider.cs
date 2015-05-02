using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RotationCollider : MonoBehaviour
{

	private Cube Cube;
	
	private void Awake()
	{
		Cube = FindObjectOfType<Cube>();
	}

	public GameObject[] GetQuadsInCollision()
	{
		return Cube.QuadList
			.Where( quad => collider.bounds.Contains( quad.transform.position ) )
			.ToArray();
	}

	public GameObject[] GetOthersInCollision()
	{
		List<GameObject> results = new List<GameObject>();

		if ( Cube.EndPointCollider.bounds.Intersects( collider.bounds ) )
		{
			results.Add( Cube.EndPointCollider.gameObject );
		}

		if ( Cube.StartPointCollider.bounds.Intersects( collider.bounds ) )
		{
			results.Add( Cube.StartPointCollider.gameObject );
		}

		if ( Cube.GoatCollider.renderer.bounds.Intersects( collider.bounds ) )
		{
			results.Add( Cube.GoatCollider.transform.parent.gameObject );
		}

		return results.ToArray();
	}
}
