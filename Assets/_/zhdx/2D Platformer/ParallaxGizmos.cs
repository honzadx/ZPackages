using UnityEngine;

namespace zhdx
{
    namespace Platformer2D 
    { 
        [ExecuteInEditMode]
        public class ParallaxGizmos : MonoBehaviour
        {
            private Camera cam;

            private void Start()
            {
                cam = Camera.main;
            }

            private void OnDrawGizmos()
            {
                if (cam == null)
                    return;

                Gizmos.DrawWireCube(new Vector3(cam.transform.position.x, cam.transform.position.y, -cam.transform.position.z), new Vector3(10, 10, 0));
            }
        }
    }
}
