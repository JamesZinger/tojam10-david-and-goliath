using System.Collections;
using UnityEngine;

public class Quad : MonoBehaviour
{
	
	public string NodeName;
	public Node Node;

	private Cube Cube;

	public void Configure()
	{
		Cube = FindObjectOfType<Cube>();

		if ( Node == null ) return;

		if ( Node.Type == NodeTypeEnum.Start )
		{
			var go = Instantiate( Cube.StartPointPrefab ) as GameObject;
			if ( go != null )
			{
				go.transform.parent = transform;
				go.transform.localPosition = Vector3.zero;
				go.transform.localEulerAngles = new Vector3( -90, 0, 0 );
			}

		}
		else if ( Node.Type == NodeTypeEnum.End )
		{
			var go = Instantiate( Cube.EndPointPrefab ) as GameObject;
			if ( go != null )
			{
				go.transform.parent = transform;
				go.transform.localPosition = Vector3.zero;
				go.transform.localEulerAngles = new Vector3( -90, 0, 0 );
			}
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
