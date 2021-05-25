﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Sound
{
    public static class SoundManager
    {
        public enum Sound
        {
            Jump0,
            Jump1,
            Jump2,
            Jump3,
            ImpactJump4,
            ImpactJump5,
            ImpactJump6,
            PlaceHolder4,
            PlaceHolder5,
        }

        private static Dictionary<Sound, float> soundTimerDictionary;
        private static Dictionary<Sound, int> soundLoopDictionary;

        private static GameObject oneShotGameObject;
        private static AudioSource oneShotAudioSource;
        private static AudioSource loopAudioSource;
        
        public static void Initialize()
        {
            soundTimerDictionary = new Dictionary<Sound, float>();
            //soundTimerDictionary[Sound.Walk] = 0f; //This is foor loop sounds 
        }

        public static void PlaySound(Sound sound, Vector3 position)
        {
            if (CanPlaySound(sound))
            {
                GameObject soundGameObject = new GameObject("Sound");
                soundGameObject.transform.position = position;
                AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
                audioSource.clip = GetAudioClip(sound);
                audioSource.maxDistance = 100f;
                audioSource.spatialBlend = 1f;
                audioSource.rolloffMode = AudioRolloffMode.Linear;
                audioSource.dopplerLevel = 0f;
                audioSource.Play();
                
                Object.Destroy(soundGameObject, audioSource.clip.length);
            }
        }
        
        public static void PlaySound(Sound sound)
        {
            if (CanPlaySound(sound))
            {
                if (oneShotGameObject == null)
                {
                    oneShotGameObject = new GameObject("One shot sound");
                    oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
                }
                
                oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
            }
        }

        private static bool CanPlaySound(Sound sound)
        {
            switch (sound)
            {
               default:
                   return true;
               
               /*case Sound.sound:
               {
                   if (soundTimerDictionary.ContainsKey(sound))
                   {
                       float lastTimePlayed = soundTimerDictionary[sound];
                       float playerMoveTimerMax = 1f;
               
                       if (lastTimePlayed + playerMoveTimerMax < Time.time)
                       {
                           soundTimerDictionary[sound] = Time.time;
                           return true;
                       }
                       else
                       {
                           return false;
                       }
                   }
                   else
                   {
                       return true;
                   }*/
               
               // //break;
            }
        }
        
        private static AudioClip GetAudioClip(Sound sound)
        {
            foreach (var soundAudioClip in GameAssets.i.soundAudioClipArray)
            {
                if (soundAudioClip.sound == sound)
                {
                    return soundAudioClip.audioClip;
                }
            }
            Debug.LogError("Sound" + sound + "not found!");
            return null;
        }

        public static Sound GetRandomSoundRange(int x, int y)
        {
            var values = Enum.GetValues(typeof(SoundManager.Sound));
            int rand = Random.Range(x, y);
            var randomSound = (SoundManager.Sound)values.GetValue(rand);
            return randomSound;
        }
    }
}