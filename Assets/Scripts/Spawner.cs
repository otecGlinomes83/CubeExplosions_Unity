using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Cube _cubePrefab;

    [SerializeField] private Divider _divider;

    [SerializeField, Min(2)] private int _initCubesCount = 2;

    private ColorChanger _colorChanger = new ColorChanger();
    private Exploder _exploder = new Exploder();

    private int _maxSpawnCount = 6;
    private int _minSpawnCount = 2;

    private float _scaleDivider = 2f;
    private float _chanceDivider = 2f;

    private void OnEnable()
    {
        _divider.CubeDivided += Spawn;
    }

    private void OnDisable()
    {
        _divider.CubeDivided -= Spawn;
    }

    private void Start()
    {
        for (int i = 0; i < _initCubesCount; i++)
        {
            Instantiate(_cubePrefab, _spawnPoint);
        }
    }

    private void Spawn(Cube parentCube)
    {
        int spawnCount = Random.Range(_minSpawnCount, _maxSpawnCount + 1);

        Vector3 parentPosition = parentCube.transform.position;
        Vector3 parentScale = parentCube.transform.localScale;

        float newDivideChance = parentCube.CurrentDivideChance / _chanceDivider;
        Vector3 newScale = parentCube.transform.localScale / _scaleDivider;


        for (int i = 0; i < spawnCount; i++)
        {
            Cube createdCube = Instantiate(_cubePrefab, parentPosition, Quaternion.identity);

            createdCube.Initialize(newDivideChance, newScale);

            _colorChanger.ChangeColor(createdCube.Renderer);
            _exploder.ExplodeChildCubes(createdCube, parentPosition);
        }
    }
}