using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

//Joycon関係の汎用メソッド

public static class JoyconHandler
{
    //JoyconManagerから左のジョイコンを取得する(失敗すればnullが取得される)
    public static async UniTask<Joycon> GetLeftJoyconAsync(JoyconManager joyconManager,CancellationToken ct)
    {
        try
        {
            if (joyconManager == null) return null;

            Joycon retJoycon = null;

            await UniTask.WaitUntil(() => TryGetJoycon(joyconManager, true, out retJoycon), cancellationToken: ct);

            return retJoycon;
        }
        catch(OperationCanceledException)
        {
            return null;
        }
    }

    //JoyconManagerから右のジョイコンを取得する(失敗すればnullが取得される)
    public static async UniTask<Joycon> GetRightJoyconAsync(JoyconManager joyconManager,CancellationToken ct)
    {
        try
        {
            if (joyconManager == null) return null;

            Joycon retJoycon = null;

            await UniTask.WaitUntil(() => TryGetJoycon(joyconManager, false, out retJoycon), cancellationToken: ct);

            return retJoycon;
        }
        catch (OperationCanceledException)
        {
            return null;
        }
    }

    static bool TryGetJoycon(JoyconManager joyconManager, bool isLeft, out Joycon joycon)
    {
        foreach (var j in joyconManager.j)
        {
            if (j.isLeft == isLeft)
            {
                joycon = j;
                return true;
            }
        }

        joycon = null;
        return false;
    }
}
