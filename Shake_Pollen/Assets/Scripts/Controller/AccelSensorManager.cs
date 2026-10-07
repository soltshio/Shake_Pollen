using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class AccelSensorManager : MonoBehaviour
{
    [SerializeField]
    int _movingAverageWindowSize = 20;

    Vector3MovingAverage _vector3MovingAverage;

    Joycon _joycon;
    
    Vector3 _accel;
    float _accelMagnitude;

    public Vector3 Accel { get { return _accel; } }
    public float AccelMagnitude { get { return _accelMagnitude; } }

    //キー操作用
    private InputAction _shakeAction;


    void Awake()
    {
        _vector3MovingAverage = new Vector3MovingAverage(_movingAverageWindowSize);

        _accel = Vector3.zero;
    }

    void Start()
    {
        _shakeAction = InputSystem.actions.FindActionMap(ActionMapNameList.shakeAM).FindAction("Shake");
    }

    async void OnEnable()
    {
        var ct = this.GetCancellationTokenOnDestroy();

        //コントローラーの取得
        _joycon = await JoyconHandler.GetJoyconAsync(ct, EJoyconSide.Any);
    }

    void Update()
    {
        Vector3 accel;//加速度

        Vector3 smoothedAccel;//加速度の移動平均


        if(_joycon != null)//ジョイコンが接続されている場合、加速度センサーを使用
        {
            accel = _joycon.GetAccel();
        }
        else if(_shakeAction.IsPressed())//キー操作用デバッグ機能
        {
            accel = new Vector3(1.3f, 1.3f, 1.3f);//キーを押している間は加速度の大きさがだいたい２になるようにする
        }
        else
        {
            accel = Vector3.zero;
        }


        smoothedAccel = _vector3MovingAverage.AddValue(accel);

        _accel = smoothedAccel;
        _accelMagnitude = accel.magnitude;
    }
}
