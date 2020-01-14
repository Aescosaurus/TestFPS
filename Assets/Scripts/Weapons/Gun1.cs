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
		bulletSpawnPos = transform.Find( "Bullet Spawn Pos" );
		Assert.IsNotNull( bulletSpawnPos );
	}

	void Update()
	{
		if( refire.Update( Time.deltaTime ) &&
			Input.GetAxis( "Attack" ) > 0.0f )
		{
			refire.Reset();

			var bullet = Instantiate( bulletPrefab );
			bullet.transform.position = bulletSpawnPos.position;
			bullet.GetComponent<Rigidbody>().AddForce(
				transform.right * bulletSpeed,
				ForceMode.Impulse );
			var bulletScr = bullet.GetComponent<Bullet>();
			bulletScr.knockback = knockback;
			// RaycastHit hit;
			// if( Physics.Raycast( transform.position,
			// 	transform.right,out hit,range,
			// 	LayerMask.GetMask( "Movable" ) ) )
			// {
			// 	hit.rigidbody.AddForce( transform.right *
			// 		knockback,ForceMode.Impulse );
			// }
		}
	}

	GameObject bulletPrefab;
	Transform bulletSpawnPos;

	[SerializeField] Timer refire = new Timer( 0.3f );
	[SerializeField] float bulletSpeed = 10.0f;
	// [SerializeField] float range = 50.0f;
	[SerializeField] float knockback = 1.2f;
}
