using System;
using System.Collections.Generic;


public class Graph
{
	public Node[,] Nodes;

	private static Dictionary<string, Func<Graph>> levelSelectionDictionary = new Dictionary<string, Func<Graph>>()
	{
		{ "Level 1", LevelOne }
	};

	public Graph( int rows, int columns )
	{
		Nodes = new Node[ rows, columns ];
		ClearNodes();
	}

	#region Static methods

	public static Graph LoadLevel( string levelName )
	{
		Graph graph = new Graph( 4, 8 );

		var func = levelSelectionDictionary[ levelName ];

		return func == null ? null : func();
	}

	private static Graph LevelOne()
	{
		Graph graph = new Graph( 4, 8 );

		graph.Nodes[ 0, 6 ].Type = NodeTypeEnum.Start;
		graph.Nodes[ 0, 5 ].Type = NodeTypeEnum.End;

		graph.Nodes[ 0, 6 ].MoveableDirections.Add( Node.Direction.up );
		graph.Nodes[ 1, 6 ].MoveableDirections.Add( Node.Direction.up );
		graph.Nodes[ 1, 6 ].MoveableDirections.Add( Node.Direction.down );
		graph.Nodes[ 2, 6 ].MoveableDirections.Add( Node.Direction.up );
		graph.Nodes[ 2, 6 ].MoveableDirections.Add( Node.Direction.left );
		graph.Nodes[ 2, 5 ].MoveableDirections.Add( Node.Direction.left );
		graph.Nodes[ 2, 5 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 2, 4 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 2, 4 ].MoveableDirections.Add( Node.Direction.up );
		graph.Nodes[ 1, 4 ].MoveableDirections.Add( Node.Direction.down );
		graph.Nodes[ 1, 4 ].MoveableDirections.Add( Node.Direction.up );
		graph.Nodes[ 0, 5 ].MoveableDirections.Add( Node.Direction.right );

		graph.Nodes[ 2, 6 ].AddNeighbor( graph.Nodes[ 2, 5 ] );
		graph.Nodes[ 2, 5 ].AddNeighbor( graph.Nodes[ 2, 4 ] );

		return graph;
	}

	public static void SaveGraphToCsv( string csvPath )
	{
		// TODO Save Graph as CSV file.
	}

	#endregion

	#region Node Operations

	private void ClearNodes()
	{
		for ( var row = 0; row < 4; row++ )
		{
			for ( var col = 0; col < 8; col++ )
			{
				Nodes[ row, col ] = new Node();
				Nodes[ row, col ].Name = row + "," + col;
			}
		}
	}

	private void CreateTestPath()
	{


		// TODO Hardcode a path here until file loading works.
		Nodes[ 0, 6 ].Type = NodeTypeEnum.Start;
		Nodes[ 0, 5 ].Type = NodeTypeEnum.End;

		Nodes[ 0, 6 ].MoveableDirections.Add( Node.Direction.up );
		Nodes[ 1, 6 ].MoveableDirections.Add( Node.Direction.up );
		Nodes[ 1, 6 ].MoveableDirections.Add( Node.Direction.down );
		Nodes[ 2, 6 ].MoveableDirections.Add( Node.Direction.up );
		Nodes[ 2, 6 ].MoveableDirections.Add( Node.Direction.left );
		Nodes[ 2, 5 ].MoveableDirections.Add( Node.Direction.left );
		Nodes[ 2, 5 ].MoveableDirections.Add( Node.Direction.right );
		Nodes[ 2, 4 ].MoveableDirections.Add( Node.Direction.right );
		Nodes[ 2, 4 ].MoveableDirections.Add( Node.Direction.up );
		Nodes[ 1, 4 ].MoveableDirections.Add( Node.Direction.down );
		Nodes[ 1, 4 ].MoveableDirections.Add( Node.Direction.up );
		Nodes[ 0, 5 ].MoveableDirections.Add( Node.Direction.right );

		Nodes[ 2, 6 ].AddNeighbor( Nodes[ 2, 5 ] );
		Nodes[ 2, 5 ].AddNeighbor( Nodes[ 2, 4 ] );
	}

