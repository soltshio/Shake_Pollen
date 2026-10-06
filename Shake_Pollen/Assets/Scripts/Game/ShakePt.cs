using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TMPro;
using UnityEngine;

//ジョイコンを振るとポイントが得られるようにする

public class ShakePt : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _pointText;

    [SerializeField]
    float _getPointInterval = 0.1f;

    [SerializeField]
    float _pointMagnification = 0.1f;

    [SerializeField]
    CedarShakeManager _cedarShakeManager;

    float _point = 0;

    Joycon _joycon;


    //加速度の移動平均
    [Header("加速度の移動平均")]
    
    Vector3MovingAverage _vector3MovingAverage;
    
    [SerializeField]
    int _movingAverageWindowSize = 20;


    SingleTaskCancellation _singleTaskCancellation=new();

    public float Point { get { return _point; } }

    void Awake()
    {
        _vector3MovingAverage = new Vector3MovingAverage(_movingAverageWindowSize);
    }

    async void OnEnable()
    {
        var ct = _singleTaskCancellation.CancelAndReCreateToken(this.GetCancellationTokenOnDestroy());

        try
        {
            //ポイントの初期化
            _point = 0;

            //コントローラーの取得
            _joycon = await JoyconHandler.GetJoyconAsync(ct,EJoyconSide.Any);

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
        if (_joycon == null) return;

        //加速度を取得
        Vector3 accel = _joycon.GetAccel();

        //加速度を移動平均
        Vector3 maAccel = _vector3MovingAverage.AddValue(accel);

        //加速度の大きさを計算
        float magnitudeAccel = maAccel.magnitude;

        Debug.Log(magnitudeAccel);

        //木の揺れに反映
        _cedarShakeManager.SetAmplitude(magnitudeAccel);

        //加算ポイントを計算
        float addPoint = magnitudeAccel * _pointMagnification;

        //反映
        _point += addPoint;
        _pointText.text = _point.ToString("0");
    }
}
