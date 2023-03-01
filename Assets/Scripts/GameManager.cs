using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public int lives = 3;
    public int bombs = 3;
    public int keys = 0;
    public int level = 1;
    public float maxStamina = 100;
    public float currentStamina;
    public Text bombsText;
    public GameObject KeyRawImage;

    public Text uiTextStamina;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        currentStamina = maxStamina;
    }

    void Start()
    {
        keys = 0;
        StartCoroutine(UpdateStaminaUI());  
        KeyRawImage.SetActive(false);
        UpdateUI();
    }

    public void PickupBomb()
    {
        bombs++;
        UpdateUI();
    }

    public void PickupKey()
    {
        keys++;
        KeyRawImage.SetActive(true);
        UpdateUI();
    }

    IEnumerator UpdateStaminaUI()
    {
        while (true)
        {
            uiTextStamina.text = "" + currentStamina.ToString("0");
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void PickupWater(float staminaToAdd)
    {
        currentStamina = Mathf.Min(currentStamina + staminaToAdd, maxStamina);
    }

    public void DecreaseStamina(float amount)
    {
        currentStamina -= amount;
        if (currentStamina <= 0)
        {
            LoseLife();
        }
    }

    public void LoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            currentStamina = maxStamina;
        }
    }

    public void GameOver()
    {
        // Add code to handle game over here
    }

    private void UpdateUI()
    {
        bombsText.text = "" + bombs;        
    }

    private void Update()
    {
        DecreaseStamina(Time.deltaTime);
        UpdateUI();
    }
}
