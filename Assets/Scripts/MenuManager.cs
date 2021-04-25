using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject howToPlayPanel;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuPanel.SetActive(true);
        howToPlayPanel.SetActive(false);
    }

    public void ClickOnStartButton()
    {
        SceneManager.LoadScene("Level");
    }

    public void ClickOnHowToPlayButton()
    {
        mainMenuPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
    }

    public void ClickBackButton()
    {
        howToPlayPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void ClickQuitButton()
    {
        Debug.Log("Quit Game");
        //Application.Quit();
    }
}
