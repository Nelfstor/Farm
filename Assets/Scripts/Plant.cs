using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GrowingState // can be extended to bigger count of states
{
    ready,
    growing
}

public class Plant : ScriptableObject
{
    [SerializeField] protected float _totalGrowingTime;
    protected float _growingTime;

    public float GrowingPercent => _growingTime / _totalGrowingTime;

    //[SerializeField] protected GameObject _grownPrefab; // alternative idea, require more models
    [SerializeField] protected GameObject _startPrefab;

    [SerializeField] GameObject _collectedPrefab;
    protected GameObject _attachedGameObject;

    [SerializeField] Sprite _icon;
    public Sprite Icon => _icon;

    protected GrowingState _state;
    public GrowingState State => _state;

    protected Transform _transform;

    public void Put(Transform transform)
    {
        _growingTime = 0f;
        _attachedGameObject = Instantiate(_startPrefab, transform, false);
        float scale = 0.2f + _growingTime / _totalGrowingTime * 0.8f; // idea is to put object 0.2 of bigger size;
        _attachedGameObject.transform.localScale = new Vector3(scale, scale, scale);
        _state = GrowingState.growing;
    }

    public void Grow(float timeInterval)
    {
        if (State != GrowingState.ready)
        {
            _growingTime += timeInterval;
            float scale = 0.2f + _growingTime / _totalGrowingTime * 0.8f;
            if (scale > 1) scale = 1f;
            _attachedGameObject.transform.localScale = new Vector3(scale, scale, scale);
            if (_growingTime >= _totalGrowingTime) _state = GrowingState.ready;
        }
    }
}


