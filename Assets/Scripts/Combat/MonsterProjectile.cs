using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    private const float damage = 5f;
    private void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Sleepometer sleepometer = FindObjectOfType<Sleepometer>();
            if (sleepometer)
            {
                sleepometer.ReceiveHarm(damage);
            }
            else {Debug.Log("Sleepometer not found.");}
        }
        
        if (other.gameObject.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            gameObject.SetActive(false);
        }
    }
}
