using TMPro;
using UnityEngine;
using System.Collections;

public class ThoughtBubble : MonoBehaviour
{
    public static ThoughtBubble Instance { get; private set; }

    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Transform _followTarget; // Arrastrá al personaje
    [SerializeField] private Vector3 _offset = new Vector3(0, 1.2f, 0);

    private Camera _mainCamera;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        _mainCamera = Camera.main;
        _panel.SetActive(false);
    }

    public void Show(string message, float duration = 2.5f)
    {
        Debug.Log("[ThougtBubble] mostrando el mensaje al mirar con click derecho");
        StopAllCoroutines();
        _text.text = message;
        _panel.SetActive(true);
        StartCoroutine(HideAfter(duration));
    }

    private IEnumerator HideAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _panel.SetActive(false);
    }

    private void LateUpdate()
    {
        if (!_panel.activeSelf || _followTarget == null) return;
        Vector3 screenPos = _mainCamera.WorldToScreenPoint(_followTarget.position + _offset);
        _panel.transform.position = screenPos;
    }
}
