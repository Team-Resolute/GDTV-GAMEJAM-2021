using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftTrigger : MonoBehaviour
{
    [SerializeField] private int shiftNum;
    private void OnTriggerEnter(Collider other)
    {
        SceneShifter.Instance.Shift(shiftNum);
        Destroy(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
