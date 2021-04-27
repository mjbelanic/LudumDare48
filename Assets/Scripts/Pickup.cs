using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public PickupType type;
    public int value;
    private AudioClip pickupAudioCLip;
    bool isColliding;

    public void Awake()
    {
        pickupAudioCLip = GetComponent<AudioSource>().clip;
        isColliding = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (isColliding) return;
            isColliding = true;
            switch (type)
            {
                case PickupType.HEALTH:
                    AudioSource.PlayClipAtPoint(pickupAudioCLip, Camera.main.transform.position);
                    FindObjectOfType<Player>().IncreaseHealth(value);
                    Destroy(gameObject);
                    break;
                case PickupType.POWER:
                    AudioSource.PlayClipAtPoint(pickupAudioCLip, Camera.main.transform.position);
                    FindObjectOfType<Player>().IncreasePower(value);
                    Destroy(gameObject);
                    break;
                case PickupType.PERSON:
                    AudioSource.PlayClipAtPoint(pickupAudioCLip, Camera.main.transform.position);
                    FindObjectOfType<Player>().IncreaseSavePeopleCount(value);
                    FindObjectOfType<GameManager>().CheckAllMinersSaved();
                    Destroy(gameObject);
                    break;
                case PickupType.TIME:
                    AudioSource.PlayClipAtPoint(pickupAudioCLip, Camera.main.transform.position);
                    FindObjectOfType<Player>().IncreaseTime(value);
                    Destroy(gameObject);
                    break;
                default:
                    break;
            }
            isColliding = false;
        }
    }
}
