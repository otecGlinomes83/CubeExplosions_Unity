using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 500f;
    [SerializeField] private float _explosionRadius = 5f;

    public void Explode(Cube createdCube, Vector3 explosionPosition)
    {
        createdCube.Rigidbody.AddExplosionForce(_explosionForce,explosionPosition, _explosionRadius);
    }
}
