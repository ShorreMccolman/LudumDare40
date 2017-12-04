using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour {

	public Slider healthbar;
	public Text timeLabel;
	public Text weaponLabel;
	public Text pickupLabel;
	public Text pickupInfoLabel;

	public PauseMenu pauseMenu;
	public GameoverMenu gameoverMenu;

	float gametime = 0.0f;
	float targetHealth;

	void Start()
	{
		gametime = 0.0f;

		ClearPickupLabel ();
	}

	void Update()
	{
		gametime += Time.deltaTime;
		timeLabel.text = gametime.ToString ("F1");

		healthbar.value = Mathf.Lerp (healthbar.value, targetHealth, 5.0f * Time.deltaTime);
	}

	public void UpdateHUD (Player player) {

		if(player.IsDead) {
			gameoverMenu.gameObject.SetActive (true);
			gameoverMenu.OpenMenu (player);
			return;
		}

		targetHealth = player.HealthRatio;

		weaponLabel.text = player.CurrentWeapon == null ? "" : player.CurrentWeapon.DisplayName;

		((RectTransform)healthbar.transform).sizeDelta = new Vector2 ( player.GetHealthbarWidth(), 65.0f);

		if(player.RecentPickup != null) {
			pickupLabel.text = "You pickup up " + player.RecentPickup.DisplayName;
			pickupInfoLabel.text = player.RecentPickup.PickupText;
			player.ClearRecentPickup ();
			CancelInvoke ();
			Invoke ("ClearPickupLabel", 5.0f);
		}
			
		if(player.IsPaused) {
			pauseMenu.gameObject.SetActive (true);
			pauseMenu.OpenMenu (player);
		} else {
			pauseMenu.gameObject.SetActive (false);
		}
	}

	public void ClosePause()
	{
		Player player = GameObject.FindObjectOfType<Player> ();
		if(player) {
			player.TogglePause ();
		}
	}

	public void RestartLevel()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Main");
	}

	public void QuitToMenu()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Menu");
	}

	public void ClearPickupLabel()
	{
		pickupLabel.text = "";
		pickupInfoLabel.text = "";
	}
}
