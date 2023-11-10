using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomColorGenerator : MonoBehaviour
{
    [SerializeField]private Image _image;
    [SerializeField] private bool randomizeColor;
    [SerializeField] private float randomizeFrequency = 2f;
    private void Start()
    {
        if (_image == null)
            _image = GetComponent<Image>();

        if (randomizeColor)
            InvokeRepeating(nameof(RandomizeColor), randomizeFrequency, randomizeFrequency);
    }

    private void RandomizeColor()
    {
        _image.color = GetRandomColor();
    }

    private Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}
