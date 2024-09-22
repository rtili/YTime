using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Clock : MonoBehaviour
{
    [SerializeField] private GameObject _pointerSeconds;
    [SerializeField] private GameObject _pointerMinutes;
    [SerializeField] private GameObject _pointerHours;

    private int _minutes, _hours, _seconds;
    public int Minutes { get { return _minutes; } set { _minutes = value; } }
    public int Hours { get { return _hours; } set { _hours = value; } }
    public int Seconds => _seconds;

    private float _msecs;

    private void Start()
    {
        StartCoroutine(GetTimeFromServer());
        StartCoroutine(UpdateTimeOnHour());
    }

    private void Update()
    {
        CalculateTime();
        MovePointers();
    }

    private void CalculateTime()
    {
        _msecs += Time.deltaTime;
        if (_msecs >= 1.0f)
        {
            _msecs -= 1.0f;
            _seconds++;
            if (_seconds >= 60)
            {
                _seconds = 0;
                _minutes++;
                if (_minutes >= 60)
                {
                    _minutes = 0;
                    _hours++;
                    if (_hours >= 24)
                        _hours = 0;
                }
            }
        }
    }

    private void MovePointers()
    {
        float rotationSeconds = (360.0f / 60.0f) * _seconds;
        float rotationMinutes = (360.0f / 60.0f) * _minutes;
        float rotationHours = ((360.0f / 12.0f) * _hours) + ((360.0f / (60.0f * 12.0f)) * _minutes);

        _pointerSeconds.transform.localEulerAngles = new Vector3(0.0f, 0.0f, -rotationSeconds);
        _pointerMinutes.transform.localEulerAngles = new Vector3(0.0f, 0.0f, -rotationMinutes);
        _pointerHours.transform.localEulerAngles = new Vector3(0.0f, 0.0f, -rotationHours);
    }

    private IEnumerator UpdateTimeOnHour()
    {
        yield return new WaitForSeconds(3600);
        StartCoroutine(GetTimeFromServer());
        StartCoroutine(UpdateTimeOnHour());
    }

    private IEnumerator GetTimeFromServer()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("https://yandex.com/time/sync.json"))
        {
            yield return webRequest.SendWebRequest();

            string json = webRequest.downloadHandler.text;
            TimeSync timeSync = JsonUtility.FromJson<TimeSync>(json);
            DateTime dateTime = DateTime.UnixEpoch.AddMilliseconds(timeSync.time);
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
            DateTime utcTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, localTimeZone);

            if (_hours != utcTime.Hour || _minutes != utcTime.Minute || _seconds != utcTime.Second)
            {
                _hours = utcTime.Hour;
                _minutes = utcTime.Minute;
                _seconds = utcTime.Second;
            }
        }
    }
}
