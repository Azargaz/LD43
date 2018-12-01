using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour 
{
	Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void OnCollisionEnter2D(Collision2D other) 
	{
		if(other.gameObject.layer == 8)
		{
			Vector2 colliderRange = GetComponent<Collider2D>().bounds.extents;
			Vector2 distance = other.transform.position - transform.position;
			
			if(Mathf.Abs(distance.x) < colliderRange.x)
			{
				animator.SetBool("pressed", true);
			}		
		}
	}

	void OnCollisionExit2D(Collision2D other)
	{
		if(other.gameObject.layer == 8)
		{
			animator.SetBool("pressed", false);
		}
	}
}
