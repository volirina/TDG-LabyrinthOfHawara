using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Update the UI to show the new number of keys
            GameManager.instance.PickupKey();

            // Destroy the key game object
            Destroy(gameObject);
        }
    }
}

