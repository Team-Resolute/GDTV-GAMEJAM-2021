using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Sound;
using UnityEngine;
using UnityEngine.UI;

public class PlsyerInteraction : MonoBehaviour
{
    [SerializeField] private Image bar;
    private float maxRange = 4f;
    private bool interacting = false;
    private Vector3 interactingPos = default;
    private float timer = default;
    private float maxTimer = default;
    private InteractableData target = null;
    [SerializeField] private LayerMask interactionLayer;

    void Start()
    {
        //interactionLayer = ~interactionLayer;
        if (bar) {bar.gameObject.SetActive(false);}
    }
    void Update()
    {
        if (interacting && Vector3.Distance(interactingPos, transform.position) > maxRange)
        {
            InteractCancel();
            return;
        }

        if (interacting)
        {
            ProcessTimer();
            return;
        }

        if (!interacting && Input.GetButtonDown("Fire1"))
        {
            Collider[] potentialTargets = Physics.OverlapSphere(transform.position, maxRange, interactionLayer, QueryTriggerInteraction.Collide);
            List<InteractableData> targetsNearby = new List<InteractableData>();
            foreach (Collider potentialTarget in potentialTargets)
            {
                InteractableData interactData = potentialTarget.gameObject.GetComponent<InteractableData>();
                if (interactData)
                {
                    targetsNearby.Add(interactData);
                }
            }
            
            if (targetsNearby.Count > 0)
            {
                target = targetsNearby[Random.Range(0, targetsNearby.Count - 1)];
                if (target != null && target.IsInteractable())
                {
                    interacting = true;
                    InteractStart();
                }
            }
        }
    }

    public bool IsInteractableObjectNear()
    {
        bool interactableNearby = false;
        Collider[] potentialTargets = Physics.OverlapSphere(transform.position, maxRange, interactionLayer, QueryTriggerInteraction.Collide);
        if (potentialTargets.Length > 0)
        {
            interactableNearby = true;
        }

        return interactableNearby;
    }

    void InteractStart()
    {
        if (target == null) { return;}
        float shrinkFactor = 1;
        interacting = true;

        interactingPos = transform.position;

        //TODO Sound of tinkering
        SoundManager.PlaySoundLoop(SoundManager.Sound.Tinkering, interactingPos,this);

        maxTimer = target.GetInteractTime();
        timer = maxTimer;
        bar.gameObject.SetActive(true);
        bar.fillAmount = 1f;
        Vector3 adjustedScale = Vector3.one;
        adjustedScale.x = timer / shrinkFactor;
        bar.transform.localScale = adjustedScale;
    }

    void ProcessTimer()
    {
        if (target == null) { return; }
        
        timer -= Time.deltaTime;
        bar.fillAmount = timer / maxTimer;
        if (timer < 0f)
        {
            InteractSuccess();
        }
    }
    
    void InteractCancel()
    {
        target = null;
        bar.gameObject.SetActive(false);
        bar.fillAmount = 1f;
        interacting = false;

        SoundManager.StopLoop(this);
    }

    void InteractSuccess()
    {
        bar.gameObject.SetActive(false);
        bar.fillAmount = 1f;
        interacting = false;

        SoundManager.StopLoop(this);

        target.Interact(this.gameObject);
        target = null;
    }

    public bool IsPlayerInteracting()
    {
        return interacting;
    }
}
