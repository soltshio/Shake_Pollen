using UnityEngine;

public class FeverLight : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;

    [SerializeField]
    float _interval;

    [Tooltip("StartRotから始まる")] [SerializeField]
    float _startRot;

    [SerializeField]
    float _endRot;

    ParticleSystem.MainModule _main;

    float _time;

    void Start()
    {
        _main = _particleSystem.main;

        _time = 0;
    }

    void Update()
    {
        _time += Time.deltaTime;
        _time %= _interval;

        var timeRate = _time / _interval;

        float rate = Mathf.Cos(2 * Mathf.PI * timeRate);

        float rot = MathfExtension.Remap(rate, -1, 1, _endRot, _startRot);

        Debug.Log(rot);

        _main.startRotation = rot * Mathf.Deg2Rad;
    }
}
