using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sound
{
    public class LoopTest : MonoBehaviour
    {
        private void Start()
        {
            SoundManager.Initialize();
            SoundManager.PlaySoundLoop(SoundManager.Sound.Tinkering,transform.position, this);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space)){
                 SoundManager.StopLoop(this);
            }
        }
    }
}