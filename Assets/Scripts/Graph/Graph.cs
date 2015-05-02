using UnityEngine;
using System;
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

		for ( var row = 0; row < 4; row++ )
		{
			for ( var col = 0; col < 8; col++ )
			{
				Nodes[ row, col ] = new Node();
			}
		}

		#endregion
		
		#region Link the nodes into a cube map.

		//// Row 0
		
		//// Non-connected node

		//Nodes[ 0, 1 ].AddNeighbor( Nodes[ 0, 2 ] );
		//Nodes[ 0, 1 ].AddNeighbor( Nodes[ 0, 6 ] );
		//Nodes[ 0, 1 ].AddNeighbor( Nodes[ 1, 1 ] );
		//Nodes[ 0, 1 ].AddNeighbor( Nodes[ 1, 0 ] );
		
		//Nodes[ 0, 2 ].AddNeighbor( Nodes[ 1, 3 ] );
		//Nodes[ 0, 2 ].AddNeighbor( Nodes[ 0, 5 ] );
		//Nodes[ 0, 2 ].AddNeighbor( Nodes[ 1, 2 ] );
		//Nodes[ 0, 2 ].AddNeighbor( Nodes[ 0, 1 ] );
		
		//// Non-connected node
		//// Non-connected node
		
		//Nodes[ 0, 5 ].AddNeighbor( Nodes[ 0, 6 ] );
		//Nodes[ 0, 5 ].AddNeighbor( Nodes[ 0, 2 ] );
		//Nodes[ 0, 5 ].AddNeighbor( Nodes[ 1, 5 ] );
		//Nodes[ 0, 5 ].AddNeighbor( Nodes[ 1, 4 ] );
		
		//Nodes[ 0, 6 ].AddNeighbor( Nodes[ 1, 7 ] );
		//Nodes[ 0, 6 ].AddNeighbor( Nodes[ 0, 1 ] );
		//Nodes[ 0, 6 ].AddNeighbor( Nodes[ 1, 6 ] );
		//Nodes[ 0, 6 ].AddNeighbor( Nodes[ 0, 5 ] );
		
		//// Non-connected node
		
		//// Row 1
		
		//Nodes[ 1, 0 ].AddNeighbor( Nodes[ 1, 1 ] );
		//Nodes[ 1, 0 ].AddNeighbor( Nodes[ 0, 1 ] );
		//Nodes[ 1, 0 ].AddNeighbor( Nodes[ 2, 1 ] );
		//Nodes[ 1, 0 ].AddNeighbor( Nodes[ 1, 7 ] );
		
		//Nodes[ 1, 1 ].AddNeighbor( Nodes[ 1, 2 ] );
		//Nodes[ 1, 1 ].AddNeighbor( Nodes[ 0, 1 ] );
		//Nodes[ 1, 1 ].AddNeighbor( Nodes[ 2, 1 ] );
		//Nodes[ 1, 1 ].AddNeighbor( Nodes[ 1, 0 ] );
		
		//Nodes[ 1, 2 ].AddNeighbor( Nodes[ 1, 3 ] );
		//Nodes[ 1, 2 ].AddNeighbor( Nodes[ 0, 2 ] );
		//Nodes[ 1, 2 ].AddNeighbor( Nodes[ 2, 2 ] );
		//Nodes[ 1, 2 ].AddNeighbor( Nodes[ 1, 1 ] );
		
		//Nodes[ 1, 3 ].AddNeighbor( Nodes[ 1, 4 ] );
		//Nodes[ 1, 3 ].AddNeighbor( Nodes[ 0, 2 ] );
		//Nodes[ 1, 3 ].AddNeighbor( Nodes[ 2, 3 ] );
		//Nodes[ 1, 3 ].AddNeighbor( Nodes[ 1, 2 ] );
		
		//Nodes[ 1, 4 ].AddNeighbor( Nodes[ 1, 5 ] );
		//Nodes[ 1, 4 ].AddNeighbor( Nodes[ 0, 5 ] );
		//Nodes[ 1, 4 ].AddNeighbor( Nodes[ 2, 4 ] );
		//Nodes[ 1, 4 ].AddNeighbor( Nodes[ 1, 3 ] );
		
		//Nodes[ 1, 5 ].AddNeighbor( Nodes[ 1, 6 ] );
		//Nodes[ 1, 5 ].AddNeighbor( Nodes[ 0, 5 ] );
		//Nodes[ 1, 5 ].AddNeighbor( Nodes[ 2, 5 ] );
		//Nodes[ 1, 5 ].AddNeighbor( Nodes[ 1, 4 ] );
		
		//Nodes[ 1, 6 ].AddNeighbor( Nodes[ 1, 7 ] );
		//Nodes[ 1, 6 ].AddNeighbor( Nodes[ 0, 6 ] );
		//Nodes[ 1, 6 ].AddNeighbor( Nodes[ 2, 6 ] );
		//Nodes[ 1, 6 ].AddNeighbor( Nodes[ 1, 5 ] );
		
		//Nodes[ 1, 7 ].AddNeighbor( Nodes[ 1, 0 ] );
		//Nodes[ 1, 7 ].AddNeighbor( Nodes[ 0, 6 ] );
		//Nodes[ 1, 7 ].AddNeighbor( Nodes[ 2, 7 ] );
		//Nodes[ 1, 7 ].AddNeighbor( Nodes[ 1, 6 ] );
		
		//// Row 2
		
		//Nodes[ 2, 0 ].AddNeighbor( Nodes[ 2, 1 ] );
		//Nodes[ 2, 0 ].AddNeighbor( Nodes[ 1, 0 ] );
		//Nodes[ 2, 0 ].AddNeighbor( Nodes[ 3, 1 ] );
		//Nodes[ 2, 0 ].AddNeighbor( Nodes[ 2, 7 ] );
		
		//Nodes[ 2, 1 ].AddNeighbor( Nodes[ 2, 2 ] );
		//Nodes[ 2, 1 ].AddNeighbor( Nodes[ 1, 1 ] );
		//Nodes[ 2, 1 ].AddNeighbor( Nodes[ 3, 1 ] );
		//Nodes[ 2, 1 ].AddNeighbor( Nodes[ 2, 0 ] );
		
		//Nodes[ 2, 2 ].AddNeighbor( Nodes[ 2, 3 ] );
		//Nodes[ 2, 2 ].AddNeighbor( Nodes[ 1, 2 ] );
		//Nodes[ 2, 2 ].AddNeighbor( Nodes[ 3, 2 ] );
		//Nodes[ 2, 2 ].AddNeighbor( Nodes[ 2, 1 ] );
		
		//Nodes[ 2, 3 ].AddNeighbor( Nodes[ 2, 4 ] );
		//Nodes[ 2, 3 ].AddNeighbor( Nodes[ 1, 3 ] );
		//Nodes[ 2, 3 ].AddNeighbor( Nodes[ 3, 2 ] );
		//Nodes[ 2, 3 ].AddNeighbor( Nodes[ 2, 2 ] );
		
		//Nodes[ 2, 4 ].AddNeighbor( Nodes[ 2, 5 ] );
		//Nodes[ 2, 4 ].AddNeighbor( Nodes[ 1, 4 ] );
		//Nodes[ 2, 4 ].AddNeighbor( Nodes[ 3, 5 ] );
		//Nodes[ 2, 4 ].AddNeighbor( Nodes[ 2, 3 ] );
		
		//Nodes[ 2, 5 ].AddNeighbor( Nodes[ 2, 6 ] );
		//Nodes[ 2, 5 ].AddNeighbor( Nodes[ 1, 5 ] );
		//Nodes[ 2, 5 ].AddNeighbor( Nodes[ 3, 5 ] );
		//Nodes[ 2, 5 ].AddNeighbor( Nodes[ 2, 4 ] );
		
		//Nodes[ 2, 6 ].AddNeighbor( Nodes[ 2, 7 ] );
		//Nodes[ 2, 6 ].AddNeighbor( Nodes[ 1, 6 ] );
		//Nodes[ 2, 6 ].AddNeighbor( Nodes[ 3, 6 ] );
		//Nodes[ 2, 6 ].AddNeighbor( Nodes[ 2, 5 ] );
		
		//Nodes[ 2, 7 ].AddNeighbor( Nodes[ 2, 0 ] );
		//Nodes[ 2, 7 ].AddNeighbor( Nodes[ 1, 7 ] );
		//Nodes[ 2, 7 ].AddNeighbor( Nodes[ 3, 6 ] );
		//Nodes[ 2, 7 ].AddNeighbor( Nodes[ 2, 6 ] );
		
		//// Row 3
		
		//// Non-connected node
		
		//Nodes[ 3, 1 ].AddNeighbor( Nodes[ 0, 2 ] );
		//Nodes[ 3, 1 ].AddNeighbor( Nodes[ 0, 6 ] );
		//Nodes[ 3, 1 ].AddNeighbor( Nodes[ 1, 1 ] );
		//Nodes[ 3, 1 ].AddNeighbor( Nodes[ 1, 0 ] );
		
		//Nodes[ 3, 2 ].AddNeighbor( Nodes[ 1, 3 ] );
		//Nodes[ 3, 2 ].AddNeighbor( Nodes[ 0, 5 ] );
		//Nodes[ 3, 2 ].AddNeighbor( Nodes[ 1, 2 ] );
		//Nodes[ 3, 2 ].AddNeighbor( Nodes[ 0, 1 ] );
		
		//// Non-connected node
		//// Non-connected node
		
		//Nodes[ 3, 5 ].AddNeighbor( Nodes[ 0, 6 ] );
		//Nodes[ 3, 5 ].AddNeighbor( Nodes[ 0, 2 ] );
		//Nodes[ 3, 5 ].AddNeighbor( Nodes[ 1, 5 ] );
		//Nodes[ 3, 5 ].AddNeighbor( Nodes[ 1, 4 ] );
		
		//Nodes[ 3, 6 ].AddNeighbor( Nodes[ 1, 7 ] );
		//Nodes[ 3, 6 ].AddNeighbor( Nodes[ 0, 1 ] );
		//Nodes[ 3, 6 ].AddNeighbor( Nodes[ 1, 6 ] );
		//Nodes[ 3, 6 ].AddNeighbor( Nodes[ 0, 5 ] );
		
		//// Non-connected node

		#endregion
	}

	#region Rotations

	private void RotateX( RotationEnum direction )
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

	private void RotateY( RotationEnum direction )
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

	private void RotateZ( RotationEnum direction )
	{
		if ( direction == RotationEnum.Clockwise )
		{
			// TODO The whole thing!!!

			//Nodes[ 0, 5 ].ReplaceNeighbor( Nodes[ 1, 0 ], Nodes[ 0, 2 ] );
			//Nodes[ 0, 6 ].ReplaceNeighbor( Nodes[ 2, 0 ], Nodes[ 0, 1 ] );

			//Nodes[ 1, 7 ].ReplaceNeighbor( Nodes[ 3, 1 ], Nodes[ 1, 0 ] );
			//Nodes[ 2, 7 ].ReplaceNeighbor( Nodes[ 3, 2 ], Nodes[ 2, 0 ] );

			//Nodes[ 3, 6 ].ReplaceNeighbor( Nodes[ 2, 4 ], Nodes[ 3, 1 ] );
			//Nodes[ 3, 5 ].ReplaceNeighbor( Nodes[ 1, 4 ], Nodes[ 3, 2 ] );

			//Nodes[ 2, 4 ].ReplaceNeighbor( Nodes[ 0, 1 ], Nodes[ 2, 3 ] );
			//Nodes[ 1, 4 ].ReplaceNeighbor( Nodes[ 0, 2 ], Nodes[ 1, 3 ] );

			// Transform all of the affected nodes by moving them horizontally in the array.
			//for ( var i = 0; i < 2; i++ )
			//{
			//	for ( var j = 0; j < 2; j++ )
			//	{
			//		var iMin = i;
			//		var iMax = 3 - i;
			//		var jMin = j + 2;
			//		var jMax = 3 - j + 2;

			//		var temp = Nodes[ i, j ];
			//		Nodes[ iMin, jMin ] = Nodes[ iMax, jMin ];
			//		Nodes[ iMax, jMin ] = Nodes[ iMax, jMax ];
			//		Nodes[ iMax, jMax ] = Nodes[ iMin, jMax ];
			//		Nodes[ iMin, jMax ] = temp;
			//	}
			//}
		}
		else
		{
			throw new NotImplementedException( "CCW rotation isn't in yet!" );
		}
	}

	#endregion
}
