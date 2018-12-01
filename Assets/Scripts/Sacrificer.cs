using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sacrificer : MonoBehaviour 
{
	SacrificeController controller;

	void Start () 
	{
		controller = FindObjectOfType<SacrificeController>();
	}
	
	void Activate()
	{
		
	}
}
