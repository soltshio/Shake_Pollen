using UnityEngine;

//デモシーン

public class GamePhaseStateTypeDemo : GamePhaseStateTypeBase
{
    [SerializeField]
    Canvas _demoCanvas;

    [SerializeField]
    ShakePtManager _shakePt;

    [SerializeField]
    PollenEffect _pollenEffect;

    void Start()
    {
        _demoCanvas.enabled = false;
    }
    
    public override void OnEnter(GamePhaseStateMachine stateMachine)
    {
        _shakePt.enabled = false;
        _pollenEffect.enabled = false;

        _demoCanvas.enabled = true;
    }

    public override void OnUpdate(GamePhaseStateMachine stateMachine)
    {

    }

    public override void OnExit(GamePhaseStateMachine stateMachine)
    {
        
    }
}
