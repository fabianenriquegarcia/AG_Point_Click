using UnityEngine;

public class TrafficLight : Interactable
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _colorRed = Color.red;
    [SerializeField] private Color _colorGreen = Color.green;

    private bool _isGreen = false;

    public override void interact()
    {
        base.interact();

        _isGreen = !_isGreen;
        _spriteRenderer.color = _isGreen ? _colorGreen : _colorRed;

        Debug.Log($"Semáforo ahora está: {(_isGreen ? "VERDE" : "ROJO")}");
    }
}