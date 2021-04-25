using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private static Player _instance;

    public static Player Instance { get { return _instance; } }

    private float power;
    private float health;
    private float peopleSaved;
    private bool isDrilling;

    [SerializeField]
    private float driveSpeed;
    [SerializeField]
    private float hoverForce;
    [SerializeField]
    UIManager uIManager;
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    Drill drill;
    [SerializeField]
    AudioClip drillClip;
    [SerializeField]
    AudioClip hoverClip;

    Rigidbody2D tankRigidbody;
    Animator tankAnimator;
    AudioSource drillAudioSource;
    AudioSource hoverAudioSource;
    BoxCollider2D tankCollider;
    float defaultGravityScale;
    Scene currentScene;

    public void Awake()
    {
        drillAudioSource = AddAudioFiles(drillClip, true, false, -1, 1);
        hoverAudioSource = AddAudioFiles(hoverClip, true, false, -2.2f, .25f);
    }

    private AudioSource AddAudioFiles(AudioClip clip, bool loops, bool playAwake, float pitch, float vol)
    {
        AudioSource newAudio = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        newAudio.clip = clip;
        newAudio.loop = loops;
        newAudio.playOnAwake = playAwake;
        newAudio.pitch = pitch;
        newAudio.volume = vol;
        return newAudio;
    }

    internal void IncreaseTime(int value)
    {
        gameManager.AddMoreTime(value);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        tankRigidbody = GetComponent<Rigidbody2D>();
        tankCollider = GetComponent<BoxCollider2D>();
        tankAnimator = GetComponent<Animator>();
        drillAudioSource = GetComponent<AudioSource>();
        defaultGravityScale = tankRigidbody.gravityScale;
        if (currentScene.name == "Level")
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        }
        health = 100;
        power = 100;
        peopleSaved = 0;
    }

    public float GetPeopleSavedCount()
    {
        return peopleSaved;
    }

    public void IncreaseHealth(float value)
    {
        if (health >= 100)
        {
            return;
        }
        health += value;
    }

    public void DecreaseHealth(float value)
    {
        health -= value * Time.deltaTime;
    }

    public void IncreasePower(float value)
    {
        if (power >= 100)
        {
            return;
        }
        power += value;
    }

    public void DecreasePower(float value)
    {
        power -= value * Time.deltaTime;
    }

    public void IncreaseSavePeopleCount(float value)
    {
        peopleSaved += value;
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetPower()
    {
        return power;
    }

    public float GetPeopleSaved()
    {
        return peopleSaved;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            StopAllAudio();
            uIManager.DisplayGameOverPanel("You have run out of health and the rescue drill has become inoperable.");
        }
        if(power <= 0)
        {
            StopAllAudio();
            uIManager.DisplayGameOverPanel("You ran out of power. You will be unable to get out of the mine at this point.");
        }
        Move();
        SetSpriteDirection();
        CheckForDamage();
        Drill();
        if(currentScene.name == "Level")
        {
            uIManager.SetRemaingsMinersText(peopleSaved);
            uIManager.UpdateHealthSlider(health);
            uIManager.UpdatePowerSlider(power);
        }
    }

    private void FixedUpdate()
    {
        Hover();
    }

    private void CheckForDamage()
    {
        if (tankCollider.IsTouchingLayers(LayerMask.GetMask("Hazard")))
        {
            StartCoroutine("DecreaseHealth", 10);
        }
    }

    private void Move()
    {
        float movement = Input.GetAxis("Horizontal");
        Vector2 velocity = new Vector2(movement * driveSpeed, tankRigidbody.velocity.y);
        tankRigidbody.velocity = velocity;

        bool isDriving = Mathf.Abs(tankRigidbody.velocity.x) > Mathf.Epsilon;
    }

    private void Hover()
    {
        if (Input.GetAxis("Vertical") == 1)
        {
            hoverAudioSource.Play(0);
            tankAnimator.SetBool("Hovering", true);
            tankRigidbody.AddForce(new Vector2(0, hoverForce));
            if (currentScene.name == "Level")
            {
                StartCoroutine("DecreasePower", 2);
            }
        }
        else
        {
            hoverAudioSource.Stop();
            tankAnimator.SetBool("Hovering", false);
        }
    }

    private void SetSpriteDirection()
    {
        bool isDriving = Mathf.Abs(tankRigidbody.velocity.x) > Mathf.Epsilon;
        if (isDriving)
        {
            transform.localScale = new Vector2(Mathf.Sign(tankRigidbody.velocity.x), 1f);
        }
    }

    private void Drill()
    {
        if (Input.GetKey(KeyCode.E))
        {
            drillAudioSource.Play(0);
            drill.RunDrill();
            if (currentScene.name == "Level")
            {
                StartCoroutine("DecreasePower", 2);
            }
        }
        else
        {
            drillAudioSource.Stop();
            drill.StopDrill();
        }
    }

    public void StopAllAudio()
    {
        drillAudioSource.Stop();
        hoverAudioSource.Stop();
    }
}
