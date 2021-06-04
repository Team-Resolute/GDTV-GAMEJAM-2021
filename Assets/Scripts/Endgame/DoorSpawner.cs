using System;
using System.Collections;
using System.Collections.Generic;
using Sound;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoorSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPointOne = default;
    [SerializeField] private Transform spawnPointTwo = default;
    [SerializeField] private GameObject DoorPrefab;
    [SerializeField] private Monster monster;

    [SerializeField] GameObject Percent20Dialogue = default;
    [SerializeField] GameObject Percent40Dialogue = default;
    [SerializeField] GameObject Percent80Dialogue = default;

    //[SerializeField] private GameObject shiftTriggerPrefab = default; 
    private void Start()
    {
        monster.onDeath += BeginSpawnDoorSequence;
    }

    private void OnDisable()
    {
        monster.onDeath -= BeginSpawnDoorSequence;
    }

    private void BeginSpawnDoorSequence()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 spawnPos = ChooseSpawnLocation(player);
        Vector3 groundSpawnPos = spawnPos;
        spawnPos = spawnPos + Vector3.up + Vector3.up;
        Vector3 playerPos = player.position + Vector3.up + Vector3.up;
        Vector3 vectToPlayer = playerPos - spawnPos;
        
        SpawnDialogue(spawnPos, vectToPlayer);
        //SpawnDreamShiftTrigger(spawnPos, vectToPlayer);
        SpawnDoor(groundSpawnPos);
        Destroy(gameObject,1f);
    }

    private Vector3 ChooseSpawnLocation(Transform player)
    {
        float dist1 = Vector3.Distance(spawnPointOne.position, player.position);
        float dist2 = Vector3.Distance(spawnPointTwo.position, player.position);
        if (dist1 > dist2)
        {
            return spawnPointOne.position;
        }
        else
        {
            return spawnPointTwo.position;
        }
    }

    private void SpawnDialogue(Vector3 doorSpawnPos, Vector3 vectToPlayer)
    {
        Percent20Dialogue.SetActive(true);
        Percent40Dialogue.SetActive(true);
        Percent80Dialogue.SetActive(true);
        
        Percent20Dialogue.transform.position = doorSpawnPos + (vectToPlayer * 0.25f);
        Percent40Dialogue.transform.position = doorSpawnPos + (vectToPlayer * 0.45f);
        Percent80Dialogue.transform.position = doorSpawnPos + (vectToPlayer * 0.8f);
    }

    private void SpawnDoor(Vector3 spawnPos)
    {
        Instantiate(DoorPrefab, spawnPos, Quaternion.identity);
        SoundManager.PlaySound(SoundManager.Sound.DoorAppearing);
    }

    /*private void SpawnDreamShiftTrigger(Vector3 spawnPos, Vector3 vectToPlayer)
    {
        spawnPos = spawnPos + (vectToPlayer * 0.15f);
        GameObject shiftTrigger = Instantiate(shiftTriggerPrefab, spawnPos, Quaternion.identity);
        shiftTrigger.SetActive(true);
        
    }*/
    
    
}
