using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerPickup
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
		pickupBuffer.Update( Time.deltaTime );
		if( Input.GetAxis( "Interact" ) > 0.0f &&
			pickupBuffer.IsDone() )
		{
			pickupBuffer.Reset();
			if( heldObject == null )
			{
				RaycastHit hit;
				if( Physics.Raycast( transform.position,
					cam.transform.forward,out hit,
					holdDist * 2.0f,LayerMask.GetMask( "Movable" ) ) )
				{
					heldObject = hit.transform.gameObject;
					heldObject.GetComponent<Rigidbody>().useGravity = false;
				}
			}
			else
			{
				DropObject();
			}
		}
	}

	void FixedUpdate()
	{
		if( heldObject != null )
		{
			var desiredPos = transform.position +
				cam.transform.forward * holdDist;
			// var diff = heldObject.transform.position - desiredPos;
			// 
			// heldObject.transform.position += diff.normalized *
			// 	correctionSpeed * Time.fixedDeltaTime;
			heldObject.transform.position = desiredPos;
			heldObject.transform.rotation = cam.transform.rotation;
		}
	}

	void DropObject()
	{
		heldObject.GetComponent<Rigidbody>().useGravity = true;
		heldObject = null;
	}

	Camera cam;

	[SerializeField] float holdDist = 1.5f;
	// [SerializeField] float correctionSpeed = 0.9f;
	[SerializeField] Timer pickupBuffer = new Timer( 0.2f );

	GameObject heldObject = null;
}
