using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

//ゲーム終了時のフェーズ

public class GamePhaseStateTypeFinish : GamePhaseStateTypeBase
{
    [Header("終了")]

    [Tooltip("終了してから溜め始めるまでに待つ時間")] [SerializeField]
    float _waitDurationFromFinishToCharge = 2f;

    [SerializeField]
    AudioClip _finishSE;

    [SerializeField]
    Canvas _finishCanvas;

    [SerializeField]
    CinemachineMixingCamera _inGameCamera;

    [SerializeField]
    CinemachineCamera _chargeCamera;

    [SerializeField]
    TextMeshProUGUI _finishText;

    [Header("溜め")]

    [Tooltip("溜め始めてからカメラ切り替え始めるまでに時間")] [SerializeField]
    float _waitDurationFromChargeToSwitchCamera = 2f;

    [SerializeField]
    AudioClip _chargeSE;

    [SerializeField]
    CinemachineCamera _announceCamera;

    [SerializeField]
    TextMeshProUGUI _announceText;

    [Tooltip("カメラ切り替え始めてから結果発表までに時間")] [SerializeField]
    float _waitDurationFromSwitchCameraToAnnounce = 2f;

    [Header("以下はその他")]

    [SerializeField]
    AudioSource _seAudioSource;

    [SerializeField]
    CedarShake _cedarShake;

    [SerializeField]
    PlayerInput _playerInput;

    [SerializeField]
    FeverTime _feverTime;

    void Start()
    {
        _chargeCamera.enabled = false;
        _announceCamera.enabled = false;
        _finishText.enabled = false;
        _announceText.enabled = false;
    }

    public override void OnEnter(GamePhaseStateMachine stateMachine)
    {
        _playerInput.SwitchCurrentActionMap(ActionMapNameList.finish);

        _cedarShake.enabled = false;//杉が揺れないようにする

        _feverTime.StopFever();

        ShowScoreUIAsync(this.GetCancellationTokenOnDestroy(),stateMachine).Forget();
    }

    public override void OnUpdate(GamePhaseStateMachine stateMachine)
    {
        
    }

    public override void OnExit(GamePhaseStateMachine stateMachine)
    {
        
    }

    async UniTask ShowScoreUIAsync(CancellationToken ct, GamePhaseStateMachine stateMachine)
    {
        //終了
        _finishCanvas.enabled = true;
        _finishText.enabled = true;
        _seAudioSource.PlayOneShot(_finishSE);

        //少し待つ
        await UniTask.Delay(TimeSpan.FromSeconds(_waitDurationFromFinishToCharge), cancellationToken: ct);

        //溜め始める
        _finishText.enabled = false;
        _announceText.enabled = true;

        _inGameCamera.enabled = false;
        _chargeCamera.enabled = true;

        _seAudioSource.PlayOneShot(_chargeSE);

        //少し待ってから...
        await UniTask.Delay(TimeSpan.FromSeconds(_waitDurationFromChargeToSwitchCamera), cancellationToken: ct);

        //締め

        //カメラ切り替え
        _chargeCamera.enabled = false;
        _announceCamera.enabled = true;

        //少し待ってから結果発表に移行
        await UniTask.Delay(TimeSpan.FromSeconds(_waitDurationFromSwitchCameraToAnnounce), cancellationToken: ct);

        //終了時のキャンバスを非表示に
        _finishCanvas.enabled = false;

        stateMachine.ChangeState(EGamePhaseState.Score);
    }
}
