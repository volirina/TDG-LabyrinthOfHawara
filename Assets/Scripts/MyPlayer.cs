using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{
    public GameObject bombPrefab; // The bomb prefab to be instantiated
    public float explosionRadius = 1f; // The radius of the explosion
    public float explosionDelay = 1f; // The delay before the explosion occurs

    private GameObject currentBomb; // The current bomb game object
    private float _currentStamina;

    void Start()
    {
        _currentStamina = GameManager.instance.maxStamina;
    }

    void Update()
    {
        // Check if the B key is pressed
        if (Input.GetKeyDown(KeyCode.B))
        {
            // Instantiate the bomb prefab at the player's position
            currentBomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
            GameManager.instance.bombs--;
            // Start a coroutine to explode the bomb after a delay
            StartCoroutine(ExplodeAfterDelay(currentBomb));
        }
    }

    IEnumerator ExplodeAfterDelay(GameObject bomb)
    {
        // Wait for the specified delay before exploding
        yield return new WaitForSeconds(explosionDelay);

        // Check if the bomb game object is null before accessing it
        if (bomb != null)
        {
            // Get all the colliders in the explosion radius that have the "Wall" tag
            Collider[] colliders = Physics.OverlapSphere(bomb.transform.position, explosionRadius);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Wall"))
                {
                    // Destroy any object with the "Wall" tag in the explosion radius
                    Destroy(collider.gameObject);
                }

                if (collider.CompareTag("Player"))
                {
                    GameManager.instance.DecreaseStamina(10.0f);
                }
            }

            // Destroy the bomb object
            Destroy(bomb);
        }
    }
}
