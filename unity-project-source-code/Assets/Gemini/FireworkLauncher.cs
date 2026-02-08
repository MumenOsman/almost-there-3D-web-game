using UnityEngine;

namespace Gemini
{
    public class FireworkLauncher : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("The Particle System to trigger.")]
        public ParticleSystem fireworks;
        [Tooltip("The Audio Source for explosion sounds.")]
        public AudioSource audioSource;

        [Header("Settings")]
        public int burstCount = 5;
        public float timeBetweenBursts = 0.5f;

        /// <summary>
        /// Call this method via UnityEvent (e.g. ScoreTracker.onTargetReached)
        /// </summary>
        public void Launch()
        {
            if (fireworks == null)
            {
                Debug.LogWarning("Gemini: No Particle System assigned to the FireworkLauncher!");
                return;
            }

            StartCoroutine(CelebrationSequence());
        }

        private System.Collections.IEnumerator CelebrationSequence()
        {
            for (int i = 0; i < burstCount; i++)
            {
                fireworks.Play();
                
                if (audioSource != null)
                {
                    audioSource.Play();
                }

                Debug.Log($"Gemini: Firework Burst {i + 1} launched!");
                yield return new WaitForSeconds(timeBetweenBursts);
            }
        }
    }
}
