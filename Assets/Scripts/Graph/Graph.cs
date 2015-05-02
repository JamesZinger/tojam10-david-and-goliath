using UnityEngine;
using System.Collections;

public class Graph
{
	public Node[,] Nodes;

	public Graph( int rows, int columns )
	{
		Nodes = new Node[ rows, columns ];
	}

	public static Graph LoadGraphFromCsv( string csvString )
	{
		Graph graph = new Graph( 4, 8 );

		#region Barf out a cubemap of nodes.
		for ( var i = 0; i < 4; i++ )
		{
			for ( var j = 0; j < 8; j++ )
			{
				graph.Nodes[ i, j ] = new Node();
			}
		}
		#endregion

		#region Connect nodes to one another.
		for ( var i = 1; i < 2; i++ )
		{
			for ( var j = 0; j < 8; j++ )
			{
				var currentNode = graph.Nodes[ i, j ];
				currentNode.East  = new Edge( currentNode, graph.Nodes[ i, ( j + 7 ) % 8 ], false );
				currentNode.West  = new Edge( currentNode, graph.Nodes[ i, ( j + 1 ) % 8 ], false );
			}
		}
		
		// Row 0
		
		// Non-connected node
		
		graph.Nodes[ 0, 1 ].East  = new Edge( graph.Nodes[ 0, 1 ], graph.Nodes[ 0, 2 ], false );
		graph.Nodes[ 0, 1 ].North = new Edge( graph.Nodes[ 0, 1 ], graph.Nodes[ 0, 6 ], false );
		graph.Nodes[ 0, 1 ].South = new Edge( graph.Nodes[ 0, 1 ], graph.Nodes[ 1, 1 ], false );
		graph.Nodes[ 0, 1 ].West  = new Edge( graph.Nodes[ 0, 1 ], graph.Nodes[ 1, 0 ], false );
		
		graph.Nodes[ 0, 2 ].East  = new Edge( graph.Nodes[ 1, 2 ], graph.Nodes[ 1, 3 ], false );
		graph.Nodes[ 0, 2 ].North = new Edge( graph.Nodes[ 1, 2 ], graph.Nodes[ 0, 5 ], false );
		graph.Nodes[ 0, 2 ].South = new Edge( graph.Nodes[ 1, 2 ], graph.Nodes[ 1, 2 ], false );
		graph.Nodes[ 0, 2 ].West  = new Edge( graph.Nodes[ 1, 2 ], graph.Nodes[ 0, 1 ], false );
		
		// Non-connected node
		// Non-connected node
		
		graph.Nodes[ 0, 5 ].East  = new Edge( graph.Nodes[ 0, 5 ], graph.Nodes[ 0, 6 ], false );
		graph.Nodes[ 0, 5 ].North = new Edge( graph.Nodes[ 0, 5 ], graph.Nodes[ 0, 2 ], false );
		graph.Nodes[ 0, 5 ].South = new Edge( graph.Nodes[ 0, 5 ], graph.Nodes[ 1, 5 ], false );
		graph.Nodes[ 0, 5 ].West  = new Edge( graph.Nodes[ 0, 5 ], graph.Nodes[ 1, 4 ], false );
		
		graph.Nodes[ 0, 6 ].East  = new Edge( graph.Nodes[ 0, 6 ], graph.Nodes[ 1, 7 ], false );
		graph.Nodes[ 0, 6 ].North = new Edge( graph.Nodes[ 0, 6 ], graph.Nodes[ 0, 1 ], false );
		graph.Nodes[ 0, 6 ].South = new Edge( graph.Nodes[ 0, 6 ], graph.Nodes[ 1, 6 ], false );
		graph.Nodes[ 0, 6 ].West  = new Edge( graph.Nodes[ 0, 6 ], graph.Nodes[ 0, 5 ], false );
		
		// Non-connected node
		
		// Row 1

		graph.Nodes[ 1, 0 ].East  = new Edge( graph.Nodes[ 1, 0 ], graph.Nodes[ 1, 1 ], false );
		graph.Nodes[ 1, 0 ].North = new Edge( graph.Nodes[ 1, 0 ], graph.Nodes[ 0, 1 ], false );
		graph.Nodes[ 1, 0 ].South = new Edge( graph.Nodes[ 1, 0 ], graph.Nodes[ 2, 1 ], false );
		graph.Nodes[ 1, 0 ].West  = new Edge( graph.Nodes[ 1, 0 ], graph.Nodes[ 1, 7 ], false );
		
		graph.Nodes[ 1, 1 ].East  = new Edge( graph.Nodes[ 1, 1 ], graph.Nodes[ 1, 2 ], false );
		graph.Nodes[ 1, 1 ].North = new Edge( graph.Nodes[ 1, 1 ], graph.Nodes[ 0, 1 ], false );
		graph.Nodes[ 1, 1 ].South = new Edge( graph.Nodes[ 1, 1 ], graph.Nodes[ 2, 1 ], false );
		graph.Nodes[ 1, 1 ].West  = new Edge( graph.Nodes[ 1, 1 ], graph.Nodes[ 1, 0 ], false );
		
		graph.Nodes[ 1, 2 ].East  = new Edge( graph.Nodes[ 1, 2 ], graph.Nodes[ 1, 3 ], false );
		graph.Nodes[ 1, 2 ].North = new Edge( graph.Nodes[ 1, 2 ], graph.Nodes[ 0, 2 ], false );
		graph.Nodes[ 1, 2 ].South = new Edge( graph.Nodes[ 1, 2 ], graph.Nodes[ 2, 2 ], false );
		graph.Nodes[ 1, 2 ].West  = new Edge( graph.Nodes[ 1, 2 ], graph.Nodes[ 1, 1 ], false );
		
		graph.Nodes[ 1, 3 ].East  = new Edge( graph.Nodes[ 1, 3 ], graph.Nodes[ 1, 4 ], false );
		graph.Nodes[ 1, 3 ].North = new Edge( graph.Nodes[ 1, 3 ], graph.Nodes[ 0, 2 ], false );
		graph.Nodes[ 1, 3 ].South = new Edge( graph.Nodes[ 1, 3 ], graph.Nodes[ 2, 3 ], false );
		graph.Nodes[ 1, 3 ].West  = new Edge( graph.Nodes[ 1, 3 ], graph.Nodes[ 1, 2 ], false );
		
		graph.Nodes[ 1, 4 ].East  = new Edge( graph.Nodes[ 1, 4 ], graph.Nodes[ 1, 5 ], false );
		graph.Nodes[ 1, 4 ].North = new Edge( graph.Nodes[ 1, 4 ], graph.Nodes[ 0, 5 ], false );
		graph.Nodes[ 1, 4 ].South = new Edge( graph.Nodes[ 1, 4 ], graph.Nodes[ 2, 4 ], false );
		graph.Nodes[ 1, 4 ].West  = new Edge( graph.Nodes[ 1, 4 ], graph.Nodes[ 1, 3 ], false );
		
		graph.Nodes[ 1, 5 ].East  = new Edge( graph.Nodes[ 1, 5 ], graph.Nodes[ 1, 6 ], false );
		graph.Nodes[ 1, 5 ].North = new Edge( graph.Nodes[ 1, 5 ], graph.Nodes[ 0, 5 ], false );
		graph.Nodes[ 1, 5 ].South = new Edge( graph.Nodes[ 1, 5 ], graph.Nodes[ 2, 5 ], false );
		graph.Nodes[ 1, 5 ].West  = new Edge( graph.Nodes[ 1, 5 ], graph.Nodes[ 1, 4 ], false );
		
		graph.Nodes[ 1, 6 ].East  = new Edge( graph.Nodes[ 1, 6 ], graph.Nodes[ 1, 7 ], false );
		graph.Nodes[ 1, 6 ].North = new Edge( graph.Nodes[ 1, 6 ], graph.Nodes[ 0, 6 ], false );
		graph.Nodes[ 1, 6 ].South = new Edge( graph.Nodes[ 1, 6 ], graph.Nodes[ 2, 6 ], false );
		graph.Nodes[ 1, 6 ].West  = new Edge( graph.Nodes[ 1, 6 ], graph.Nodes[ 1, 5 ], false );
		
		graph.Nodes[ 1, 7 ].East  = new Edge( graph.Nodes[ 1, 7 ], graph.Nodes[ 1, 0 ], false );
		graph.Nodes[ 1, 7 ].North = new Edge( graph.Nodes[ 1, 7 ], graph.Nodes[ 0, 6 ], false );
		graph.Nodes[ 1, 7 ].South = new Edge( graph.Nodes[ 1, 7 ], graph.Nodes[ 2, 7 ], false );
		graph.Nodes[ 1, 7 ].West  = new Edge( graph.Nodes[ 1, 7 ], graph.Nodes[ 1, 6 ], false );
		
		// Row 2

		graph.Nodes[ 2, 0 ].East  = new Edge( graph.Nodes[ 2, 0 ], graph.Nodes[ 2, 1 ], false );
		graph.Nodes[ 2, 0 ].North = new Edge( graph.Nodes[ 2, 0 ], graph.Nodes[ 1, 0 ], false );
		graph.Nodes[ 2, 0 ].South = new Edge( graph.Nodes[ 2, 0 ], graph.Nodes[ 3, 1 ], false );
		graph.Nodes[ 2, 0 ].West  = new Edge( graph.Nodes[ 2, 0 ], graph.Nodes[ 2, 7 ], false );
		
		graph.Nodes[ 2, 1 ].East  = new Edge( graph.Nodes[ 2, 1 ], graph.Nodes[ 2, 2 ], false );
		graph.Nodes[ 2, 1 ].North = new Edge( graph.Nodes[ 2, 1 ], graph.Nodes[ 1, 1 ], false );
		graph.Nodes[ 2, 1 ].South = new Edge( graph.Nodes[ 2, 1 ], graph.Nodes[ 3, 1 ], false );
		graph.Nodes[ 2, 1 ].West  = new Edge( graph.Nodes[ 2, 1 ], graph.Nodes[ 2, 0 ], false );
		
		graph.Nodes[ 2, 2 ].East  = new Edge( graph.Nodes[ 2, 2 ], graph.Nodes[ 2, 3 ], false );
		graph.Nodes[ 2, 2 ].North = new Edge( graph.Nodes[ 2, 2 ], graph.Nodes[ 1, 2 ], false );
		graph.Nodes[ 2, 2 ].South = new Edge( graph.Nodes[ 2, 2 ], graph.Nodes[ 3, 2 ], false );
		graph.Nodes[ 2, 2 ].West  = new Edge( graph.Nodes[ 2, 2 ], graph.Nodes[ 2, 1 ], false );
		
		graph.Nodes[ 2, 3 ].East  = new Edge( graph.Nodes[ 2, 3 ], graph.Nodes[ 2, 4 ], false );
		graph.Nodes[ 2, 3 ].North = new Edge( graph.Nodes[ 2, 3 ], graph.Nodes[ 1, 3 ], false );
		graph.Nodes[ 2, 3 ].South = new Edge( graph.Nodes[ 2, 3 ], graph.Nodes[ 3, 2 ], false );
		graph.Nodes[ 2, 3 ].West  = new Edge( graph.Nodes[ 2, 3 ], graph.Nodes[ 2, 2 ], false );
		
		graph.Nodes[ 2, 4 ].East  = new Edge( graph.Nodes[ 2, 4 ], graph.Nodes[ 2, 5 ], false );
		graph.Nodes[ 2, 4 ].North = new Edge( graph.Nodes[ 2, 4 ], graph.Nodes[ 1, 4 ], false );
		graph.Nodes[ 2, 4 ].South = new Edge( graph.Nodes[ 2, 4 ], graph.Nodes[ 3, 5 ], false );
		graph.Nodes[ 2, 4 ].West  = new Edge( graph.Nodes[ 2, 4 ], graph.Nodes[ 2, 3 ], false );
		
		graph.Nodes[ 2, 5 ].East  = new Edge( graph.Nodes[ 2, 5 ], graph.Nodes[ 2, 6 ], false );
		graph.Nodes[ 2, 5 ].North = new Edge( graph.Nodes[ 2, 5 ], graph.Nodes[ 1, 5 ], false );
		graph.Nodes[ 2, 5 ].South = new Edge( graph.Nodes[ 2, 5 ], graph.Nodes[ 3, 5 ], false );
		graph.Nodes[ 2, 5 ].West  = new Edge( graph.Nodes[ 2, 5 ], graph.Nodes[ 2, 4 ], false );
		
		graph.Nodes[ 2, 6 ].East  = new Edge( graph.Nodes[ 2, 6 ], graph.Nodes[ 2, 7 ], false );
		graph.Nodes[ 2, 6 ].North = new Edge( graph.Nodes[ 2, 6 ], graph.Nodes[ 1, 6 ], false );
		graph.Nodes[ 2, 6 ].South = new Edge( graph.Nodes[ 2, 6 ], graph.Nodes[ 3, 6 ], false );
		graph.Nodes[ 2, 6 ].West  = new Edge( graph.Nodes[ 2, 6 ], graph.Nodes[ 2, 5 ], false );
		
		graph.Nodes[ 2, 7 ].East  = new Edge( graph.Nodes[ 2, 7 ], graph.Nodes[ 2, 0 ], false );
		graph.Nodes[ 2, 7 ].North = new Edge( graph.Nodes[ 2, 7 ], graph.Nodes[ 1, 7 ], false );
		graph.Nodes[ 2, 7 ].South = new Edge( graph.Nodes[ 2, 7 ], graph.Nodes[ 3, 6 ], false );
		graph.Nodes[ 2, 7 ].West  = new Edge( graph.Nodes[ 2, 7 ], graph.Nodes[ 2, 6 ], false );
		
		// Row 3
		
		// Non-connected node
		
		graph.Nodes[ 3, 1 ].East  = new Edge( graph.Nodes[ 0, 1 ], graph.Nodes[ 0, 2 ], false );
		graph.Nodes[ 3, 1 ].North = new Edge( graph.Nodes[ 0, 1 ], graph.Nodes[ 0, 6 ], false );
		graph.Nodes[ 3, 1 ].South = new Edge( graph.Nodes[ 0, 1 ], graph.Nodes[ 1, 1 ], false );
		graph.Nodes[ 3, 1 ].West  = new Edge( graph.Nodes[ 0, 1 ], graph.Nodes[ 1, 0 ], false );
		
		graph.Nodes[ 3, 2 ].East  = new Edge( graph.Nodes[ 1, 2 ], graph.Nodes[ 1, 3 ], false );
		graph.Nodes[ 3, 2 ].North = new Edge( graph.Nodes[ 1, 2 ], graph.Nodes[ 0, 5 ], false );
		graph.Nodes[ 3, 2 ].South = new Edge( graph.Nodes[ 1, 2 ], graph.Nodes[ 1, 2 ], false );
		graph.Nodes[ 3, 2 ].West  = new Edge( graph.Nodes[ 1, 2 ], graph.Nodes[ 0, 1 ], false );
		
		// Non-connected node
		// Non-connected node
		
		graph.Nodes[ 3, 5 ].East  = new Edge( graph.Nodes[ 0, 5 ], graph.Nodes[ 0, 6 ], false );
		graph.Nodes[ 3, 5 ].North = new Edge( graph.Nodes[ 0, 5 ], graph.Nodes[ 0, 2 ], false );
		graph.Nodes[ 3, 5 ].South = new Edge( graph.Nodes[ 0, 5 ], graph.Nodes[ 1, 5 ], false );
		graph.Nodes[ 3, 5 ].West  = new Edge( graph.Nodes[ 0, 5 ], graph.Nodes[ 1, 4 ], false );
		
		graph.Nodes[ 3, 6 ].East  = new Edge( graph.Nodes[ 0, 6 ], graph.Nodes[ 1, 7 ], false );
		graph.Nodes[ 3, 6 ].North = new Edge( graph.Nodes[ 0, 6 ], graph.Nodes[ 0, 1 ], false );
		graph.Nodes[ 3, 6 ].South = new Edge( graph.Nodes[ 0, 6 ], graph.Nodes[ 1, 6 ], false );
		graph.Nodes[ 3, 6 ].West  = new Edge( graph.Nodes[ 0, 6 ], graph.Nodes[ 0, 5 ], false );
		
		// Non-connected node

		#endregion

		return graph;
	}
}
