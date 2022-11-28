using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellUI : MonoBehaviour
{

    [SerializeField] TMPro.TextMeshProUGUI _status;
    [SerializeField] Slider _slider;
    public void UpdateStatus(float percent)
    {
        _slider.value = percent;
        //_status.text = ((int)percent).ToString() + "/100";
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
