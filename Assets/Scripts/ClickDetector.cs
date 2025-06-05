using Assets.Scripts;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _rayLength;

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
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable == null)
            {
                return;
            }

            interactable.Interact();
        }
    }
}
