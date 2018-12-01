using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateMechanism : Mechanism
{
	Animator animator;

	void Start () 
	{
		animator = GetComponent<Animator>();
		uponActivation += PressurePlateActivated;
		uponDeactivation += PressurePlateDeactivated;
	}
	
	void PressurePlateActivated()
	{
		gameObject.SetActive(false);
	}

	void PressurePlateDeactivated()
	{
		gameObject.SetActive(true);
	}
}
