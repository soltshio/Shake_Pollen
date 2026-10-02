using System;
using UnityEngine;

//タイマー

public class Timer : MonoBehaviour
{
    [SerializeField]
    float _maxTime = 0;

    public event Action OnTimeUp;

    float _remainingTime = 0;
    bool _isPlaying = false;

    public float MaxTime { get { return _maxTime; } }
    public float RemainingTime { get { return _remainingTime; } }
    public bool IsPlaying { get { return _isPlaying; } }

    public void Play()
    {
        if (_isPlaying) return;
    }

    public void Stop()
    {
        if (!_isPlaying) return;
    }

    void Update()
    {
        if (!_isPlaying) return;

        _remainingTime -= Time.deltaTime;

        if (_remainingTime >= 0) return;

        _isPlaying = false;
        OnTimeUp?.Invoke();
    }

    
}
