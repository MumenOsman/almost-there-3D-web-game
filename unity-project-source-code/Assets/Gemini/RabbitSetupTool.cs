#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine.InputSystem;
using TMPro;

namespace Gemini
{
    public class RabbitSetupTool : EditorWindow
    {
        [MenuItem("Tools/Gemini/Setup Rabbit Controller")]
        public static void SetupRabbit()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected == null)
            {
                EditorUtility.DisplayDialog("Error", "Please select the Rabbit GameObject in the scene.", "OK");
                return;
            }

            // 1. Add/Fix Character Controller
            var charController = selected.GetComponent<CharacterController>();
            if (charController == null) charController = selected.AddComponent<CharacterController>();
            
            // Adjust defaults for a small rabbit
            charController.center = new Vector3(0, 0.5f, 0);
            charController.radius = 0.45f;
            charController.height = 1f;
            // Step Offset must be <= Height + Radius * 2. 
            charController.stepOffset = 0.1f;
            // Skin Width prevents hovering on small characters
            charController.skinWidth = 0.01f;

            // 2. Add/Fix Input System (New Input System)
            var playerInput = selected.GetComponent<PlayerInput>();
            if (playerInput == null) playerInput = selected.AddComponent<PlayerInput>();

            // 2b. Add/Fix Score Tracker
            var scoreTracker = selected.GetComponent<ScoreTracker>();
            if (scoreTracker == null) scoreTracker = selected.AddComponent<ScoreTracker>();

            // Ensure the Rabbit is tagged as Player
            selected.tag = "Player";

            // Try to find the default InputActions asset
            string actionsPath = "Assets/InputSystem_Actions.inputactions";
            InputActionAsset actionsAsset = AssetDatabase.LoadAssetAtPath<InputActionAsset>(actionsPath);
            
            if (actionsAsset != null)
            {
                playerInput.actions = actionsAsset;
                playerInput.defaultControlScheme = "Keyboard&Mouse";
            }
            else
            {
                Debug.LogWarning("Gemini: Could not find 'Assets/InputSystem_Actions.inputactions'. Please assign your Input Action Asset to the PlayerInput component manually.");
            }

            // 3. Add Rabbit Controller
            var controller = selected.GetComponent<RabbitController>();
            if (controller == null) controller = selected.AddComponent<RabbitController>();

            // 4. Create/Assign Animator Controller
            string animatorPath = "Assets/Gemini/RabbitAnimator.controller";
            AnimatorController animatorController = AssetDatabase.LoadAssetAtPath<AnimatorController>(animatorPath);

            if (animatorController == null)
            {
                animatorController = AnimatorController.CreateAnimatorControllerAtPath(animatorPath);
                animatorController.AddParameter("Speed", AnimatorControllerParameterType.Float);
                animatorController.AddParameter("IsMoving", AnimatorControllerParameterType.Bool);
                animatorController.AddParameter("Jump", AnimatorControllerParameterType.Trigger);
                animatorController.AddParameter("IsGrounded", AnimatorControllerParameterType.Bool);
                animatorController.AddParameter("VerticalVelocity", AnimatorControllerParameterType.Float);
            }
            else
            {
                // Ensure all parameters exist
                AddParameterIfMissing(animatorController, "Jump", AnimatorControllerParameterType.Trigger);
                AddParameterIfMissing(animatorController, "IsGrounded", AnimatorControllerParameterType.Bool);
                AddParameterIfMissing(animatorController, "VerticalVelocity", AnimatorControllerParameterType.Float);
            }

            var animator = selected.GetComponent<Animator>();
            if (animator == null) animator = selected.AddComponent<Animator>();
            animator.runtimeAnimatorController = animatorController;
            // Disable Root Motion so the script handles movement
            animator.applyRootMotion = false;

            // 5. Setup Camera
            Camera mainCam = Camera.main;
            if (mainCam != null)
            {
                var camFollow = mainCam.gameObject.GetComponent<CameraFollow>();
                if (camFollow == null) camFollow = mainCam.gameObject.AddComponent<CameraFollow>();
                camFollow.target = selected.transform;
            }

