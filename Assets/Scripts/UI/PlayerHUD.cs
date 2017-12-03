using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour {

	public Slider healthbar;
	public Text timeLabel;

	float gametime = 0.0f;
	float targetHealth;

	void Start()
	{
		gametime = 0.0f;
	}

	void Update()
	{
		gametime += Time.deltaTime;
		timeLabel.text = gametime.ToString ("F1");

		healthbar.value = Mathf.Lerp (healthbar.value, targetHealth, 5.0f * Time.deltaTime);
	}

	public void UpdateHUD (Player player) {

		targetHealth = player.HealthRatio;

	}
}
