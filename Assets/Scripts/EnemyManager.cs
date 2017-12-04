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
}
