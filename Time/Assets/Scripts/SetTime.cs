using System;
using UnityEngine;
using UnityEngine.UI;

public class SetTime : MonoBehaviour
{
    [SerializeField] private InputField _hourInput;
    [SerializeField] private InputField _minuteInput;
    [SerializeField] private Clock _clock;
    public Action _formatChanged;

    public void SetUserTime()
    {
        if (_hourInput.text != "")        
            _clock.Hours = Convert.ToInt32(_hourInput.text);
        if (_minuteInput.text != "")
            _clock.Minutes = Convert.ToInt32(_minuteInput.text);        
    }

    public void ChangeFormat()
    {
        _formatChanged?.Invoke();
    }
}
