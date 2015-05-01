using UnityEngine;
using System.Collections;

public class Graph
{
	public Node[,] Nodes;

	public Graph( int rows, int columns )
	{
		Nodes = new Node[ rows, columns ];
	}

	#region Static methods

	public static Graph LoadGraphFromCsv( string csvPath )
	{
		Graph graph = new Graph( 4, 8 );

		graph.CreateCubeMapNodes();
		
		// TODO Load graph from the CSV file.

		return graph;
	}

	public static void SaveGraphToCsv( string csvPath )
	{
		// TODO Save graph as CSV file.
	}

	#endregion

	private void CreateCubeMapNodes()
	{
		#region Barf out a rectangular array of nodes.

		for ( var i = 0; i < 4; i++ )
		{
			for ( var j = 0; j < 8; j++ )
			{
				Nodes[ i, j ] = new Node();
			}
		}

		#endregion
		
		#region Link the nodes into a cube map.

		// Row 0
		
		// Non-connected node
		
		Nodes[ 0, 1 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 0, 1 ].East  = new Edge( Nodes[ 0, 1 ], Nodes[ 0, 2 ], false );
		Nodes[ 0, 1 ].North = new Edge( Nodes[ 0, 1 ], Nodes[ 0, 6 ], false );
		Nodes[ 0, 1 ].South = new Edge( Nodes[ 0, 1 ], Nodes[ 1, 1 ], false );
		Nodes[ 0, 1 ].West  = new Edge( Nodes[ 0, 1 ], Nodes[ 1, 0 ], false );
		
		Nodes[ 0, 2 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 0, 2 ].East  = new Edge( Nodes[ 1, 2 ], Nodes[ 1, 3 ], false );
		Nodes[ 0, 2 ].North = new Edge( Nodes[ 1, 2 ], Nodes[ 0, 5 ], false );
		Nodes[ 0, 2 ].South = new Edge( Nodes[ 1, 2 ], Nodes[ 1, 2 ], false );
		Nodes[ 0, 2 ].West  = new Edge( Nodes[ 1, 2 ], Nodes[ 0, 1 ], false );
		
		// Non-connected node
		// Non-connected node
		
		Nodes[ 0, 5 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 0, 5 ].East  = new Edge( Nodes[ 0, 5 ], Nodes[ 0, 6 ], false );
		Nodes[ 0, 5 ].North = new Edge( Nodes[ 0, 5 ], Nodes[ 0, 2 ], false );
		Nodes[ 0, 5 ].South = new Edge( Nodes[ 0, 5 ], Nodes[ 1, 5 ], false );
		Nodes[ 0, 5 ].West  = new Edge( Nodes[ 0, 5 ], Nodes[ 1, 4 ], false );
		
		Nodes[ 0, 6 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 0, 6 ].East  = new Edge( Nodes[ 0, 6 ], Nodes[ 1, 7 ], false );
		Nodes[ 0, 6 ].North = new Edge( Nodes[ 0, 6 ], Nodes[ 0, 1 ], false );
		Nodes[ 0, 6 ].South = new Edge( Nodes[ 0, 6 ], Nodes[ 1, 6 ], false );
		Nodes[ 0, 6 ].West  = new Edge( Nodes[ 0, 6 ], Nodes[ 0, 5 ], false );
		
		// Non-connected node
		
		// Row 1
		
		Nodes[ 1, 0 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 1, 0 ].East  = new Edge( Nodes[ 1, 0 ], Nodes[ 1, 1 ], false );
		Nodes[ 1, 0 ].North = new Edge( Nodes[ 1, 0 ], Nodes[ 0, 1 ], false );
		Nodes[ 1, 0 ].South = new Edge( Nodes[ 1, 0 ], Nodes[ 2, 1 ], false );
		Nodes[ 1, 0 ].West  = new Edge( Nodes[ 1, 0 ], Nodes[ 1, 7 ], false );
		
		Nodes[ 1, 1 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 1, 1 ].East  = new Edge( Nodes[ 1, 1 ], Nodes[ 1, 2 ], false );
		Nodes[ 1, 1 ].North = new Edge( Nodes[ 1, 1 ], Nodes[ 0, 1 ], false );
		Nodes[ 1, 1 ].South = new Edge( Nodes[ 1, 1 ], Nodes[ 2, 1 ], false );
		Nodes[ 1, 1 ].West  = new Edge( Nodes[ 1, 1 ], Nodes[ 1, 0 ], false );
		
		Nodes[ 1, 2 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 1, 2 ].East  = new Edge( Nodes[ 1, 2 ], Nodes[ 1, 3 ], false );
		Nodes[ 1, 2 ].North = new Edge( Nodes[ 1, 2 ], Nodes[ 0, 2 ], false );
		Nodes[ 1, 2 ].South = new Edge( Nodes[ 1, 2 ], Nodes[ 2, 2 ], false );
		Nodes[ 1, 2 ].West  = new Edge( Nodes[ 1, 2 ], Nodes[ 1, 1 ], false );
		
		Nodes[ 1, 3 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 1, 3 ].East  = new Edge( Nodes[ 1, 3 ], Nodes[ 1, 4 ], false );
		Nodes[ 1, 3 ].North = new Edge( Nodes[ 1, 3 ], Nodes[ 0, 2 ], false );
		Nodes[ 1, 3 ].South = new Edge( Nodes[ 1, 3 ], Nodes[ 2, 3 ], false );
		Nodes[ 1, 3 ].West  = new Edge( Nodes[ 1, 3 ], Nodes[ 1, 2 ], false );
		
		Nodes[ 1, 4 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 1, 4 ].East  = new Edge( Nodes[ 1, 4 ], Nodes[ 1, 5 ], false );
		Nodes[ 1, 4 ].North = new Edge( Nodes[ 1, 4 ], Nodes[ 0, 5 ], false );
		Nodes[ 1, 4 ].South = new Edge( Nodes[ 1, 4 ], Nodes[ 2, 4 ], false );
		Nodes[ 1, 4 ].West  = new Edge( Nodes[ 1, 4 ], Nodes[ 1, 3 ], false );
		
		Nodes[ 1, 5 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 1, 5 ].East  = new Edge( Nodes[ 1, 5 ], Nodes[ 1, 6 ], false );
		Nodes[ 1, 5 ].North = new Edge( Nodes[ 1, 5 ], Nodes[ 0, 5 ], false );
		Nodes[ 1, 5 ].South = new Edge( Nodes[ 1, 5 ], Nodes[ 2, 5 ], false );
		Nodes[ 1, 5 ].West  = new Edge( Nodes[ 1, 5 ], Nodes[ 1, 4 ], false );
		
		Nodes[ 1, 6 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 1, 6 ].East  = new Edge( Nodes[ 1, 6 ], Nodes[ 1, 7 ], false );
		Nodes[ 1, 6 ].North = new Edge( Nodes[ 1, 6 ], Nodes[ 0, 6 ], false );
		Nodes[ 1, 6 ].South = new Edge( Nodes[ 1, 6 ], Nodes[ 2, 6 ], false );
		Nodes[ 1, 6 ].West  = new Edge( Nodes[ 1, 6 ], Nodes[ 1, 5 ], false );
		
		Nodes[ 1, 7 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 1, 7 ].East  = new Edge( Nodes[ 1, 7 ], Nodes[ 1, 0 ], false );
		Nodes[ 1, 7 ].North = new Edge( Nodes[ 1, 7 ], Nodes[ 0, 6 ], false );
		Nodes[ 1, 7 ].South = new Edge( Nodes[ 1, 7 ], Nodes[ 2, 7 ], false );
		Nodes[ 1, 7 ].West  = new Edge( Nodes[ 1, 7 ], Nodes[ 1, 6 ], false );
		
		// Row 2
		
		Nodes[ 2, 0 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 2, 0 ].East  = new Edge( Nodes[ 2, 0 ], Nodes[ 2, 1 ], false );
		Nodes[ 2, 0 ].North = new Edge( Nodes[ 2, 0 ], Nodes[ 1, 0 ], false );
		Nodes[ 2, 0 ].South = new Edge( Nodes[ 2, 0 ], Nodes[ 3, 1 ], false );
		Nodes[ 2, 0 ].West  = new Edge( Nodes[ 2, 0 ], Nodes[ 2, 7 ], false );
		
		Nodes[ 2, 1 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 2, 1 ].East  = new Edge( Nodes[ 2, 1 ], Nodes[ 2, 2 ], false );
		Nodes[ 2, 1 ].North = new Edge( Nodes[ 2, 1 ], Nodes[ 1, 1 ], false );
		Nodes[ 2, 1 ].South = new Edge( Nodes[ 2, 1 ], Nodes[ 3, 1 ], false );
		Nodes[ 2, 1 ].West  = new Edge( Nodes[ 2, 1 ], Nodes[ 2, 0 ], false );
		
		Nodes[ 2, 2 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 2, 2 ].East  = new Edge( Nodes[ 2, 2 ], Nodes[ 2, 3 ], false );
		Nodes[ 2, 2 ].North = new Edge( Nodes[ 2, 2 ], Nodes[ 1, 2 ], false );
		Nodes[ 2, 2 ].South = new Edge( Nodes[ 2, 2 ], Nodes[ 3, 2 ], false );
		Nodes[ 2, 2 ].West  = new Edge( Nodes[ 2, 2 ], Nodes[ 2, 1 ], false );
		
		Nodes[ 2, 3 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 2, 3 ].East  = new Edge( Nodes[ 2, 3 ], Nodes[ 2, 4 ], false );
		Nodes[ 2, 3 ].North = new Edge( Nodes[ 2, 3 ], Nodes[ 1, 3 ], false );
		Nodes[ 2, 3 ].South = new Edge( Nodes[ 2, 3 ], Nodes[ 3, 2 ], false );
		Nodes[ 2, 3 ].West  = new Edge( Nodes[ 2, 3 ], Nodes[ 2, 2 ], false );
		
		Nodes[ 2, 4 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 2, 4 ].East  = new Edge( Nodes[ 2, 4 ], Nodes[ 2, 5 ], false );
		Nodes[ 2, 4 ].North = new Edge( Nodes[ 2, 4 ], Nodes[ 1, 4 ], false );
		Nodes[ 2, 4 ].South = new Edge( Nodes[ 2, 4 ], Nodes[ 3, 5 ], false );
		Nodes[ 2, 4 ].West  = new Edge( Nodes[ 2, 4 ], Nodes[ 2, 3 ], false );
		
		Nodes[ 2, 5 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 2, 5 ].East  = new Edge( Nodes[ 2, 5 ], Nodes[ 2, 6 ], false );
		Nodes[ 2, 5 ].North = new Edge( Nodes[ 2, 5 ], Nodes[ 1, 5 ], false );
		Nodes[ 2, 5 ].South = new Edge( Nodes[ 2, 5 ], Nodes[ 3, 5 ], false );
		Nodes[ 2, 5 ].West  = new Edge( Nodes[ 2, 5 ], Nodes[ 2, 4 ], false );
		
		Nodes[ 2, 6 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 2, 6 ].East  = new Edge( Nodes[ 2, 6 ], Nodes[ 2, 7 ], false );
		Nodes[ 2, 6 ].North = new Edge( Nodes[ 2, 6 ], Nodes[ 1, 6 ], false );
		Nodes[ 2, 6 ].South = new Edge( Nodes[ 2, 6 ], Nodes[ 3, 6 ], false );
		Nodes[ 2, 6 ].West  = new Edge( Nodes[ 2, 6 ], Nodes[ 2, 5 ], false );
		
		Nodes[ 2, 7 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 2, 7 ].East  = new Edge( Nodes[ 2, 7 ], Nodes[ 2, 0 ], false );
		Nodes[ 2, 7 ].North = new Edge( Nodes[ 2, 7 ], Nodes[ 1, 7 ], false );
		Nodes[ 2, 7 ].South = new Edge( Nodes[ 2, 7 ], Nodes[ 3, 6 ], false );
		Nodes[ 2, 7 ].West  = new Edge( Nodes[ 2, 7 ], Nodes[ 2, 6 ], false );
		
		// Row 3
		
		// Non-connected node
		
		Nodes[ 3, 1 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 3, 1 ].East  = new Edge( Nodes[ 0, 1 ], Nodes[ 0, 2 ], false );
		Nodes[ 3, 1 ].North = new Edge( Nodes[ 0, 1 ], Nodes[ 0, 6 ], false );
		Nodes[ 3, 1 ].South = new Edge( Nodes[ 0, 1 ], Nodes[ 1, 1 ], false );
		Nodes[ 3, 1 ].West  = new Edge( Nodes[ 0, 1 ], Nodes[ 1, 0 ], false );
		
		Nodes[ 3, 1 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 3, 2 ].East  = new Edge( Nodes[ 1, 2 ], Nodes[ 1, 3 ], false );
		Nodes[ 3, 2 ].North = new Edge( Nodes[ 1, 2 ], Nodes[ 0, 5 ], false );
		Nodes[ 3, 2 ].South = new Edge( Nodes[ 1, 2 ], Nodes[ 1, 2 ], false );
		Nodes[ 3, 2 ].West  = new Edge( Nodes[ 1, 2 ], Nodes[ 0, 1 ], false );
		
		// Non-connected node
		// Non-connected node
		
		Nodes[ 3, 5 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 3, 5 ].East  = new Edge( Nodes[ 0, 5 ], Nodes[ 0, 6 ], false );
		Nodes[ 3, 5 ].North = new Edge( Nodes[ 0, 5 ], Nodes[ 0, 2 ], false );
		Nodes[ 3, 5 ].South = new Edge( Nodes[ 0, 5 ], Nodes[ 1, 5 ], false );
		Nodes[ 3, 5 ].West  = new Edge( Nodes[ 0, 5 ], Nodes[ 1, 4 ], false );
		
		Nodes[ 3, 6 ].Type  = NodeTypeEnum.Normal;
		Nodes[ 3, 6 ].East  = new Edge( Nodes[ 0, 6 ], Nodes[ 1, 7 ], false );
		Nodes[ 3, 6 ].North = new Edge( Nodes[ 0, 6 ], Nodes[ 0, 1 ], false );
		Nodes[ 3, 6 ].South = new Edge( Nodes[ 0, 6 ], Nodes[ 1, 6 ], false );
		Nodes[ 3, 6 ].West  = new Edge( Nodes[ 0, 6 ], Nodes[ 0, 5 ], false );
		
		// Non-connected node

		#endregion
	}

	#region Rotations

	private void RotateX( RotationEnum direction )
	{
		// TODO Handle rotation direction.


	}

	private void RotateY( RotationEnum direction )
	{
		// TODO Handle rotation direction.


	}

	private void RotateZ( RotationEnum direction )
	{
		// TODO Handle rotation direction.


	}

	#endregion
}