	public Node GetNodeByName( string name )
	{
		for ( var row = 0; row < 4; row++ )
		{
			for ( var col = 0; col < 8; col++ )
			{
				if ( name == Nodes[ row, col ].Name )
				{
					return Nodes[ row, col ];
				}
			}
		}
		return null;
	}

	#endregion

	#region Rotations

	public void RotateX( RotationEnum direction )
	{
		if ( direction == RotationEnum.Clockwise )
		{
			// Update all of the edge nodes with their new neighbors.
			Nodes[ 0, 5 ].ReplaceNeighbor( Nodes[ 1, 0 ], Nodes[ 0, 2 ] );
			Nodes[ 0, 6 ].ReplaceNeighbor( Nodes[ 2, 0 ], Nodes[ 0, 1 ] );
			Nodes[ 1, 7 ].ReplaceNeighbor( Nodes[ 3, 1 ], Nodes[ 1, 0 ] );
			Nodes[ 2, 7 ].ReplaceNeighbor( Nodes[ 3, 2 ], Nodes[ 2, 0 ] );
			Nodes[ 3, 6 ].ReplaceNeighbor( Nodes[ 2, 4 ], Nodes[ 3, 1 ] );
			Nodes[ 3, 5 ].ReplaceNeighbor( Nodes[ 1, 4 ], Nodes[ 3, 2 ] );
			Nodes[ 2, 4 ].ReplaceNeighbor( Nodes[ 0, 1 ], Nodes[ 2, 3 ] );
			Nodes[ 1, 4 ].ReplaceNeighbor( Nodes[ 0, 2 ], Nodes[ 1, 3 ] );

			// Transform all of the affected nodes by 90 degrees in the array.
			var temp = Nodes[ 0, 5 ];
			Nodes[ 0, 5 ] = Nodes[ 2, 4 ];
			Nodes[ 2, 4 ] = Nodes[ 3, 6 ];
			Nodes[ 3, 6 ] = Nodes[ 1, 7 ];
			Nodes[ 1, 7 ] = temp;
			temp = Nodes[ 0, 6 ];
			Nodes[ 0, 6 ] = Nodes[ 1, 4 ];
			Nodes[ 1, 4 ] = Nodes[ 3, 5 ];
			Nodes[ 3, 5 ] = Nodes[ 2, 7 ];
			Nodes[ 2, 7 ] = temp;
			temp = Nodes[ 1, 5 ];
			Nodes[ 1, 5 ] = Nodes[ 2, 5 ];
			Nodes[ 2, 5 ] = Nodes[ 2, 6 ];
			Nodes[ 2, 6 ] = Nodes[ 1, 6 ];
			Nodes[ 1, 6 ] = temp;
		}
		else
		{
			throw new NotImplementedException( "CCW rotation isn't in yet!" );
		}
	}

