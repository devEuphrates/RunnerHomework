using UnityEngine;

public class Box : MonoBehaviour
{
    int _boxLayer;
    [SerializeField] int _obstacleLayer;

    [Space]
    [Header("Channels")]
    [SerializeField] BoxEventSO _onBoxAdded;
    [SerializeField] BoxEventSO _onBoxRemoved;

    [SerializeField] bool _isCollected = false;
    bool _isDisabled = false;

    private void Awake() => _boxLayer = gameObject.layer;

    private void OnTriggerEnter(Collider other)
    {
        if (_isDisabled)
            return;

        switch (_isCollected)
        {
            case true:

                if (other.gameObject.layer != _obstacleLayer || !other.gameObject.TryGetComponent<IObstacle>(out var obstacle))
                    return;

                OnObstacleHit(obstacle);

                return;

            case false:
                if (other.gameObject.layer != _boxLayer)
                    return;

                _isCollected = true;
                _onBoxAdded?.Invoke(this);

                return;
        }
    }

    public void OnObstacleHit(IObstacle obstacle)
    {
        _isDisabled = true;
        _onBoxRemoved?.Invoke(this);

        obstacle.OnHit(this);
    }
}
