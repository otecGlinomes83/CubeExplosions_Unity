using System.Collections.Generic;
using UnityEngine;

public class Exploder
{
    private float _defaultExplosionForce = 2000f;
    private float _defaultExplosionRadius = 10f;

    public void Explode(Cube createdCube, Vector3 explosionPosition)
    {
        createdCube.Rigidbody.AddExplosionForce(_defaultExplosionForce, explosionPosition, _defaultExplosionRadius);
    }

    public void Explode(Cube parentCube)
    {
        Vector3 explosionPosition = parentCube.transform.position;

        float ratio = 1 / parentCube.transform.localScale.x;

        float currentRadius = _defaultExplosionRadius * ratio;
        float currentForce = _defaultExplosionForce * ratio;

        List<Rigidbody> explodable = GetExplodableObjects(explosionPosition, currentRadius);

        foreach (Rigidbody explodableObject in explodable)
            explodableObject.AddExplosionForce(currentForce, explosionPosition, currentRadius);

        Debug.Log($"force - {currentForce} radius - {currentRadius} ratio - {ratio}");
    }

    private List<Rigidbody> GetExplodableObjects(Vector3 explosionPosition, float radius)
    {
        Collider[] hits = Physics.OverlapSphere(explosionPosition, _defaultExplosionRadius);

        List<Rigidbody> explodableObjects = new List<Rigidbody>();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
                explodableObjects.Add(hit.attachedRigidbody);
        }

        return explodableObjects;
    }
}