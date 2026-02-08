#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Gemini
{
    public class LightingTools : EditorWindow
    {
        [MenuItem("Tools/Gemini/Fix Lighting (Clear Bakes)")]
        public static void FixLighting()
        {
            // 1. Clear Lightmaps
            Lightmapping.Clear();
            Lightmapping.ClearDiskCache();
            Debug.Log("Gemini: Cleared Lightmaps and Disk Cache.");

            // 2. Disable Baked GI in Lighting Settings (if accessible via API, otherwise warn)
            // Note: Modifying LightingSettings via API can be tricky in older versions, 
            // but we can ensure the main light is Realtime.
            
            Light[] lights = Object.FindObjectsByType<Light>(FindObjectsSortMode.None);
            foreach (Light l in lights)
            {
                if (l.type == LightType.Directional)
                {
                    l.lightmapBakeType = LightmapBakeType.Realtime;
                    Debug.Log($"Gemini: Set Light '{l.name}' to Realtime.");
                }
            }

            // 3. Force Environment Lighting Update
            DynamicGI.UpdateEnvironment();
            
            EditorUtility.DisplayDialog("Lighting Fixed", 
                "Baked data cleared!\n\nIf objects still look dark:\n1. Go to Window > Rendering > Lighting\n2. Click 'Generate Lighting' once while in Realtime mode.", 
                "OK");
        }
    }
}
#endif
