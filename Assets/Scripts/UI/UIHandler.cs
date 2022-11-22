using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIHandler : MonoBehaviour
{
    [SerializeField] List<UIElement> _elements = new List<UIElement>();

    void OnEnable()
    {
        foreach (var ui in _elements)
        {
            for (int i = 0; i < ui.EnableEvents.Length; i++)
                ui.EnableEvents[i].AddListener(ui.EnableActions.Invoke);

            for (int i = 0; i < ui.DisableEvents.Length; i++)
                ui.DisableEvents[i].AddListener(ui.DisableActions.Invoke);
        }
    }

    void OnDisable()
    {
        foreach (var ui in _elements)
        {
            for (int i = 0; i < ui.EnableEvents.Length; i++)
                ui.EnableEvents[i].RemoveListener(ui.EnableActions.Invoke);

            for (int i = 0; i < ui.DisableEvents.Length; i++)
                ui.DisableEvents[i].RemoveListener(ui.DisableActions.Invoke);
        }
    }

    [System.Serializable]
    public struct UIElement
    {
        public string Name;

        [Space]
        public TriggerEventSO[] EnableEvents;
        public UnityEvent EnableActions;

        [Space]
        public TriggerEventSO[] DisableEvents;
        public UnityEvent DisableActions;
    }
}
