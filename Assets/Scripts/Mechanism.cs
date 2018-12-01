using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanism : MonoBehaviour 
{
	bool active = false;
	
	protected delegate void Activation();
	protected Activation uponActivation;

	protected delegate void Deactivation();
	protected Deactivation uponDeactivation;

	public void Activate()
	{
		if(active)
			return;

		active = true;
		uponActivation();
	}

	public void Deactivate()
	{
		if(!active)
			return;
		
		active = false;
		uponDeactivation();
	}
	
}
