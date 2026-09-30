//作成者:杉山
//ゲームフェーズ(enum型)

public enum EGamePhaseState
{
    None = -1,//エラー

    //☆どのシーンでも共通に使用可能
    Start,//開始
    Finish,//終了時

    //☆インゲームシーン限定
    Game_InGameScene,//ゲーム中
}
