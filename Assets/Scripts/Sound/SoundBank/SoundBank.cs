using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    [UnityEngine.CreateAssetMenu(fileName = "SoundBank", menuName = "Sound", order = 0)]
    public class SoundBank : UnityEngine.ScriptableObject
    {
        public AudioClip[] soundBank;
    }
}