using System;
using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]

public class Cube : MonoBehaviour, IInteractable
{
    public event Action<Cube> Divided;

    public MeshRenderer Renderer { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public float CurrentDivideChance { get; private set; } = 100f;

    public void Initialize(float currentDivideChance, Vector3 scale)
    {
        CurrentDivideChance = currentDivideChance;
        transform.localScale = scale;
    }

    private void Awake()
    {
        Renderer = GetComponent<MeshRenderer>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void Interact()
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