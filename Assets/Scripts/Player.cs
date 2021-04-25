using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    Rigidbody2D tankRigidbody;
    Animator playerAnimator;
    BoxCollider2D[] tankColliders; 
    float defaultGravityScale;



    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        tankRigidbody = GetComponent<Rigidbody2D>();
        tankColliders = GetComponents<BoxCollider2D>();
        defaultGravityScale = tankRigidbody.gravityScale;
        health = 50;
        power = 60;
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
        if (power >= 200)
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
        Debug.Log("Person Saved");
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
        if(health <= 0 || power <= 0)
        {
            //Game OVer
        }
        Move();
        SetSpriteDirection();
        uIManager.SetRemaingsMinersText(peopleSaved);
        uIManager.UpdateHealthSlider(health);
        uIManager.UpdatePowerSlider(power);
    }

    private void FixedUpdate()
    {
        Hover();
    }

    private void Move()
    {
        float movement = Input.GetAxis("Horizontal");
        Vector2 velocity = new Vector2(movement * driveSpeed, tankRigidbody.velocity.y);
        tankRigidbody.velocity = velocity;

        bool isDriving = Mathf.Abs(tankRigidbody.velocity.x) > Mathf.Epsilon;
        // animation;
    }

    private void Hover()
    {
        if(Input.GetAxis("Vertical") == 1)
        {
            tankRigidbody.AddForce(new Vector2(0, hoverForce));
            StartCoroutine("DecreasePower", 2);
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
        if (tankColliders[0].IsTouchingLayers(LayerMask.GetMask("Breakable")) && isDrilling)
        {

        }
    }
}
