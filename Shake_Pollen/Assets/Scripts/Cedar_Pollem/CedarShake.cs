using UnityEngine;

//杉の木の振動

public class CedarShake : MonoBehaviour
{
    [SerializeField]
    Transform _cedarTrs;

    [SerializeField]
    AccelSensorManager _accelSensorManager;

    [SerializeField]
    float _interval = 0.5f;

    [Header("加速度による振り幅の変化")]

    [SerializeField]
    float _minAccelMagnitude;

    [SerializeField]
    float _maxAccelMagnitude;

    [SerializeField]
    float _minAmplitude;

    [SerializeField]
    float _maxAmplitude;

    float _time = 0;

    float _amplitude;

    const float _timeRateOffset = 0.25f;//最初は真ん中になるようにしたいので、揺れの周期的に真ん中から始まるようにずらす

    public float Amplitude { get => _amplitude; }

    private void Update()
    {
        SetAmplitude(_accelSensorManager.Accel.magnitude);

        if (_amplitude <= 0) return;//振れ幅が無いなら揺らさなくてよい

        _time += Time.deltaTime;
        _time %= _interval;

        float xPosRate = MathfExtension.TriangleWave01(_time, 0, _interval);
        float xPos = Mathf.Lerp(-_amplitude, _amplitude, xPosRate);

        Vector3 currentPos = _cedarTrs.position;
        currentPos.x = xPos;
        _cedarTrs.position = currentPos;
    }

    void SetAmplitude(float currentAccelMagnitude)
    {
        _amplitude = MathfExtension.Remap(currentAccelMagnitude, _minAccelMagnitude, _maxAccelMagnitude, _minAmplitude, _maxAmplitude);

        _amplitude = Mathf.Clamp(_amplitude, _minAmplitude, _maxAmplitude);
    }

    void Start()
    {
        _amplitude = 0f;

        _time += _timeRateOffset * _interval;
    }
}
