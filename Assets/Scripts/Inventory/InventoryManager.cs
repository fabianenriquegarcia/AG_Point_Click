using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [SerializeField] private int _maxSlots = 4;
    private List<ItemSO> _items = new List<ItemSO>();

    public event Action OnInventoryChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool AddItem(ItemSO item)
    {
        if (_items.Count >= _maxSlots)
        {
            Debug.Log("Inventario lleno");
            return false;
        }

        _items.Add(item);
        OnInventoryChanged?.Invoke();
        Debug.Log($"[InventoryManager] Recogido: {item.itemName}");
        return true;
    }

    public bool HasItem(ItemSO item) => _items.Contains(item);

    public bool RemoveItem(ItemSO item)
    {
        bool removed = _items.Remove(item);
        if (removed) OnInventoryChanged?.Invoke();
        return removed;
    }

    public List<ItemSO> GetItems() => _items;
}