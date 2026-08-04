using UnityEngine;

[CreateAssetMenu(fileName = "NuevoItem", menuName = "Aventura/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    [TextArea(2, 4)]
    public string description;
}
