using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour 
{
	Animator animator;

	public GameObject stopSign;
	bool stopSignActive = false;
	public GameObject afterDeathPrefab;

	MovementController controller;

	void Start () 
	{
		animator = GetComponent<Animator>();
		controller = GetComponent<MovementController>();		
	}

	public void Kill()
	{
		controller.stop = true;
		animator.SetTrigger("death");		
	}

	void AnimKill()
	{
		Instantiate(afterDeathPrefab, transform.position + new Vector3(0, 0.05f, 0), Quaternion.identity);
		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D other) 
	{
		if(other.gameObject.layer == 10)
		{
			Kill();
		}
	}

	void OnMouseDown() 
	{
		stopSignActive = !stopSignActive;
		stopSign.SetActive(stopSignActive);
	}
}
