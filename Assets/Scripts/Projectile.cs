using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	public int damage = 1;
	public float speed = 1.0f;
	public Explosion explosion;

	Vector3 origin;

	public void SetOrigin(Vector3 originPosition)
	{
		origin = originPosition;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position += transform.forward * speed * Time.fixedDeltaTime;

		if(Vector3.Distance(transform.position, origin) > 10.0f) {
			if (explosion) {
				Explosion obj = Instantiate (explosion);
				obj.transform.position = transform.position;
			}
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.isTrigger)
			return;

		Actor actor = other.GetComponent<Actor> ();
		if(actor) {
			actor.Damage (damage);

			actor.GetComponent<Rigidbody> ().AddExplosionForce (500.0f, transform.position, 1.0f);
		}

		if (explosion) {
			Explosion obj = Instantiate (explosion);
			obj.transform.position = transform.position;
		}

		Destroy (this.gameObject);
	}
}
