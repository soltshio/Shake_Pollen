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

    bool _isFever = false;

    public void StopFever()
    {
        if (!_isFever) return;

        //最初は照明を消灯しておく
        for (int i = 0; i < _lights.Length; i++)
        {
            _lights[i].gameObject.SetActive(false);
        }

        //最初は画面の周りのビカビカを消しておく
        _rainbowEffect.SetActive(false);


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
        //最初は照明を消灯しておく
        for (int i = 0; i < _lights.Length; i++)
        {
            _lights[i].gameObject.SetActive(false);
        }

        //最初は画面の周りのビカビカを消しておく
        _rainbowEffect.SetActive(false);
    }

    void JudgeFever(float point)
    {
        if (point < _feverThresholdPoint) return;

        _shakePtManager.OnPointChanged -= JudgeFever;
        StartFeverAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }

    async UniTask StartFeverAsync(CancellationToken ct)
    {
        if (_isFever) return;

        //照明を点灯させる
        for (int i=0; i<_lights.Length ;i++)
        {
            _lights[i].gameObject.SetActive(true);
        }

        //少し遅らせる(ライトが光るまで少し時間がかかるため)
        await UniTask.Delay(TimeSpan.FromSeconds(_waitDurationToRainbowEffect), cancellationToken: ct);

        //画面の周りをビカビカさせる
        _rainbowEffect.SetActive(true);

        _isFever = true;
    }
}
