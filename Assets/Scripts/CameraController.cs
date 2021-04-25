using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    private Scene currentScene;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Vector3 offset;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentScene.name == "Level")
        {
            transform.position = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, offset.z);
        }
    }
}
