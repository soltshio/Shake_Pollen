using Cysharp.Threading.Tasks;
using System.Threading;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

//デモシーン

public class GamePhaseStateTypeDemo : GamePhaseStateTypeBase
{
    [SerializeField]
    Canvas _demoCanvas;

    [SerializeField]
    ShakePtManager _shakePt;

    [SerializeField]
    PollenEffect _pollenEffect;

    [SerializeField]
    PlayerInput _playerInput;

    [SerializeField]
    AudioSource _demoBGMSource;

    GamePhaseStateMachine _stateMachine;

    public void GetInputGameStart(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        _stateMachine.ChangeState(EGamePhaseState.Countdown);
    }
    
    public override void OnEnter(GamePhaseStateMachine stateMachine)
    {
        _shakePt.enabled = false;
        _pollenEffect.enabled = false;

        _demoCanvas.enabled = true;

        _demoBGMSource.Play();//デモBGMを流す

        _playerInput.SwitchCurrentActionMap(ActionMapNameList.demo);

        _stateMachine = stateMachine;
    }

    public override void OnUpdate(GamePhaseStateMachine stateMachine)
    {

    }

    public override void OnExit(GamePhaseStateMachine stateMachine)
    {
        _demoCanvas.enabled = false;
    }
}
