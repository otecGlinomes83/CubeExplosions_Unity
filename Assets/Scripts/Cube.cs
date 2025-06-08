using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]

public class Cube : MonoBehaviour
{
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
}