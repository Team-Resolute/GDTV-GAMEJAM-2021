using UnityEngine;

namespace Sound
{
    public class AudioAssets : MonoBehaviour
    {
        private static AudioAssets _i;

        public static AudioAssets i
        {
            get
            {
                if (_i == null)
                {
                    _i = (Instantiate(Resources.Load("AudioAssets")) as GameObject)?.GetComponent<AudioAssets>();
                }

                return _i;
            }
        }
        
        public SoundBanks[] soundBanks;
        
        [System.Serializable]
        public class SoundBanks
        {
            public SoundManager.Sound sound;
            public SoundBank audioClip;
        }
    }
}