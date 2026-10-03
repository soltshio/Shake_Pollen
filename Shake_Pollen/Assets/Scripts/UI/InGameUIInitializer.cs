using UnityEngine;

//ゲーム中のUI初期化クラス

public class InGameUIInitializer : MonoBehaviour
{
    [SerializeField]
    Canvas _startCanvas;

    [SerializeField]
    Canvas _inGameCanvas;

    [SerializeField]
    Canvas _finishCanvas;

    void Start()
    {
        //初期状態では全UIを非表示にする
        _startCanvas.enabled = false;
        _inGameCanvas.enabled = false;
        _finishCanvas.enabled = false;
    }
}
