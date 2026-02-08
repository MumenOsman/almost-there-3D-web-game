#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class RoomGenerator : EditorWindow
{
    [MenuItem("Tools/Gemini/Create Room")]
    public static void CreateRoom()
    {
        // Check if a room already exists to avoid duplicates or prompt
        GameObject existingRoom = GameObject.Find("Generated Room");
        if (existingRoom != null)
        {
            if (!EditorUtility.DisplayDialog("Room Exists", "A 'Generated Room' already exists in the scene. Do you want to create another one?", "Yes", "No"))
            {
                return;
            }
        }

        GameObject roomParent = new GameObject("Generated Room");
        
        // Define room dimensions
        float width = 10f;
        float depth = 10f;
        float height = 3f;
        float thickness = 0.1f;

        // Create Floor
        CreateObject("Floor", new Vector3(0, -thickness/2, 0), new Vector3(width, thickness, depth), roomParent.transform);
        
        // Create Walls
        // North Wall
        CreateObject("Wall North", new Vector3(0, height/2, depth/2), new Vector3(width, height, thickness), roomParent.transform);
        // South Wall
        CreateObject("Wall South", new Vector3(0, height/2, -depth/2), new Vector3(width, height, thickness), roomParent.transform);
        // East Wall
        CreateObject("Wall East", new Vector3(width/2, height/2, 0), new Vector3(thickness, height, depth), roomParent.transform);
        // West Wall
        CreateObject("Wall West", new Vector3(-width/2, height/2, 0), new Vector3(thickness, height, depth), roomParent.transform);

        // Undo support
        Undo.RegisterCreatedObjectUndo(roomParent, "Create Room");

        // Select the new room in the hierarchy
        Selection.activeGameObject = roomParent;
        
        // Focus scene view on the new room
        if (SceneView.lastActiveSceneView != null)
        {
            SceneView.lastActiveSceneView.FrameSelected();
        }

        Debug.Log("Room created successfully via Gemini Tools!");
    }

    private static void CreateObject(string name, Vector3 pos, Vector3 scale, Transform parent)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.name = name;
        obj.transform.parent = parent;
        obj.transform.localPosition = pos;
        obj.transform.localScale = scale;

        // Optional: Set a default tag or layer if needed
    }
}
#endif
