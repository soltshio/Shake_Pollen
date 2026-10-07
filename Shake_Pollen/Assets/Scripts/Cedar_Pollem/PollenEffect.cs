using UnityEngine;

//花粉のエフェクト

public class PollenEffect : MonoBehaviour
{
    [SerializeField]
    ParticleSystem[] _pollenParticles;

    [SerializeField]
    AccelSensorManager _accelSensorManager;

    [Header("加速度による花粉パーティクル量の変化")]

    [SerializeField]
    float _minAccelMagnitude;

    [SerializeField]
    float _maxAccelMagnitude;

    [SerializeField]
    float _minRateOverTime;

    [SerializeField]
    float _maxRateOverTime;

    private void Update()
    {
        SetPollenParticleAmount(_accelSensorManager.AccelMagnitude);
    }

    void SetPollenParticleAmount(float currentAccelMagnitude)
    {
        float rateOverTime = MathfExtension.Remap(currentAccelMagnitude, _minAccelMagnitude, _maxAccelMagnitude, _minRateOverTime, _maxRateOverTime);
        rateOverTime = Mathf.Clamp(rateOverTime, _minRateOverTime, _maxRateOverTime);

        SetRateOverTime(rateOverTime);
    }

    void Start()
    {
        SetRateOverTime(0f);
    }

    void SetRateOverTime(float rateOverTime)
    {
        // 全ての花粉パーティクルに適用
        foreach (ParticleSystem pollenParticle in _pollenParticles)
        {
            var emission = pollenParticle.emission;
            emission.rateOverTime = rateOverTime;
        }
    }
}
