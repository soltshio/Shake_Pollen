using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//数学的な処理を汎用に使えるようにするためのクラス

public class MathfExtension
{
    /// <summary>
    /// 値を範囲内で循環させる
    /// 範囲最小(rangeMin)以上、範囲最大(rangeMax)以下を範囲とする
    /// </summary>
    public static int CircularWrapping(int num,int rangeMax)//範囲最小が0
    {
        return CircularWrapping(num, 0, rangeMax);
    }

    public static int CircularWrapping(int num,int rangeMin,int rangeMax)//範囲最小も指定可能
    {
        //rangeMaxの方が小さかったら警告を出す
        if(rangeMin > rangeMax)
        {
            Debug.Log("rangeMinの方が大きくなっています！");
            (rangeMin, rangeMax) = (rangeMax, rangeMin);//値の入れ替え
        }

        int range = rangeMax - rangeMin + 1;

        num -= rangeMin;//範囲最小を0にした時に合わせる

        num %= range;
        num = (num + range) % range;

        num += rangeMin;//元に戻す

        return num;
    }



    /// <summary>
    /// 値を増加・減少させ、変化後の値を範囲内で循環させる
    /// /// 範囲最小(rangeMin)以上、範囲最大(rangeMax)以下を範囲とする
    /// </summary>
    public static int CircularWrapping_Delta(int num,int delta,int rangeMax)//範囲最小が0
    {
        return CircularWrapping_Delta(num,delta,0,rangeMax);
    }

    public static int CircularWrapping_Delta(int num,int delta,int rangeMin,int rangeMax)//範囲最小も指定可能
    {
        int range = rangeMax - rangeMin + 1;

        delta %= range;
        num += delta;

        return CircularWrapping(num,rangeMin,rangeMax);
    }

    /// <summary>
    /// 返り値がalphaの倍数になるように端数を切り捨て
    /// </summary>
    public static float FloorByAlpha(float value, float alpha)
    {
        return Mathf.Floor(value / alpha) * alpha;
    }

    /// <summary>
    /// 返り値がalphaの倍数になるように端数を切り上げ
    /// </summary>
    public static float CeilByAlpha(float value, float alpha)
    {
        return Mathf.Ceil(value / alpha) * alpha;
    }

    /// <summary>
    /// 返り値がalphaの倍数になるように端数を四捨五入（のようにする）
    /// </summary>
    public static float RoundByAlpha(float value, float alpha)
    {
        return Mathf.Round(value / alpha) * alpha;
    }


    /// <summary>
    /// maxよりもminの方が大きければ、自動的に入れ替える(int型)
    /// </summary>
    public static void NormalizeRange(ref int min,ref int max)
    {
        if(min > max)
        {
            (min, max) = (max, min);
        }
    }

    /// <summary>
    /// maxよりもminの方が大きければ、自動的に入れ替える(float型)
    /// </summary>
    public static void NormalizeRange(ref float min, ref float max)
    {
        if (min > max)
        {
            (min, max) = (max, min);
        }
    }


    /// <summary>
    /// 値(int型)が範囲内か(min以上、max以下)を返す
    /// </summary>
    public static bool IsInRange(int value,int min,int max)
    {
        NormalizeRange(ref min,ref max);
        return value >= min && value <= max;
    }

    /// <summary>
    /// 値(float型)が範囲内か(min以上、max以下)を返す
    /// </summary>
    public static bool IsInRange(float value, float min, float max)
    {
        NormalizeRange(ref min, ref max);
        return value >= min && value <= max;
    }

    /// <summary>
    /// 値を別の範囲に線形補間して変換する
    /// </summary>
    /// <param name="value">変換する値</param>
    /// <param name="inMin">入力範囲の最小値</param>
    /// <param name="inMax">入力範囲の最大値</param>
    /// <param name="outMin">出力範囲の最小値</param>
    /// <param name="outMax">出力範囲の最大値</param>
    /// <returns>変換後の値</returns>
    public static float Remap(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return Mathf.Lerp(outMin, outMax, Mathf.InverseLerp(inMin, inMax, value));
    }
}
