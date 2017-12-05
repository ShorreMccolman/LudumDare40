using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour {

	public Slider healthbar;
	public Text levelLabel;
	public Text weaponLabel;
	public Text pickupLabel;
	public Text pickupInfoLabel;

	public PauseMenu pauseMenu;
	public GameoverMenu gameoverMenu;
	public GameObject aboutPopup;

	float gametime = 0.0f;
	float targetHealth;

	void Start()
	{
		ClearPickupLabel ();
	}

	void Update()
	{
		healthbar.value = Mathf.Lerp (healthbar.value, targetHealth, 5.0f * Time.deltaTime);
	}

	public void ShowEndGame(Player player, bool victory = false)
	{
		gameoverMenu.gameObject.SetActive (true);
		gameoverMenu.OpenMenu (player);
		gameoverMenu.SetResult (victory);
	}

	public void UpdateHUD (Player player) {

		targetHealth = player.HealthRatio;

		weaponLabel.text = player.CurrentWeapon == null ? "" : player.CurrentWeapon.DisplayName;

		((RectTransform)healthbar.transform).sizeDelta = new Vector2 ( player.GetHealthbarWidth(), 65.0f);

		levelLabel.text = "Threat Level; " + player.ItemsCollected;
		levelLabel.color = new Color ( 1.0f, 1.0f - (float)player.ItemsCollected / 8.0f , 1.0f - (float)player.ItemsCollected / 8.0f);

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

	public void ShowAbout()
	{
		SoundManager.Instance.PlaySoundEffect ("Click");
		aboutPopup.SetActive (true);
	}

	public void HideAbout()
	{
		SoundManager.Instance.PlaySoundEffect ("Click");
		aboutPopup.SetActive (false);
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
		SoundManager.Instance.PlaySoundEffect ("Click");
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Main");
	}

	public void QuitToMenu()
	{
		SoundManager.Instance.PlaySoundEffect ("Click");
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Menu");
	}

	public void ClearPickupLabel()
	{
		pickupLabel.text = "";
		pickupInfoLabel.text = "";
	}
}
