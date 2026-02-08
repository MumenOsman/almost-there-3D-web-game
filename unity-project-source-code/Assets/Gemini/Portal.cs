using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gemini
{
    [AddComponentMenu("Gemini/Level Portal")]
    public class Portal : MonoBehaviour
    {
        [Header("Scene Transition Settings")]
        [Tooltip("Name of the scene to load when entering the portal.")]
        public string nextSceneName;

        [Header("Effects")]
        [Tooltip("Optional: Sound or particles when entering.")]
        public UnityEngine.Events.UnityEvent onPortalEnter;

        private void OnTriggerEnter(Collider other)
        {
            // Only the player should trigger the portal
            if (other.CompareTag("Player") || other.GetComponentInParent<ScoreTracker>() != null)
            {
                Debug.Log($"Gemini: Player entered portal. Loading {nextSceneName}...");
                onPortalEnter?.Invoke();
                
                if (!string.IsNullOrEmpty(nextSceneName))
                {
                    SceneManager.LoadScene(nextSceneName);
                }
                else
                {
                    Debug.LogWarning("Gemini: Portal has no Next Scene Name assigned!");
                }
            }
        }
    }
}
