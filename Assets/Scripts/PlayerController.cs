using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour {

	public const string FORWARD_MOVEMENT_INPUT = "Forward";
	public const string SIDEWAYS_MOVEMENT_INPUT = "Sideways";
	public const string PITCH_ROTATION_INPUT = "Pitch";
	public const string YAW_ROTATION_INPUT = "Rotate";
	public const string FIRE_WEAPON_INPUT = "Fire";
	public const string SHIFT_INPUT = "Shift";

	public delegate void InputBinding (float input);
	public InputBinding forwardMovement;
	public InputBinding sidewaysMovement;
	public InputBinding verticalMovement;
	public InputBinding pitchRotation;
	public InputBinding yawRotation;
	public InputBinding rollRotation;

	public delegate void KeyBinding();
	public KeyBinding fireButton;

	bool inputShift;

	Player player;
	public Player Player
	{
		get {
			return player;
		}
		private set {
			player = value;
		}
	}

	// Use this for initialization
	void Start () {
		Player = GetComponent<Player> ();
		Player.SetupController (this);

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		inputShift = Input.GetButton (SHIFT_INPUT);

		if(inputShift) {
			if(verticalMovement != null) {
				verticalMovement (Input.GetAxis (FORWARD_MOVEMENT_INPUT) * Time.fixedDeltaTime);
			}	
			if(rollRotation != null) {
				rollRotation (Input.GetAxis (SIDEWAYS_MOVEMENT_INPUT) * Time.fixedDeltaTime);
			}
		} else {
			if(forwardMovement != null) {
				forwardMovement (Input.GetAxis (FORWARD_MOVEMENT_INPUT) * Time.fixedDeltaTime);
			}	
			if(sidewaysMovement != null) {
				sidewaysMovement (Input.GetAxis (SIDEWAYS_MOVEMENT_INPUT) * Time.fixedDeltaTime);
			}
		}

		if(pitchRotation != null) {
			pitchRotation (Input.GetAxis (PITCH_ROTATION_INPUT) * -Time.fixedDeltaTime);
		}	
		if(yawRotation != null) {
			yawRotation (Input.GetAxis (YAW_ROTATION_INPUT) * Time.fixedDeltaTime);
		}

		if(Input.GetButtonDown(FIRE_WEAPON_INPUT)) {
			if(fireButton != null) {
				fireButton ();
			}
		}

		if(Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit ();
		}
	}
}
