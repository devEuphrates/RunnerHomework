using UnityEngine;

public class EndGame : MonoBehaviour
{
	[SerializeField] TriggerEventSO _winEvent;
	[SerializeField] int _boxLayer;
    [SerializeField] ParticleSystem _particles;

    bool _disabled = false;
    void OnTriggerEnter(Collider other)
    {
        if (_disabled || other.gameObject.layer != _boxLayer)
            return;

        _disabled = true;

        if (_particles)
            _particles.Play();

        _winEvent?.Invoke();
    }
}
