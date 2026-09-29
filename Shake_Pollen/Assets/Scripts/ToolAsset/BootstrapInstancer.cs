using UnityEngine;

//作成者:杉山
//起動時にResources/Bootstrapに入っているプレハブを全てインスタンス化する

public class BootstrapInstancer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        var bootstrapPrefabs = Resources.LoadAll<GameObject>("Bootstrap");

        foreach (var prefab in bootstrapPrefabs)
        {
            GameObject obj = GameObject.Instantiate(prefab);
            Object.DontDestroyOnLoad(obj);
        }
    }
}
