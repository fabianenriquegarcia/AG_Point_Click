using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody2D;

    [SerializeField] private float _moveSpeed = 2f;

    [SerializeField] private float _stopDistance = 0.05f;

    private Vector2 _destination;

    private bool _isMoving;

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            Move();
        }
    }

    public void SetDestination(Vector2 destination)
    {
        _destination = destination;
        _isMoving = true;
    }

    public void Move()
    {
        Vector2 currentPosition = _rigidBody2D.position;

        float distance = Vector2.Distance(currentPosition, _destination);

        if (distance < _stopDistance)
        {
            Stop();
            return;
        }

        Vector2 direction = (_destination - currentPosition).normalized;

        Vector2 newPosition = currentPosition + direction * _moveSpeed * Time.deltaTime;

        _rigidBody2D.MovePosition(newPosition);
    }

    public void Stop()
    {
        _isMoving = false;
        _rigidBody2D.MovePosition(_destination);
    }
}
