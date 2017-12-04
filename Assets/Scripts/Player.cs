using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Player : Actor {

	public PostProcessingProfile profile;
	public Light flashlight;
	public GameObject[] weaponVisuals;
	PlayerHUD hud;

	bool isPaused;
	public bool IsPaused
	{
		get { return isPaused; }
		set{ 
			isPaused = value;

			Time.timeScale = isPaused ? 0.0f : 1.0f;
			Cursor.visible = isPaused ? true : false;
			Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
			hud.UpdateHUD (this);
		}
	}

	bool isDead;
	public bool IsDead
	{
		get { return isDead; }
		set{ 
			isDead = value;

			Time.timeScale = isDead ? 0.0f : 1.0f;
			Cursor.visible = isDead ? true : false;
			Cursor.lockState = isDead ? CursorLockMode.None : CursorLockMode.Locked;
		}
	}

	public int PlayerLevel
	{
		get { return ItemsCollected + 1; } 
	}

	int itemsCollected;
	public int ItemsCollected
	{
		get { return itemsCollected; }
		private set { itemsCollected = value; }
	}

	int healthCollected;
	public int HealthCollected
	{
		get { return healthCollected; }
		set { healthCollected = value; }
	}

	Item recentPickup;
	public Item RecentPickup
	{
		get { return recentPickup; }
		private set { recentPickup = value; }
	}

	List<Weapon> weapons;
	public List<Weapon> Weapons
	{
		get { return weapons; }
		private set { weapons = value; }
	}

	bool hasSpeedboost;
	public bool HasSpeedboost
	{
		get { return hasSpeedboost; }
		private set{ hasSpeedboost = value;}
	}

	bool hasFlashlight;
	public bool HasFlightlight
	{
		get {return hasFlashlight; }
		private set { hasFlashlight = value; }
	}

	bool hasLamp;
	public bool HasLamp
	{
		get { return hasLamp; }
		private set { hasLamp = value; }
	}

	bool hasCar;
	public bool HasCar
	{
		get { return hasCar; }
		private set { hasCar = value; }
	}

	void Update()
	{
		var vignette = profile.vignette.settings;
		if (vignette.intensity > 0.01f) {
			vignette.intensity = vignette.intensity - 0.5f * Time.deltaTime;
			profile.vignette.settings = vignette;
		} else {
			profile.vignette.enabled = false;
		}
	}

	protected override void Init()
	{
		base.Init ();

		flashlight.enabled = false;
		hud = FindObjectOfType<PlayerHUD> ();
		hud.UpdateHUD (this);

		EnemyManager.Instance.UpdateWithLevel (PlayerLevel);
		SoundManager.Instance.StartMainGame ();
		IsPaused = false;

		Weapons = new List<Weapon> ();
		weaponVisuals [0].SetActive (false);
		weaponVisuals [1].SetActive (false);
	}

	public void CollectItem(Item item)
	{
		ItemsCollected += 1;

		if(ItemsCollected % 2 == 1)
			SoundManager.Instance.AddNextMusicTrack ();

		EnemyManager.Instance.UpdateWithLevel (PlayerLevel);

		RecentPickup = item;
		item.EquipItemToPlayer (this);
		hud.UpdateHUD (this);
	}

	public void ClearRecentPickup()
	{
		RecentPickup = null;
	}

	public override void Damage(int damage)
	{
		base.Damage (damage);

		var vignette = profile.vignette.settings;
		vignette.intensity = 0.666f;
		profile.vignette.settings = vignette;
		profile.vignette.enabled = true;

		if(CurrentHealth <= 0) {
			IsDead = true;
		}

		hud.UpdateHUD (this);
	}

	protected override void FireWeapon()
	{
		base.FireWeapon ();
	}

	public void SetupController(PlayerController controller)
	{
		controller.forwardMovement += ForwardMotion;
		controller.sidewaysMovement += SidewaysMotion;
		controller.verticalMovement += VerticalMotion;
		controller.pitchRotation += PitchRotation;
		controller.yawRotation += YawRotation;
		controller.rollRotation += RollRotation;
		controller.fireButton += FireWeapon;
		controller.lightButton += ToggleFlashlight;
		controller.toggleButton += ToggleWeapon;
		controller.pauseButton += TogglePause;
	}

	public void TogglePause()
	{
		IsPaused = !IsPaused;
	}

	public void UpgradeHealth(int amount)
	{
		adjustedHealth += amount;
		CurrentHealth = adjustedHealth;
		hud.UpdateHUD (this);
	}

	public void EquipFlashlight()
	{
		HasFlightlight = true;
		ToggleFlashlight ();
	}

	public void ToggleFlashlight()
	{
		if (!HasFlightlight) {
			flashlight.enabled = false;
			return;
		}

		flashlight.enabled = !flashlight.enabled;
	}

	public void EquipSpeedBoost()
	{
		HasSpeedboost = true;
		forwardSpeed *= 2.0f;
		sidewaysSpeed *= 2.0f;
		verticalSpeed *= 2.0f;
	}

	public void EquipCollectable(CollectableType type)
	{
		switch(type) {
		case CollectableType.LavaLamp:
			HasLamp = true;
			break;
		case CollectableType.ToyCar:
			HasCar = true;
			break;
		}
	}

	public void ToggleWeapon()
	{
		if(Weapons.Count > 1) {
			int weapon = (Weapons.IndexOf (CurrentWeapon) + 1) % Weapons.Count;
			if (weapon == 0)
				EnableBlasters ();
			else
				EnableCannon ();
			CurrentWeapon = Weapons [weapon];
			hud.UpdateHUD (this);
		}
	}

	public void EnableBlasters()
	{
		weaponVisuals [0].SetActive (true);
		weaponVisuals [1].SetActive (false);
	}

	public void EnableCannon()
	{
		weaponVisuals [1].SetActive (true);
		weaponVisuals [0].SetActive (false);
	}
}
