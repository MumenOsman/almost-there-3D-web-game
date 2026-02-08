using UnityEngine;
using TMPro; // In case we want to support UI later

namespace Gemini
{
    public class ScoreTracker : MonoBehaviour
    {
        [Header("Score Settings")]
        [SerializeField] private int _currentScore = 0;
        public int targetScore = 10;
        
        public int currentScore => _currentScore;

        public delegate void OnScoreChanged(int newScore);
        public event OnScoreChanged onScoreChanged;

        [Header("Events")]
        public UnityEngine.Events.UnityEvent onTargetReached;
        
        [Header("Audio")]
        public AudioSource audioSource;
        public AudioClip scoreSound;
        [Range(0f, 1f)] public float soundVolume = 1f;

        private bool _targetReachedTriggered = false;

        public void AddScore(int amount)
        {
            _currentScore += amount;
            onScoreChanged?.Invoke(_currentScore);
            Debug.Log($"Gemini: Current Total Score: {_currentScore}");

            // Play Score Sound
            if (scoreSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(scoreSound, soundVolume);
            }
            else if (scoreSound != null && AudioManager.Instance != null)
            {
                // Fallback to AudioManager if no local source assigned
                AudioManager.Instance.PlaySFX(scoreSound, soundVolume);
            }

            if (!_targetReachedTriggered && _currentScore >= targetScore)
            {
                _targetReachedTriggered = true;
                onTargetReached?.Invoke();
                Debug.Log("Gemini: Target Score Reached! Level Complete event fired.");
            }
        }

        public void ResetScore()
        {
            _currentScore = 0;
            onScoreChanged?.Invoke(_currentScore);
        }
    }
}
