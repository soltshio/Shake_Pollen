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

    [Header("縦揺れ系")]

    [SerializeField]
    Animator _cedarAnimator;

    [SerializeField]
    float _interval;

    float _time = 0f;
    bool _isFever = false;

    private static readonly int SkyTintID = Shader.PropertyToID("_Tint");

    private static readonly int BlendYID = Animator.StringToHash("BlendY");

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

        //木を元に戻す
        _cedarAnimator.SetFloat(BlendYID, 0);

        _isFever = false;
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

    void Update()
    {
        if (!_isFever) return;

        _time += Time.deltaTime;
        _time %= _interval;

        var timeRate = _time / _interval;

        float rate = Mathf.Sin(2 * Mathf.PI * timeRate);

        _cedarAnimator.SetFloat(BlendYID, rate);
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
        
        _isFever = true;
    }
}
