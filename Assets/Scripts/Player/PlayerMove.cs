using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerMove
	:
	MonoBehaviour
{
	void Start()
	{
		body = GetComponent<Rigidbody>();
		Assert.IsNotNull( body );
		cam = transform.Find( "Main Camera" );
		Assert.IsNotNull( cam );
		boxColl = GetComponent<BoxCollider>();
	}

	void Update()
	{
		moveInput.x = Input.GetAxis( "Horizontal" );
		moveInput.y = Input.GetAxis( "Vertical" );

		var footOverlaps = Physics.OverlapBox( boxColl.bounds.center,
			boxColl.bounds.extents );
		bool touchingGround = false;
		foreach( var footOverlap in footOverlaps )
		{
			if( footOverlap.gameObject.layer != LayerMask
				.NameToLayer( "Player" ) )
			{
				touchingGround = true;
				break;
			}
		}
		
		jumping = false;
		if( Input.GetAxis( "Jump" ) > 0.0f && touchingGround )
		{
			jumping = true;
		}

		var forward = cam.forward;
		forward.y = 0.0f;

		var xMove = cam.right * moveInput.x;
		var yMove = forward * moveInput.y;
		var move = ( xMove + yMove ).normalized;
		move.y = 0.0f;

		transform.Translate( move * moveSpeed * Time.fixedDeltaTime );

		if( jumping )
		{
			var vel = body.velocity;
			vel.y = 0.0f;
			body.velocity = vel;
			body.AddForce( Vector3.up * jumpPower,
				ForceMode.Impulse );
			jumping = false;
		}
	}

	Rigidbody body;
	Transform cam;
	BoxCollider boxColl;

	[SerializeField] float moveSpeed = 5.0f;
	[SerializeField] float jumpPower = 1.0f;

	Vector2 moveInput = Vector2.zero;
	bool jumping = false;
}
