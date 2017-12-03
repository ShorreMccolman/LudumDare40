using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Player : Actor {

	public PostProcessingProfile profile;
	PlayerHUD hud;

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

	protected override void Init()
	{
		base.Init ();

		hud = FindObjectOfType<PlayerHUD> ();
		hud.UpdateHUD (this);
	}

	void Update()
	{
		var vignette = profile.vignette.settings;
		if (vignette.intensity > 0.01f) {
			vignette.intensity = vignette.intensity - Time.deltaTime;
			profile.vignette.settings = vignette;
		}
	}

	public void CollectItem()
	{
		ItemsCollected += 1;
	}

	public override void Damage(int damage)
	{
		base.Damage (damage);

		var vignette = profile.vignette.settings;
		vignette.intensity = 0.5f;
		profile.vignette.settings = vignette;
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
	}
}
