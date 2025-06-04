using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    [SerializeField] private float _explosionForce = 500f;
    [SerializeField] private float _explosionRadius = 5f;

    private void OnEnable()
    {
        _spawner.CubeSpawned += Explode;
    }

    private void OnDisable()
    {
        _spawner.CubeSpawned -= Explode;
    }

    public void Explode(Cube createdCube)
    {
        createdCube.Rigidbody.AddExplosionForce(_explosionForce, createdCube.transform.position, _explosionRadius);
    }
}
