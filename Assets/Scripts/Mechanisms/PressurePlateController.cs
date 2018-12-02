using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateController : MechanismController
{
    Animator animator;

    List<GameObject> objectsOnPlate = new List<GameObject>();

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(objectsOnPlate.Count == 0)
        {                
            animator.SetBool("pressed", false);
            Deactivate();   
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        objectsOnPlate.Add(other.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        objectsOnPlate.Remove(other.gameObject);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Vector2 colliderRange = GetComponent<Collider2D>().bounds.extents + other.GetComponent<Collider2D>().bounds.extents;
        Vector2 distance = other.transform.position - transform.position;

        if (Mathf.Abs(distance.x) < colliderRange.x && objectsOnPlate.Count > 0)
        {
            Activate();
            animator.SetBool("pressed", true);
        }        
    }
}
