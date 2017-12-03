using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	public float lifetime = 0.8f;

	void Start()
	{
		Invoke ("Die", lifetime);
	}

	void Die()
	{
		Destroy (this.gameObject);
	}
}
