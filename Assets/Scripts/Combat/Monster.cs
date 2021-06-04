using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Sound;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public interface IDamageable
{
    void TakeDamage(int damage);
}

public class Monster : MonoBehaviour, IDamageable
{

    private int hitpoints = 3;
    private float maxMoveSpeed = 2f;
    private float moveSpeed = 10f;
    private bool attacking = false;
    private float attackDelay = 3f;
    private float attackTimer = default;

    [SerializeField] private GameObject projectilePrefab;
    private List<GameObject> projectilePool = new List<GameObject>();
    private const int projectilesPerAttack = 20;
    [SerializeField] private Transform projectileEmptyParent = default;
    [SerializeField] private Transform projectileLaunchOrigin;
    [SerializeField] private Transform raycastOrigin = default;
    [SerializeField] private Transform raycastTarget = default;
    [SerializeField] private LayerMask layerMask;

    [SerializeField ]private GameObject lootPrefab = default;
    [SerializeField ]private GameObject deathEffectPrefab = default;

    public Action onDeath;
    

    void Start()
    {
        attackTimer = attackDelay;
        if (!projectilePrefab || !projectileEmptyParent) { return; }
        for (int i = 0; i < projectilesPerAttack; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity, projectileEmptyParent);
            projectilePool.Add(projectile);
            projectile.SetActive(false);
        }
    }

    
    void Update()
    {
        if (attackTimer > 0 && !attacking) { attackTimer -= Time.deltaTime;}
        
        float rayDist = Vector3.Distance(raycastOrigin.position, raycastTarget.position);
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hitInfo, rayDist, layerMask, QueryTriggerInteraction.Collide);
        if (!hit)
        {
            float move = Mathf.Clamp(Mathf.Sin(Time.time) * moveSpeed, moveSpeed * 0.2f, moveSpeed);
            
            // TODO Probably changed that to reflect player's stepping sound implementation 

            transform.position = transform.position + transform.forward * (move * Time.deltaTime);
            return;
        }

        if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            transform.forward = -transform.forward;
        }

        if (hitInfo.collider.gameObject.tag == "Player" && !attacking && attackTimer <= 0f)
        {
            Attack();
        }
    }

    private void Attack()
    {
        moveSpeed = 0;
        attacking = true;
        StartCoroutine(nameof(Attacking));
    }

    private IEnumerator Attacking()
    {
        for (int i=0; i<projectilePool.Count; i++)
        {
            projectilePool[i].SetActive(true);
            projectilePool[i].transform.position = projectileLaunchOrigin.position;
            projectilePool[i].transform.forward = projectileLaunchOrigin.forward;
            float projectileLaunchForce = 16f;
            float variation = projectileLaunchForce / 2f;
            projectileLaunchForce = projectileLaunchForce + Random.Range(-variation, variation);
            projectilePool[i].GetComponent<Rigidbody>().velocity = projectileLaunchOrigin.forward * projectileLaunchForce;

            SoundManager.PlaySound(SoundManager.Sound.MonsterProjectile, projectileLaunchOrigin.position, 0.3f);

            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.5f);
        moveSpeed = maxMoveSpeed;
        attacking = false;
        attackTimer = attackDelay;
    }

    public void TakeDamage(int damage)
    {
        SoundManager.PlaySound(SoundManager.Sound.MonsterHurt, transform.position);
        hitpoints -= damage;
        if (hitpoints < 1)
        {
            Die();
        }
    }

    private void Die()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
        SoundManager.PlaySound(SoundManager.Sound.MonsterDeath, transform.position, 0.5f);

        if (deathEffectPrefab)
        {
            GameObject deathEffect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(deathEffect,3f);
        }

        if (lootPrefab)
        {
            Instantiate(lootPrefab, transform.position, Quaternion.identity);
        }

        if (onDeath != null)
        {
            onDeath.Invoke();
        }
        
        Destroy(this.gameObject);
    }

    private void Hide()
    {
        
    }
    
}
