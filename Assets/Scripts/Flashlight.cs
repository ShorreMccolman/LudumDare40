using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : Item {

	public override void EquipItemToPlayer(Player player)
	{
		player.EquipFlashlight ();
	}
}
