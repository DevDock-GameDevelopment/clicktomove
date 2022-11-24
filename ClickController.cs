using UnityEngine;

public class ClickController : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;
    [SerializeField] private float maxTimeBetweenClicks = 0.2f;
    [SerializeField] private LayerMask _movementLayerMask;

    private Ray _previousRay;
    private bool _clickedOnce;
    private float _deltaTime;
    private float _lastTimeClicked;

    public delegate void MovementClickDelegate(RaycastHit hit);
    public delegate void MovementDoubleClickDelegate(RaycastHit hit);

    public event MovementClickDelegate OnMovementClick;
    public event MovementDoubleClickDelegate OnMovementDoubleClick;

    private void Update()
    {
        var ray = gameCamera.ScreenPointToRay(Input.mousePosition);

        _deltaTime = Time.time - _lastTimeClicked;

        if (Input.GetMouseButtonDown(0))
        {
            if (!_clickedOnce)
            {
                _clickedOnce = true;
                _lastTimeClicked = Time.time;
                _previousRay = ray;
                return;
            }

            if (_deltaTime < maxTimeBetweenClicks)
            {
                HandleMovementClick(ray, true);
                _lastTimeClicked = Time.time;
                _clickedOnce = false;
            }
        }

        if (_clickedOnce && _deltaTime > maxTimeBetweenClicks)
        {
            HandleMovementClick(_previousRay, false);
            _clickedOnce = false;
            _lastTimeClicked = Time.time;
        }
    }

    private void HandleMovementClick(Ray ray, bool isDouble)
    {
        if (Physics.Raycast(ray, out var hit, int.MaxValue, _movementLayerMask))
        {
            if (isDouble)
                OnMovementDoubleClick?.Invoke(hit);
            else
                OnMovementClick?.Invoke(hit);
        }
    }
}