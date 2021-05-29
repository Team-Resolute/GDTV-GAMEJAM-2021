using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles = default;
    [SerializeField] private GameObject playerPrefab = default;
    [SerializeField] private Sleepometer sleepometer;
    private Coroutine spawningCoroutine = default; 
    
    IEnumerator Spawn()
    {
        int partSpawnCount = Random.Range(1, 6);
        if (particles)
        {
            yield return new WaitForSeconds(0.25f);
            while (particles.particleCount < 250)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    yield return null;
                }

                particles.Emit(partSpawnCount);
                partSpawnCount = Random.Range(1, 6);
                yield return new WaitForSeconds(0.0125f);
            }
        }

        GameObject player = Instantiate(playerPrefab, transform.position, Quaternion.identity);

        if (particles)
        {
            while (particles.particleCount < 250)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    yield return null;
                }

                particles.Emit(partSpawnCount);
                partSpawnCount = Random.Range(1, 6);
                yield return new WaitForSeconds(0.125f);
            }
        }

        yield return new WaitForSeconds(0.25f);
        // player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
        sleepometer.StartTimer();
    }

    
    void Start()
    {
        spawningCoroutine = StartCoroutine(nameof(Spawn));
    }
}
