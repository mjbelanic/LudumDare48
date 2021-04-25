using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    UIManager uIManager;
    float time = 300f;
    float totalNumberOfMiners = 50f;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseTime();
        uIManager.UpdateTimeText(time);
        if(time <= 0)
        {
            player.StopAllAudio();
            uIManager.DisplayGameOverPanel("Time is up.");
        }
    }

    private void DecreaseTime()
    {
        time -= Time.deltaTime;
    }

    internal void CheckAllMinersSaved()
    {
        if(player.GetPeopleSaved() == totalNumberOfMiners)
        {
            player.StopAllAudio();
            uIManager.DisplaySuccessPanel();
        }
    }

    internal string GetTotalPeopleToBeSave()
    {
        return totalNumberOfMiners.ToString();
    }

    internal void AddMoreTime(int value)
    {
        time += value;
    }
}
