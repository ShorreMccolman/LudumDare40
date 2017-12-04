using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
	LavaLamp,
	ToyCar,
	Invalid
}

public class Collectable : Item {
	public CollectableType type;
	public override void EquipItemToPlayer(Player player)
	{
		player.EquipCollectable (type);
	}
}
