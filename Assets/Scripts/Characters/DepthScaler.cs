using UnityEngine;

public class DepthScaler : MonoBehaviour
{
    [SerializeField] private Transform _depthBottom; // Arrastrá el objeto de la escena
    [SerializeField] private Transform _depthTop;
    [SerializeField] private float _scaleAtBottom = 1.0f;
    [SerializeField] private float _scaleAtTop = 0.5f;

    private float _yBottom;
    private float _yTop;

    private void Start()
    {
        _yBottom = _depthBottom.position.y;
        _yTop = _depthTop.position.y;
    }

    private void LateUpdate()
    {
        float t = Mathf.InverseLerp(_yBottom, _yTop, transform.position.y);
        float scale = Mathf.Lerp(_scaleAtBottom, _scaleAtTop, t);
        transform.localScale = new Vector3(scale, scale, 1f);
    }
}
