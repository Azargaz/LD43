using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour 
{
	public int amountToSpawn = 2;
	public bool spawnPoint = true;
	public GameObject playerPrefab;
	
	void Start () 
	{
		if(spawnPoint)
			amountToSpawn = LevelGenerator.currentLevel.numberOfStartingSkeletons;
		Spawn();
	}

	void Spawn()
	{
		for (int i = 0; i < amountToSpawn; i++)
		{
			Instantiate(playerPrefab, transform.position + new Vector3(i * 0.3f, 0, 0), Quaternion.identity);
		}
	}
}
