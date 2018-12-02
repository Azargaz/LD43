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
		animator.SetTrigger("hide");
	}

	void PressurePlateDeactivated()
	{
		animator.SetTrigger("unhide");
	}
}
