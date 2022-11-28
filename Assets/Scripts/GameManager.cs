using Global;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonPersistent<GameManager>
{
    [SerializeField] Grid _grid;
    [SerializeField] CellPanel _cellPanel;
    [SerializeField] PlayerController _playerController;

    List<Plant> _allTypesPlants;
    public List<Plant> AllTypesPlants => _allTypesPlants;

    StartGamePanel _panel;

    private bool _gameStarted; // 4 restartScene
    public bool GameStarted => _gameStarted;

    private new void Awake()
    {
        base.Awake();

        _panel = GetComponentInChildren<StartGamePanel>();        
        _panel.StartGameButton.onClick.AddListener(StartGame);
        _panel.gameObject.SetActive(true);

        GetAllTypesOfPlants();
        _cellPanel.gameObject.SetActive(true);
        _cellPanel._callToCollect.AddListener(SendPlayerToCollect);
        _cellPanel._callToPut.AddListener(SendPlayerToPut);
    }

    private void GetAllTypesOfPlants()
    {
        _allTypesPlants = new List<Plant>();
        
        _allTypesPlants.AddRange(Resources.LoadAll<Plant>("Plants"));
        _allTypesPlants.AddRange(Resources.LoadAll<Plant>("/Plants/Collectables"));
        foreach(var item in _allTypesPlants)
        {
            Debug.Log($"{item.name} added");
        }        
    }

    private void StartGame()
    {
        if (_gameStarted)
        {
            
            //restart Scene
        }
        else
        {
            _gameStarted = true;
            _grid.gameObject.SetActive(true);
            _grid.SetGrid((int)_panel.xSlider.value, (int)_panel.ySlider.value);

            _panel.gameObject.SetActive(false);
        }
    }

    private void SendPlayerToCollect(Cell cell)
    {
        _playerController.MoveToNextCell(cell);
    }
    private void SendPlayerToPut(Cell cell)
    {
        _playerController.MoveToNextCell(cell);
    }  

}
