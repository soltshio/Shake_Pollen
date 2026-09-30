using Cysharp.Threading.Tasks;
using System;
using System.Threading;

//Joycon関係の汎用メソッド

public static class JoyconHandler
{

    //JoyconManagerから左のジョイコンが取得できるまで待って取得する(何か異常があって失敗すればnullが取得される)
    public static async UniTask<Joycon> GetLeftJoyconAsync(CancellationToken ct)
    {
        try
        {
            var joyconManager = await GetJoyconManagerAsync(ct);

            Joycon retJoycon = null;

            //ジョイコンを取得できた場合はすぐに返す、取得出来なかった場合は取得できるまで待ってから返す(WaitUnitlを使うと確実に1フレーム以上は待たされるから)
            if (TryGetJoycon(joyconManager, true, out retJoycon)) return retJoycon;

            await UniTask.WaitUntil(() => TryGetJoycon(joyconManager, true, out retJoycon), cancellationToken: ct);
            return retJoycon;
        }
        catch(OperationCanceledException)
        {
            return null;
        }
    }


    //JoyconManagerから右のジョイコンが取得できるまで待って取得する(何か異常があって失敗すればnullが取得される)
    public static async UniTask<Joycon> GetRightJoyconAsync(CancellationToken ct)
    {
        try
        {
            var joyconManager = await GetJoyconManagerAsync(ct);

            Joycon retJoycon = null;

            //ジョイコンを取得できた場合はすぐに返す、取得出来なかった場合は取得できるまで待ってから返す(WaitUnitlを使うと確実に1フレーム以上は待たされるから)
            if (TryGetJoycon(joyconManager, false, out retJoycon)) return retJoycon;

            await UniTask.WaitUntil(() => TryGetJoycon(joyconManager, false, out retJoycon), cancellationToken: ct);
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
