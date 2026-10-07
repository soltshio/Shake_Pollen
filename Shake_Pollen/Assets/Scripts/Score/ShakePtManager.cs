using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TMPro;
using UnityEngine;

//ジョイコンを振るとポイントが得られるようにする

public class ShakePtManager : MonoBehaviour
{
    [SerializeField]
    AccelSensorManager _accelSensorManager;

    [SerializeField]
    Timer _timer;

    [Tooltip("ポイント取得処理をするインターバル")] [SerializeField]
    float _getPointInterval = 0.1f;

    [Header("加速度によるポイント取得")]

    [Tooltip("ポイントが取得できる加速度の最小値")] [SerializeField]
    float _minAccelMagnitude = 1.2f;

    [Tooltip("ポイントが取得できる加速度の最大値")] [SerializeField]
    float _maxAccelMagnitude = 2.3f;

    [Tooltip("ポイント最小値")] [SerializeField]
    float _minPoint = 1f;

    [Tooltip("ポイント最大値")] [SerializeField]
    float _maxPoint = 5f;

    [Header("時間経過によるポイント倍率")]

    [Tooltip("最小(ゲーム開始直前)の時のポイント倍率")] [SerializeField]
    float _minPointMagnification = 1f;

    [Tooltip("最大(ゲーム終了直前)の時のポイント倍率")] [SerializeField]
    float _maxPointMagnification = 2f;

    float _point = 0;

    SingleTaskCancellation _singleTaskCancellation=new();

    public event Action <float> OnPointChanged;

    public float Point { get { return _point; } }

    void OnEnable()
    {
        var ct = _singleTaskCancellation.CancelAndReCreateToken(this.GetCancellationTokenOnDestroy());

        //ポイントの初期化
        _point = 0;

        //ポイント取得処理の開始
        GetPointAsync(ct).Forget();
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
        float magnitudeAccelMagnitude = _accelSensorManager.AccelMagnitude;

        //加算ポイントを計算
        float addPoint = MathfExtension.Remap(magnitudeAccelMagnitude, _minAccelMagnitude, _maxAccelMagnitude, _minPoint, _maxPoint);

        //加算ポイントが範囲外にならないようにする
        addPoint = Mathf.Clamp(addPoint, _minPoint, _maxPoint);

        //加算ポイントに時間経過による倍率をかける
        float magnification = Mathf.Lerp(_minPointMagnification, _maxPointMagnification, _timer.Progress);
        addPoint *= magnification;

        //反映
        _point += addPoint;

        //ポイントが変化したことを通知
        OnPointChanged?.Invoke(_point);
    }
}
