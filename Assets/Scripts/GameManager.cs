using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public int bombs = 9;
    public int keys = 0;
    public int level = 1;
    public float maxStamina = 100;
    public float currentStamina;
    public Text bombsText;
    public GameObject KeyRawImage;
    public Button startButton;
    public GameObject startUI;
    public GameObject restartButton;
    public GameObject restartPanel;

    public Text uiTextStamina;

    public GameObject statsPanel;
    public Text statsText;

    private bool isFinalScene = false;
    private int restarts = 0;

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
        startButton.onClick.AddListener(LoadLevelOne);
        restartButton.GetComponent<Button>().onClick.AddListener(RestartLevel);
        restartPanel.SetActive(false);
        bombs = 9;
}

    public void LoadLevelOne()
    {
        SceneManager.LoadScene(1);
        startUI.SetActive(false);
        currentStamina = maxStamina; 
        DecreaseStamina(0f);
        bombs = 9;
        UpdateUI();
        isFinalScene = false;
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
            GameOver();
        }
    }

    public void GameOver()
    {
        restartPanel.SetActive(true);
        restartButton.SetActive(true);
    }

    public void RestartLevel()
    {
        restarts++;    
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        restartButton.SetActive(false);
        restartPanel.SetActive(false);
        currentStamina = maxStamina; 
        bombs = 9; 
        UpdateUI();
        isFinalScene = false;

    }

    private void UpdateUI()
    {
        bombsText.text = "" + bombs;        
    }

    private void Update()
    {

        if (SceneManager.GetActiveScene().name != "Final") 
        {
            DecreaseStamina(Time.deltaTime);
            UpdateUI();
        }

        if (SceneManager.GetActiveScene().name == "Final" && !isFinalScene) 
        {
            isFinalScene = true;
            ShowStats();
        }
        if(keys == 0)
        {
            GameManager.instance.KeyRawImage.SetActive(false);
        }
    }
    private void ShowStats()
    {
        restartButton.SetActive(false);
        statsPanel.SetActive(true);
        statsText.text = "Stats:\n" +
            "Restarts: " + restarts + "\n" +
            "Bombs left: " + bombs + "\n" +
            "Stamina left: " + currentStamina.ToString("0") + " / " + maxStamina.ToString("0");
}
}
