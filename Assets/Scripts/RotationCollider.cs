using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RotationCollider : MonoBehaviour
{
	public Cube.RotationAxis Axis;

	private Cube Cube;
	
	private void Awake()
	{
		Cube = FindObjectOfType<Cube>();
	}

	public GameObject[] GetAllObjectsToMove()
	{
		var results = Cube.QuadArray
			.Where( quad => collider.bounds.Contains( quad.transform.position ) )
			.Select( quad => quad.gameObject )
			.ToList();
		
		var ray = new Ray( Cube.GoatCollider.transform.position + Cube.GoatCollider.transform.up, -Cube.GoatCollider.transform.up );
		var layerMask = LayerMask.GetMask( "Rotaters" );

		var didHit = Physics.RaycastAll( ray, 1.5f, layerMask );
		if ( didHit.Length > 0 )
		{
			results.AddRange(
				didHit.Where( hit => hit.collider.gameObject == gameObject )
				.Select( hit => Cube.GoatCollider.gameObject )
			);
		}
		return results.ToArray();
	}
}
