using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CellState
{
    empty,
    growing,
    collectable,
    nonCollectable
}


[RequireComponent(typeof(Collider))]
public class Cell : MonoBehaviour
{

    // [SerializeField] public GameObject cellPrefab;
    // [SerializeField] public Collider collider;
    //bool _occupied;
    Plant _plant;
    public Plant Plant => _plant;

    private CellState _state;
    public CellState State => _state;

    private CellUI _cellUI;
    //Button collectButton;
    //Button putButton;

    public UnityAction<Plant> _plantCollected;
    public UnityAction<Cell> _cellClicked;
    public UnityAction<Cell> _plantGrown;

    private void Awake()
    {
        _cellUI = GetComponentInChildren<CellUI>();
        _cellUI.Hide();
    }

    public void Grow(float timeInterval)
    {
        if (_state == CellState.growing)
        {
            _plant.Grow(timeInterval);
            if (_plant.State != GrowingState.ready)
            {
                _cellUI.UpdateStatus(_plant.GrowingPercent);
            }
            else if (_plant is CollectablePlant)
            {
                _state = CellState.collectable;
                _cellUI.UpdateStatus(_plant.GrowingPercent);
                _plantGrown.Invoke(this);
            }
            else
            {
                _state = CellState.nonCollectable;
                _plantGrown.Invoke(this);
                _cellUI.Hide();
            }
        }
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            _cellClicked.Invoke(this);

    }

    public void WaitingForPutPlant(Plant plant)
    {
        _plant = Instantiate(plant, transform);
    }
    public void PutByPlayer()
    {
        _state = CellState.growing;
        _plant.Put(transform);
        _cellUI.Show();
        _cellUI.UpdateStatus(_plant.GrowingPercent);
    }

    public void CollectPlant()
    {
        _plantCollected.Invoke(_plant);
        (_plant as CollectablePlant).Collect();
        _state = CellState.empty;
        _cellUI.UpdateStatus(0f);
        _cellUI.Hide();
    }
}
