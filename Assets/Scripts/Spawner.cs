using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Cube _cubePrefab;

    [SerializeField, Min(2)] private int _initCubesCount = 2;

    private ColorChanger _colorChanger = new ColorChanger();
    private Exploder _exploder = new Exploder();

    private Divider _divider;
    private ClickDetector _clickDetector;

    private int _maxSpawnCount = 6;
    private int _minSpawnCount = 2;

    private float _scaleDivider = 2f;
    private float _chanceDivider = 2f;

    private void Awake()
    {
        _clickDetector = GetComponent<ClickDetector>();
        _divider = GetComponent<Divider>();
    }

    private void OnEnable()
    {
        _clickDetector.CubeClicked += _divider.Divide;
        _divider.Divided += Spawn;
    }

    private void OnDisable()
    {
        _clickDetector.CubeClicked -= _divider.Divide;
        _divider.Divided -= Spawn;
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
            _exploder.Explode(createdCube, parentPosition);
        }
    }
}