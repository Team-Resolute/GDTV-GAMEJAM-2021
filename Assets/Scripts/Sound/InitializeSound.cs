using UnityEngine;

namespace Sound
{
    public class InitializeSound : MonoBehaviour
    {
        private void Awake()
        {
            SoundManager.Initialize();
        }
    }
}