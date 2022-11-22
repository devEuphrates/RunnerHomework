using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Integer SO", menuName = "SO Variable/Integer")]
public class IntSO : ScriptableObject
{
    public event Action <int> OnChange;

    [SerializeField] int _value;
    public int Value
    {
        get => _value;

        set 
        {
            int tmp = _value;
            _value = value;
            OnChange?.Invoke(_value - tmp);
        }
    }
}
