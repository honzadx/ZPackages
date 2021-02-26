using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowView : MonoBehaviour
{
    [SerializeField]
    private float duration = 1;
    [SerializeField]
    private float sleep = 1;
    [SerializeField]
    private AnimationCurve curve = null;
    
    private float timer;

    private RectTransform rectTransform;

    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();

        StartCoroutine(ViewAnimation());
    }

    private IEnumerator ViewAnimation()
    {
        timer = 0;
        rectTransform.localScale = new Vector3(0, 1, 1);
        yield return new WaitForSeconds(sleep);
        while (timer < duration)
        {
            timer += Time.deltaTime;
            rectTransform.localScale = new Vector3(curve.Evaluate(Mathf.Clamp(timer/duration,0, 1)), 1, 1);
            yield return null;
        }
        rectTransform.localScale = new Vector3(1, 1, 1);
    }
}
