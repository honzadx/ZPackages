using UnityEngine;

namespace zhdx
{
    namespace Platformer2D
    {
        public class ParallaxEffect : MonoBehaviour
        {
            [SerializeField]
            private float zOrigin = 0;

            private Camera cam;
            private Vector2 camOrigin;
            private float dirMultiplier;
            private float camDistance;

            private void Start()
            {
                dirMultiplier = zOrigin - transform.position.z;
                cam = Camera.main;
                camOrigin = cam.transform.position;
                camDistance = zOrigin - cam.transform.position.z;
            }
            private void Update()
            {
                var pos = cam.transform.position;
                var mtp = (dirMultiplier / camDistance);

                transform.position = new Vector3((camOrigin.x - pos.x) * mtp, (camOrigin.y - pos.y) * mtp, transform.position.z);
            }
        }
    }
}