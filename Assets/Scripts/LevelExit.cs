using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		Player player = other.GetComponent<Player> ();
		if(player) {
			player.Victory ();
		}
	}
}
