using System;
using UnityEngine;

//タイマー

public class Timer : MonoBehaviour
{
    [SerializeField]
    float _setUpTime = 0;

    public event Action OnTimeUp;

    float _remainingTime = 0;
    bool _isRunning = false;

    public float SetUpTime { get { return _setUpTime; } }
    public float RemainingTime { get { return _remainingTime; } }
    public bool IsPlaying { get { return _isRunning; } }

    public void Initialize()
    {
        _isRunning = false;
        _remainingTime = _setUpTime;
    }

    public void Run()
    {
        if (_isRunning) return;

        _isRunning = true;
    }

    public void Stop()
    {
        if (!_isRunning) return;

        _isRunning = false;
    }

    void Update()
    {
        if (!_isRunning) return;

        _remainingTime -= Time.deltaTime;

        if (_remainingTime >= 0) return;

        //タイムアップ
        _remainingTime = 0;
        _isRunning = false;
        OnTimeUp?.Invoke();
    }
}