            // 6. Setup UI
            SetupHUD(selected);

            Debug.Log($"Rabbit Controller Setup Complete for '{selected.name}'! \n1. Open Animator window.\n2. Drag animations from 'Rabbit_Grey.fbx' into the animator.\n3. Check the Top-Left of your screen in Play mode for the Carrot counter!");
        }

        [MenuItem("Tools/Gemini/Create Start Menu")]
        public static void CreateStartMenu()
        {
            // ... (existing content)
            // 1. Setup Canvas
            var canvas = Object.FindAnyObjectByType<Canvas>();
            if (canvas == null || canvas.gameObject.name != "MainMenuCanvas")
            {
                GameObject canvasObj = new GameObject("MainMenuCanvas");
                canvas = canvasObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
                canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();
            }

            // 2. Setup EventSystem
            SetupEventSystem();

            // 3. Setup Manager
            GameObject managerObj = new GameObject("MainMenuManager");
            var manager = managerObj.AddComponent<MainMenuManager>();

            // 4. Setup UI Container
            GameObject panel = new GameObject("Panel");
            panel.transform.SetParent(canvas.transform, false);
            RectTransform panelRect = panel.AddComponent<RectTransform>();
            panelRect.anchorMin = Vector2.zero;
            panelRect.anchorMax = Vector2.one;
            panelRect.sizeDelta = Vector2.zero;
            panel.AddComponent<UnityEngine.UI.Image>().color = new Color(0,0,0,0.6f);

            // 5. Title
            GameObject titleObj = new GameObject("Title");
            titleObj.transform.SetParent(panel.transform, false);
            var titleText = titleObj.AddComponent<TextMeshProUGUI>();
            titleText.text = "RABBIT ADVENTURE";
            titleText.fontSize = 72;
            titleText.alignment = TextAlignmentOptions.Center;
            RectTransform titleRect = titleObj.GetComponent<RectTransform>();
            titleRect.anchoredPosition = new Vector2(0, 150);

            // 6. Buttons
            CreateMenuButton("Play Button", "PLAY", new Vector2(0, 0), panel.transform, manager.PlayGame);
            CreateMenuButton("Quit Button", "QUIT", new Vector2(0, -60), panel.transform, manager.QuitGame);

            Selection.activeGameObject = managerObj;
            Debug.Log("Gemini: Start Menu created successfully!");
        }

        [MenuItem("Tools/Gemini/Create Pause Menu")]
        public static void CreatePauseMenu()
        {
            // 1. Setup Canvas
            var canvas = Object.FindAnyObjectByType<Canvas>();
            if (canvas == null || canvas.gameObject.name != "MainMenuCanvas")
            {
                GameObject canvasObj = new GameObject("PauseCanvas");
                canvas = canvasObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.sortingOrder = 100; // Ensure it's on top
                canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
                canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();
            }

            // 2. Setup EventSystem
            SetupEventSystem();

            // 3. Setup Manager
            GameObject managerObj = new GameObject("PauseMenuManager");
            var manager = managerObj.AddComponent<PauseMenuManager>();

            // 4. Setup UI Container
            GameObject panel = new GameObject("PausePanel");
            panel.transform.SetParent(canvas.transform, false);
            RectTransform panelRect = panel.AddComponent<RectTransform>();
            panelRect.anchorMin = Vector2.zero;
            panelRect.anchorMax = Vector2.one;
            panelRect.sizeDelta = Vector2.zero;
            
            var img = panel.AddComponent<UnityEngine.UI.Image>();
            img.color = new Color(0, 0, 0, 0.85f); // Dark background

            // Link Panel to Manager
            manager.pauseMenuUI = panel;

            // 5. Title
            GameObject titleObj = new GameObject("Title");
            titleObj.transform.SetParent(panel.transform, false);
            var titleText = titleObj.AddComponent<TextMeshProUGUI>();
            titleText.text = "PAUSED";
            titleText.fontSize = 64;
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.color = Color.white;
            RectTransform titleRect = titleObj.GetComponent<RectTransform>();
            titleRect.anchoredPosition = new Vector2(0, 150);

            // 6. Buttons
            CreateMenuButton("ResumeButton", "RESUME", new Vector2(0, 20), panel.transform, manager.Resume);
            CreateMenuButton("MenuButton", "MENU", new Vector2(0, -50), panel.transform, manager.LoadMenu);
            CreateMenuButton("QuitButton", "QUIT", new Vector2(0, -120), panel.transform, manager.QuitGame);

            Selection.activeGameObject = managerObj;
            Debug.Log("Gemini: Pause Menu created successfully! Press 'ESC' in Play Mode to test.");
        }

        [MenuItem("Tools/Gemini/Create Audio Manager")]
        public static void CreateAudioManager()
        {
            var audioManager = Object.FindAnyObjectByType<AudioManager>();
            if (audioManager == null)
            {
                GameObject obj = new GameObject("AudioManager");
                audioManager = obj.AddComponent<AudioManager>();
                Debug.Log("Gemini: AudioManager created! Drag your music/sfx into its slot (or use scripts to play).");
            }
            else
            {
                Debug.Log("Gemini: AudioManager already exists.");
            }
            Selection.activeGameObject = audioManager.gameObject;
        }

        private static void CreateMenuButton(string name, string label, Vector2 pos, Transform parent, UnityEngine.Events.UnityAction action)
        {
            GameObject btnObj = new GameObject(name);
            btnObj.transform.SetParent(parent, false);
            
            RectTransform rect = btnObj.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(200, 50);
            rect.anchoredPosition = pos;

            var img = btnObj.AddComponent<UnityEngine.UI.Image>();
            img.color = new Color(1, 1, 1, 0.1f);

            var btn = btnObj.AddComponent<UnityEngine.UI.Button>();
            btn.targetGraphic = img;
            
            // CRITICAL FIX: Use Persistent Listener so connection survives Play Mode
            UnityEditor.Events.UnityEventTools.AddPersistentListener(btn.onClick, action);

            // Add Audio Script
            btnObj.AddComponent<MenuButtonAudio>();

            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(btnObj.transform, false);
            var text = textObj.AddComponent<TextMeshProUGUI>();
            text.text = label;
            text.fontSize = 24;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.white;

            RectTransform textRect = textObj.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.sizeDelta = Vector2.zero;
        }

        private static void SetupHUD(GameObject player)
        {
            // Ensure Canvas exists
            Canvas canvas = Object.FindAnyObjectByType<Canvas>();
            if (canvas == null)
            {
                GameObject canvasObj = new GameObject("HUD Canvas");
                canvas = canvasObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
                canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();
            }

            // Ensure EventSystem exists and uses the New Input System module
            SetupEventSystem();

            // Create HUD Text if missing
            ScoreHUD hud = canvas.GetComponentInChildren<ScoreHUD>();
            if (hud == null)
            {
                GameObject hudObj = new GameObject("Score HUD");
                hudObj.transform.SetParent(canvas.transform, false);

                var text = hudObj.AddComponent<TextMeshProUGUI>();
                text.text = "Carrots: 0/10";
                text.fontSize = 36;
                text.alignment = TextAlignmentOptions.MidlineLeft;

                // Position top left
                RectTransform rect = hudObj.GetComponent<RectTransform>();
                rect.anchorMin = new Vector2(0, 1);
                rect.anchorMax = new Vector2(0, 1);
                rect.pivot = new Vector2(0, 1);
                rect.anchoredPosition = new Vector2(20, -20);
                rect.sizeDelta = new Vector2(300, 50);

                hud = hudObj.AddComponent<ScoreHUD>();
                hud.scoreText = text;
                hud.scoreTracker = player.GetComponent<ScoreTracker>();
            }

            // Create Controls Display
            SetupControlsUI(canvas.gameObject);
        }

        private static void SetupControlsUI(GameObject canvas)
        {
            if (canvas.transform.Find("Controls Display") != null) return;

            GameObject controlsObj = new GameObject("Controls Display");
            controlsObj.transform.SetParent(canvas.transform, false);

            RectTransform rootRect = controlsObj.AddComponent<RectTransform>();
            rootRect.anchorMin = new Vector2(1, 0);
            rootRect.anchorMax = new Vector2(1, 0);
            rootRect.pivot = new Vector2(1, 0);
            rootRect.anchoredPosition = new Vector2(-30, 30);
            rootRect.sizeDelta = new Vector2(140, 140);

            float keySize = 40f;
            float spacing = 5f;
            float offset = keySize + spacing;

            // WASD Layout
            CreateKeyBox("W", new Vector2(0, offset), new Vector2(keySize, keySize), controlsObj.transform);
            CreateKeyBox("A", new Vector2(-offset, 0), new Vector2(keySize, keySize), controlsObj.transform);
            CreateKeyBox("S", new Vector2(0, 0), new Vector2(keySize, keySize), controlsObj.transform);
            CreateKeyBox("D", new Vector2(offset, 0), new Vector2(keySize, keySize), controlsObj.transform);

            // Space Layout
            CreateKeyBox("SPACE", new Vector2(0, -offset), new Vector2(keySize * 3 + spacing * 2, keySize), controlsObj.transform);
        }

        private static void CreateKeyBox(string key, Vector2 pos, Vector2 size, Transform parent)
        {
            GameObject boxObj = new GameObject("Key_" + key);
            boxObj.transform.SetParent(parent, false);

            RectTransform rect = boxObj.AddComponent<RectTransform>();
            rect.sizeDelta = size;
            rect.anchoredPosition = pos;

            // Background
            var img = boxObj.AddComponent<UnityEngine.UI.Image>();
            img.color = new Color(0, 0, 0, 0.4f); // Elegant semi-transparent black

            // Text
            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(boxObj.transform, false);
            var text = textObj.AddComponent<TextMeshProUGUI>();
            text.text = key;
            text.fontSize = (key == "SPACE") ? 14 : 20;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.white;

            RectTransform textRect = textObj.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.sizeDelta = Vector2.zero;
        }

        private static void AddParameterIfMissing(AnimatorController controller, string name, AnimatorControllerParameterType type)
        {
            foreach (var param in controller.parameters)
            {
                if (param.name == name) return;
            }
            controller.AddParameter(name, type);
        }

        private static void SetupEventSystem()
        {
            var eventSystem = Object.FindAnyObjectByType<UnityEngine.EventSystems.EventSystem>();
            if (eventSystem == null)
            {
                GameObject esObj = new GameObject("EventSystem");
                eventSystem = esObj.AddComponent<UnityEngine.EventSystems.EventSystem>();
            }

            // Check for Old Input Module
            var legacyModule = eventSystem.GetComponent<UnityEngine.EventSystems.StandaloneInputModule>();
            if (legacyModule != null)
            {
                // We desire the new Input System UI Module
                Object.DestroyImmediate(legacyModule);
            }

            // Check for New Input Module
            var newModule = eventSystem.GetComponent<UnityEngine.InputSystem.UI.InputSystemUIInputModule>();
            if (newModule == null)
            {
                newModule = eventSystem.gameObject.AddComponent<UnityEngine.InputSystem.UI.InputSystemUIInputModule>();
                
                // Assign the Default Input Actions if possible
                string actionsPath = "Assets/InputSystem_Actions.inputactions";
                InputActionAsset actionsAsset = AssetDatabase.LoadAssetAtPath<InputActionAsset>(actionsPath);
                if (actionsAsset != null)
                {
                    newModule.actionsAsset = actionsAsset;
                }
            }
        }
    }
}
#endif
