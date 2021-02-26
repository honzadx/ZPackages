using UnityEditor;
using UnityEngine;

namespace zhdx
{
    namespace Grid2D
    {
        [RequireComponent(typeof(Grid2DMono))]
        public class Grid2DVisualizer : MonoBehaviour
        {
            public Color textColor;
            public Color gridColor;

            private Grid2DMono grid;

            public void Awake()
            {
                grid = GetComponent<Grid2DMono>();
            }

#if UNITY_EDITOR
            private void OnDrawGizmos()
            {
                if (grid == null)
                {
                    grid = GetComponent<Grid2DMono>();
                }
                var cellSize = grid.cellSize;
                for (int x = 0; x < grid.width; ++x)
                {
                    for (int y = 0; y < grid.height; ++y)
                    {
                        var pos = new Vector3(x, y, 0) * cellSize + transform.position;
                        var value = grid.Grid.GetValue(x, y);

                        Gizmos.color = gridColor;
                        // Draw top
                        Gizmos.DrawLine(pos + new Vector3(0, 0, 0), pos + new Vector3(cellSize, 0, 0));
                        // Draw bottom
                        Gizmos.DrawLine(pos + new Vector3(0, cellSize, 0), pos + new Vector3(cellSize, cellSize, 0));
                        // Draw left
                        Gizmos.DrawLine(pos + new Vector3(0, 0, 0), pos + new Vector3(0, cellSize, 0));
                        // Draw right
                        Gizmos.DrawLine(pos + new Vector3(cellSize, 0, 0), pos + new Vector3(cellSize, cellSize, 0));

                        GUI.color = textColor;
                        Handles.Label(pos - new Vector3(-(cellSize * 0.1f), -cellSize + (cellSize * 0.1f), 0), value.ToString());
                    }
                }
            }
#endif
        }
    }
}