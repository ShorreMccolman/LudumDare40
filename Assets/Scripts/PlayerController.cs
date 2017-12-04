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
	public const string TOGGLE_LIGHT_INPUT = "Light";
	public const string TOGGLE_WEAPON_INPUT = "Toggle";

	public delegate void InputBinding (float input);
	public InputBinding forwardMovement;
	public InputBinding sidewaysMovement;
	public InputBinding verticalMovement;
	public InputBinding pitchRotation;
	public InputBinding yawRotation;
	public InputBinding rollRotation;

	public delegate void KeyBinding();
	public KeyBinding fireButton;
	public KeyBinding lightButton;
	public KeyBinding toggleButton;
	public KeyBinding pauseButton;

	bool inputShift;
	float toggleBuffer;


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
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		inputShift = Input.GetButton (SHIFT_INPUT);

		if(inputShift) {
//			if(verticalMovement != null) {
//				verticalMovement (Input.GetAxis (FORWARD_MOVEMENT_INPUT) * Time.fixedDeltaTime);
//			}	
			if(rollRotation != null) {
				rollRotation (Input.GetAxis (SIDEWAYS_MOVEMENT_INPUT) * Time.fixedDeltaTime);
			}
		} else {
//			if(forwardMovement != null) {
//				forwardMovement (Input.GetAxis (FORWARD_MOVEMENT_INPUT) * Time.fixedDeltaTime);
//			}	
			if(sidewaysMovement != null) {
				sidewaysMovement (Input.GetAxis (SIDEWAYS_MOVEMENT_INPUT) * Time.fixedDeltaTime);
			}
		}

		if(forwardMovement != null) {
			forwardMovement (Input.GetAxis (FORWARD_MOVEMENT_INPUT) * Time.fixedDeltaTime);
		}

		if(pitchRotation != null) {
			pitchRotation (Input.GetAxis (PITCH_ROTATION_INPUT) * -Time.fixedDeltaTime);
		}	
		if(yawRotation != null) {
			yawRotation (Input.GetAxis (YAW_ROTATION_INPUT) * Time.fixedDeltaTime);
		}

		if(toggleButton != null) {
			if(toggleBuffer <= 0.0f && Mathf.Abs(Input.GetAxis(TOGGLE_WEAPON_INPUT)) > 0.8f) {
				toggleButton ();
				toggleBuffer = 0.3f;
			}

			if (toggleBuffer > 0.0f)
				toggleBuffer -= Time.fixedDeltaTime;
		}
	}

	void Update()
	{
		if(Input.GetButtonDown(FIRE_WEAPON_INPUT)) {
			if(fireButton != null) {
				fireButton ();
			}
		}

		if(Input.GetButtonDown(TOGGLE_LIGHT_INPUT)) {
			if(lightButton != null) {
				lightButton ();
			}
		}

		if(Input.GetButtonDown(TOGGLE_WEAPON_INPUT)) {
			if(toggleButton != null) {
				toggleButton ();
			}
		}

		if(Input.GetKeyDown(KeyCode.Escape)) {
			if (pauseButton != null)
				pauseButton ();
		}
	}
}
