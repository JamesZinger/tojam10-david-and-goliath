using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad : MonoBehaviour
{
	
	public string NodeName;
	public Node Node;

	private Cube Cube;

	public void Start()
	{
		Cube = FindObjectOfType<Cube>();

		if ( Node == null ) return;

		Transform startpoint = transform.FindChild( "Startpoint" );
		if ( startpoint != null && Node.Type == NodeTypeEnum.Start )
		{
			startpoint.gameObject.SetActive( true );
		}

		Transform endpoint = transform.FindChild( "Endpoint" );
		if ( endpoint != null && Node.Type == NodeTypeEnum.End )
		{
			endpoint.gameObject.SetActive( true );
		}

		Node.Quad = this;

		StartCoroutine( WaitForFrame() );
	}

	public IEnumerator WaitForFrame()
	{
		yield return null;

		if ( Node.MoveableDirections.Count > 1 )
		{
			

			var go = Instantiate( Cube.QuadPointerCenter ) as GameObject;

			if ( go != null )
			{
				go.transform.parent = transform;

				go.transform.localPosition = Vector3.zero;
				go.transform.localRotation = Quaternion.Euler( 90, 0, 0 );
			}

		}

		foreach ( var direction in Node.MoveableDirections )
		{
			Vector3 rotationEuler = Vector3.zero;
			switch ( direction )
			{
				case Node.Direction.up:    rotationEuler = new Vector3 { x = 0, y = 0, z = 0   }; break;
				case Node.Direction.down:  rotationEuler = new Vector3 { x = 0, y = 0, z = 180 }; break;
				case Node.Direction.right: rotationEuler = new Vector3 { x = 0, y = 0, z = 270 }; break;
				case Node.Direction.left:  rotationEuler = new Vector3 { x = 0, y = 0, z = 90  }; break;
			}

			var go = Instantiate( Cube.DirectionPointer ) as GameObject;

			if ( go == null ) continue;

			go.transform.parent = transform;

			go.transform.localRotation = Quaternion.Euler( rotationEuler );
			go.transform.localPosition = Vector3.back * 0.001f;

		}
	}
}
