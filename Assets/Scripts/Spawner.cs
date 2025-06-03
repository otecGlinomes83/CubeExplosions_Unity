using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _ñubePrefab;
    [SerializeField] private Cube _initialCube;

    [SerializeField] private float _explosionForce = 500f;
    [SerializeField] private float _explosionRadius = 5f;

    private List<Cube> _activeCubes = new List<Cube>();

    private int _maxSpawnCount = 6;
    private int _minSpawnCount = 2;
    private float _scaleMultiplier = 2f;
    private float _chanceMultiplier = 2f;

    private float _minColorValue = 0f;
    private float _maxColorValue = 1f;

    private void OnEnable()
    {
        SubscribeToCube(_initialCube);
    }

    private void OnDisable()
    {
        foreach (Cube cube in _activeCubes)
        {
            cube.Divided -= Spawn;
        }

        _activeCubes.Clear();
    }

    private void SubscribeToCube(Cube cube)
    {
        cube.Divided += Spawn;
        _activeCubes.Add(cube);
    }

    private void Spawn(Cube cube)
    {
        int spawnCount = Random.Range(_minSpawnCount, _maxSpawnCount + 1);

        for (int i = 0; i < spawnCount; i++)
        {
            Cube currentCube = Instantiate(_ñubePrefab, cube.transform.position, Quaternion.identity);

            currentCube.Initialize(cube.CurrentDivideChance / _chanceMultiplier, cube.transform.localScale / _scaleMultiplier, GetRandomColor());

            SubscribeToCube(currentCube);

            currentCube.Rigidbody.AddExplosionForce(_explosionForce, cube.transform.position, _explosionRadius);
        }
    }

    private Color GetRandomColor() =>
                new Color
                (
                 Random.Range(_minColorValue, _maxColorValue),
                 Random.Range(_minColorValue, _maxColorValue),
                 Random.Range(_minColorValue, _maxColorValue)
                );
}