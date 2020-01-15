using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWalk
	:
	MonoBehaviour
{
	void Start()
	{
		// GetComponent<Animator>().SetTrigger( "Walk" );
	}

	void Update()
	{
		if( !walkDuration.Update( Time.deltaTime ) )
		{
			transform.Translate( Vector3.forward * speed *
				Time.deltaTime );
		}
		else
		{
			walkDuration.Reset();
			transform.Rotate( Vector3.up,180.0f );
		}
	}

	[SerializeField] float speed = 1.5f;
	[SerializeField] Timer walkDuration = new Timer( 5.0f );
}
