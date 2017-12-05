using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnim : MonoBehaviour {

	public GameObject shipmesh;
	bool hold = true;

	// Use this for initialization
	void Start () {
		Invoke ("Release", 0.1f);
	}

	void Update()
	{
		if(hold) {
			return;
		}

		float dist = Vector3.Distance (transform.localPosition, Vector3.zero);

		if(dist < 0.3f) {

			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			this.enabled = false;

		}

		if(dist < 1.0f) {
			shipmesh.SetActive (false);
		}

		transform.localPosition = Vector3.Lerp (transform.localPosition, Vector3.zero,  0.8f * Time.deltaTime);
		transform.localRotation = Quaternion.Lerp (transform.localRotation, Quaternion.identity, 0.8f * Time.deltaTime);
	}

	void Release()
	{
		hold = false;
	}
}
