using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using zhdx.General;

public class PeriodicDamage : MonoBehaviour
{
    [SerializeField]
    private float amount = 1;
    [SerializeField]
    private float waitForSeconds = 1;

    private Health health;

    private Coroutine coroutine;

    private void OnEnable()
    {
        health = GetComponent<Health>();
        coroutine = StartCoroutine(Damage());
    }

    private void OnDisable()
    {
        StopCoroutine(coroutine);
    }

    public IEnumerator Damage()
    {
        while(true)
        {
            yield return new WaitForSeconds(waitForSeconds);
            health.Damage(amount);
        }
    }
}
