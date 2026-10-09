using UnityEngine;

//ゲーム中のUI初期化クラス

public class InGameUIInitializer : MonoBehaviour
{
    [SerializeField]
    Canvas _startCanvas;

    [SerializeField]
    Canvas _countdownCanvas;

    [SerializeField]
    Canvas _inGameCanvas;

    [SerializeField]
    Canvas _finishCanvas;

    [SerializeField]
    Canvas _scoreCanvas;

    void Start()
    {
        //初期状態では全UIを非表示にする
        _startCanvas.enabled = false;
        _countdownCanvas.enabled = false;
        _inGameCanvas.enabled = false;
        _finishCanvas.enabled = false;
        _scoreCanvas.enabled = false;
    }
}
