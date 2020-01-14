using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerCamera
	:
	MonoBehaviour
{
	void Start()
	{
		cam = transform.Find( "Main Camera" );
		Assert.IsNotNull( cam );
		crosshair = Resources.Load<Texture2D>( "Images/Crosshair" );
		Assert.IsNotNull( crosshair );

		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		if( Input.GetKeyDown( KeyCode.Escape ) )
		{
			Cursor.lockState = CursorLockMode.None;
		}
		if( Input.GetMouseButtonDown( 0 ) )
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		var aim = new Vector2( Input.GetAxis( "Mouse X" ),
			Input.GetAxis( "Mouse Y" ) );

		cam.transform.eulerAngles = new Vector3(
			cam.eulerAngles.x - aim.y * rotationSpeed * Time.deltaTime,
			cam.eulerAngles.y + aim.x * rotationSpeed * Time.deltaTime,
			cam.eulerAngles.z );
	}

	void OnGUI()
	{
		float x = ( Screen.width / 2 ) - ( crosshair.width / 2 );
		float y = ( Screen.height / 2 ) - ( crosshair.height / 2 );
		GUI.DrawTexture( new Rect( x,y,crosshair.width,crosshair.height ),crosshair );
	}

	Transform cam;
	Texture2D crosshair;

	[SerializeField] float rotationSpeed = 5.0f;
}
