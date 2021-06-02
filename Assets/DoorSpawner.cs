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
        SpawnDialogue(player, spawnPos);
        SpawnDoor(spawnPos);
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

    private void SpawnDialogue(Transform player, Vector3 doorSpawnPos)
    {
        Percent20Dialogue.SetActive(true);
        Percent40Dialogue.SetActive(true);
        Percent80Dialogue.SetActive(true);
        doorSpawnPos = doorSpawnPos + Vector3.up + Vector3.up;
        Vector3 playerPos = player.position + Vector3.up + Vector3.up;
        Vector3 vectToPlayer = playerPos - doorSpawnPos;
        Percent20Dialogue.transform.position = doorSpawnPos + (vectToPlayer * 0.2f);
        Percent40Dialogue.transform.position = doorSpawnPos + (vectToPlayer * 0.4f);
        Percent80Dialogue.transform.position = doorSpawnPos + (vectToPlayer * 0.8f);
    }

    private void SpawnDoor(Vector3 spawnPos)
    {
        Instantiate(DoorPrefab, spawnPos, Quaternion.identity);
        SoundManager.PlaySound(SoundManager.Sound.DoorAppearing);
    }
    
    
}
