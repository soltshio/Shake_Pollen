using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

//ゲーム終了時のフェーズ

public class GamePhaseStateTypeFinish : GamePhaseStateTypeBase
{
    [SerializeField]
    AudioSource _audioSource;

    [SerializeField]
    AudioClip _finishSE;

    [SerializeField]
    Canvas _finishCanvas;

    [SerializeField]
    TextMeshProUGUI _scoreText;

    [SerializeField]
    ShakePtManager _shakePt;

    [SerializeField]
    CedarShake _cedarShake;

    [SerializeField]
    PlayerInput _playerInput;

    void Start()
    {
        _finishCanvas.enabled = false;
    }

    public override void OnEnter(GamePhaseStateMachine stateMachine)
    {
        _playerInput.SwitchCurrentActionMap(ActionMapNameList.finish);

        _audioSource.PlayOneShot(_finishSE);
        _finishCanvas.enabled = true;

        _cedarShake.enabled = false;

        _scoreText.text = _shakePt.Point.ToString("0");
    }

    public override void OnUpdate(GamePhaseStateMachine stateMachine)
    {
        
    }

    public override void OnExit(GamePhaseStateMachine stateMachine)
    {
        
    }
}
