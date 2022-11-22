using UnityEngine;

public class CoinHandler : MonoBehaviour
{
	[SerializeField] IntSO _collectedCoins;
	[SerializeField] IntSO _totalCoins;

	[Header("Events"), Space]
	[SerializeField] TriggerEventSO _levelComplete;
	[SerializeField] TriggerEventSO _levelFailed;

    void OnEnable()
    {
        _levelComplete.AddListener(OnLevelComplete);
        _levelFailed.AddListener(OnLevelFailed);
    }

    void OnDisable()
    {
        _levelComplete.RemoveListener(OnLevelComplete);
        _levelFailed.RemoveListener(OnLevelFailed);
    }

    void Awake() => _collectedCoins.Value = 0;

    void OnLevelComplete()
	{
        _totalCoins.Value += _collectedCoins.Value;
        _collectedCoins.Value = 0;
	}

	void OnLevelFailed() => _collectedCoins.Value = 0;
}
