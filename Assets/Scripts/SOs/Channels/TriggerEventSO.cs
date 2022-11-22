using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Trigger Channel", menuName = "SO/Channel/Trigger")] // This attribute allows us to instantiate our scriptable object from the create menu.
public class TriggerEventSO : ScriptableObject
{
    // Trigger event.
    event UnityAction _onTrigger;

    // Function to subscribe new listener to our event.
    public void AddListener(UnityAction listener) => _onTrigger += listener;

    // Function to unsubscribe listener from our event.
    public void RemoveListener(UnityAction listener) => _onTrigger -= listener;

    // Invoking the event, thus every other subscriber.
    public void Invoke() => _onTrigger?.Invoke();
}