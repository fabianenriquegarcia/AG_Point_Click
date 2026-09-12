using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [Header("Iconos (arrastrar las Images)")]
    [SerializeField] private GameObject _cursorMain;   // Patitas
    [SerializeField] private GameObject _cursorLeft;   // Engranaje
    [SerializeField] private GameObject _cursorRight;  // Ojo
    [SerializeField] private GameObject _cursorArrow;  // Flecha

    [Header("Offsets respecto a las patitas (píxeles)")]
    [SerializeField] private Vector2 _offsetLeft = new Vector2(-24f, 24f);
    [SerializeField] private Vector2 _offsetRight = new Vector2(24f, 24f);

    [Header("Layers")]
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private LayerMask _transitionLayer;

    private Camera _mainCamera;
    private RectTransform _rectTransform;

    private void Start()
    {
        Cursor.visible = false;               // Ocultamos el mouse de Windows
        Cursor.lockState = CursorLockMode.Confined;
        _mainCamera = Camera.main;
        _rectTransform = GetComponent<RectTransform>();

        // Solo patitas al inicio
        SetState(true, false, false, false);
    }

    private void Update()
    {
        // 1. El cursor sigue al mouse exacto
        _rectTransform.position = Input.mousePosition;

        // 2. Raycast en el mundo para ver qué hay debajo
        Vector2 worldPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hitInteractable = Physics2D.OverlapPoint(worldPos, _interactableLayer);
        Collider2D hitTransition = Physics2D.OverlapPoint(worldPos, _transitionLayer);

        bool isInteractable = hitInteractable != null;
        bool isTransition = hitTransition   != null;

        // 3. Decidir qué mostrar
        if (isTransition)
        {
            // Flecha reemplaza a las patitas (o anda junto, como prefieras)
            SetState(false, false, false, true);
        }
        else if (isInteractable)
        {
            // Patitas + engranaje arriba-izq + ojo arriba-der
            SetState(true, true, true, false);
            PositionIcons();
        }
        else
        {
            // Solo patitas
            SetState(true, false, false, false);
        }
    }

    private void SetState(bool main, bool left, bool right, bool arrow)
    {
        _cursorMain.SetActive(main);
        _cursorLeft.SetActive(left);
        _cursorRight.SetActive(right);
        _cursorArrow.SetActive(arrow);
    }

    private void PositionIcons()
    {
        // Movemos los iconos locales respecto al centro de las patitas
        _cursorLeft.GetComponent<RectTransform>().anchoredPosition  = _offsetLeft;
        _cursorRight.GetComponent<RectTransform>().anchoredPosition = _offsetRight;
    }
}