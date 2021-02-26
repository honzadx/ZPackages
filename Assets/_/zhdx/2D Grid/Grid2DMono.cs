using UnityEngine;

namespace zhdx
{
    namespace Grid2D
    {
        public class Grid2DMono : MonoBehaviour
        {
            public int width = 1;
            public int height = 1;
            public float cellSize = 1;

            private Grid2D grid;
            public Grid2D Grid => (grid == null) ? CreateGrid() : grid;

            public void Awake()
            {
                CreateGrid();
            }

            public Grid2D CreateGrid()
            {
                grid = new Grid2D(width, height, cellSize);
                return grid;
            }
        }
    }
}