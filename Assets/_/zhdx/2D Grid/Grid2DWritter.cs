using UnityEngine;

namespace zhdx
{
    namespace Grid2D
    {
        [RequireComponent(typeof(Grid2DMono))]
        public class Grid2DWritter : MonoBehaviour
        {
            public Grid2DMono grid;
            public Grid2DMono Grid => grid == null ? grid = GetComponent<Grid2DMono>() : grid;

            public bool raycastEdit;
            public int raycastValue;

            private void Start()
            {
                grid = GetComponent<Grid2DMono>();
            }

            private void Update()
            {
                if (raycastEdit)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        CastRay(raycastValue);
                    }
                    else if (Input.GetMouseButtonDown(1))
                    {
                        CastRay(0);
                    }
                }
            }

            public void Set(int x, int y, int value)
            {
                Grid.Grid.SetValue(x, y, value);
            }

            public void Clear(int x, int y)
            {
                Grid.Grid.SetValue(x, y, 0);
            }

            public void ClearAll()
            {
                Grid.CreateGrid();
            }

            public void CastRay(int value)
            {
                var mousePos = Input.mousePosition;
                mousePos.z = 10.0f;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                int x;
                int y;

                Grid.Grid.GetXY(mousePos, transform, out x, out y);

                Grid.Grid.SetValue(x, y, value);
            }
        }
    }
}
