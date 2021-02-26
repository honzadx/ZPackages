using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx
{
    namespace General
    {
        public class FlutterCurveMovement : MonoBehaviour
        {
            [System.Serializable]
            public struct Move
            {
                public AnimationCurve curveX, curveY, curveZ;
                public Vector3 x;
                public float duration;
            }

            [SerializeField]
            private List<Move> moves = null;

            [SerializeField]
            private bool onEnable = true;

            private bool initialized;
            private int index = 0;
            private float timer = 0;
            private Vector3 localOrigin;

            private Transform _transform;

            private void Awake()
            {
                _transform = GetComponent<Transform>();
            }

            private void OnEnable()
            {
                if (!initialized)
                    Initialize();
                if (onEnable)
                    Play();
            }

            private void Initialize()
            {
                localOrigin = transform.localPosition;
                initialized = true;
            }

            public void Play()
            {
                StopAllCoroutines();
                StartCoroutine(Animation());
            }

            public void Stop()
            {
                StopAllCoroutines();
                _transform.localPosition = localOrigin;
            }

            private IEnumerator Animation()
            {
                while(moves.Count > 0)
                {
                    var move = moves[index];
                    timer = .0f;

                    while(timer < move.duration)
                    {
                        var percentage = timer / move.duration;
                        _transform.localPosition = localOrigin + new Vector3(move.curveX.Evaluate(percentage) * move.x.x, move.curveY.Evaluate(percentage) * move.x.y, move.curveZ.Evaluate(percentage) * move.x.z);
                        timer += Time.deltaTime;
                        yield return null;
                    }

                    index++;
                    if (index >= moves.Count)
                        index = 0;

                }
            }
        }
    }
}
