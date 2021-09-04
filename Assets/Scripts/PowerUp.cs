using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int payoutAmount;
    public bool payoutOnCrash;
    public bool payoutOnHit;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && payoutOnCrash)
            payOut();
        if (collision.CompareTag("PlayerProjectile") && payoutOnHit)
            payOut();
    }

    private void payOut()
    {
        Score.instance.moneyGained += payoutAmount;
    }
}
