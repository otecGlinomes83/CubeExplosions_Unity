using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    [SerializeField, Min(2)] private int _initCubesCount = 2;

    private ColorChanger _colorChanger = new ColorChanger();
    private Exploder _exploder = new Exploder();

    private List<Cube> _activeCubes = new List<Cube>();

    private int _maxSpawnCount = 6;
    private int _minSpawnCount = 2;

    private float _scaleDivider = 2f;
    private float _chanceDivider = 2f;

    private void Start()
    {
        for (int i = 0; i < _initCubesCount; i++)
        {
            SubscribeToCube(Instantiate(_cubePrefab));
        }
    }

    private void OnDisable()
    {
        foreach (Cube cube in _activeCubes)
        {
            cube.Divided -= Spawn;
        }

        _activeCubes.Clear();
    }

    private void SubscribeToCube(Cube createdCube)
    {
        createdCube.Divided += Spawn;
        _activeCubes.Add(createdCube);
    }

    private void Spawn(Cube parentCube)
    {
        int spawnCount = Random.Range(_minSpawnCount, _maxSpawnCount + 1);

        for (int i = 0; i < spawnCount; i++)
        {
            Cube createdCube = Instantiate(_cubePrefab, parentCube.transform.position, Quaternion.identity);

            createdCube.Initialize
                (
                parentCube.CurrentDivideChance / _chanceDivider,
                parentCube.transform.localScale / _scaleDivider
                );

            _colorChanger.ChangeColor(createdCube.Renderer);

            _exploder.Explode(createdCube, parentCube.transform.position);

            SubscribeToCube(createdCube);
        }
    }
}

public class Exploder
{
    private float _explosionForce = 500f;
    private float _explosionRadius = 5f;

    public void Explode(Cube createdCube, Vector3 explosionPosition)
    {
        createdCube.Rigidbody.AddExplosionForce(_explosionForce, explosionPosition, _explosionRadius);
    }
}

public class ColorChanger
{
    private float _minColorValue = 0f;
    private float _maxColorValue = 1f;

    public void ChangeColor(MeshRenderer cubeRenderer)
    {
        cubeRenderer.material.color = GenerateRandomColor();
    }

    private Color GenerateRandomColor() =>
            new Color
            (
             Random.Range(_minColorValue, _maxColorValue),
             Random.Range(_minColorValue, _maxColorValue),
             Random.Range(_minColorValue, _maxColorValue)
            );
}