using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HighResultManager : MonoBehaviour
{
    [Tooltip("高いスコアの閾値")] [SerializeField]
    private int _highScoreThreshold = 0;

    [SerializeField]
    private ScriptableRendererFeature _rainbowEffect;

    [Header("粉状花粉関係")]

    [SerializeField]
    ParticleSystem _resultPellenEffect_Powder;

    [SerializeField]
    float _minScore_Powder;

    [SerializeField]
    float _maxScore_Powder;

    [SerializeField]
    float _minEmissionRate_Powder;

    [SerializeField]
    float _maxEmissionRate_Powder;

    [Header("大粒花粉関係")]

    [SerializeField]
    ParticleSystem _resultPellenEffect_LargeGrain;

    [SerializeField]
    float _minScore_LargeGrain;

    [SerializeField]
    float _maxScore_LargeGrain;

    [SerializeField]
    float _minEmissionRate_LargeGrain;

    [SerializeField]
    float _maxEmissionRate_LargeGrain;

    public void CheckHighScore(float score)
    {
        //粉状花粉のエミッションレートをスコアに応じて調整
        float emissionRatePowder = MathfExtension.Remap(score, _minScore_Powder, _maxScore_Powder, _minEmissionRate_Powder, _maxEmissionRate_Powder);
        var emissionPowder = _resultPellenEffect_Powder.emission;
        emissionPowder.rateOverTime = emissionRatePowder;
        _resultPellenEffect_Powder.Play();


        //大粒花粉のエミッションレートをスコアに応じて調整
        float emissionRateLargeGrain = MathfExtension.Remap(score, _minScore_LargeGrain, _maxScore_LargeGrain, _minEmissionRate_LargeGrain, _maxEmissionRate_LargeGrain);
        var emissionLargeGrain = _resultPellenEffect_LargeGrain.emission;
        emissionLargeGrain.rateOverTime = emissionRateLargeGrain;
        _resultPellenEffect_LargeGrain.Play();


        //虹色エフェクトの表示/非表示をスコアに応じて切り替え
        if (score >= _highScoreThreshold)
        {
            // ハイスコア達成時の処理
            _rainbowEffect.SetActive(true);
        }
        else
        {
            // ハイスコア未達成時の処理
            _rainbowEffect.SetActive(false);
        }
    }
}
