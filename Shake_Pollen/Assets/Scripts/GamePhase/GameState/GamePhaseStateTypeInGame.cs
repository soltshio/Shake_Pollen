using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TMPro;
using UnityEngine;

//ゲーム中のフェーズ

public class GamePhaseStateTypeInGame : GamePhaseStateTypeBase
{
    [SerializeField]
    Canvas _inGameCanvas;

    [SerializeField]
    CanvasGroup _canvasGroup;

    [SerializeField]
    TextMeshProUGUI _timerText;

    [SerializeField]
    Timer _timer;

    [SerializeField]
    ShakePtManager _shakePt;

    [SerializeField]
    PollenEffect _pollenEffect;

    [Header("UI関係")]

    [Tooltip("ゲーム開始してから何秒でUIの透明度を上げるか")] [SerializeField]
    float _waitDurationFromStartGameToUpUIAlpha = 1f;

    [SerializeField]
    float _offUIAlpha = 0.3f;

    [SerializeField]
    float _onUIAlpha = 1f;

    [Header("BGM関係")]

    [SerializeField]
    AudioSource _inGameBGMSource;

    GamePhaseStateMachine _stateMachine;

    private void Start()
    {
        _inGameCanvas.enabled = false;
    }

    public override void OnEnter(GamePhaseStateMachine stateMachine)
    {
        _stateMachine = stateMachine;

        ShowUIAsync(this.GetCancellationTokenOnDestroy()).Forget();

        _inGameBGMSource.Play();//ゲーム中のBGMを再生

        //振った時にポイントが入り、花粉が出るようにする
        _shakePt.enabled = true;
        _pollenEffect.enabled = true;

        //タイマー開始
        _timer.Initialize();
        _timer.Run();
        _timer.OnTimeUp += ChangeStateToFinishScene;
    }

    public override void OnUpdate(GamePhaseStateMachine stateMachine)
    {
        _timerText.text = _timer.RemainingTime.ToString("0");
    }

    public override void OnExit(GamePhaseStateMachine stateMachine)
    {
        _inGameCanvas.enabled = false;

        //振ってもポイントが入らず、花粉が出ないようにする
        _shakePt.enabled = false;
        _pollenEffect.enabled = false;
    }

    void ChangeStateToFinishScene()
    {
        if (_stateMachine == null) return;

        _stateMachine.ChangeState(EGamePhaseState.Finish);
    }

    async UniTask ShowUIAsync(CancellationToken ct)
    {
        _inGameCanvas.enabled = true;
        _canvasGroup.alpha = _offUIAlpha;

        await UniTask.Delay(TimeSpan.FromSeconds(_waitDurationFromStartGameToUpUIAlpha), cancellationToken: ct);

        _canvasGroup.alpha = _onUIAlpha;
    }
}
