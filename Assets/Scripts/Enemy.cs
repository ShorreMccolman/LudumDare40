using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor {
	public float fireRate = 1.0f;
	public float viewAngle = 10.0f;
	float cooldown;

	public Light headlight;
	
	// Update is called once per frame
	void FixedUpdate () {

		Player player = FindObjectOfType<Player> ();
		if(player) {
			float dist = Vector3.Distance (player.transform.position, transform.position);
			if (dist < 15.0f) {
				transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (player.transform.position - transform.position), Time.fixedDeltaTime);

				if (dist > 7.0f) {
					headlight.color = Color.yellow;
					ForwardMotion (1.0f * Time.fixedDeltaTime);
				} else {
					if (dist < 5.0f) {
						ForwardMotion (-1.0f * Time.fixedDeltaTime);
					}

					headlight.color = Color.red;
					if(cooldown <= 0) {
						if( Vector3.Angle (transform.forward, player.transform.position - transform.position) < viewAngle)
							FireWeapon ();
					} else {
						cooldown -= Time.deltaTime;
					}
				}
			} else {
				headlight.color = Color.white;
			}
		}
	}

	void UpdateWithPlayerLevel(int level)
	{
		
	}

	public override void Damage(int damage)
	{
		base.Damage (damage);

		if(CurrentHealth <= 0) {
			Destroy (this.gameObject);
		}
	}
		
	protected override void FireWeapon()
	{
		base.FireWeapon ();

		cooldown = fireRate;
	}
}
