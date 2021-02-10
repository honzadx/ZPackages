using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace zhdx.General
{
    public class FollowPath : MonoBehaviour
    {
        public Transform follower;

        public List<Transform> waypoints;
        public int index;

        public FollowSettingsSO followPositionSettings;
        public FollowSettingsSO followRotationSettings;
        public void SetFollowSettings(FollowSettingsSO newSettings) => followPositionSettings = newSettings;
        public float extraMtp = 1;
        public void SetMtp(float newExtraMtp) => extraMtp = newExtraMtp;

        [Range(0.01f, 5)]
        public float positionThreshold;

        private void Update()
        {
            var target = waypoints[index];
            followPositionSettings?.FollowPosition(Time.deltaTime, follower, target, extraMtp);
            followRotationSettings?.FollowRotation(Time.deltaTime, follower, target, extraMtp);
            if (Vector3.Distance(follower.position, target.position) < positionThreshold)
            {
                index = (index + 1 >= waypoints.Count) ? 0 : index + 1;
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (Selection.Contains(gameObject))
                for (int i = 0; i < waypoints.Count; i++)
                {
                    Vector2 waypoint = waypoints[i].position;

                    Gizmos.color = i == index ? new Color(0, 1, 1, 0.8f) : new Color(0, 1, 1, 0.5f);
                    Gizmos.DrawSphere(waypoint, .25f);
                    Gizmos.DrawWireSphere(waypoint, .3f);
                }
        }
#endif
    }
}
