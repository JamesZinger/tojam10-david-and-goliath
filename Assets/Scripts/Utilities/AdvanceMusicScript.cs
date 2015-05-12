using UnityEngine;
using System.Collections;

public class AdvanceMusicScript : MonoBehaviour
{
	private bool isBeginComplete = false;
	private AudioSource musicBegin;
	private AudioSource musicLoop;

	void Start() 
	{
		Transform t = transform.FindChild( "MusicBegin" );
		if ( t != null )
		{
			musicBegin = t.audio;
		}		
		t = transform.FindChild( "MusicLoop" );
		if ( t != null )
		{
			musicLoop = t.audio;
		}
	}
	
	// Update is called once per frame
	void Update() 
	{
		if ( !isBeginComplete && !musicBegin.isPlaying )
		{
			isBeginComplete = true;
			musicLoop.Play();
		}
	}
}
