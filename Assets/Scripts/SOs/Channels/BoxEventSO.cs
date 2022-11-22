using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Box Channel", menuName = "SO/Channel/Box")] // This attribute allows us to instantiate our scriptable object from the create menu.
public class BoxEventSO : ScriptableObject
{
	// Trigger that will pass the information about the box.
	event UnityAction<Box> _onTrigger;

	// Function to subscribe new listener to our event.
	public void AddListener(UnityAction<Box> listener) => _onTrigger += listener;

    // Function to unsubscribe listener from our event.
    public void RemoveListener(UnityAction<Box> listener) => _onTrigger -= listener;

	// Invoking the event, thus every other subscriber with the box information.
	public void Invoke(Box box) => _onTrigger?.Invoke(box);
}
