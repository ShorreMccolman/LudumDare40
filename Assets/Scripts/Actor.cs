using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Actor : MonoBehaviour {
	[SerializeField]
	protected int maxHealth = 2;
	[SerializeField]
	protected float forwardSpeed;
	[SerializeField]
	protected float sidewaysSpeed;
	[SerializeField]
	protected float verticalSpeed;
	[SerializeField]
	protected float pitchSpeed;
	[SerializeField]
	protected float yawSpeed;
	[SerializeField]
	protected float rollSpeed;

	protected int adjustedHealth;

	public Transform ProjectileSpawnPosition;

	Weapon currentWeapon;
	public Weapon CurrentWeapon
	{
		get { return currentWeapon; }
		set { currentWeapon = value; }
	}

	int currentHealth;
	public int CurrentHealth
	{
		get { return currentHealth; }
		protected set { currentHealth = value; }
	}

	public float HealthRatio
	{
		get {
			return (float)CurrentHealth / (float)adjustedHealth;
		}
	}

	protected Rigidbody rb;

	void Start()
	{
		Init ();
	}

	protected virtual void Init()
	{
		rb = GetComponent<Rigidbody> ();
		adjustedHealth = maxHealth;
		CurrentHealth = adjustedHealth;
	}

	public virtual void Damage(int damage)
	{
		CurrentHealth -= damage;
	}

	protected void ForwardMotion(float input)
	{
		rb.AddForce (transform.forward * forwardSpeed * input);
	}

	protected void SidewaysMotion(float input)
	{
		rb.AddForce (transform.right * sidewaysSpeed * input);
	}

	protected void VerticalMotion(float input)
	{
		rb.AddForce (transform.up * verticalSpeed * input);
	}

	protected void PitchRotation(float input)
	{
		transform.Rotate (Vector3.right * pitchSpeed * input);
	}

	protected void YawRotation(float input)
	{
		transform.Rotate (Vector3.up * yawSpeed * input);
	}

	protected void RollRotation(float input)
	{
		transform.Rotate (Vector3.forward * rollSpeed * input);
	}

	protected virtual void FireWeapon()
	{
		if(CurrentWeapon == null) {
			return;
		}

		Projectile obj = Instantiate (CurrentWeapon.ProjectilePrefab);
		obj.transform.position = ProjectileSpawnPosition.position;
		obj.transform.rotation = ProjectileSpawnPosition.rotation;
		obj.SetOrigin (ProjectileSpawnPosition.position);

		SoundManager.Instance.PlaySoundEffect (CurrentWeapon.soundID,true);
	}

	public float GetHealthbarWidth()
	{
		return (adjustedHealth - maxHealth) * 10.0f + 200.0f;
	}
}
