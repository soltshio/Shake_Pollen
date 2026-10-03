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
    ShakePt _shakePt;

    GamePhaseStateMachine _stateMachine;

    public override void OnEnter(GamePhaseStateMachine stateMachine)
    {
        _stateMachine = stateMachine;

        _inGameCanvas.enabled = true;

        _shakePt.enabled = true;

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

        _shakePt.enabled = false;
    }

    void ChangeStateToFinishScene()
    {
        if (_stateMachine == null) return;

        _stateMachine.ChangeState(EGamePhaseState.Finish);
    }
}
