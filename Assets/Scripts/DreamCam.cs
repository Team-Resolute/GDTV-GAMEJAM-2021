using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DreamCam : MonoBehaviour
{
    private Camera cam = default;
    [SerializeField] private Transform focusPoint = default;
    [Range(1f,10f)] private float followSpeed = 20f;
    
    [Range(1f,10f)] private const float wobbleStrength = 1f;
    private float wobble = 0f;
    private float maxWobble = 5f;
    private float wobbleAmt = 0f;
    [SerializeField] private bool isWobbling = false;
    
    private Vector3 focusOffset = default;
    private Vector3 desiredPos = default;
    
    private void Start()
    {
        cam = GetComponent<Camera>();
        focusOffset = transform.position - focusPoint.position;
    }

    void LateUpdate()
    {
        desiredPos = focusPoint.position + focusOffset;
        
        transform.position = Vector3.MoveTowards(transform.position, desiredPos, followSpeed * Time.deltaTime);
        if (isWobbling) {Wobble();}
        
    }

    void Wobble()
    {
        float wobbleAmt = Random.Range(-wobbleStrength, wobbleStrength);
        wobbleAmt = Mathf.Clamp(wobbleAmt, -maxWobble, maxWobble);
        wobble = wobble + (wobbleAmt * Time.deltaTime);
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y, wobble);
        transform.rotation = Quaternion.Euler(rot);
    }

    public void FocusOnPlayer()
    {
        focusOffset = transform.position - focusPoint.position;
        FocusPoint focusBehavior = focusPoint.gameObject.GetComponent<FocusPoint>();
        if (focusBehavior) {focusBehavior.FocusOnPlayer();}
        transform.position = focusPoint.position + focusOffset;
    }
}
