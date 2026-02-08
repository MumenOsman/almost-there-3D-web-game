using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Gemini
{
    [RequireComponent(typeof(Button))]
    public class MenuButtonAudio : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        [Header("Audio Clips")]
        public AudioClip hoverSound;
        public AudioClip clickSound;

        [Header("Settings")]
        [Range(0f, 1f)] public float volume = 1f;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (AudioManager.Instance == null) return;

            AudioClip clipToPlay = hoverSound != null ? hoverSound : AudioManager.Instance.defaultHoverSound;
            
            if (clipToPlay != null)
            {
                AudioManager.Instance.PlaySFX(clipToPlay, volume * 0.5f);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (AudioManager.Instance == null) return;

            AudioClip clipToPlay = clickSound != null ? clickSound : AudioManager.Instance.defaultClickSound;

            if (clipToPlay != null)
            {
                AudioManager.Instance.PlaySFX(clipToPlay, volume);
            }
        }
    }
}
