using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldLimmiter : MonoBehaviour
{
    [SerializeField] private InputField _inputField;
    [SerializeField] private int _maxValue;
    [SerializeField] private int _minValue;

    public void OnValueChanged(string value)
    {
        if (int.TryParse(value, out int parsedValue))
        {
            if (parsedValue > _maxValue)            
                _inputField.text = _maxValue.ToString();            
            else if (parsedValue < _minValue)           
                _inputField.text = _minValue.ToString();          
        }
    }
}
