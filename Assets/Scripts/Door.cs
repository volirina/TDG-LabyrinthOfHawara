using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string nextSceneName;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.instance.keys > 0)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                // Decrease the number of keys in the GameManager
                GameManager.instance.keys--;
                // Load the next scene
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.instance.keys > 0)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                // Decrease the number of keys in the GameManager
                GameManager.instance.keys--;
                // Load the next scene
                SceneManager.LoadScene(nextSceneName);
                
            }
        }
    }
}