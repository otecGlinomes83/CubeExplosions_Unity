using System;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _rayLength;

    public event Action<Cube> CubeClicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _rayLength))
        {
            Cube cube = hit.collider.GetComponent<Cube>();

            if (cube == null)
            {
                return;
            }

            CubeClicked?.Invoke(cube);
        }
    }
}
