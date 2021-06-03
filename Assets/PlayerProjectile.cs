using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private const int damage = 1;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
       //transform.Rotate(0f,0f,200f);
       transform.LookAt(transform.position + rb.velocity);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            IDamageable damageableEnemy = other.gameObject.GetComponent<IDamageable>();

            if (damageableEnemy != null)
            {
                damageableEnemy.TakeDamage(damage);
            }
            else
            {
                Debug.Log("Enemy cannot be damaged.");
            }
        }

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            gameObject.SetActive(false);
        }
    }
}
