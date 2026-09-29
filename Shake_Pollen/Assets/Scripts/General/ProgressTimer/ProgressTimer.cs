using UnityEngine;

//作成者:杉山
//指定した時間で進行度を管理するタイマー

public class ProgressTimer
{
    private readonly float _duration;
    private float _elapsed;

    public ProgressTimer(float duration)
    {
        _duration = duration;
        _elapsed = 0f;
    }

    public float CalcProgress()
    {
        return Mathf.Clamp01(_elapsed / _duration);
    }

    public bool IsFinished => _elapsed >= _duration;

    public void Tick()
    {
        _elapsed += Time.deltaTime;
    }
}