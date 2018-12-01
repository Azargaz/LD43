using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificerController : MechanismController
{
	public int sacrificesNeeded = 1;
	int sacrificesOffered = 0;
	
	int SacrificesOffered
	{
		get
		{
			return sacrificesOffered;
		}
		set
		{
			sacrificesOffered = value;
			if(sacrificesOffered >= sacrificesNeeded)
				Activate();	
		}
	}

    void OnTriggerStay2D(Collider2D other)
    {
		if(other.gameObject.layer == 8)
		{
			if(Input.GetButtonDown("Sacrifice"))
			{
				if(sacrificesNeeded > SacrificesOffered)
				{
					SkeletonController skeleton = other.gameObject.GetComponent<SkeletonController>();
					skeleton.Kill();
					SacrificesOffered++;
				}
			}
		}
    }
}
