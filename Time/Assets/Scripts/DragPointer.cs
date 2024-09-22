using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragPointer : MonoBehaviour, IDragHandler
{
    [SerializeField] private Transform _centerPoint;
    [SerializeField] private InputField _timeInputField;
    [SerializeField] private int _value;
    [SerializeField] private bool _isHourPointer;
    [SerializeField] private SetTime _setTime;
    private bool _isAM = true;

    private void Start()
    {
        _setTime._formatChanged += FormatChanged;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = (Vector2)Input.mousePosition - (Vector2)_centerPoint.position;
        float angle = Vector2.SignedAngle(Vector2.up, direction);
        transform.localEulerAngles = new Vector3(0.0f, 0.0f, angle);
        UpdateInputField(-angle);
    }

    private void UpdateInputField(float angle)
    {
        int time = Mathf.RoundToInt(angle / 360 * _value);
        time = (time + _value) % _value;
        if (_isHourPointer)
        {
            time = _isAM ? time : time + 12;
            _timeInputField.text = time.ToString();
        }
        else        
            _timeInputField.text = time.ToString();
    }

    private void FormatChanged()
    {
        _isAM = !_isAM;
    }
}
