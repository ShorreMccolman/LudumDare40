using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	public Image weaponIcon1;
	public Text weaponLabel1;
	public Image weaponIcon2;
	public Text weaponLabel2;

	public Image upgradeIcon1;
	public Text upgradeLabel1;
	public Image upgradeIcon2;
	public Text upgradeLabel2;
	public Image upgradeIcon3;
	public Text upgradeLabel3;
	public Image upgradeIcon4;
	public Text upgradeLabel4;

	public Image collectableIcon1;
	public Text collectableLabel1;
	public Image collectableIcon2;
	public Text collectableLabel2;

	public void OpenMenu(Player player)
	{
		if(player.Weapons.Count > 0) {
			weaponIcon1.color = Color.white;
			weaponLabel1.text = "Blaster";
		} else {
			weaponIcon1.color = new Color (0.0f, 0.0f, 0.0f, 0.5f);
			weaponLabel1.text = "???";
		}

		if(player.Weapons.Count > 1) {
			weaponIcon2.color = Color.white;
			weaponLabel2.text = "Cannon";
		} else {
			weaponIcon2.color = new Color (0.0f, 0.0f, 0.0f, 0.5f);
			weaponLabel2.text = "???";
		}

		if(player.HasCar) {
			collectableIcon1.color = Color.white;
			collectableLabel1.text = "Toy Car";
		} else {
			collectableIcon1.color = new Color (0.0f, 0.0f, 0.0f, 0.5f);
			collectableLabel1.text = "???";
		}

		if(player.HasLamp) {
			collectableIcon2.color = Color.white;
			collectableLabel2.text = "Lava Lamp";
		} else {
			collectableIcon2.color = new Color (0.0f, 0.0f, 0.0f, 0.5f);
			collectableLabel2.text = "???";
		}

		if(player.HealthCollected > 0) {
			upgradeIcon1.color = Color.white;
			upgradeLabel1.text = "Health Upgrade";
		} else {
			upgradeIcon1.color = new Color (0.0f, 0.0f, 0.0f, 0.5f);
			upgradeLabel1.text = "???";
		}

		if(player.HealthCollected > 1) {
			upgradeIcon2.color = Color.white;
			upgradeLabel2.text = "Health Upgrade";
		} else {
			upgradeIcon2.color = new Color (0.0f, 0.0f, 0.0f, 0.5f);
			upgradeLabel2.text = "???";
		}

		if(player.HasFlightlight) {
			upgradeIcon3.color = Color.white;
			upgradeLabel3.text = "Flashlight";
		} else {
			upgradeIcon3.color = new Color (0.0f, 0.0f, 0.0f, 0.5f);
			upgradeLabel3.text = "???";
		}

		if(player.HasSpeedboost) {
			upgradeIcon4.color = Color.white;
			upgradeLabel4.text = "Speed Boost";
		} else {
			upgradeIcon4.color = new Color (0.0f, 0.0f, 0.0f, 0.5f);
			upgradeLabel4.text = "???";
		}
	}
}
