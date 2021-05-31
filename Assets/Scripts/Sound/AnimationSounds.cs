using UnityEngine;

namespace Sound
{
    public class AnimationSounds : MonoBehaviour
    {
        public void PlayStepSound()
        {
            SoundManager.PlaySound(SoundManager.Sound.Step, transform.position); 
            Debug.Log("Sound");
        }
    }
}