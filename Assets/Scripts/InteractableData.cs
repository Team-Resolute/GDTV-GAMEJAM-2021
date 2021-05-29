using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public interface IInteractable
{
    public void OnInteract(GameObject player);
}

public class InteractableData : MonoBehaviour
{
    [SerializeField] private float interactionTime = 10f;
    private IInteractable interactionScript = default;
    private bool hasInteracted = false;
    private bool interactable = true;

    private void Start()
    {
        interactionScript = GetComponentInParent<IInteractable>();
    }
    
    public void Interact(GameObject player)
    {
        interactionScript?.OnInteract(player);
    }

    public float GetInteractTime()
    {
        return interactionTime;
    }

    public bool IsInteractable()
    {
        if (hasInteracted) { return false; }
        if (interactable) { return true; }

        return false;
    }
}
