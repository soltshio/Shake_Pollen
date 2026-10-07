using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TMPro;
using UnityEngine;

//ジョイコンを振るとポイントが得られるようにする

public class ShakePtManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _pointText;

    [SerializeField]
    AccelSensorManager _accelSensorManager;

    [SerializeField]
    float _getPointInterval = 0.1f;

    [SerializeField]
    float _pointMagnification = 0.1f;

    [SerializeField]
    CedarShake _cedarShake;

    [SerializeField]
    PollenEffect _pollenEffect;

    float _point = 0;

    SingleTaskCancellation _singleTaskCancellation=new();

    public float Point { get { return _point; } }

    async void OnEnable()
    {
        var ct = _singleTaskCancellation.CancelAndReCreateToken(this.GetCancellationTokenOnDestroy());

        try
        {
            //ポイントの初期化
            _point = 0;

            //ポイント取得処理の開始
            GetPointAsync(ct).Forget();
        }
        catch (OperationCanceledException)
        {

        }
    }

    void OnDisable()
    {
        //ポイント取得処理の停止
        _singleTaskCancellation.Cancel();
    }

    async UniTask GetPointAsync(CancellationToken ct)
    {
        //_getPointInterval秒ごとにポイント取得処理をするようにする
        while (true)
        {
            GetPoint();

            await UniTask.Delay(TimeSpan.FromSeconds(_getPointInterval), cancellationToken: ct);
        }
    }

    void GetPoint()
    {
        //加速度の大きさを計算
        float magnitudeAccel = _accelSensorManager.Accel.magnitude;

        Debug.Log(magnitudeAccel);


        //木の揺れに反映
        _cedarShake.SetAmplitude(magnitudeAccel);

        //花粉のパーティクル量に反映
        _pollenEffect.SetAmplitude(magnitudeAccel);


        //加算ポイントを計算
        float addPoint = magnitudeAccel * _pointMagnification;

        //反映
        _point += addPoint;
        _pointText.text = _point.ToString("0");
    }
}
