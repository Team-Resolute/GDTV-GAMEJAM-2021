using System;
using System.Collections;
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
            ButtonFail,
            ClickButton,
            ClockTicking,
            Died,
            Hurt,
            Jump,
            JumpImpact,
            MonsterWalking,
            MonsterProjectile,
            MotherDialogue,
            SandmanDialogue,
            MinorCharacterDialogue,
            Spawn,
            Step,
            Tinkering,
            ItemCollected,
            PlayerShooting,
            MonsterHurt
        }

        private static Dictionary<Sound, float> soundTimerDictionary = new Dictionary<Sound, float>();
        private static Dictionary<int, GameObject> soundLoopDictionary = new Dictionary<int, GameObject>();

        private static GameObject oneShotGameObject;
        private static AudioSource oneShotAudioSource;
        private static AudioSource loopAudioSource;
        
        public static void Initialize()
        {
            soundTimerDictionary = new Dictionary<Sound, float>();
            //soundTimerDictionary[Sound.Walk] = 0f; //This is foor loop sounds 
            
            soundLoopDictionary = new Dictionary<int, GameObject>();
        }

        public static void PlaySound(Sound sound, Vector3 position)
        {
            if (GetAudioClip(sound) != null)
            {
                GameObject soundGameObject = new GameObject("Sound");
                soundGameObject.transform.position = position;
                AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
                audioSource.clip = GetAudioClip(sound);
                audioSource.maxDistance = 1000f;
                audioSource.spatialBlend = 1f;
                audioSource.rolloffMode = AudioRolloffMode.Linear;
                audioSource.dopplerLevel = 0f;
                audioSource.Play();
                
                Object.Destroy(soundGameObject, audioSource.clip.length);
            }
        }
        
        public static void PlaySoundLoop(Sound sound, Vector3 position, MonoBehaviour gameObject)
        {
            if (GetAudioClip(sound) != null)
            {
                GameObject soundGameObject = new GameObject("Sound");
                soundGameObject.transform.position = position;
                AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
                audioSource.clip = GetAudioClip(sound);
                audioSource.maxDistance = 1000f;
                audioSource.spatialBlend = 1f;
                audioSource.rolloffMode = AudioRolloffMode.Linear;
                audioSource.dopplerLevel = 0f;
                audioSource.loop = true;
                audioSource.Play();
                soundLoopDictionary.Add(gameObject.GetInstanceID(), soundGameObject);
            }
        }

        public static void StopLoop(MonoBehaviour gameObjectId)
        {
            if (soundLoopDictionary.ContainsKey(gameObjectId.GetInstanceID()))
            {
                gameObjectId.StartCoroutine(StartFade(soundLoopDictionary[gameObjectId.GetInstanceID()].GetComponent<AudioSource>(), 1f, 0f));
                soundLoopDictionary.Remove(gameObjectId.GetInstanceID());
            }
        }
        
        public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
        {
            float currentTime = 0;
            float start = audioSource.volume;

            while (audioSource.volume > 0)
            {
                //if (audioSource != null)
                {
                     currentTime += Time.deltaTime;
                     audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);   
                }
                yield return null;
            }
            audioSource.GetInstanceID();
            Object.Destroy(audioSource.gameObject);
            
            yield break;
        }
        
        public static void PlaySound(Sound sound)
        {
            if (GetAudioClip(sound) != null)
            {
                if (oneShotGameObject == null)
                {
                    oneShotGameObject = new GameObject("One shot sound");
                    oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
                }
                
                oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
            }
        }
        
        private static AudioClip GetAudioClip(Sound sound)
        {
            foreach (var soundAudioClip in AudioAssets.i.soundBanks)
            {
                if (soundAudioClip.sound == sound)
                {
                    return (AudioClip)soundAudioClip.audioClip.soundBank.GetValue(Random.Range(0,soundAudioClip.audioClip.soundBank.Length));
                }
            }
            Debug.Log("Sound need to be added!");
            return null;
        }

        public static Sound GetRandomSoundRange(int x, int y)
        {
            var values = Enum.GetValues(typeof(Sound));
            int rand = Random.Range(x, y);
            var randomSound = (Sound)values.GetValue(rand);
            return randomSound;
        }
    }
}