using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObject : MonoBehaviour
{
    [SerializeField]
    private Vector3 amount = Vector3.one;
    [SerializeField]
    private Vector3 speed = Vector3.one;

    private Vector3 sinPos;
    private Vector3 startingPos;

    private void Awake()
    {
        startingPos = transform.position;
        sinPos = new Vector3(Random.Range(0, Mathf.PI), Random.Range(0, Mathf.PI), Random.Range(0, Mathf.PI));
    }

    private void Update()
    {
        var x = Mathf.Sin(sinPos.x + Time.time * speed.x) * amount.x;
        var y = Mathf.Sin(sinPos.y + Time.time * speed.y) * amount.y;
        var z = Mathf.Sin(sinPos.z + Time.time * speed.z) * amount.z;

        transform.position = startingPos + new Vector3(x, y, z);
    }
}
