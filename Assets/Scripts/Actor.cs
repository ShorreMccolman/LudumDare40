using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Actor : MonoBehaviour {
	[SerializeField]
	int maxHealth = 2;
	[SerializeField]
	float forwardSpeed;
	[SerializeField]
	float sidewaysSpeed;
	[SerializeField]
	float verticalSpeed;
	[SerializeField]
	float pitchSpeed;
	[SerializeField]
	float yawSpeed;
	[SerializeField]
	float rollSpeed;

	public Projectile ProjectilePrefab;
	public Transform ProjectileSpawnPosition;

	int currentHealth;
	public int CurrentHealth
	{
		get { return currentHealth; }
		private set { currentHealth = value; }
	}

	public float HealthRatio
	{
		get {
			return (float)CurrentHealth / (float)maxHealth;
		}
	}

	Rigidbody rb;

	void Start()
	{
		Init ();
	}

	protected virtual void Init()
	{
		rb = GetComponent<Rigidbody> ();
		CurrentHealth = maxHealth;
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
		Projectile obj = Instantiate (ProjectilePrefab);
		obj.transform.position = ProjectileSpawnPosition.position;
		obj.transform.rotation = ProjectileSpawnPosition.rotation;
	}
}
