using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificerMechanism : Mechanism
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
		uponActivation += SacrificerActivated;
    }

    void SacrificerActivated()
    {
        animator.SetTrigger("activate");
    }
}
