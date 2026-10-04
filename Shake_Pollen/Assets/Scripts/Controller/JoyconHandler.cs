using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

//Joycon関係の汎用メソッド

public static class JoyconHandler
{
    //JoyconManagerから左のジョイコンが取得できるまで待って取得する(何か異常があって失敗すればnullが取得される)、joyconSideで左右を指定することができる
    public static async UniTask<Joycon> GetJoyconAsync(CancellationToken ct,EJoyconSide joyconSide)
    {
        try
        {
            var joyconManager = await GetJoyconManagerAsync(ct);

            Joycon retJoycon = null;

            //ジョイコンを取得できた場合はすぐに返す、取得出来なかった場合は取得できるまで待ってから返す(WaitUnitlを使うと確実に1フレーム以上は待たされるから)
            if (TryGetJoycon(joyconManager, joyconSide, out retJoycon)) return retJoycon;

            await UniTask.WaitUntil(() => TryGetJoycon(joyconManager, joyconSide, out retJoycon), cancellationToken: ct);
            return retJoycon;
        }
        catch (OperationCanceledException)
        {
            return null;
        }
    }

    //JoyconManagerを取得できるまで待って取得する(何か異常があって失敗すればnullが取得される)
    public static async UniTask<JoyconManager> GetJoyconManagerAsync(CancellationToken ct)
    {
        //ジョイコンマネージャーを取得できた場合はすぐに返す、取得出来なかった場合は取得できるまで待ってから返す(WaitUnitlを使うと確実に1フレーム以上は待たされるから)
        if (JoyconManager.Instance != null) return JoyconManager.Instance;

        await UniTask.WaitUntil(() => (JoyconManager.Instance != null), cancellationToken: ct);
        return JoyconManager.Instance;
    }


    static bool TryGetJoycon(JoyconManager joyconManager, EJoyconSide joyconSide, out Joycon joycon)
    {
        foreach (var j in joyconManager.j)
        {
            //どちらでもいい場合
            if(joyconSide == EJoyconSide.Any)
            {
                joycon = j;
                return true;
            }
            //左の場合
            else if(joyconSide == EJoyconSide.Left && j.isLeft)
            {
                joycon = j;
                return true;
            }
            //右の場合
            else if(joyconSide == EJoyconSide.Right && !j.isLeft)
            {
                joycon = j;
                return true;
            }
        }

        joycon = null;
        return false;
    }
}
