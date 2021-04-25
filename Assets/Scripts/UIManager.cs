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
    Slider healthSlider;
    [SerializeField]
    Slider energySlider;
    [SerializeField]
    TextMeshPro remainingMinersText;
    [SerializeField]
    TextMeshPro remainingTime;

    float maxHealth;
    float maxEnergy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickPauseButton();
        }
    }

    public void ClickPauseButton()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ClickResumeButtoN()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void ClickMainManuButton()
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
        remainingMinersText.text = peopleSaved.ToString() + " /100";
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
