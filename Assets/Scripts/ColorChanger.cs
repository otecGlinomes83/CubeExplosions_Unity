using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private float _minColorValue = 0f;
    private float _maxColorValue = 1f;

    public Color GenerateRandomColor() =>
            new Color
            (
             Random.Range(_minColorValue, _maxColorValue),
             Random.Range(_minColorValue, _maxColorValue),
             Random.Range(_minColorValue, _maxColorValue)
            );
}
