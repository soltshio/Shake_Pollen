using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.Universal;

//フィーバータイム

public class FeverTime : MonoBehaviour
{
    [SerializeField]
    float _waitDurationToRainbowEffect = 0.7f;

    [SerializeField]
    ShakePtManager _shakePtManager;

    [SerializeField]
    float _feverThresholdPoint;

    [SerializeField]
    ParticleSystem[] _lights;

    [SerializeField]
    private ScriptableRendererFeature _rainbowEffect;

    [Header("SkyBox関係")]

    [SerializeField] 
    private Material _skyboxMaterial;

    [SerializeField]
    Color _normalColor;

    [SerializeField]
    Color _feverColor;

    private static readonly int SkyTintID = Shader.PropertyToID("_Tint");

    public void StopFever()
    {
        //最初は照明を消灯しておく
        for (int i = 0; i < _lights.Length; i++)
        {
            _lights[i].gameObject.SetActive(false);
        }

        //最初は画面の周りのビカビカを消しておく
        _rainbowEffect.SetActive(false);

        //空の色を元に戻す
        _skyboxMaterial.SetColor(SkyTintID, _normalColor);
    }

    void OnEnable()
    {
        _shakePtManager.OnPointChanged += JudgeFever;
    }

    void OnDisable()
    {
        _shakePtManager.OnPointChanged -= JudgeFever;
    }

    void Start()
    {
        StopFever();
    }

    void JudgeFever(float point)
    {
        if (point < _feverThresholdPoint) return;

        _shakePtManager.OnPointChanged -= JudgeFever;
        StartFeverAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }

    async UniTask StartFeverAsync(CancellationToken ct)
    {
        //照明を点灯させる
        for (int i=0; i<_lights.Length ;i++)
        {
            _lights[i].gameObject.SetActive(true);
        }

        //少し遅らせる(ライトが光るまで少し時間がかかるため)
        await UniTask.Delay(TimeSpan.FromSeconds(_waitDurationToRainbowEffect), cancellationToken: ct);

        //空の色をフィーバー色に変える
        _skyboxMaterial.SetColor(SkyTintID, _feverColor);

        //画面の周りをビカビカさせる
        _rainbowEffect.SetActive(true);
    }
}
