using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using Sound;
using UnityEngine.UI;

public class Combat : MonoBehaviour
{
    [SerializeField] private int ammo;
    private int maxAmmo = 10;
    private float baseShootingForce = 24;
    [SerializeField] private GameObject projectilePrefab;
    private List<Rigidbody> projectilePool = new List<Rigidbody>();

    [SerializeField] private GameObject ammoUIBox = default;
    [SerializeField] private TextMeshProUGUI ammoUIBoxText = default;
    [SerializeField] private Image ammoBar = default;
    private Transform firePoint = default;
    
    private int projectileNum = 1;
    private const float maxShootingTimer = 1f;
    private float shootingTimer = 0f;

    private PlsyerInteraction playerInteraction = default;
    
    [SerializeField] private DialogueEventOnTimer needMoreAmmo;

    private void Start()
    {
        for (int i = 0; i < maxAmmo; i++)
        {
            GameObject projectileGO = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectilePool.Add(projectileGO.GetComponent<Rigidbody>());
            projectileGO.SetActive(false);
        }
    }
    
    private void Update()
    {
        if (!ammoUIBox || !ammoUIBoxText || !ammoBar)
        {
            return;
        }
        
        if (shootingTimer < maxShootingTimer)
        {
            shootingTimer += Time.deltaTime;
        }

        shootingTimer = Mathf.Clamp(shootingTimer + Time.deltaTime, 0f, maxShootingTimer);

        if (Input.GetButtonDown("Fire1") && ammo > 0 && shootingTimer > maxShootingTimer - 0.1f)
        {
            if (!playerInteraction)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (!player) { return; }

                playerInteraction = player.GetComponent<PlsyerInteraction>();
                if (!playerInteraction) { return; }
            }

            if (playerInteraction.isActiveAndEnabled && playerInteraction.IsPlayerInteracting()) { return;}

            if (playerInteraction.isActiveAndEnabled && playerInteraction.IsInteractableObjectNear()) { return;}
                
            if (!firePoint)
            {
                firePoint = GameObject.FindGameObjectWithTag("FirePoint").transform;
            }

            projectileNum++;
            if (projectileNum > maxAmmo)
            {
                projectileNum = 1;
            }
            shootingTimer = 0f;
            float variation = baseShootingForce * 0.20f;
            float shootingForce = baseShootingForce + Random.Range(-variation, variation);

            Rigidbody projectile = projectilePool[projectileNum-1];
            projectile.gameObject.SetActive(true);
            SoundManager.PlaySound(SoundManager.Sound.PlayerShooting, playerInteraction.transform.position, 0.2f);
            projectile.transform.position = firePoint.position;
            projectile.transform.forward = firePoint.forward;
            projectile.velocity = firePoint.forward * shootingForce;
            ammo--;
            if (ammo < 1)
            {
                if (needMoreAmmo) {needMoreAmmo.gameObject.SetActive(true);}
            }
            
        }

        UpdateAmmoUI();
        
    }

    public void Reload()
    {
        ammo = maxAmmo;
        GameObject player = GameObject.Find("Player(Clone)");
        SoundManager.PlaySound(SoundManager.Sound.ItemCollected, player.transform.position, 0.3f);
        UpdateAmmoUI();
    }

    private void UpdateAmmoUI()
    {
        if (ammo>0) {ammoUIBox.SetActive(true);}
        ammoUIBoxText.text = "x " + ammo;

        if (shootingTimer > maxShootingTimer - 0.1f)
        {
            ammoBar.color = Color.green;
        }
        else
        {
            ammoBar.color = Color.red;
        }

        ammoBar.fillAmount = shootingTimer / maxShootingTimer;
    }
}
