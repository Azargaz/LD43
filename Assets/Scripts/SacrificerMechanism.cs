using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificerMechanism : Mechanism
{
    void Start()
    {
		uponActivation += SacrificerActivated;
		uponDeactivation += SacrificerDeactivated;
    }

    void SacrificerActivated()
    {
        gameObject.SetActive(false);
    }

    void SacrificerDeactivated()
    {
        gameObject.SetActive(true);
    }
}
