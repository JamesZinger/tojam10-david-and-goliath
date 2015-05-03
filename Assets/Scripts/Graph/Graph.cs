using System;
using System.Collections.Generic;


public class Graph
{
	#region Members

	public Node[,] Nodes;

	public static Dictionary<string, Func<Graph>> levelSelectionDictionary = new Dictionary<string, Func<Graph>>()
	{
		{ "Level 1", Level1 },
		{ "Level 2", Level2 },
		{ "Level 3", Level3 },
		{ "Level 4", Level4 },
		{ "Level 5", Level5 },
		{ "Level 6", Level6 }
	};

	#endregion

	#region Constructor

	public Graph( int rows, int columns )
	{
		Nodes = new Node[ rows, columns ];
		ClearNodes();
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

	#region Static methods

	public static Graph LoadLevel( string levelName )
	{
		var levelSetupFunc = levelSelectionDictionary[ levelName ];
		if ( levelSetupFunc == null )
		{
			return null;
		}
		return levelSetupFunc();
	}

	private static Graph Level1()
	{
		Graph graph = new Graph( 4, 8 );

		graph.Nodes[ 0, 6 ].Type = NodeTypeEnum.Start;
		graph.Nodes[ 0, 1 ].Type = NodeTypeEnum.End;

		graph.Nodes[ 0, 6 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 1, 6 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 1, 6 ].MoveableDirections.Add( Node.Direction.down  );
		graph.Nodes[ 2, 6 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 2, 6 ].MoveableDirections.Add( Node.Direction.left  );
		graph.Nodes[ 2, 5 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 2, 5 ].MoveableDirections.Add( Node.Direction.left  );
		graph.Nodes[ 2, 4 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 2, 4 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 1, 4 ].MoveableDirections.Add( Node.Direction.down  );
		graph.Nodes[ 1, 4 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 1, 5 ].MoveableDirections.Add( Node.Direction.left  );
		graph.Nodes[ 1, 5 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 0, 5 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 0, 5 ].MoveableDirections.Add( Node.Direction.down  );
		graph.Nodes[ 0, 2 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 0, 2 ].MoveableDirections.Add( Node.Direction.left  );
		graph.Nodes[ 0, 1 ].MoveableDirections.Add( Node.Direction.right );

		return graph;
	}

	private static Graph Level2()
	{
		Graph graph = new Graph( 4, 8 );

		graph.Nodes[ 1, 4 ].Type = NodeTypeEnum.Start;
		graph.Nodes[ 0, 1 ].Type = NodeTypeEnum.End;

		graph.Nodes[ 1, 4 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 1, 5 ].MoveableDirections.Add( Node.Direction.left  );
		graph.Nodes[ 1, 5 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 1, 6 ].MoveableDirections.Add( Node.Direction.left  );
		graph.Nodes[ 1, 6 ].MoveableDirections.Add( Node.Direction.down  );
		graph.Nodes[ 2, 6 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 2, 6 ].MoveableDirections.Add( Node.Direction.down  );
		graph.Nodes[ 3, 6 ].MoveableDirections.Add( Node.Direction.left  );
		graph.Nodes[ 3, 6 ].MoveableDirections.Add( Node.Direction.down  );
		graph.Nodes[ 3, 5 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 3, 5 ].MoveableDirections.Add( Node.Direction.left  );
		graph.Nodes[ 2, 5 ].MoveableDirections.Add( Node.Direction.down  );
		graph.Nodes[ 2, 5 ].MoveableDirections.Add( Node.Direction.left  );
		graph.Nodes[ 2, 4 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 2, 4 ].MoveableDirections.Add( Node.Direction.left  );
		graph.Nodes[ 0, 2 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 0, 2 ].MoveableDirections.Add( Node.Direction.left  );
		graph.Nodes[ 0, 1 ].MoveableDirections.Add( Node.Direction.right );

		return graph;
	}

	private static Graph Level3()
	{
		Graph graph = new Graph( 4, 8 );
		
		graph.Nodes[ 0, 6 ].Type = NodeTypeEnum.Start;
		graph.Nodes[ 2, 3 ].Type = NodeTypeEnum.End;

		graph.Nodes[ 2, 3 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 1, 3 ].MoveableDirections.Add( Node.Direction.down  );
		graph.Nodes[ 1, 3 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 0, 2 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 0, 2 ].MoveableDirections.Add( Node.Direction.left  );
		graph.Nodes[ 0, 1 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 0, 1 ].MoveableDirections.Add( Node.Direction.down  );
		graph.Nodes[ 1, 1 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 1, 1 ].MoveableDirections.Add( Node.Direction.left  );
		graph.Nodes[ 1, 0 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 1, 0 ].MoveableDirections.Add( Node.Direction.left  );
		graph.Nodes[ 1, 7 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 1, 7 ].MoveableDirections.Add( Node.Direction.left  );
		graph.Nodes[ 1, 6 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 1, 6 ].MoveableDirections.Add( Node.Direction.left  );
		graph.Nodes[ 1, 5 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 1, 5 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 0, 5 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 0, 5 ].MoveableDirections.Add( Node.Direction.left  );
		graph.Nodes[ 0, 6 ].MoveableDirections.Add( Node.Direction.right );

		return graph;
	}
	
	private static Graph Level4()
	{
		Graph graph = new Graph( 4, 8 );

		graph.Nodes[ 1, 4 ].Type = NodeTypeEnum.Start;
		graph.Nodes[ 0, 1 ].Type = NodeTypeEnum.End;

		// TODO

		return graph;
	}
	
	private static Graph Level5()
	{
		Graph graph = new Graph( 4, 8 );

		graph.Nodes[ 0, 1 ].Type = NodeTypeEnum.Start;
		graph.Nodes[ 0, 2 ].Type = NodeTypeEnum.End;

		graph.Nodes[ 0, 1 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 0, 6 ].MoveableDirections.Add( Node.Direction.down    );
		graph.Nodes[ 0, 6 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 1, 6 ].MoveableDirections.Add( Node.Direction.left    );


		// TODO

		return graph;
	}
	
	private static Graph Level6()
	{
		Graph graph = new Graph( 4, 8 );

		graph.Nodes[ 1, 4 ].Type = NodeTypeEnum.Start;
		graph.Nodes[ 0, 1 ].Type = NodeTypeEnum.End;

		// TODO

		return graph;
	}

	#endregion
}
