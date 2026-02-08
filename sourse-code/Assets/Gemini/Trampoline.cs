using UnityEngine;

namespace Gemini
{
    public class Trampoline : MonoBehaviour
    {
        [Header("Bounce Settings")]
        [Tooltip("The upward force applied to the player.")]
        public float bouncePower = 10f;

        [Tooltip("Minimum time between bounces to prevent 'vibrating' on the trampoline.")]
        public float cooldown = 0.5f;

        [Header("Effects")]
        public UnityEngine.Events.UnityEvent onBounce;
        public AudioSource audioSource;
        public AudioClip bounceSound;

        private float _lastBounceTime;

        private void OnTriggerEnter(Collider other)
        {
            if (Time.time < _lastBounceTime + cooldown) return;

            // Try to find the RabbitController on the object or its parent
            RabbitController rabbit = other.GetComponent<RabbitController>();
            if (rabbit == null) rabbit = other.GetComponentInParent<RabbitController>();

            if (rabbit != null)
            {
                _lastBounceTime = Time.time;
                rabbit.Bounce(bouncePower);
                
                // Play Sound
                if (bounceSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(bounceSound);
                }

                // Fire optional visual/audio events
                onBounce?.Invoke();
            }
        }
    }
}
