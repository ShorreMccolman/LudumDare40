using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsManager : MonoBehaviour {

	public static List<Light[]> activeLights = new List<Light[]>();

	public Light[] lights;

	void OnTriggerEnter(Collider other) {
		Player player = other.GetComponent<Player> ();
		if(player) {
			activeLights.Add (lights);
			foreach(Light light in lights) {
				light.enabled = true;
			}
		}
	}

	void OnTriggerExit(Collider other) {
		Player player = other.GetComponent<Player> ();
		if(player) {
			activeLights.Remove (lights);

			foreach (Light light in lights) {
				bool isOn = false;
				foreach (Light[] arr in activeLights) {
					foreach(Light arrl in arr) {
						if(arrl == light) {
							isOn = true;
						}
					}
				}

				if (!isOn)
					light.enabled = false;
			}
		}
	}
}
