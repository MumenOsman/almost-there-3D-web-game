using UnityEngine;

namespace Gemini
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("Audio Sources")]
        public AudioSource musicSource;
        public AudioSource sfxSource;

        [Header("Music Settings")]
        public AudioClip musicClip;
        public bool playOnStart = true;
        [Range(0f, 1f)] public float musicVolume = 0.5f;

        [Header("Default UI Sounds")]
        public AudioClip defaultHoverSound;
        public AudioClip defaultClickSound;

        private void Awake()
        {
            // Singleton Pattern: Ensure only one AudioManager exists
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Keep this object when loading new scenes
            }
            else
            {
                // If a new AudioManager is loaded (e.g., inside a new scene)
                // and it has a DIFFERENT music clip, we want to switch to that music.
                if (playOnStart && musicClip != null && musicClip != Instance.musicSource.clip)
                {
                    Instance.PlayMusic(musicClip, musicVolume);
                }

                Destroy(gameObject); // Destroy duplicate
                return;
            }

            // Create Sources if missing
            if (musicSource == null)
            {
                musicSource = gameObject.AddComponent<AudioSource>();
                musicSource.loop = true;
                musicSource.playOnAwake = false;
            }

            if (sfxSource == null)
            {
                sfxSource = gameObject.AddComponent<AudioSource>();
                sfxSource.loop = false;
                sfxSource.playOnAwake = false;
            }
        }

        private void Start()
        {
            if (playOnStart && musicClip != null)
            {
                PlayMusic(musicClip, musicVolume);
            }
        }

        public void PlayMusic(AudioClip clip, float volume = 0.5f)
        {
            if (clip == null) return;

            // Don't restart the same song
            if (musicSource.clip == clip && musicSource.isPlaying) return;

            musicSource.clip = clip;
            musicSource.volume = volume;
            musicSource.Play();
        }

        public void PlaySFX(AudioClip clip, float volume = 1f)
        {
            if (clip == null) return;
            sfxSource.PlayOneShot(clip, volume);
        }
    }
}
