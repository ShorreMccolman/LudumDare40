using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	public int damage = 1;
	public float speed = 1.0f;
	public Explosion explosion;
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position += transform.forward * speed * Time.fixedDeltaTime;
	}

	void OnTriggerEnter(Collider other)
	{
		Actor actor = other.GetComponent<Actor> ();
		if(actor) {
			actor.Damage (damage);
		}

		if (explosion) {
			Explosion obj = Instantiate (explosion);
			obj.transform.position = transform.position;
		}

		Destroy (this.gameObject);
	}
}
