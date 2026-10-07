//作成者:杉山
//ゲームフェーズ(enum型)

public enum EGamePhaseState
{
    None = -1,//エラー

    //☆どのシーンでも共通に使用可能
    Countdown=0,//カウントダウン
    Finish,//終了時
    Demo,//デモシーン

    //☆インゲームシーン限定
    Game_InGameScene=1000,//ゲーム中
}
