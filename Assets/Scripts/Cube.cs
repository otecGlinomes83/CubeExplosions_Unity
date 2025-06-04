using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]

public class Cube : MonoBehaviour
{
    public event Action<Cube> Divided;

    private MeshRenderer _renderer;

    public float CurrentDivideChance { get; private set; } = 100f;
    public Rigidbody Rigidbody { get; private set; }

    public void Initialize(float currentDivideChance, Vector3 scale, Color color)
    {
        CurrentDivideChance = currentDivideChance;
        transform.localScale = scale;
        _renderer.material.color = color;
    }

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        Divide();
    }

    private void Divide()
    {
        float maxChance = 100f;
        float minChance = 0f;

        float chance = UnityEngine.Random.Range(minChance, maxChance);

        if (chance >= CurrentDivideChance)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector3 position = transform.position;
            Vector3 scale = transform.localScale;

            Divided?.Invoke(this);

            Destroy(gameObject);
        }
    }
}