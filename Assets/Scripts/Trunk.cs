using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Trunk : MonoBehaviour
{

    [SerializeField] Node root;
    [SerializeField] TextMeshProUGUI productionText;
    [SerializeField] TextMeshProUGUI consumptionText;
    [SerializeField] TextMeshProUGUI depthText;
    [SerializeField] TextMeshProUGUI gameOverText, winText;
    [SerializeField] Button menuButton, quitButton;
    [SerializeField] Sprite[] treePhases;
    [SerializeField] Sprite deadTree;
    [SerializeField] int[] waterThresholds;
    [SerializeField] AudioClip[] treeClips;
    AudioSource audioSource;
    //Serialized for debug
    [SerializeField] int waterConsumption=0, waterReserve=300, totalWaterConsumed = 0;
    int treePhaseIndex = 0;
    int maxDepth;
    int fixedCounter = 0;
    bool pauseGame = false;
    bool gameOver = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        if (!gameOver)
        {
            int [] inOut = root.Visit();
            waterReserve += inOut[0];
            waterConsumption = inOut[1];
            maxDepth = inOut[2];
            totalWaterConsumed += waterConsumption;
            
            if (fixedCounter++ % 4 == 0)
                waterReserve -= waterConsumption;
            
            if (waterReserve < 0)
            {
                gameOver = true;
                GetComponent<SpriteRenderer>().sprite = deadTree;
                gameOverText.gameObject.SetActive(true);
                ShowButtons();
            }

            productionText.text = "Water Reserve: " + waterReserve.ToString();
            consumptionText.text = "Water Consumption: " + waterConsumption.ToString();
            depthText.text = "Depth: " + maxDepth.ToString();

            if (totalWaterConsumed >= waterThresholds[treePhaseIndex])
            {
                audioSource.clip = treeClips[treePhaseIndex];
                audioSource.Play();
                GetComponent<SpriteRenderer>().sprite = treePhases[treePhaseIndex++];
            }

            if (treePhaseIndex == treePhases.Length)
            {
                gameOver = true;
                audioSource.loop = true;
                winText.gameObject.SetActive(true);
                FindObjectOfType<MainCamera>().GetComponent<AudioSource>().Pause();
                ShowButtons();
            }
        }
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseGame)
            {
                PauseGame();
            }
            else
            {
                UnPauseGame();
            }
        }

    }

    void PauseGame()
    {
        Time.timeScale = 0;
        pauseGame = true;
        ShowButtons();
    }

    void UnPauseGame()
    {
        Time.timeScale = 1;
        pauseGame = false;
        HideButtons();
    }
    private void ShowButtons()
    {
        menuButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }
    private void HideButtons()
    {
        menuButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);        
    }

    public int GetDepth()
    {
        return maxDepth;
    }
}
