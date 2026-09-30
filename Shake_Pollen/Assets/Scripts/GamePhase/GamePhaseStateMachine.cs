using UnityEngine;

//作成者:杉山
//メインゲームのフェーズを管理するステートマシン

public class GamePhaseStateMachine : MonoBehaviour
{
    [SerializeField]
    SerializableDictionary<EGamePhaseState, GamePhaseStateTypeBase> _stateDictionary;

    [Tooltip("最初のステート")] [SerializeField]
    EGamePhaseState _initialState=EGamePhaseState.None;

    GamePhaseStateTypeBase _currentState;
    EGamePhaseState _currentEState=EGamePhaseState.None;

    public EGamePhaseState CurrentState => _currentEState;//現在のステートを取得

    void Start()
    {
        ChangeState(_initialState);
    }

    void Update()
    {
        if (_currentState != null) _currentState.OnUpdate(this);
    }

    //ステートの変更
    public void ChangeState(EGamePhaseState nextState)
    {
        //存在しないステートが指定された場合は、処理を行わない
        if (nextState == EGamePhaseState.None) return;
        if (!_stateDictionary.TryGetValue(nextState, out var nextStateInstance)) return;


        if (_currentState != null) _currentState.OnExit(this);

        _currentState = nextStateInstance;
        _currentEState = nextState;

        if(_currentState != null) _currentState.OnEnter(this);
    }
}