	public void RotateY( RotationEnum direction )
	{
		if ( direction == RotationEnum.Clockwise )
		{
			// Update all of the edge nodes with their new neighbors.
			Nodes[ 1, 0 ].ReplaceNeighbor( Nodes[ 2, 6 ], Nodes[ 2, 0 ] );
			Nodes[ 1, 1 ].ReplaceNeighbor( Nodes[ 2, 7 ], Nodes[ 2, 1 ] );
			Nodes[ 1, 2 ].ReplaceNeighbor( Nodes[ 2, 0 ], Nodes[ 2, 2 ] );
			Nodes[ 1, 3 ].ReplaceNeighbor( Nodes[ 2, 1 ], Nodes[ 2, 3 ] );
			Nodes[ 1, 4 ].ReplaceNeighbor( Nodes[ 2, 2 ], Nodes[ 3, 4 ] );
			Nodes[ 1, 5 ].ReplaceNeighbor( Nodes[ 2, 3 ], Nodes[ 3, 5 ] );
			Nodes[ 1, 6 ].ReplaceNeighbor( Nodes[ 2, 4 ], Nodes[ 2, 6 ] );
			Nodes[ 1, 7 ].ReplaceNeighbor( Nodes[ 2, 5 ], Nodes[ 1, 7 ] );
			
			// Transform all of the affected nodes by 90 degrees in the array.
			var temp = Nodes[ 1, 7 ];
			Nodes[ 1, 7 ] = Nodes[ 1, 5 ];
			Nodes[ 1, 5 ] = Nodes[ 1, 3 ];
			Nodes[ 1, 3 ] = Nodes[ 1, 1 ];
			Nodes[ 1, 1 ] = temp;
			temp = Nodes[ 1, 6 ];
			Nodes[ 1, 6 ] = Nodes[ 1, 4 ];
			Nodes[ 1, 4 ] = Nodes[ 1, 2 ];
			Nodes[ 1, 2 ] = Nodes[ 1, 0 ];
			Nodes[ 1, 0 ] = temp;
			temp = Nodes[ 0, 1 ];
			Nodes[ 0, 1 ] = Nodes[ 0, 2 ];
			Nodes[ 0, 2 ] = Nodes[ 0, 5 ];
			Nodes[ 0, 5 ] = Nodes[ 0, 6 ];
			Nodes[ 0, 6 ] = temp;
		}
		else
		{
			throw new NotImplementedException( "CCW rotation isn't in yet!" );
		}
	}

	private void RotateZ( RotationEnum direction )
	{
		if ( direction == RotationEnum.Clockwise )
		{
			// Update all of the edge nodes with their new neighbors.
			Nodes[ 0, 2 ].ReplaceNeighbor( Nodes[ 1, 6 ], Nodes[ 0, 1 ] );
			Nodes[ 0, 5 ].ReplaceNeighbor( Nodes[ 2, 6 ], Nodes[ 0, 6 ] );
			Nodes[ 1, 5 ].ReplaceNeighbor( Nodes[ 3, 6 ], Nodes[ 1, 6 ] );
			Nodes[ 2, 5 ].ReplaceNeighbor( Nodes[ 3, 1 ], Nodes[ 2, 6 ] );
			Nodes[ 3, 5 ].ReplaceNeighbor( Nodes[ 2, 1 ], Nodes[ 3, 6 ] );
			Nodes[ 3, 2 ].ReplaceNeighbor( Nodes[ 1, 1 ], Nodes[ 3, 1 ] );
			Nodes[ 2, 2 ].ReplaceNeighbor( Nodes[ 0, 1 ], Nodes[ 2, 1 ] );
			Nodes[ 1, 2 ].ReplaceNeighbor( Nodes[ 0, 6 ], Nodes[ 1, 1 ] );
			
			// Transform all of the affected nodes by 90 degrees in the array.
			var temp = Nodes[ 0, 2 ];
			Nodes[ 0, 2 ] = Nodes[ 2, 2 ];
			Nodes[ 2, 2 ] = Nodes[ 3, 5 ];
			Nodes[ 3, 5 ] = Nodes[ 1, 5 ];
			Nodes[ 2, 2 ] = temp;
			temp = Nodes[ 0, 5 ];
			Nodes[ 0, 5 ] = Nodes[ 1, 2 ];
			Nodes[ 1, 2 ] = Nodes[ 3, 2 ];
			Nodes[ 3, 2 ] = Nodes[ 2, 5 ];
			Nodes[ 1, 2 ] = temp;
			temp = Nodes[ 1, 3 ];
			Nodes[ 1, 3 ] = Nodes[ 2, 3 ];
			Nodes[ 2, 3 ] = Nodes[ 2, 4 ];
			Nodes[ 2, 4 ] = Nodes[ 1, 4 ];
			Nodes[ 1, 4 ] = temp;
		}
		else
		{
			throw new NotImplementedException( "CCW rotation isn't in yet!" );
		}
	}

	#endregion
}
