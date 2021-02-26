using UnityEngine;

namespace zhdx
{
    namespace Grid2D
    {
        public class Grid2D
        {
            private int[,] grid;

            private int width;
            public int Width => width;
            private int height;
            public int Height => height;
            private float cellSize;
            public float CellSize => cellSize;

            public Grid2D(int width, int height, float cellSize)
            {
                grid = new int[width, height];

                this.width = width;
                this.height = height;
                this.cellSize = cellSize;
            }

            public int GetValue(int x, int y)
            {
                if (x < 0 || x >= width || y < 0 || y >= height)
                    return 0;

                return grid[x, y];
            }

            public void SetValue(int x, int y, int value)
            {
                if (x < 0 || x >= width || y < 0 || y >= height)
                    return;

                grid[x, y] = value;
            }

            public void GetOriginPosition(int x, int y, Transform local, out float xPos, out float yPos, out float zPos)
            {
                xPos = local.position.x + (x * cellSize);
                yPos = local.position.y + (y * cellSize);
                zPos = local.position.z;
            }

            public void GetXY(Vector3 worldPos, Transform local, out int x, out int y)
            {
                var offsetPos = worldPos - local.position;
                x = Mathf.FloorToInt(offsetPos.x / cellSize);
                y = Mathf.FloorToInt(offsetPos.y / cellSize);
            }
        }
    }
}