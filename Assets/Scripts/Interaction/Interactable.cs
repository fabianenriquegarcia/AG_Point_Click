using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;

    public Transform interactionPoint => _interactionPoint;

    public virtual void interact()
    {
        Debug.Log($"[Interactable] Interactuando con {gameObject.name}");
    }
}
