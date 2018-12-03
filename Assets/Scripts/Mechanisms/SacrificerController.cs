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

	List<SkeletonController> skeletonsInRange = new List<SkeletonController>();
	
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
		sacrificesNeeded = LevelGenerator.currentLevel.sacrificesNeeded;
	}

	void Update()
	{
		counter.text = (sacrificesNeeded - sacrificesOffered).ToString();

		if(Input.GetButtonDown("Sacrifice"))
		{
			while(skeletonsInRange.Count > 0 && sacrificesNeeded > sacrificesOffered)
				Sacrifice();
		}
	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if(other.gameObject.layer == 8)
		{
			skeletonsInRange.Add(other.gameObject.GetComponent<SkeletonController>());
		}
    }

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.layer == 8)
		{
			skeletonsInRange.Remove(other.gameObject.GetComponent<SkeletonController>());
		}
	}

	void Sacrifice()
	{
		if(skeletonsInRange.Count == 0)
			return;

		if(sacrificesNeeded > SacrificesOffered)
		{
			skeletonsInRange[0].Sacrifice();
			skeletonsInRange.RemoveAt(0);
			SacrificesOffered++;
		}
	}
}
