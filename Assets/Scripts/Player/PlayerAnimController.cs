using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
	[SerializeField] Animator _animator;
	[SerializeField] Rigidbody _rigidbody;

	[Header("Animation Trigger Names"), Space]
	[SerializeField] string _fallTrigger;
	[SerializeField] string _landTrigger;
	[SerializeField] string _deathTrigger;
	[SerializeField] string _loseTrigger;
	[SerializeField] string _winTrigger;

	[Header("Events"), Space]
	[SerializeField] TriggerEventSO _death;
	[SerializeField] TriggerEventSO _lose;
	[SerializeField] TriggerEventSO _win;

	bool _disabled = false;

	bool _isFalling = true;
	bool _wasFalling = true;

    void OnEnable()
    {
		_death.AddListener(OnDeath);
		_lose.AddListener(OnLose);
		_win.AddListener(OnWin);
    }

    private void OnDisable()
    {
        _death.RemoveListener(OnDeath);
        _lose.RemoveListener(OnLose);
        _win.RemoveListener(OnWin);
    }

    void FixedUpdate()
    {
		if (_disabled)
			return;

        _isFalling = _rigidbody.velocity.y < -1f;

		if (_wasFalling && !_isFalling)
			_animator.SetTrigger(_landTrigger);

		if (!_wasFalling && _isFalling)
			_animator.SetTrigger(_fallTrigger);

		_wasFalling = _isFalling;
    }

	void OnDeath()
	{
		_disabled = true;
		_animator.SetTrigger(_deathTrigger);
	}

	void OnLose()
	{
		_disabled = true;
		_animator.SetTrigger(_loseTrigger);
	}

    void OnWin()
    {
        _disabled = true;
        _animator.SetTrigger(_winTrigger);
    }
}
