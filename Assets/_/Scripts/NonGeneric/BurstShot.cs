using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using zhdx.General;

public class BurstShot : MonoBehaviour
{
    [SerializeField]
    private ObjectPool pool = null;
    [SerializeField]
    private float repeatCooldown = 5;
    [SerializeField]
    private float burstBulletCount = 2;
    [SerializeField]
    private float positionOffset = 1;

    private float timer;

    private void Start()
    {
        ResetCooldown();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            ResetCooldown();
            Shoot();
        }
    }

    private void Shoot()
    {
        for(int i = 0; i < burstBulletCount; i++)
        {
            var offset = new Vector3(Random.Range(-positionOffset, positionOffset), Random.Range(-positionOffset, positionOffset), Random.Range(-positionOffset, positionOffset));
            var item = pool.TakeItem().gameObject;
            item.transform.position = transform.position + offset;
            item.SetActive(true);
        }
    }

    private void ResetCooldown()
    {
        timer = repeatCooldown;
    }
}
