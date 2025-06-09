using System;
using UnityEngine;

internal class Divider : MonoBehaviour
{
    [SerializeField] private ClickDetector _clickDetector;

    public event Action<Cube> CubeDivided;

    private Exploder _exploder = new Exploder();

    private void OnEnable()
    {
        _clickDetector.CubeClicked += TryDivide;
    }

    private void OnDisable()
    {
        _clickDetector.CubeClicked -= TryDivide;
    }

    public void TryDivide(Cube cube)
    {
        float maxChance = 100f;
        float minChance = 0f;

        float divideChance = UnityEngine.Random.Range(minChance, maxChance);

        if (divideChance <= cube.CurrentDivideChance)
        {
            Vector3 position = cube.transform.position;
            Vector3 scale = cube.transform.localScale;

            CubeDivided?.Invoke(cube);
        }
        else
        {
            _exploder.ExplodeAllCubes(cube);
        }

        Destroy(cube.gameObject);
    }
}

