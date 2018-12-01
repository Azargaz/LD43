using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour 
{
	public int startingNumberOfSkeletons = 2;
	public GameObject playerPrefab;
	
	void Start () 
	{
		Spawn();
	}

	void Spawn()
	{
		for (int i = 0; i < startingNumberOfSkeletons; i++)
		{
			Instantiate(playerPrefab, transform.position + new Vector3(i * 0.3f, 0, 0), Quaternion.identity);
		}
	}
}
