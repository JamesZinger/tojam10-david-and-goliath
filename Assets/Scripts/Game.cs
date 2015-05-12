using System;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
	public static Graph LevelGraph { get; private set; }
	public static int LevelNumber { get; private set; }
	private static List<Func<Graph>> levelGenerationFuncs = new List<Func<Graph>>() {
		GenerateLevel01,
		GenerateLevel02,
		GenerateLevel03,
		GenerateLevel04,
		GenerateLevel05,
		GenerateLevel06
	};


	#region Level Generation

	public static Graph GenerateLevelGraph( int levelNumber )
	{
		// Check that the passed level number is sane.
		if ( levelNumber < 1 || levelNumber > levelGenerationFuncs.Count )
		{
			throw new ArgumentOutOfRangeException( "The requested level doesn't exist!" );
		}

		// Call the indexed function.
		return levelGenerationFuncs[ levelNumber - 1 ]();
	}


	private static Graph GenerateLevel01()
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
	

	private static Graph GenerateLevel02()
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
	

	private static Graph GenerateLevel03()
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
	

	private static Graph GenerateLevel04()
	{
		Graph graph = new Graph( 4, 8 );

		graph.Nodes[ 2, 6 ].Type = NodeTypeEnum.Start;
		graph.Nodes[ 1, 3 ].Type = NodeTypeEnum.End;

		graph.Nodes[ 2, 6 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 1, 0 ].MoveableDirections.Add( Node.Direction.down    );
		graph.Nodes[ 1, 0 ].MoveableDirections.Add( Node.Direction.left    );
		graph.Nodes[ 0, 5 ].MoveableDirections.Add( Node.Direction.left    );
		graph.Nodes[ 0, 5 ].MoveableDirections.Add( Node.Direction.down    );
		graph.Nodes[ 0, 6 ].MoveableDirections.Add( Node.Direction.right    );
		graph.Nodes[ 0, 6 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 1, 6 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 1, 6 ].MoveableDirections.Add( Node.Direction.down   );
		graph.Nodes[ 2, 4 ].MoveableDirections.Add( Node.Direction.down    );
		graph.Nodes[ 2, 4 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 3, 5 ].MoveableDirections.Add( Node.Direction.down    );
		graph.Nodes[ 3, 5 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 0, 2 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 0, 2 ].MoveableDirections.Add( Node.Direction.down    );
		graph.Nodes[ 1, 2 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 1, 2 ].MoveableDirections.Add( Node.Direction.down    );
		graph.Nodes[ 1, 5 ].MoveableDirections.Add( Node.Direction.down    );
		graph.Nodes[ 1, 5 ].MoveableDirections.Add( Node.Direction.left    );
		graph.Nodes[ 1, 4 ].MoveableDirections.Add( Node.Direction.right    );
		graph.Nodes[ 1, 4 ].MoveableDirections.Add( Node.Direction.down    );
		graph.Nodes[ 1, 3 ].MoveableDirections.Add( Node.Direction.down    );

		return graph;
	}
	

	private static Graph GenerateLevel05()
	{
		Graph graph = new Graph( 4, 8 );

		graph.Nodes[ 0, 1 ].Type = NodeTypeEnum.Start;
		graph.Nodes[ 0, 2 ].Type = NodeTypeEnum.End;

		graph.Nodes[ 0, 1 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 0, 6 ].MoveableDirections.Add( Node.Direction.down    );
		graph.Nodes[ 0, 6 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 1, 6 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 1, 6 ].MoveableDirections.Add( Node.Direction.down    );
		graph.Nodes[ 2, 4 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 2, 4 ].MoveableDirections.Add( Node.Direction.left   );
		graph.Nodes[ 2, 3 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 2, 3 ].MoveableDirections.Add( Node.Direction.right    );
		graph.Nodes[ 1, 0 ].MoveableDirections.Add( Node.Direction.left    );
		graph.Nodes[ 1, 0 ].MoveableDirections.Add( Node.Direction.right    );
		graph.Nodes[ 1, 7 ].MoveableDirections.Add( Node.Direction.down    );
		graph.Nodes[ 1, 7 ].MoveableDirections.Add( Node.Direction.right    );
		graph.Nodes[ 1, 1 ].MoveableDirections.Add( Node.Direction.left   );
		graph.Nodes[ 1, 1 ].MoveableDirections.Add( Node.Direction.down    );
		graph.Nodes[ 2, 5 ].MoveableDirections.Add( Node.Direction.up   );
		graph.Nodes[ 2, 5 ].MoveableDirections.Add( Node.Direction.down    );
		graph.Nodes[ 3, 5 ].MoveableDirections.Add( Node.Direction.right    );
		graph.Nodes[ 3, 5 ].MoveableDirections.Add( Node.Direction.left    );
		graph.Nodes[ 3, 6 ].MoveableDirections.Add( Node.Direction.left    );
		graph.Nodes[ 3, 6 ].MoveableDirections.Add( Node.Direction.right    );
		graph.Nodes[ 2, 6 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 2, 6 ].MoveableDirections.Add( Node.Direction.down    );
		graph.Nodes[ 1, 2 ].MoveableDirections.Add( Node.Direction.right  );
		graph.Nodes[ 1, 2 ].MoveableDirections.Add( Node.Direction.down    );
		graph.Nodes[ 1, 3 ].MoveableDirections.Add( Node.Direction.up   );
		graph.Nodes[ 1, 3 ].MoveableDirections.Add( Node.Direction.left    );
		graph.Nodes[ 0, 2 ].MoveableDirections.Add( Node.Direction.right    );

		return graph;
	}
	

	private static Graph GenerateLevel06()
	{
		Graph graph = new Graph( 4, 8 );

		graph.Nodes[ 2, 3 ].Type = NodeTypeEnum.Start;
		graph.Nodes[ 3, 2 ].Type = NodeTypeEnum.End;
		
		graph.Nodes[ 2, 3 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 1, 7 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 1, 7 ].MoveableDirections.Add( Node.Direction.left  );
		graph.Nodes[ 1, 6 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 1, 6 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 0, 6 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 0, 6 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 2, 5 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 2, 5 ].MoveableDirections.Add( Node.Direction.left  );
		graph.Nodes[ 2, 4 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 2, 4 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 1, 3 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 1, 3 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 0, 2 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 0, 2 ].MoveableDirections.Add( Node.Direction.down  );
		graph.Nodes[ 1, 2 ].MoveableDirections.Add( Node.Direction.up    );
		graph.Nodes[ 1, 2 ].MoveableDirections.Add( Node.Direction.left  );
		graph.Nodes[ 2, 6 ].MoveableDirections.Add( Node.Direction.down  );
		graph.Nodes[ 2, 6 ].MoveableDirections.Add( Node.Direction.left  );
		graph.Nodes[ 3, 6 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 3, 6 ].MoveableDirections.Add( Node.Direction.left  );
		graph.Nodes[ 1, 5 ].MoveableDirections.Add( Node.Direction.right );
		graph.Nodes[ 1, 5 ].MoveableDirections.Add( Node.Direction.down  );
		graph.Nodes[ 3, 2 ].MoveableDirections.Add( Node.Direction.up    );

		return graph;
	}

	#endregion




	#region Level Loading

	public static void LoadLevel( int levelNumber )
	{
		// Check that the passed level number is sane.
		if ( levelNumber > levelGenerationFuncs.Count )
		{
			Application.LoadLevel( "MainMenu" );
		}
		
		// Set the level number and use it to generate the level's graph.
		Game.LevelNumber = levelNumber;
		Game.LevelGraph = GenerateLevelGraph( levelNumber );

		// Load the gameplay scene.
		Application.LoadLevel( "Gameplay" );
	}


	public static void LoadFirstLevel()
	{
		Game.LoadLevel( 1 );
	}


	public static void LoadNextLevel()
	{
		Game.LoadLevel( Game.LevelNumber + 1 );
	}


	public static void ReturnToMainMenu()
	{
		Application.LoadLevel( "MainMenu" );
	}

	#endregion
}
