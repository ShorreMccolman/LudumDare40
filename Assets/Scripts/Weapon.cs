using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {
	
	public Projectile ProjectilePrefab;

	public override void EquipItemToPlayer(Player player)
	{
		if (player.CurrentWeapon == null)
			player.EnableBlasters ();

		player.CurrentWeapon = this;
		player.Weapons.Add (this);
	}
}
