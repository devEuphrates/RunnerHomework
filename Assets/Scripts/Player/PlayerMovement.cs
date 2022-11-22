using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public static Vector3 Move = Vector3.zero;
    Rigidbody _rigidbody;

    [Header("Player Speed Values")]
    [SerializeField] float _playerForwardSpeed = .5f;
    [SerializeField] float _playerHorizontalSpeed = 10f;

    [Space]
    [Header("Move Constraints"), Space]
    [SerializeField] float _xMaxValue = .2f;

    [Space]
    [Header("Channels")]
    [SerializeField] TriggerEventSO _startEvent;
    [SerializeField] TriggerEventSO _loseEvent;
    [SerializeField] TriggerEventSO _winEvent;

    void OnEnable()
    {
        _startEvent.AddListener(OnStart);
        _loseEvent.AddListener(OnDeath);
        _winEvent.AddListener(OnDeath);
    }

    void OnDisable()
    {
        _startEvent.RemoveListener(OnStart);
        _loseEvent.RemoveListener(OnDeath);
        _winEvent.RemoveListener(OnDeath);
    }

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    Vector2 _initialTouch;
    float _xMove = 0f;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _initialTouch = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            Vector2 mpos = Input.mousePosition;
            Vector2 move = mpos - _initialTouch;

            _xMove = move.x;

            _initialTouch = mpos;
        }

        if (Input.GetMouseButtonUp(0))
            _xMove = 0f;
    }

    bool _isDisabled = false;
    bool _started = false;

    void FixedUpdate()
    {
        if (_isDisabled || !_started)
            return;
        
        float newX = Mathf.Clamp(_rigidbody.position.x + _xMove * Time.fixedDeltaTime, -_xMaxValue, _xMaxValue);
        _rigidbody.position = new Vector3(newX, _rigidbody.position.y, _rigidbody.position.z + Time.fixedDeltaTime * _playerForwardSpeed);
    }

    void OnStart() => _started = true;

    void OnDeath()
    {
        _rigidbody.isKinematic = true;
        _isDisabled = true;
    }
}
