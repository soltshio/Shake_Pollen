using UnityEngine;

//作成者:杉山
//ゲームフェーズの状態の基底クラス

public abstract class GamePhaseStateTypeBase : MonoBehaviour
{
    public abstract void OnEnter(GamePhaseStateMachine stateMachine);//ステートの開始時の処理
    public abstract void OnUpdate(GamePhaseStateMachine stateMachine);//ステートの毎フレームの処理
    public abstract void OnExit(GamePhaseStateMachine stateMachine);//ステートの終了時の処理
}
