using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : Item {

	public override void EquipItemToPlayer(Player player)
	{
		player.EquipSpeedBoost ();
	}
}
