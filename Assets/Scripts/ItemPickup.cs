using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

	public Item item;

	void OnTriggerEnter(Collider other)
	{
		Player player = other.GetComponent<Player> ();
		if(player) {
			player.CollectItem (item);
			Destroy (this.gameObject);
		}
	}
}
