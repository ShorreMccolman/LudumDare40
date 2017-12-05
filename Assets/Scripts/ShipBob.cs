using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBob : MonoBehaviour {

	public float magnitude;
	public float speed;

	int dir = 1;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		Vector3 pos = transform.localPosition;
		transform.localPosition += speed * Vector3.up * dir * Time.deltaTime;

		if (pos.y > magnitude) {
			dir = -1;
		} else if (pos.y < -magnitude) {
			dir = 1;
		}

	}
}
