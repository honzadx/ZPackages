using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace zhdx
{
    namespace General
    {
        public class Timeout : MonoBehaviour
        {
            public enum TimeoutType
            {
                DISABLE, DESTROY, HEALTH, CUSTOM_TIMEOUT_EVENT
            }

            public enum TimeoutStart
            {
                ON_ENABLE, CALL
            }

            [SerializeField]
            private TimeoutType type = TimeoutType.CUSTOM_TIMEOUT_EVENT;
            public void SetType(TimeoutType type) => this.type = type;
            [SerializeField]
            private TimeoutStart start = TimeoutStart.CALL;
            public void SetStart(TimeoutStart start) => this.start = start;
            [SerializeField]
            private float time = 0;
            public void SetTime(float time) => this.time = time;
            public float GetTime() => time;

            private float timer;
            public float GetTimer() => timer;
            private bool ticking;
            public bool IsTicking() => ticking;

            public UnityEvent timeout;

            public Timeout Initialize(TimeoutType type, TimeoutStart start, float time)
            {
                this.type = type;
                this.start = start;
                this.time = time;

                return this;
            }

            private void OnEnable()
            {
                if (start == TimeoutStart.ON_ENABLE)
                {
                    StartTimeout();
                }
            }

            public void StartTimeout()
            {
                timer = time;
                ticking = true;
            }

            private void Update()
            {
                if (!ticking)
                    return;

                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timeout?.Invoke();
                    switch (type)
                    {
                        case TimeoutType.DESTROY:
                            Destroy(gameObject);
                            break;
                        case TimeoutType.DISABLE:
                            gameObject.SetActive(false);
                            break;
                        case TimeoutType.HEALTH:
                            gameObject.GetComponent<Health>().Kill();
                            break;
                    }
                }
            }
        }
    }
}
