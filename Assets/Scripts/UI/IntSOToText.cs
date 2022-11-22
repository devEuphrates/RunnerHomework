using TMPro;
using UnityEngine;

public class IntSOToText : MonoBehaviour
{
	[SerializeField] IntSO _value;
	[SerializeField] TextMeshProUGUI _text;

    private void OnEnable()
    {
        UpdateText(0);
        _value.OnChange += UpdateText;
    }

    private void OnDisable() => _value.OnChange -= UpdateText;

    void Start() => UpdateText(0);

    void UpdateText(int _) => _text.text = _value.Value.ToString();
}
