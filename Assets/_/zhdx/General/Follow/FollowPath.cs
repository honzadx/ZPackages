using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace zhdx
{
    namespace General
    {
        public class FollowPath : MonoBehaviour
        {
            public Transform follower;

            public List<Transform> waypoints;
            public int index;

            public FollowSettingsSO followPositionSettings;
            public FollowSettingsSO followRotationSettings;
            public void SetFollowSettings(FollowSettingsSO newSettings) => followPositionSettings = newSettings;
            public float positionMtp = 1;
            public float rotationMtp = 1;

            [Range(0.01f, 5)]
            public float positionThreshold;

            private void Update()
            {
                var target = waypoints[index];
                followPositionSettings?.FollowPosition(Time.deltaTime, follower, target, positionMtp);
                followRotationSettings?.FollowRotation(Time.deltaTime, follower, target, rotationMtp);
                if (Vector3.Distance(follower.position, target.position) < positionThreshold)
                {
                    index = (index + 1 >= waypoints.Count) ? 0 : index + 1;
                }
            }

#if UNITY_EDITOR
            private void OnDrawGizmos()
            {
                if (Selection.Contains(gameObject) && waypoints != null)
                    for (int i = 0; i < waypoints.Count; i++)
                    {
                        if (waypoints[i] == null)
                            continue;
                        Vector3 waypoint = waypoints[i].position;

                        Gizmos.color = i == index ? new Color(0, 1, 1, 0.8f) : new Color(0, 1, 1, 0.5f);
                        Gizmos.DrawSphere(waypoint, .25f);
                        Gizmos.DrawWireSphere(waypoint, .3f);
                    }
            }
#endif
        }
    }
}
