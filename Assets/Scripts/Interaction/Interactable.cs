using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private string _examineText = "No veo nada especial Watson jaja.";


    public Transform interactionPoint => _interactionPoint;
    public string examineText => _examineText;

    public virtual void Interact()
    {
        Debug.Log($"[Interactable] Interactuando con {gameObject.name}");
    }

    public virtual void Examine()
    {
        ThoughtBubble.Instance?.Show(_examineText);
    }
}
