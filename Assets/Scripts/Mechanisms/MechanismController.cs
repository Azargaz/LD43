using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanismController : MonoBehaviour 
{
	public List<Mechanism> mechanisms = new List<Mechanism>();

	public virtual void Activate()
	{
		foreach(Mechanism mechanism in mechanisms)
		{
			mechanism.Activate();
		}
	}

	public virtual void Deactivate()
	{
		foreach(Mechanism mechanism in mechanisms)
		{
			mechanism.Deactivate();
		}
	}

}