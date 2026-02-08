using UnityEngine;
using TMPro;

namespace Gemini
{
    public class ScoreHUD : MonoBehaviour
    {
        [Header("References")]
        public ScoreTracker scoreTracker;
        public TextMeshProUGUI scoreText;

        [Header("Settings")]
        public string prefix = "Carrots: ";
        public string completionMessage = "Level Complete!";

        private void Start()
        {
            if (scoreTracker == null)
            {
                // Try to find the player if not assigned
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null) scoreTracker = player.GetComponent<ScoreTracker>();
            }

            if (scoreTracker != null)
            {
                scoreTracker.onScoreChanged += UpdateDisplay;
                UpdateDisplay(scoreTracker.currentScore);
            }
            else
            {
                Debug.LogWarning("Gemini: ScoreHUD could not find a ScoreTracker. Please assign it manually.");
            }
        }

        private void OnDestroy()
        {
            if (scoreTracker != null)
            {
                scoreTracker.onScoreChanged -= UpdateDisplay;
            }
        }

        private void UpdateDisplay(int currentScore)
        {
            if (scoreText != null && scoreTracker != null)
            {
                if (currentScore >= scoreTracker.targetScore)
                {
                    scoreText.text = $"<color=green>{prefix}{currentScore}/{scoreTracker.targetScore}\n{completionMessage}</color>";
                }
                else
                {
                    scoreText.text = $"{prefix}{currentScore}/{scoreTracker.targetScore}";
                }
            }
        }
    }
}
