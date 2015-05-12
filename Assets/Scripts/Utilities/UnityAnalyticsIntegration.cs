using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Cloud.Analytics;

public class UnityAnalyticsIntegration : MonoBehaviour 
{
	public bool IsSendingAnalytics = true;
	public Cube Cube;
	public TheGoat Goat;


	private int Deaths;
	private string LevelName;
	private List<RotationAxis> Rotations;


	#region Init/Finalize

    void Start() 
	{
		// Create a new list to store rotations.
		Rotations = new List<RotationAxis>();

		// Start the analytics API and network interface.
		const string projectId = "42302f4c-886d-4bf1-97bc-a5f75d5dd461";
        UnityAnalytics.StartSDK( projectId );

		// Hook up delegates to listen for tracked events.
		Cube.Configured	  += OnCubeConfigured;
		Cube.Resetted	  += OnCubeResetted;
		Cube.Rotated	  += OnCubeRotated;
		Goat.ArrivedAtEnd += OnGoatArrivedAtEnd;
		Goat.Died		  += OnGoatDied;
    }


	void OnDestroy()
	{
		// Disconnect the event handlers to prevent memory leaks.
		Cube.Configured	  -= OnCubeConfigured;
		Cube.Resetted	  -= OnCubeResetted;
		Cube.Rotated	  -= OnCubeRotated;
		Goat.ArrivedAtEnd -= OnGoatArrivedAtEnd;
		Goat.Died		  -= OnGoatDied;
	}

	#endregion


	#region Event Handlers

	private void OnCubeConfigured( string levelName )
	{
		// Initialize the parameters.
		Deaths = 0;
		LevelName = levelName;
		Rotations.Clear();
	}


	private void OnCubeResetted()
	{
		// Track the current rotation list, death number, and time.
		// { "<Level Name>Reset", [<Level Name>, <Rotation List>, <Death Number>, <Time>] }
		if ( IsSendingAnalytics )
		{
			var result = UnityAnalytics.CustomEvent( "Reset", new Dictionary<string, object> 
			{
				{ "LevelName", LevelName },
				{ "Rotations", RotationsToString() },
				{ "Deaths", Deaths },
				{ "Time", Time.time }
			} );
			Debug.Log( "\"Reset\" Analytics Result: " + result );
		}

		// Clear the current rotation list.
		Rotations.Clear();

		// Clear the current death number?
		// TODO?
	}


	private void OnCubeRotated( RotationAxis axis )
	{
		// Store the rotation in the rotation list.
		Rotations.Add( axis );
	}


	private void OnGoatArrivedAtEnd()
	{
		// Track the current rotation list, death number, level name, and time.
		// { "<Level Name>Complete", [<Level Name>, <Rotation List>, <Death Number>, <Time>] }
		if ( IsSendingAnalytics )
		{
			var result = UnityAnalytics.CustomEvent( "Complete", new Dictionary<string, object> 
			{
				{ "LevelName", LevelName },
				{ "Rotations", RotationsToString() },
				{ "Deaths", Deaths },
				{ "Time", Time.time }
			} );
			Debug.Log( "\"Complete\" Analytics Result: " + result );
		}

		// Clear the current rotation list.
		Rotations.Clear();

		// Clear the current death number.
		Deaths = 0;
	}


	private void OnGoatDied( GoatDeathTypeEnum reason )
	{
		// Increment the death counter.
		Deaths++;

		// Track the current rotation list, death number, and time.
		// { "<Level Name>Death", [<Level Name>, <Rotation List>, <Death Number>, <Reason>, <Time>] }
		if ( IsSendingAnalytics )
		{
			var result = UnityAnalytics.CustomEvent( "Death", new Dictionary<string, object> 
			{
				{ "LevelName", LevelName },
				{ "Rotations", RotationsToString() },
				{ "Deaths", Deaths },
				{ "Reason", DeathReasonToString( reason ) }, 
				{ "Time", Time.time }
			} );
			Debug.Log( "\"Death\" Analytics Result: " + result );
		}

		// Clear the current rotation list.
		Rotations.Clear();
	}

	#endregion


	#region Helpers

	private string DeathReasonToString( GoatDeathTypeEnum reason )
	{
		switch ( reason )
		{
			case GoatDeathTypeEnum.RanOffPath:		return "RanOffPath";
			case GoatDeathTypeEnum.RanOffEdge:		return "RanOffEdge";
			case GoatDeathTypeEnum.RotatedOffEdge:	return "RotatedOffEdge";
			case GoatDeathTypeEnum.EdgeBug:			return "EdgeBug";
			case GoatDeathTypeEnum.ReversedToStart: return "ReversedToStart";
			default:								return "Unknown";
		}
	}


	private string RotationsToString()
	{
		StringBuilder sb = new StringBuilder();
		for ( int i = 0; i < Rotations.Count; i++ )
		{
			switch ( Rotations[ i ] )
			{
				case RotationAxis.X: sb.Append( 'X' ); break;
				case RotationAxis.Y: sb.Append( 'Y' ); break;
				case RotationAxis.Z: sb.Append( 'Z' ); break;
			}
		}
		return sb.ToString();
	}

	#endregion
}