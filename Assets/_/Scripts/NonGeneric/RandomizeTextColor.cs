using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomizeTextColor : MonoBehaviour
{
    [SerializeField]
    private List<Color> colors = null;

    private TextMeshPro textMeshPro;

    public void Start()
    {
        textMeshPro = GetComponent<TextMeshPro>();
    }

    public void Randomize()
    {
        textMeshPro.color = colors[Random.Range(0, colors.Count)];
    }
}
