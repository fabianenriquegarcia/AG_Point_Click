using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private PlayerController _playerController;

    [Header("Layer")]
    [SerializeField] private LayerMask _walkableLayer;


    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            HandleLeftClick();
        }
    }

    private void HandleLeftClick()
    {
        Vector2 destination = GetMouseWorldPosition();

        Debug.Log($"[PlayerInput]{destination}");

        if (CanWalk(destination))
        {
            _playerController.SetDestination(destination);
        }
    }

    private Vector2 GetMouseWorldPosition()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

        Vector2 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        return mouseWorldPosition;
    }

    private bool CanWalk(Vector2 point)
    {
        return Physics2D.OverlapPoint(point, _walkableLayer) != null; ;
    }
}
