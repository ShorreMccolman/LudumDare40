using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StatsForLevel {

	public bool dormant;
	public float firerate;
	public float rotationSpeed;
	public float moveSpeed;

	public StatsForLevel(int Level)
	{
		dormant = false;
		firerate = 1.0f;
		rotationSpeed = 1.0f;
		moveSpeed = 1.0f;

		if(Level == 1) {
			
			dormant = true;
			firerate = 1.5f;
			rotationSpeed = 0.5f;
			moveSpeed = 0.0f;

		} else if (Level == 2) {

			firerate = 1.0f;
			rotationSpeed = 1.0f;
			moveSpeed = 0.0f;
			
		} else if (Level == 3) {

			firerate = 1.0f;
			rotationSpeed = 1.0f;
			moveSpeed = 0.5f;
			
		} else if (Level == 4) {

			firerate = 0.8f;
			rotationSpeed = 1.0f;
			moveSpeed = 1.0f;

		} else if (Level == 5) {

			firerate = 0.7f;
			rotationSpeed = 1.5f;
			moveSpeed = 1.0f;

		} else if (Level == 6) {

			firerate = 0.6f;
			rotationSpeed = 1.5f;
			moveSpeed = 1.25f;

		} else if (Level == 7) {

			firerate = 0.5f;
			rotationSpeed = 2.0f;
			moveSpeed = 1.5f;

		} else if (Level == 8) {

			firerate = 0.3f;
			rotationSpeed = 2.0f;
			moveSpeed = 1.8f;

		} else if (Level == 9) {

			firerate = 0.2f;
			rotationSpeed = 3.0f;
			moveSpeed = 2.0f;

		}
	}
}

public class Enemy : Actor {
	public float viewAngle = 10.0f;

	float fireRate;
	float rotationSpeed;
	float moveSpeed;
	bool dormant;
	float cooldown;

	public Weapon defaultWeapon;
	public Light headlight;

	Vector3 origin;
	
	// Update is called once per frame
	void FixedUpdate () {

		if (dormant)
			return;

		Player player = FindObjectOfType<Player> ();
		if(player) {
			float dist = Vector3.Distance (player.transform.position, transform.position);
			if (dist < 15.0f) {

				Vector3 towardPlayer = player.transform.position - transform.position;

				RaycastHit hit;
				Ray ray = new Ray (transform.position, towardPlayer);
				if(Physics.Raycast (ray, out hit)) {
					if(hit.collider.tag != "Player") {
						return;
					}
				}

				transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (player.transform.position - transform.position), rotationSpeed * Time.fixedDeltaTime);

				float angle = Vector3.Angle (transform.forward, towardPlayer);

				if (dist > 10.0f) {
					headlight.color = Color.yellow;
					if(angle < viewAngle * 2.0f)
						ForwardMotion (moveSpeed * Time.fixedDeltaTime);
				} else {
					if (dist < 5.0f && angle < viewAngle) {
						ForwardMotion (-moveSpeed * Time.fixedDeltaTime);
					}

					headlight.color = Color.red;
					if(cooldown <= 0) {
						if( angle < viewAngle)
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

	protected override void Init()
	{
		base.Init ();

		CurrentWeapon = defaultWeapon;
		origin = transform.position;
	}

	public void Reset()
	{
		transform.position = origin;
		CurrentHealth = maxHealth;
	}

	public void UpdateWithPlayerLevel(int level)
	{
		StatsForLevel stats = new StatsForLevel (level);
		dormant = stats.dormant;
		fireRate = stats.firerate;
		rotationSpeed = stats.rotationSpeed;
		moveSpeed = stats.moveSpeed;
	}

	public override void Damage(int damage)
	{
		base.Damage (damage);

		if(CurrentHealth <= 0) {
			EnemyManager.Instance.KillEnemy (this);
		}
	}
		
	protected override void FireWeapon()
	{
		base.FireWeapon ();

		cooldown = fireRate;
	}
}
