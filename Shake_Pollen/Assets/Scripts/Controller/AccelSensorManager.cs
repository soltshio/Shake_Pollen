using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class AccelSensorManager : MonoBehaviour
{
    [SerializeField]
    int _movingAverageWindowSize = 20;

    Vector3MovingAverage _vector3MovingAverage;

    Joycon _joycon;
    Vector3 _accel;

    public Vector3 Accel { get { return _accel; } }

    void Awake()
    {
        _vector3MovingAverage = new Vector3MovingAverage(_movingAverageWindowSize);

        _accel = Vector3.zero;
    }

    async void OnEnable()
    {
        var ct = this.GetCancellationTokenOnDestroy();

        //コントローラーの取得
        _joycon = await JoyconHandler.GetJoyconAsync(ct, EJoyconSide.Any);
    }

    void Update()
    {
        if (_joycon == null) return;

        //加速度の取得
        Vector3 accel = _joycon.GetAccel();

        //加速度の移動平均を計算
        Vector3 smoothedAccel = _vector3MovingAverage.AddValue(accel);

        _accel = smoothedAccel;
    }
}
