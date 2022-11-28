using Global;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grid : MonoBehaviour
{
    private int _x;
    private int _z;
    [SerializeField] CellPanel _cellPanel;
    [SerializeField] GameObject _cellPrefab;
    [SerializeField] float _updateFrequency = 5f;
    [SerializeField] float _timeStep = 5f;
    [SerializeField] float _offset = 5f;

    Cell[,] _cells;
    private float _cellSizeX;
    private float _cellSizeY;

    public void SetGrid(int x, int z)
    {
        {            
            _x = x;
            _z = z;

            _cellSizeX = _cellPrefab.transform.FindDeepChild("Cube").GetComponent<Renderer>().bounds.size.x; //#NB #check
            _cellSizeY = _cellPrefab.transform.FindDeepChild("Cube").GetComponent<Renderer>().bounds.size.z;
            CreateCells();
        }
    }
    private void CreateCells()
    {
        _cells = new Cell[_x, _z];
        for (int i = 0; i < _x; i++)
        {
            for (int k = 0; k < _z; k++)
            {
                var cellObject = Instantiate(_cellPrefab, new Vector3(i * (_cellSizeX + _offset), 0, k * (_cellSizeY + _offset)), Quaternion.identity);
                _cells[i,k] = cellObject.GetComponent<Cell>();
                _cells[i, k]._plantCollected += PlantCollected;
                _cells[i, k]._cellClicked += CellClicked;
                _cells[i, k]._plantGrown += PlantGrown;
            }
        }
        StartCoroutine(GrowingRoutine(_timeStep));
    }

    private IEnumerator GrowingRoutine(float timeInterval)
    {
        var waitForSeconds = new WaitForSeconds(_updateFrequency);
        while ( GameManager.Instance.GameStarted)
        {
            foreach(Cell cell in _cells)
            {
                cell.Grow(timeInterval);
            }
            yield return waitForSeconds;
        }
        yield return null;
    }
    private void PlantGrown(Cell cell)
    {
        CameraManager.Instance.SwitchToPlant(cell.transform);
        UpperLineUI.Instance.IncreaseXP();
        Log.MyGreenLog($"Plant {cell.Plant.name} Grown!");
        //UpperLineUI.Instance.Message("Plant grown!");        
    }
    
    private void PlantCollected(Plant plant)
    {
        if (plant.name.Contains("Carrot"))
        {
            UpperLineUI.Instance.IncreaseCarrotCount();
        }        
    }

    private void CellClicked(Cell cell)
    {        
        _cellPanel.UpdateUI(cell, cell.State != CellState.empty, cell.State == CellState.collectable);
    }    

}
