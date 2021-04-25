using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public PickupType type;
    public int value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (type)
        {
            case PickupType.HEALTH:
                FindObjectOfType<Player>().IncreaseHealth(value);
                Destroy(gameObject);
                break;
            case PickupType.POWER:
                FindObjectOfType<Player>().IncreasePower(value);
                Destroy(gameObject);
                break;
            case PickupType.PERSON:
                FindObjectOfType<Player>().IncreaseSavePeopleCount(value);
                FindObjectOfType<GameManager>().CheckAllMinersSaved();
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
