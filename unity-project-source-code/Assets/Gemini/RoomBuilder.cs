using UnityEngine;

namespace Gemini
{
    /// <summary>
    /// Attach this script to a GameObject in your scene and click 'Build Room' from the 
    /// component's context menu (the three vertical dots or right-click) or use the 
    /// Inspector button to generate a room around the object.
    /// </summary>
    public class RoomBuilder : MonoBehaviour
    {
        [Header("Room Settings")]
        public float width = 10f;
        public float depth = 10f;
        public float height = 4f;
        public float wallThickness = 0.2f;

        [Header("Materials (Optional)")]
        public Material floorMaterial;
        public Material wallMaterial;

        /// <summary>
        /// Generates the room walls and floor as children of this GameObject.
        /// </summary>
        [ContextMenu("Build Room")]
        public void BuildRoom()
        {
            // Clear existing children to allow rebuilding
            ClearRoom();

            // Create Floor
            GameObject floor = CreatePart("Floor", new Vector3(0, -wallThickness / 2, 0), new Vector3(width, wallThickness, depth));
            if (floorMaterial != null) floor.GetComponent<Renderer>().material = floorMaterial;

            // Create Walls
            float halfWidth = width / 2;
            float halfDepth = depth / 2;
            float halfHeight = height / 2;

            // North
            GameObject wallN = CreatePart("Wall_North", new Vector3(0, halfHeight, halfDepth), new Vector3(width, height, wallThickness));
            // South
            GameObject wallS = CreatePart("Wall_South", new Vector3(0, halfHeight, -halfDepth), new Vector3(width, height, wallThickness));
            // East
            GameObject wallE = CreatePart("Wall_East", new Vector3(halfWidth, halfHeight, 0), new Vector3(wallThickness, height, depth));
            // West
            GameObject wallW = CreatePart("Wall_West", new Vector3(-halfWidth, halfHeight, 0), new Vector3(wallThickness, height, depth));

            if (wallMaterial != null)
            {
                wallN.GetComponent<Renderer>().material = wallMaterial;
                wallS.GetComponent<Renderer>().material = wallMaterial;
                wallE.GetComponent<Renderer>().material = wallMaterial;
                wallW.GetComponent<Renderer>().material = wallMaterial;
            }

            Debug.Log("Room built successfully around " + gameObject.name);
        }

        [ContextMenu("Clear Room")]
        public void ClearRoom()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        private GameObject CreatePart(string partName, Vector3 localPos, Vector3 scale)
        {
            GameObject part = GameObject.CreatePrimitive(PrimitiveType.Cube);
            part.name = partName;
            part.transform.SetParent(this.transform);
            part.transform.localPosition = localPos;
            part.transform.localScale = scale;
            return part;
        }
    }
}
