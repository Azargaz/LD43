using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateController : MechanismController
{
    Animator animator;
    int peopleOnPlate = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            Vector2 colliderRange = GetComponent<Collider2D>().bounds.extents;
            Vector2 distance = other.transform.position - transform.position;

            if (Mathf.Abs(distance.x) < colliderRange.x)
            {
                peopleOnPlate++;
                Activate();
                animator.SetBool("pressed", true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            peopleOnPlate--;
            if (peopleOnPlate <= 0)
            {
                animator.SetBool("pressed", false);
                Deactivate();
            }
        }
    }
}
