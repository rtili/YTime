using UnityEngine;
using UnityEngine.UI;

public class Digit : MonoBehaviour
{
    [SerializeField] private Text _timeText;
    [SerializeField] private Clock _clock;

    private void Update()
    {
        _timeText.text = $"{_clock.Hours:D2} : {_clock.Minutes:D2} : {_clock.Seconds:D2}";
    }
}
