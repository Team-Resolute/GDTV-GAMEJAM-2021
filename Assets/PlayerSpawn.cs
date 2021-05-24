using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles = default;
    [SerializeField] private GameObject playerPrefab = default;
    private Coroutine spawningCoroutine = default; 
    
    IEnumerator Spawn()
    {
        int partSpawnCount = Random.Range(1, 6); 
        yield return new WaitForSeconds(0.5f);
        while (particles.particleCount < 250){
            if (Input.GetKey(KeyCode.A))
            {
                yield return null;
            }
            particles.Emit(partSpawnCount);
            partSpawnCount = Random.Range(1, 6);
            yield return new WaitForSeconds(0.025f);
        }
        GameObject player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        while (particles.particleCount < 250){
            if (Input.GetKey(KeyCode.A))
            {
                yield return null;
            }
            particles.Emit(partSpawnCount);
            partSpawnCount = Random.Range(1, 6);
            yield return new WaitForSeconds(0.025f);
        }
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<PlayerMovement>().enabled = true;
    }

    
    void Start()
    {
        spawningCoroutine = StartCoroutine(nameof(Spawn));
    }
}
