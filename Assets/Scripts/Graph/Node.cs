using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Node
{
	public List<Node> Neighbors;
	public NodeTypeEnum Type;

	public Node()
	{
		Neighbors = new List<Node>();
		Type = NodeTypeEnum.Normal;
	}

	public void AddNeighbor( Node neighbor )
	{
		if ( !Neighbors.Contains<Node>( neighbor ) )
		{
			Neighbors.Add( neighbor );
		}
	}

	public void ClearNeighbors()
	{
		Neighbors.Clear();
	}

	public void ReplaceNeighbor( Node neighbor, Node toReplace )
	{
		if ( Neighbors.Contains<Node>( neighbor ) )
		{
			Neighbors.Remove( toReplace );
			Neighbors.Add( neighbor );
		}
	}
}
