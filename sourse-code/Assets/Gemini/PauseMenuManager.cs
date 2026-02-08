using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace Gemini
{
    public class PauseMenuManager : MonoBehaviour
    {
        [Header("UI Reference")]
        [Tooltip("Assign the generic Pause Menu Panel here.")]
        public GameObject pauseMenuUI;

        [Header("Settings")]
        [Tooltip("Name of the Main Menu scene to load.")]
        public string mainMenuSceneName = "MainMenu";

        private bool isPaused = false;

        private void Start()
        {
            // Ensure menu is hidden at start
            if (pauseMenuUI != null)
                pauseMenuUI.SetActive(false);
                
            Time.timeScale = 1f;
        }

        private void Update()
        {
            // Check for Escape key using New Input System
            if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        public void Resume()
        {
            if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
            
            Time.timeScale = 1f;
            isPaused = false;

            // Re-lock cursor for gameplay
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void Pause()
        {
            if (pauseMenuUI != null) pauseMenuUI.SetActive(true);
            
            Time.timeScale = 0f;
            isPaused = true;

            // Unlock cursor for menu
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void LoadMenu()
        {
            Time.timeScale = 1f; // Must reset time before loading
            Debug.Log($"Gemini: Loading Menu '{mainMenuSceneName}'...");
            SceneManager.LoadScene(mainMenuSceneName);
        }

        public void QuitGame()
        {
            Debug.Log("Gemini: Quitting Game...");
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}
