using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Image[] _slots = new Image[4];
    [SerializeField] private Sprite _emptyIcon;

    private void Start()
    {
        InventoryManager.Instance.OnInventoryChanged += Refresh;
        Refresh();
    }

    private void OnDestroy()
    {
        if (InventoryManager.Instance != null)
            InventoryManager.Instance.OnInventoryChanged -= Refresh;
    }

    private void Refresh()
    {
        var items = InventoryManager.Instance.GetItems();

        for (int i = 0; i < _slots.Length; i++)
        {
            if (i < items.Count)
            {
                _slots[i].sprite = items[i].icon;
                _slots[i].color = Color.white;
            }
            else
            {
                _slots[i].sprite = _emptyIcon;
                _slots[i].color = new Color(1, 1, 1, 0.3f);
            }
        }
    }
}
