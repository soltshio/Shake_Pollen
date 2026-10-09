using UnityEngine;

//杉の木の振動

public class CedarShake : MonoBehaviour
{
    [SerializeField]
    AccelSensorManager _accelSensorManager;

    [SerializeField]
    ShakePtManager _shakePtManager;

    [SerializeField]
    Animator _cedarAnimator;

    [SerializeField]
    float _interval = 0.5f;

    [Header("加速度による木を揺らすかのの判断")]

    [SerializeField]
    float _accelMagnitudeThresholdToShake;

    [Header("ポイント量による振り幅の変化")]

    [SerializeField]
    float _minPt;

    [SerializeField]
    float _maxPt;

    [SerializeField] [Range(0,1)]
    float _minAmplitudeRate;

    [SerializeField] [Range(0, 1)]
    float _maxAmplitudeRate;

    float _time = 0f;

    float _amplitudeRate=0f;

    private static readonly int BlendXID = Animator.StringToHash("BlendX");

    private static readonly int BlendYID = Animator.StringToHash("BlendY");

    private void Update()
    {
        SetAmplitude(_accelSensorManager.Accel.magnitude,_shakePtManager.Point);

        _time += Time.deltaTime;
        _time %= _interval;

        var timeRate = _time / _interval;
        float rate = Mathf.Sin(2 * Mathf.PI * timeRate);

        _cedarAnimator.SetFloat(BlendXID, rate * _amplitudeRate);
    }

    void SetAmplitude(float accelMag,float point)
    {
        if(accelMag<_accelMagnitudeThresholdToShake)//加速度が達していなかったら揺らさない
        {
            _amplitudeRate = 0f;
            return;
        }

        //ポイント量によって振動幅を変える
        _amplitudeRate = MathfExtension.Remap(point, _minPt, _maxPt, _minAmplitudeRate, _maxAmplitudeRate);
    }

    void OnEnable()
    {
        SetZeroPos();//中心に戻す

        _time = 0f;
        _amplitudeRate = 0f;
    }

    private void OnDisable()
    {
        SetZeroPos();//中心に戻す
    }

    void SetZeroPos()
    {
        _cedarAnimator.SetFloat(BlendXID, 0f);
        _cedarAnimator.SetFloat(BlendYID, 0f);
    }
}
