using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject pausePanel;
    [SerializeField]
    GameObject gameOverPanel;
    [SerializeField]
    GameObject successPanel;
    [SerializeField]
    Slider healthSlider;
    [SerializeField]
    Slider energySlider;
    [SerializeField]
    TextMeshProUGUI remainingMiners;
    [SerializeField]
    TextMeshProUGUI remainingTime;
    [SerializeField]
    TextMeshProUGUI reasonForFailingText;
    [SerializeField]
    GameManager gameManager;
    float maxHealth;
    float maxEnergy;
    bool pausePanelDisplayed;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100;
        maxEnergy = 100;
        pausePanelDisplayed = false;
    }

    internal void DisplayGameOverPanel(string reasonForLosing)
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        reasonForFailingText.text = reasonForLosing;   
    }

    internal void DisplaySuccessPanel()
    {
        Time.timeScale = 0;
        successPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pausePanelDisplayed)
        {
            ClickResumeButton();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && !pausePanelDisplayed)
        {
            ClickPauseButton();
        }
    }

    public void ClickPauseButton()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pausePanelDisplayed = true;
    }

    public void ClickResumeButton()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        pausePanelDisplayed = false;
    }

    public void ClickRetryButton()
    {
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene("Level");
    }

    public void ClickMainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    internal void UpdateTimeText(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        remainingTime.text = string.Format("{0:00}:{1:00}", minutes, seconds); 
    }

    internal void SetRemaingsMinersText(float peopleSaved)
    {
        remainingMiners.text = "Saved " + peopleSaved.ToString() + "/" + gameManager.GetTotalPeopleToBeSave();
    }

    internal void UpdateHealthSlider(float health)
    {
        healthSlider.value = health / maxHealth;
    }

    internal void UpdatePowerSlider(float power)
    {
        energySlider.value = power / maxEnergy;
    }
}
