using UnityEngine;
using System;
using Random = UnityEngine.Random;


namespace Sound
{
    public class TriggerTest : MonoBehaviour
    {
        private void Awake()
        {
            SoundManager.Initialize();
        }
        
        public void PlayRandomSound()
        {
            var values = Enum.GetValues(typeof(SoundManager.Sound));
            int rand = Random.Range(0, 4);
            var randomSound = (SoundManager.Sound)values.GetValue(rand);
            SoundManager.PlaySound(randomSound, transform.position);
        }
    }
}
