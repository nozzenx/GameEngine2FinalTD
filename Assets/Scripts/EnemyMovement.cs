using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Path path;
    
    [Header("Config")]
    [SerializeField] private float speed = 5f;

    [Header("Ice Effect")] 
    private bool _inIceEffect;
    
    
    private Transform _currentTarget;
    private int _targetIndex;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
        _targetIndex = 0;
        _currentTarget = path.GetPositionByIndex(_targetIndex);
    }

    private void Update()
    {
        if (path.GetPositionByIndex(_targetIndex) is null) return;
        
        if (_currentTarget)
        {
            Vector3 direction = (_currentTarget.position - transform.position).normalized;
            _rb.MovePosition(transform.position + direction * (speed * Time.deltaTime));
            transform.rotation = Quaternion.LookRotation(direction);
        }
    
        if (Vector3.Distance(transform.position, _currentTarget.position) <= 0.05f)
        {
            _targetIndex++;
            _currentTarget = path.GetPositionByIndex(_targetIndex);
        }
    }

    public void StartIceEffect(float speedDivider)
    {
        if (!_inIceEffect)
        {
            speed /= speedDivider;
            _inIceEffect = true;
        }
    }
}
