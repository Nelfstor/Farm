using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
using TMPro;

public class UpperLineUI : SingletonPersistent<UpperLineUI>
{
    [SerializeField] TextMeshProUGUI _carrotCountUI;
    [SerializeField] TextMeshProUGUI _xpCountUI;
    [SerializeField] TextMeshProUGUI _messageStringUI;

    private int _carrotCount = 0;
    private int _XP = 0;

    private new void Awake()
    {
        base.Awake();
    }
    public void Message(string text)
    {
        _messageStringUI.text = text;
    }   

    public void IncreaseCarrotCount()
    {
        _carrotCount++;
        _carrotCountUI.text = _carrotCount.ToString();
    }

    public void IncreaseXP()
    {
        _XP++;
        _xpCountUI.text = _XP.ToString();
    }
}
