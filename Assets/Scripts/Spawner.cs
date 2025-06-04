using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _ñubePrefab;
    [SerializeField] private Cube _initialCube;

    public event Action<Cube> CubeSpawned;

    private List<Cube> _activeCubes = new List<Cube>();

    private int _maxSpawnCount = 6;
    private int _minSpawnCount = 2;
    private float _scaleDivider = 2f;
    private float _chanceDivider = 2f;

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

    private void Spawn(Cube ParentCube)
    {
        int spawnCount = UnityEngine.Random.Range(_minSpawnCount, _maxSpawnCount + 1);

        for (int i = 0; i < spawnCount; i++)
        {
            Cube currentCube = Instantiate(_ñubePrefab, ParentCube.transform.position, Quaternion.identity);

            currentCube.Initialize(ParentCube.CurrentDivideChance / _chanceDivider, ParentCube.transform.localScale / _scaleDivider);
            SubscribeToCube(currentCube);

            CubeSpawned?.Invoke(currentCube);
        }
    }
}