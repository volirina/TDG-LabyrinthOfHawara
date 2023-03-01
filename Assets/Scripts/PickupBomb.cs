using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBomb : MonoBehaviour
{
    private bool hasBeenPickedUp = false;

    void OnTriggerEnter(Collider other)
    {
        if (!hasBeenPickedUp && other.CompareTag("Player"))
        {
            // Set the hasBeenPickedUp flag to true
            hasBeenPickedUp = true;

            // Increase the bomb count in the GameManager
            GameManager.instance.bombs++;

            // Update the UI to show the new number of bombs
            // ...

            // Destroy the bomb game object
            Destroy(gameObject);
        }
    }
}
