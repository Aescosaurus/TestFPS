using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class DoorOpener
	:
	MonoBehaviour
{
	void Start()
	{
		cam = Camera.main;
		Assert.IsNotNull( cam );
	}

	void Update()
	{
		if( Input.GetAxis( "Interact" ) > 0.0f )
		{
			RaycastHit hit;
			if( Physics.Raycast( cam.transform.position,
				cam.transform.forward,out hit,range,
				LayerMask.GetMask( "Door" ) ) )
			{
				var doorScr = hit.transform.GetComponent<Door>();
				Assert.IsNotNull( doorScr );
				if( doorScr.openable )
				{
					SceneManager.LoadScene( doorScr.levelName );
				}
			}
		}
	}

	Camera cam;

	[SerializeField] float range = 0.6f;
}
