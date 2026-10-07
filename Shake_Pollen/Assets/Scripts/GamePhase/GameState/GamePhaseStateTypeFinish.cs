using System;
using TMPro;
using UnityEngine;

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

    public override void OnEnter(GamePhaseStateMachine stateMachine)
    {
        _audioSource.PlayOneShot(_finishSE);
        _finishCanvas.enabled = true;
        _scoreText.text = _shakePt.Point.ToString("0");
    }

    public override void OnUpdate(GamePhaseStateMachine stateMachine)
    {
        
    }

    public override void OnExit(GamePhaseStateMachine stateMachine)
    {
        
    }
}
