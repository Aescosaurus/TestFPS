using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Gun1
	:
	MonoBehaviour
{
	void Start()
	{
		bulletPrefab = Resources.Load<GameObject>( "Prefabs/Bullet" );
		Assert.IsNotNull( bulletPrefab );
		pickupScript = GameObject.FindGameObjectWithTag( "Player" )
			.GetComponent<PlayerPickup>();
		Assert.IsNotNull( pickupScript );
		bulletSpawnPos = pickupScript.transform;
		Assert.IsNotNull( bulletSpawnPos );
		cam = Camera.main;
		Assert.IsNotNull( cam );
	}

	void Update()
	{
		if( refire.Update( Time.deltaTime ) &&
			Input.GetAxis( "Attack" ) > 0.0f &&
			!pickupScript.HoldingObject() )
		{
			refire.Reset();

			// var bullet = Instantiate( bulletPrefab );
			// bullet.transform.position = bulletSpawnPos.position +
			// 	transform.right * 0.4f;
			// bullet.GetComponent<Rigidbody>().AddForce(
			// 	transform.right * bulletSpeed,
			// 	ForceMode.Impulse );
			// var bulletScr = bullet.GetComponent<Bullet>();
			// bulletScr.knockback = knockback;
			RaycastHit hit;
			if( Physics.Raycast( cam.transform.position,
				cam.transform.forward,out hit,range,
				LayerMask.GetMask( "Movable" ) ) )
			{
				hit.rigidbody.AddForce( cam.transform.forward *
					knockback,ForceMode.Impulse );
			}
		}
	}

	GameObject bulletPrefab;
	PlayerPickup pickupScript;
	Transform bulletSpawnPos;
	Camera cam;

	[SerializeField] Timer refire = new Timer( 0.3f );
	// [SerializeField] float bulletSpeed = 10.0f;
	[SerializeField] float range = 50.0f;
	[SerializeField] float knockback = 1.2f;
}
