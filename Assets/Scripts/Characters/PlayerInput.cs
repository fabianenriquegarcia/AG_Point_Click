using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private PlayerController _playerController;

    [Header("Layers")]
    [SerializeField] private LayerMask _walkableLayer;
    [SerializeField] private LayerMask _interactableLayer;

    private Interactable _pendingInteractable;

    private void Start()
    {
        _playerController.OnArrived += HandleArrived;
    }
    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            HandleLeftClick();
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            HandleRightClick();
        }
    }

    private void HandleLeftClick()
    {
        Vector2 worldPoint = GetMouseWorldPosition();

        //PRIORIDAD 1: ¿Hay un interactable abajo del mouse?
        Collider2D hit = Physics2D.OverlapPoint(worldPoint, _interactableLayer);

        if (hit != null)
        {
            Interactable interactable = hit.GetComponent<Interactable>();
            if (interactable != null && interactable.interactionPoint != null)
            {
                //Guardamos para interactuar al llegar
                _pendingInteractable = interactable;

                _playerController.SetDestination(interactable.interactionPoint.position);
                return;
            }
        }

        // PRIORIDAD 2 :
        if (Physics2D.OverlapPoint(worldPoint, _walkableLayer))
        {
            _pendingInteractable = null; // Cancelamos interacción previa 
            _playerController.SetDestination(worldPoint);
        }

        //Debug.Log($"[PlayerInput]{wordlPoint}");

        //if (CanWalk(wordlPoint))
        //{
        //    _playerController.SetDestination(wordlPoint);
        //}
    }

    private void HandleRightClick()
    {
        Vector2 worldPoint = GetMouseWorldPosition();
        Collider2D hit = Physics2D.OverlapPoint(worldPoint, _interactableLayer);

        if (hit != null)
        {
            Interactable interactable = hit.GetComponent<Interactable>();
            interactable?.Examine(); //sólo miramos, no caminamos
        }
    }

    // Este método se ejecuta automáticamente cuando PlayerController llega
    private void HandleArrived()
    {
        if (_pendingInteractable != null)
        {
            _pendingInteractable.Interact();
            _pendingInteractable = null;
        }
    }

    private Vector2 GetMouseWorldPosition()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

        //Vector2 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        return _mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }

 

    private void OnDestroy()
    {
        _playerController.OnArrived -= HandleArrived;
    }
}
