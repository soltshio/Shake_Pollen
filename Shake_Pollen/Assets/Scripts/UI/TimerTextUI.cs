using TMPro;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _timerText;

    [SerializeField]
    TextMeshProUGUI _timerLastCountdownText;

    [SerializeField]
    GameObject _lastCountdownUIs;

    [SerializeField]
    GameObject _normalUIs;

    [SerializeField]
    float _lastCountdownStartRemainingTime = 5f;

    Timer _timer;

    bool _isSwitched = false;

    void Start()
    {
        _lastCountdownUIs.SetActive(false);
        _normalUIs.SetActive(true);
    }

    public void Init(Timer timer)
    {
        _timer = timer;
    }
    
    public void UpdateTimerText()
    {
        if(!_isSwitched && _timer.RemainingTime <= _lastCountdownStartRemainingTime)
        {
            _lastCountdownUIs.SetActive(true);
            _normalUIs.SetActive(false);
        }

        _timerText.text = _timer.RemainingTime.ToString("0");
        _timerLastCountdownText.text = _timer.RemainingTime.ToString("0");
    }
}
