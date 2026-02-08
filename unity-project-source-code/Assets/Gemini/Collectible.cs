using UnityEngine;
using UnityEngine.Events;

namespace Gemini
{
    public class Collectible : MonoBehaviour
    {
        [Header("Collectible Settings")]
        public string collectibleName = "Carrot";
        public int pointValue = 1;
        
        [Header("Visual Effects")]
        public float rotationSpeed = 50f;
        public float bobSpeed = 2f;
        public float bobAmount = 0.2f;

        [Header("Events")]
        public UnityEvent onCollected;
        
        // Audio moved to ScoreTracker for centralized management

        private Vector3 _startPos;
        private bool _isCollected = false;

        private void Start()
        {
            _startPos = transform.position;
        }

        private void Update()
        {
            // Simple visual feedback: Rotate and Bob
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            transform.position = _startPos + Vector3.up * Mathf.Sin(Time.time * bobSpeed) * bobAmount;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isCollected) return;

            // Check if the object that entered has a ScoreTracker (or its parent)
            ScoreTracker tracker = other.GetComponent<ScoreTracker>();
            if (tracker == null) tracker = other.GetComponentInParent<ScoreTracker>();

            // Collect if it's the player
            if (tracker != null || other.CompareTag("Player"))
            {
                Collect(tracker);
            }
        }

        public void Collect(ScoreTracker tracker)
        {
            if (_isCollected) return;
            _isCollected = true;

            if (tracker != null)
            {
                tracker.AddScore(pointValue);
                Debug.Log($"Gemini: Collected {collectibleName}! Points added: {pointValue}");
            }

            // Fire event (useful for playing sounds or spawning particles)
            onCollected?.Invoke();

            // Destroy the collectible
            Destroy(gameObject);
        }
    }
}
