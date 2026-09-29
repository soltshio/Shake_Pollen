using UnityEngine;

//作成者：杉山
//Vector3に追加の機能を付与するクラス

public static class Vector3_ExHandler
{
    const float _defaultZeroVectorThreshold = 0.005f;

    public static bool IsZeroVector(this Vector3 vector, float thresholdX = _defaultZeroVectorThreshold, float thresholdY = _defaultZeroVectorThreshold, float thresholdZ = _defaultZeroVectorThreshold)
    {
        if (!MathfExtension.IsInRange(vector.x, -thresholdX, thresholdX)) return false;
        if (!MathfExtension.IsInRange(vector.y, -thresholdY, thresholdY)) return false;
        if (!MathfExtension.IsInRange(vector.z, -thresholdZ, thresholdZ)) return false;

        return true;
    }
}
