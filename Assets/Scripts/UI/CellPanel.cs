using Global;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CellPanel : MonoBehaviour
{
    [SerializeField] public TMPro.TextMeshProUGUI _putText;
    [SerializeField] public Button _collectButton;
    [SerializeField] public ScrollRect _scrollRect;
    [SerializeField] private RectTransform _content;
    private VerticalLayoutGroup _layoutGroup;
    private Plant _selectedPlant;
    private Cell _currentCell;

    public UnityEvent<Cell> _callToCollect;
    public UnityEvent<Cell> _callToPut;

    private void Awake()
    {
        _content = _scrollRect.content;
        _layoutGroup = _content.GetComponent<VerticalLayoutGroup>();
    }
    private void Start()
    {
        _collectButton.onClick.AddListener(OnCollectButtonClicked);
        AddPlantsIcons();
        gameObject.SetActive(false);
    }

    private void AddPlantsIcons()
    {
        foreach (var plant in GameManager.Instance.AllTypesPlants)
        {
            var newItem = Instantiate(Resources.Load<PlantUI>("Prefabs/PlantUI"), _content);
            newItem.Build(plant, plant.Icon);
            newItem.plantSelected += OnPlantSelected;
        }
    }

    private void OnPlantSelected(Plant plant)
    {
        _selectedPlant = plant;
        _currentCell.WaitingForPutPlant(_selectedPlant);
        _callToPut.Invoke(_currentCell);
        gameObject.SetActive(false);
    }


    private void OnCollectButtonClicked()
    {
        _callToCollect.Invoke(_currentCell);
        this.gameObject.SetActive(false);
    }

    public void UpdateUI(Cell cell, bool cellOccupied, bool plantCollectable)
    {
        _currentCell = cell;
        
        if (cellOccupied)
        {
            _putText.gameObject.SetActive(false);
            _scrollRect.gameObject.SetActive(false);
            if (plantCollectable)
            {
                _collectButton.gameObject.SetActive(true);
                gameObject.SetActive(true);
            }
            else
            {
                _collectButton.gameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(true);
            _collectButton.gameObject.SetActive(false);
            _putText.gameObject.SetActive(true);
            _scrollRect.gameObject.SetActive(true);
        }
    }
}


