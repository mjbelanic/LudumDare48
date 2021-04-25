using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    Animator drillAnimator;
    BoxCollider2D drillCollider;
    bool isDrilling;

    private void Start()
    {
        drillAnimator = GetComponent<Animator>();
        drillCollider = GetComponent<BoxCollider2D>();
        isDrilling = false;
    }

    public void RunDrill()
    {
        drillAnimator.SetBool("Drilling", true);
        isDrilling = true;
    }

    public void StopDrill()
    {
        drillAnimator.SetBool("Drilling", false);
        isDrilling = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Breakable" && isDrilling)
        {
            Destroy(collision.gameObject);
        }
    }
}
