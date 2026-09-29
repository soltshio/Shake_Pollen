using UnityEngine;

//作成者：杉山
//Vector2に追加の機能を付与するクラス

public static class Vector2_ExHandler
{
    const float _defaultZeroVectorThreshold = 0.005f;

    public static bool IsZeroVector(this Vector2 vector,float thresholdX = _defaultZeroVectorThreshold, float thresholdY = _defaultZeroVectorThreshold)
    {
        if (!MathfExtension.IsInRange(vector.x, -thresholdX, thresholdX)) return false;
        if (!MathfExtension.IsInRange(vector.y, -thresholdY, thresholdY)) return false;

        return true;
    }
}
