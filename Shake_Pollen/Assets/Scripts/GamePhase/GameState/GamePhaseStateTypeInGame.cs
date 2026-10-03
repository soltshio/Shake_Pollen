using UnityEngine;

//ゲーム中のフェーズ

public class GamePhaseStateTypeInGame : GamePhaseStateTypeBase
{
    public override void OnEnter(GamePhaseStateMachine stateMachine)
    {
        //ゲーム開始時の処理
        Debug.Log("GamePhaseStateTypeStart: OnEnter");
    }

    public override void OnUpdate(GamePhaseStateMachine stateMachine)
    {
        //ゲーム開始時の毎フレームの処理
    }

    public override void OnExit(GamePhaseStateMachine stateMachine)
    {
        //ゲーム開始時の終了時の処理
        Debug.Log("GamePhaseStateTypeStart: OnExit");
    }
}
