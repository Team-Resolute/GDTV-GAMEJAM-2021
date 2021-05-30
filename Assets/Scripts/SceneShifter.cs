using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneShifter : MonoBehaviour
{
    [SerializeField] private MeshRenderer quadToShift = default;
    [SerializeField] private List<Material> shiftedMaterials = new List<Material>();
    int shiftNumber = 0;
    
    void Start()
    {
        if (shiftedMaterials.Count > 0)
        {
            quadToShift.material = shiftedMaterials[0];
            shiftNumber++;
        }
    }

    void Update()
    {
        if (Debug.isDebugBuild && Input.GetKeyDown(KeyCode.Comma))
        {
            Shift();
        }
    }

    public void Shift()
    {
        if (shiftNumber < shiftedMaterials.Count)
        {
            quadToShift.material = shiftedMaterials[shiftNumber];
            shiftNumber++;
        }
    }
    
}
