using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

//カウントダウン時のフェーズ

public class GamePhaseStateTypeCountdown : GamePhaseStateTypeBase
{
    [SerializeField]
    AudioSource _countDownAudioSource;

    [SerializeField]
    Canvas _startCanvas;

    [SerializeField]
    TextMeshProUGUI _countDownText;

    [SerializeField]
    string _startText = "Start!";

    [Tooltip("カウントダウンのSEを流すまでに遅延させる時間")] [SerializeField]
    float _waitDurationToStartPlayCountDownSE;

    [Tooltip("ゲーム開始してから何秒で開始のUIを隠すか")] [SerializeField]
    float _waitDurationFromStartGameToHideUI = 1f;

    [SerializeField]
    PlayerInput _playerInput;

    [Header("カメラ関係")]

    [SerializeField]
    CinemachineCamera _demoCamera;

    [SerializeField]
    CinemachineMixingCamera _inGameCamera;

    [SerializeField]
    float _waitDurationCameraFade = 1.5f;

    const int _countDownTime = 3;//カウントダウンで数える秒数
    const float _countDownInterval = 1f;

    void Start()
    {
        _startCanvas.enabled = false;
    }

    public override void OnEnter(GamePhaseStateMachine stateMachine)
    {
        _playerInput.SwitchCurrentActionMap(ActionMapNameList.unControllable);

        CountDownAsync(this.GetCancellationTokenOnDestroy(),stateMachine).Forget();
    }

    public override void OnUpdate(GamePhaseStateMachine stateMachine)
    {

    }

    public override void OnExit(GamePhaseStateMachine stateMachine)
    {
        HideUIAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }

    async UniTask CountDownAsync(CancellationToken ct, GamePhaseStateMachine stateMachine)
    {
        //カメラを引きから寄せる
        _demoCamera.enabled = false;
        _inGameCamera.enabled = true;

        await UniTask.Delay(TimeSpan.FromSeconds(_waitDurationCameraFade), cancellationToken: ct);

        //少し待ってからカウントダウン開始
        _startCanvas.enabled = true;//カウントダウン開始のタイミングでカウントダウンUIを表示する

        DelayPlayCountDownSEAsync(ct).Forget();

        //カウントダウン時のUI更新
        for (int i = _countDownTime; i > 0 ; i--)
        {
            _countDownText.text = i.ToString("0");

            //1秒待ってからカウントダウンの残り秒数のUIを更新
            await UniTask.Delay(TimeSpan.FromSeconds(_countDownInterval), cancellationToken: ct);
        }

        //スタート
        _countDownText.text = _startText;

        //インゲームのフェーズに移行
        stateMachine.ChangeState(EGamePhaseState.Game_InGameScene);
    }

    async UniTask DelayPlayCountDownSEAsync(CancellationToken ct)
    {
        //少し待ってから効果音を鳴らし始める
        await UniTask.Delay(TimeSpan.FromSeconds(_waitDurationToStartPlayCountDownSE), cancellationToken: ct);

        _countDownAudioSource.Play();
    }

    async UniTask HideUIAsync(CancellationToken ct)
    {
        //少し待ってからUIを非表示にする
        await UniTask.Delay(TimeSpan.FromSeconds(_waitDurationFromStartGameToHideUI), cancellationToken: ct);

        _startCanvas.enabled = false;
    }
}
