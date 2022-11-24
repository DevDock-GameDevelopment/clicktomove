using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ClickController clickController;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private Vector3 _targetPosition;
    private float _speed;

    private void Awake()
    {
        clickController.OnMovementClick += OnMovementClick;
        clickController.OnMovementDoubleClick += OnMovementDoubleClick;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    private void OnMovementDoubleClick(RaycastHit hit)
    {
        _speed = runSpeed;
        _targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
    }

    private void OnMovementClick(RaycastHit hit)
    {
        _speed = walkSpeed;
        _targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
    }
}