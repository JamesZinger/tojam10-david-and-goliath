using UnityEngine;

public class Quad : MonoBehaviour
{
	public string NodeName;
	public Node Node;

	public void Start()
	{
		if ( Node != null )
		{
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
		}
	}
}
