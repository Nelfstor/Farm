using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlantUI : MonoBehaviour
{
    [SerializeField] private GameObject _iconPlace;
    [SerializeField] private Button _mainButton;

    public UnityAction<Plant> plantSelected;

    private Image _image;
    private Plant _plant;
    public Plant Plant => _plant;

    public void Build(Plant plant, Sprite icon)
    {
        _image = _mainButton.GetComponent<Image>();
        _mainButton.onClick.AddListener(OnButtonClicked);
        _image.sprite = icon;
        _plant = plant;
    }
    
    private void OnButtonClicked()
    {
        plantSelected(_plant);
    }
}
