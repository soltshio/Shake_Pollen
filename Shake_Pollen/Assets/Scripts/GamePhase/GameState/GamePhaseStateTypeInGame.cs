using UnityEngine;

//ゲーム中のフェーズ

public class GamePhaseStateTypeInGame : GamePhaseStateTypeBase
{
    public override void OnEnter(GamePhaseStateMachine stateMachine)
    {
        stateMachine.ChangeState(EGamePhaseState.Finish);
    }

    public override void OnUpdate(GamePhaseStateMachine stateMachine)
    {
        
    }

    public override void OnExit(GamePhaseStateMachine stateMachine)
    {
        
    }
}
