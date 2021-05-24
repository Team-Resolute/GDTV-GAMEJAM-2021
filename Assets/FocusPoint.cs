using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusPoint : MonoBehaviour
{
    private Transform player = default;
    [SerializeField] private Transform playerSpawnPoint = default;
    private const string PlayerTag = "Player";
    private const float MaxDistanceToPlayer = 1.5f;
    private const float FollowSpeed = 100f;
    
    void Update()
    {
        if (!player)
        {
            Reset();
            FocusOnPlayer();
            return;
        }

        if (Vector3.Distance(transform.position, player.position) > MaxDistanceToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, FollowSpeed);
        }
        
    }

    public void Reset()
    {
        transform.position = playerSpawnPoint.position;
    }

    public void FocusOnPlayer()
    {
        if (!player)
        {
            GameObject playerGO = GameObject.FindGameObjectWithTag(PlayerTag);
            if (playerGO)
            {
                player = playerGO.transform;
            }
            
            if (!player)
            {
                return;
            }
        }
        transform.position = player.position;
    }
}
