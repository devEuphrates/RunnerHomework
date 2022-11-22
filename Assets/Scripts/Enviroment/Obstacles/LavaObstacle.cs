using DG.Tweening;
using UnityEngine;

public class LavaObstacle : MonoBehaviour, IObstacle
{
    [SerializeField] int _playerLayer;
    [SerializeField] TriggerEventSO _death;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _playerLayer)
        {
            _death?.Invoke();
            return;
        }

        if (!other.TryGetComponent<Box>(out var box))
            return;

        box.OnObstacleHit(this);
    }

    public void OnHit(Box box)
    {
        Destroy(box.gameObject, .26f);
        box.transform.parent = null;
        box.transform.DOScaleY(0f, .25f);
        box.transform.DOMoveY(1f, .25f);
        box.GetComponent<BoxCollider>().enabled = false;
    }
}
