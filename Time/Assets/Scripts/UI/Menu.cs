using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _editForms;
    [SerializeField] private GameObject _editButton;
    [SerializeField] private GameObject _clock;

    public void Edit()
    {
        _editForms.SetActive(true);
        _editButton.SetActive(false);
        _clock.GetComponent<Clock>().enabled = false;
    }

    public void Back()
    {
        _editForms.SetActive(false);
        _editButton.SetActive(true);
        _clock.GetComponent<Clock>().enabled = true;
    }
}
