using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx
{
    namespace General
    {
        [RequireComponent(typeof(SpriteRenderer))]
        public class SpriteFadeInOut : MonoBehaviour
        {
            [SerializeField]
            private float maxA = 1;
            [SerializeField]
            private float duration = 1;

            private SpriteRenderer spriteRenderer;
            private Color baseColor;

            public void OnEnable()
            {
                if (spriteRenderer == null)
                {
                    spriteRenderer = GetComponent<SpriteRenderer>();
                    baseColor = spriteRenderer.color;
                }

                StartFadeIn();
            }

            public void StartFadeIn()
            {
                StopAllCoroutines();
                StartCoroutine(FadeIn(duration));
            }

            public void StartFadeOut()
            {
                StopAllCoroutines();
                StartCoroutine(FadeOut(duration));
            }

            private IEnumerator FadeIn(float duration)
            {
                var timer = .0f;
                while (timer < duration)
                {
                    spriteRenderer.color = new Color(baseColor.r, baseColor.g, baseColor.b, Mathf.Clamp((timer / duration) * maxA, 0, maxA));
                    timer += Time.deltaTime;
                    yield return null;
                }
                spriteRenderer.color = new Color(baseColor.r, baseColor.g, baseColor.b, maxA);
            }

            private IEnumerator FadeOut(float duration)
            {
                var timer = duration;
                while (timer > 0)
                {
                    spriteRenderer.color = new Color(baseColor.r, baseColor.g, baseColor.b, Mathf.Clamp((timer / duration) * maxA, 0, maxA));
                    timer -= Time.deltaTime;
                    yield return null;
                }
                spriteRenderer.color = new Color(baseColor.r, baseColor.g, baseColor.b, 0);
                gameObject.SetActive(false);
            }
        }

    }
}