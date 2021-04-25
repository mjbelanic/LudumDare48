using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject spawnPoint;
    GameObject playerPrefab;
    UIManager uIManager;
    float time = 180f; //300f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseTime();
        uIManager.UpdateTimeText(time);
        if(time <= 0)
        {
            Debug.Log("Out of time");
            // Game Over
        }
    }

    private void DecreaseTime()
    {
        time -= Time.deltaTime;
    }

    void SpawnPlayer()
    {
        if(playerPrefab == null)
        {
            Instantiate(playerPrefab, spawnPoint.transform);
        }
    }
}
