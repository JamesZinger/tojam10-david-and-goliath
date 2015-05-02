using System.Collections.Generic;
using UnityEngine;

public class RotationCollider : MonoBehaviour
{

	public List<GameObject> collisionQuads;

	void Awake()
	{
		collisionQuads = new List<GameObject>();
	}

	void OnTriggerEnter( Collider c )
	{
		if ( collisionQuads.Contains( c.gameObject ) ) return;

		collisionQuads.Add( c.gameObject );
	}

	void OnTriggerExit( Collider c )
	{
		Debug.Log( "Exiting!" );
		collisionQuads.Remove( c.gameObject );
	}
}
