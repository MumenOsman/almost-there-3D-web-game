using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gemini
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("The name of the scene to load when 'Play' is clicked.")]
        public string firstLevelName = "Level1";

        public void PlayGame()
        {
            Debug.Log($"Gemini: Play Button Clicked! Attempting to load '{firstLevelName}'...");
            
            if (!string.IsNullOrEmpty(firstLevelName))
            {
                if (SceneManager.GetSceneByName(firstLevelName).IsValid())
                {
                     Debug.Log($"Gemini: Scene '{firstLevelName}' is valid and loaded? Checking Build Settings...");
                }

                // Check if scene is in build settings by iterating (inefficient but good for debug)
                bool found = false;
                for(int i=0; i< SceneManager.sceneCountInBuildSettings; i++)
                {
                    string path = SceneUtility.GetScenePathByBuildIndex(i);
                    if (path.Contains(firstLevelName)) 
                    {
                        found = true; 
                        break;
                    }
                }

                if (found)
                {
                    Debug.Log($"Gemini: Loading {firstLevelName}...");
                    SceneManager.LoadScene(firstLevelName);
                }
                else
                {
                    Debug.LogError($"Gemini: Scene '{firstLevelName}' was NOT found in Build Settings! Go to File > Build Settings and drag your scene there.");
                }
            }
            else
            {
                Debug.LogError("Gemini: First Level Name is empty! Please type the scene name in the MainMenuManager component.");
            }
        }

        public void QuitGame()
        {
            Debug.Log("Gemini: Quitting game...");
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}
