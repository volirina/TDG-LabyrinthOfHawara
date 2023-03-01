using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaPickUp : MonoBehaviour
{
    public float staminaToAdd = 20f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.PickupWater(staminaToAdd);
            Destroy(gameObject);
        }
    }
}
