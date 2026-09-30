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

    float _point = 0;

    Joycon _rightJoycon;

    async void Start()
    {
        var ct = this.GetCancellationTokenOnDestroy();

        try
        {
            //ポイントの初期化
            _point = 0;

            //(右)コントローラーの取得
            var _rightJoycon = await JoyconHandler.GetRightJoyconAsync(ct);

            //ポイント取得処理の開始
            GetPointAsync(ct).Forget();
        }
        catch(OperationCanceledException)
        {

        }
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
        if (_rightJoycon == null) return;

        //加速度を取得
        Vector3 accel = _rightJoycon.GetAccel();

        //加速度の大きさを計算
        float magnitudeAccel = accel.magnitude;

        //加算ポイントを計算
        float addPoint = magnitudeAccel * _pointMagnification;

        //反映
        _point += addPoint;
        _pointText.text = _point.ToString("0");
    }
}
