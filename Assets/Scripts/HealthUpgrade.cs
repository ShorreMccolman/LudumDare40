using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : Item {

	public int upgradeAmount;

	public override void EquipItemToPlayer(Player player)
	{
		player.UpgradeHealth (upgradeAmount);
		player.HealthCollected += 1;
	}
}
