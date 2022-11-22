using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollector : MonoBehaviour
{
    [SerializeField] List<Box> _boxes = new List<Box>();
    [SerializeField] float _boxHeight = .04f;

    [Header("Refs"), Space]
    [SerializeField] Transform _boxHolder;
    [SerializeField] Transform _player;

    [Header("Channels")]
    [SerializeField] BoxEventSO _boxAddedEvent;
    [SerializeField] BoxEventSO _boxRemovedEvent;
    [SerializeField] TriggerEventSO _loseEvent;


    void OnEnable()
    {
        _boxAddedEvent.AddListener(OnBoxAdded);
        _boxRemovedEvent.AddListener(OnBoxRemoved);
    }

    void OnDisable()
    {
        _boxAddedEvent.RemoveListener(OnBoxAdded);
        _boxRemovedEvent.RemoveListener(OnBoxRemoved);
    }

    void OnBoxAdded(Box box)
    {
        box.transform.SetParent(_boxHolder);
        box.transform.DOLocalMove(_boxes.Count * _boxHeight * Vector3.down, .2f);

        transform.position = new Vector3(transform.position.x, _boxes.Count * _boxHeight, transform.position.z);

        _boxes.Add(box);
    }

    void OnBoxRemoved(Box box)
    {
        _boxes.Remove(box);

        if (_boxes.Count == 0)
            _loseEvent?.Invoke();
    }
}
