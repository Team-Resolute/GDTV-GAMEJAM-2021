using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Item : MonoBehaviour, IInteractable
{
    private bool DialogueEventOccuredOnReload = false;
    private bool DialogueEventOccuredOnAcquire = false;

    [SerializeField] DialogueEventOnTimer DialogueEventOnFirstReload;
    [SerializeField] DialogueEventOnTimer DialogueEventOnSecondReload;
    
    public void OnInteract(GameObject player)
    {
        Combat playerCombat = FindObjectOfType<Combat>();
        if (!playerCombat) { return; }

        playerCombat.Reload();
        if (DialogueEventOccuredOnAcquire && !DialogueEventOccuredOnReload)
        {
            DialogueEventOnSecondReload.gameObject.SetActive(true);
        }
        
        if (!DialogueEventOccuredOnAcquire)
        {
            DialogueEventOnFirstReload.gameObject.SetActive(true);
        }
    }
    
    
}
