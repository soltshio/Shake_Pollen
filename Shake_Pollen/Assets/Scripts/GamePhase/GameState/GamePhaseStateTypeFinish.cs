using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

//ゲーム終了時のフェーズ

public class GamePhaseStateTypeFinish : GamePhaseStateTypeBase
{
    [Tooltip("終了してから結果発表するまでに待つ時間")] [SerializeField]
    float _waitDurationForShowScore=2f;

    [SerializeField]
    AudioSource _seAudioSource;

    [SerializeField]
    AudioSource _inGameAudioSource;

    [SerializeField]
    AudioClip _finishSE;

    [SerializeField]
    Canvas _finishCanvas;

    [SerializeField]
    Canvas _scoreCanvas;

    [SerializeField]
    TextMeshProUGUI _scoreText;

    [SerializeField]
    ShakePtManager _shakePt;

    [SerializeField]
    CedarShake _cedarShake;

    [SerializeField]
    PlayerInput _playerInput;

    [SerializeField]
    FeverTime _feverTime;

    void Start()
    {
        _finishCanvas.enabled = false;
    }

    public override void OnEnter(GamePhaseStateMachine stateMachine)
    {
        _playerInput.SwitchCurrentActionMap(ActionMapNameList.finish);

        _cedarShake.enabled = false;//杉が揺れないようにする

        _feverTime.StopFever();

        ShowScoreUIAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }

    public override void OnUpdate(GamePhaseStateMachine stateMachine)
    {
        
    }

    public override void OnExit(GamePhaseStateMachine stateMachine)
    {
        
    }

    async UniTask ShowScoreUIAsync(CancellationToken ct)
    {
        //まず終了のUIを表示
        _finishCanvas.enabled = true;

        //少し待つ
        await UniTask.Delay(TimeSpan.FromSeconds(_waitDurationForShowScore), cancellationToken: ct);

        //インゲームのBGMを止める
        _inGameAudioSource.Stop();

        //スコア表示
        _seAudioSource.PlayOneShot(_finishSE);
        _finishCanvas.enabled = false;
        _scoreCanvas.enabled = true;
        _scoreText.text = _shakePt.Point.ToString("0") + "kg";
    }
}
