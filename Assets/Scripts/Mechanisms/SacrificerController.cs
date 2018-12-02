using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SacrificerController : MechanismController
{
	public int sacrificesNeeded = 1;
	int sacrificesOffered = 0;
	
	public Text counter;
	Animator animator;

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
			{
				Activate();	
				animator.SetTrigger("activated");
			}
		}
	}

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		counter.text = (sacrificesNeeded - sacrificesOffered).ToString();
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
					skeleton.Sacrifice();
					SacrificesOffered++;
				}
			}
		}
    }
}
