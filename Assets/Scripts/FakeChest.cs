using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FakeChest : MonoBehaviour, IInteractable
{
   
   [SerializeField] private bool lethal = false;
   [SerializeField] private Transform teleportPoint;

   private void Teleport(GameObject other)
   {
      PlayerController movement = other.GetComponent<PlayerController>();
      if (movement)
      {
         movement.Teleport(teleportPoint.position);
      }
      DreamCam dreamCam = Camera.main.GetComponent<DreamCam>();
      if (dreamCam)
      {
         dreamCam.FocusOnPlayer();
      }
   }

   public void OnInteract(GameObject player)
   {
      Teleport(player);
   }
   
   private void OnTriggerEnter(Collider other)
   {
      return;
      Debug.Log("You grabbed the chest!");
      if (other.gameObject.CompareTag("Player") && !lethal)
      {
         Teleport(other.gameObject);
      }
      else if (other.CompareTag("Player"))

      {
         // TODO: kill the player
      }
      
   }
}
