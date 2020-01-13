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
		// Check for input for picking up objects.
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
					heldObject = hit.rigidbody;
					heldObject.GetComponent<Rigidbody>().useGravity = false;
				}
			}
			else
			{
				DropObject();
			}
		}

		// Move object if one is held.
		if( heldObject != null )
		{
			var desiredPos = transform.position +
				cam.transform.forward * holdDist;
			// var diff = desiredPos - heldObject.transform.position;
			// 
			// if( diff.sqrMagnitude > Mathf.Pow(
			// 	correctionSpeed * Time.deltaTime,2 ) )
			// {
			// 	heldObject.transform.position += diff.normalized *
			// 		correctionSpeed * Time.fixedDeltaTime;
			// }

			heldObject.transform.position = desiredPos;
			heldObject.transform.rotation = cam.transform.rotation;

			if( Input.GetAxis( "Attack" ) > 0.0f )
			{
				heldObject.AddForce( cam.transform.forward *
					throwForce,ForceMode.Impulse );
				DropObject();
			}
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
	[SerializeField] float throwForce = 2.0f;

	Rigidbody heldObject = null;
}
