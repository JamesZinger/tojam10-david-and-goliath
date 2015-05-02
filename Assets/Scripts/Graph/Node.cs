using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

//[Serializable]
public class Node
{
	public string Name;
	public NodeTypeEnum Type;
	public Quad Quad;

	[NonSerialized] 
	public List<Node> Neighbors;

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
