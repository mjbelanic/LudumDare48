using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player _instance;

    public static Player Instance { get { return _instance; } }

    private float power;
    private float health;
    private bool isDrilling;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        // check for power to see if user can move;
        if(Input.GetAxis("Horizontal") == 1)
        {
            // Move Right;
            // move animation
        }
        else if(Input.GetAxis("Horizontal") == -1)
        {
            //Move Left;
            // move animation
        }
        else if(Input.GetAxis("Vertical") == 1)
        {
            // Hover
            // Subtract Energy
            // hover animation
        }
        else if(Input.GetAxis("Vertical") == -1)
        {
            // Drill Down?
            if (Input.GetButtonDown("Drill"))
            {
                // Drill
                // Subtract Energy
                // drill animation
            }
        }
        if(health == 0 || power == 0)
        {
            //Game OVer
        }
    }
}
