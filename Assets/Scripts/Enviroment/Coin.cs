using UnityEngine;

public class Coin : MonoBehaviour
{
	[SerializeField] IntSO _coin;
    [SerializeField] GameObject _visual;
	[SerializeField] ParticleSystem _paricles;
	[SerializeField] int _boxLayer;

    bool _disabled = false;

    void OnTriggerEnter(Collider other)
    {
        if (_disabled || other.gameObject.layer != _boxLayer)
            return;

        _disabled = true;
        _coin.Value++;

        _visual.SetActive(false);
        _paricles.Play();

        Destroy(gameObject, 3f);
    }
}
