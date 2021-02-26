using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx
{
    namespace General
    {
        public class InteractionRaycast : MonoBehaviour
        {
            [SerializeField]
            private Camera _camera = null;
            [SerializeField]
            private float distance = 0;
            [SerializeField]
            private LayerMask layerMask = 0;

            private void OnEnable()
            {
                if (_camera == null)
                {
                    _camera = Camera.main;
                }
            }

            private void Update()
            {
                if (Input.GetMouseButtonDown(0))
                {
                    CastRay(GetMouseRay());
                }

                if (Input.touchCount > 0)
                {
                    foreach (Ray touchRay in GetTouchRays())
                    {
                        CastRay(touchRay);
                    }
                }

            }

            private void CastRay(Ray ray)
            {
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, distance, layerMask))
                {
                    ActionEvent ae = hit.collider.GetComponent<ActionEvent>();
                    if (ae != null)
                    {
                        Debug.Log($"'{ae.gameObject.name}': ActionEvent called via Raycast hit from '{gameObject.name}'!");
                        ae.action?.Invoke();
                    }
                }
            }

            private Ray GetMouseRay()
            {
                return _camera.ScreenPointToRay(Input.mousePosition);
            }

            private List<Ray> GetTouchRays()
            {
                List<Ray> rays = new List<Ray>();
                for (int i = 0; i < Input.touchCount; i++)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        rays.Add(_camera.ScreenPointToRay(Input.GetTouch(i).position));
                    }
                }
                return rays;
            }
        }
    }
}
