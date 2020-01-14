using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet
	:
	MonoBehaviour
{
	void Update()
	{
		if( lifetimer.Update( Time.deltaTime ) )
		{
			Destroy( gameObject );
		}
	}

	void OnCollisionEnter( Collision coll )
	{
		if( coll.gameObject.layer != LayerMask.NameToLayer( "Player" ) )
		{
			if( coll.rigidbody != null )
			{
				coll.rigidbody.AddForce( GetComponent<Rigidbody>()
					.velocity * knockback,ForceMode.Impulse );
			}
			Destroy( gameObject );
		}
	}

	[SerializeField] Timer lifetimer = new Timer( 1.0f );

	public float knockback = 1.0f;
	// public float damage = 1.0f;
}
