using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField] private Exploder _exploder;

   [SerializeField,Min(2)] private int _initCubesCount = 2;

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
                parentCube.transform.localScale / _scaleDivider,
                _colorChanger.GenerateRandomColor()
                );

            _exploder.Explode(createdCube, parentCube.transform.position);

            SubscribeToCube(createdCube);
        }
    }
}