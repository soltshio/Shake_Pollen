using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

//ゲーム中のフェーズ

public class GamePhaseStateTypeInGame : GamePhaseStateTypeBase
{
    [SerializeField]
    Canvas _inGameCanvas;

    [SerializeField]
    TextMeshProUGUI _timerText;

    [SerializeField]
    Timer _timer;

    [SerializeField]
    ShakePtManager _shakePt;

    [SerializeField]
    PollenEffect _pollenEffect;

    GamePhaseStateMachine _stateMachine;

    private void Start()
    {
        _inGameCanvas.enabled = false;
    }

    public override void OnEnter(GamePhaseStateMachine stateMachine)
    {
        _stateMachine = stateMachine;

        _inGameCanvas.enabled = true;

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
}
