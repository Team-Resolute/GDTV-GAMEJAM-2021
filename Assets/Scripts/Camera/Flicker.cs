using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    bool flickerOn = true;
    private float flickerLimit = 1.5f;
    private float flickerStrength = 0.8f;
    private float flickerDelay = 0.3f;
    private Vector3 startPos;
    private Vector3 targetPoint;
    private Vector3 flickerVector;
    
    
    void Start()
    {
        startPos = transform.position;
        if (flickerOn) {InvokeRepeating(nameof(MoveRandomly), 0, flickerDelay);}
    }

    
    void MoveRandomly()
    {
        targetPoint.x = Random.Range(startPos.x - flickerLimit, startPos.x + flickerLimit);
        targetPoint.y = Random.Range(startPos.y - flickerLimit, startPos.y + flickerLimit);
        targetPoint.z = Random.Range(startPos.z - flickerLimit, startPos.z + flickerLimit);
        flickerVector = (targetPoint - transform.position).normalized * flickerStrength;
        transform.position += flickerVector;
    }
}
