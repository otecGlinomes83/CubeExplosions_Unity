using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private float _minColorValue = 0f;
    private float _maxColorValue = 1f;

    private void OnEnable()
    {
        _spawner.CubeSpawned += ChangeColor;
    }

    private void OnDisable()
    {
        _spawner.CubeSpawned -= ChangeColor;
    }

    public void ChangeColor(Cube cube)
    {
        cube.ChangeColor(GetRandomColor());
    }

    private Color GetRandomColor() =>
            new Color
            (
             Random.Range(_minColorValue, _maxColorValue),
             Random.Range(_minColorValue, _maxColorValue),
             Random.Range(_minColorValue, _maxColorValue)
            );
}
