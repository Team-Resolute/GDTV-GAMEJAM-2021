using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
   [SerializeField] private bool lethal = false;
   [SerializeField] private Transform teleportPoint;
   
   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.CompareTag("Player") && !lethal)
      {
         // PlayerMovement movement = other.gameObject.GetComponent<PlayerMovement>();
         PlayerController movement = other.gameObject.GetComponent<PlayerController>();
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
      else if (other.CompareTag("Player"))

      {
         // TODO: kill the player
      }
      
   }
}
