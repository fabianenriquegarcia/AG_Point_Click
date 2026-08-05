using UnityEngine;

public class Pickable : Interactable
{
    [SerializeField] private ItemSO _itemData;

    public override void Interact()
    {
        if (_itemData == null) return;

        bool added = InventoryManager.Instance.AddItem(_itemData);

        if (added)
        {
            Destroy(gameObject); // desaparece de la escena
        }
        else
        {
            ThoughtBubble.Instance?.Show("No puedo cargar más cosas");
        }
    }
}
