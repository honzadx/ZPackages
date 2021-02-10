using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using zhdx.General;

public class TimerTimeoutAnimation : MonoBehaviour
{
    private Timeout timeout;
    private TextMeshPro textMeshPro;

    private void OnEnable()
    {
        timeout = GetComponent<Timeout>();
        textMeshPro = GetComponentInChildren<TextMeshPro>();
    }

    private void Update()
    {
        textMeshPro.text = "" + timeout.GetTimer();
    }
}
