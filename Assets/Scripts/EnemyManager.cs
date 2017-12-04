using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
	public static EnemyManager Instance;
	void Awake()
	{
		Instance = this;
		enemies = FindObjectsOfType<Enemy> ();
	}

	Enemy[] enemies;
	
	public void UpdateWithLevel(int level)
	{
		foreach(Enemy enemy in enemies) {
			enemy.UpdateWithPlayerLevel (level);
		}
	}

	public void KillEnemy(Enemy enemy)
	{
		enemy.gameObject.SetActive (false);
		StartCoroutine ("Ressurect", enemy);
	}

	IEnumerator Ressurect(Enemy enemy)
	{
		yield return new WaitForSeconds (20.0f);
		enemy.gameObject.SetActive (true);
		enemy.Reset ();
	}
}
