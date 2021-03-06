﻿using System.Collections;
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

	int curLevel;
	
	public void UpdateWithLevel(int level)
	{
		curLevel = level;
		foreach(Enemy enemy in enemies) {
			enemy.UpdateWithPlayerLevel (level);
		}
	}

	public void KillEnemy(Enemy enemy)
	{
		SoundManager.Instance.PlaySoundEffect ("Death");
		enemy.gameObject.SetActive (false);
		StartCoroutine ("Ressurect", enemy);
	}

	IEnumerator Ressurect(Enemy enemy)
	{
		yield return new WaitForSeconds (20.0f);
		enemy.gameObject.SetActive (true);
		enemy.UpdateWithPlayerLevel (curLevel);
		enemy.Reset ();
	}
}
