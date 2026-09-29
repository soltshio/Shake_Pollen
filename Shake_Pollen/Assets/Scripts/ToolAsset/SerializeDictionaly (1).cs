using System;
using System.Collections.Generic;
using UnityEngine;

// inspector上で指定できるDictionary
// 参考サイト
// https://zenn.dev/tmb/articles/9b4c532da8d467#%E6%94%B9%E8%89%AF%E7%89%88---inspector%E3%81%A7%E8%A8%AD%E5%AE%9A%E3%81%A7%E3%81%8D%E3%82%8Bdictionary


[Serializable]
public class SerializableDictionary<TKey, TValue> :
    Dictionary<TKey, TValue>,
    ISerializationCallbackReceiver
{
    [Serializable]
    public class Pair
    {
        public TKey key = default;
        public TValue value = default;

        public Pair(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }
    }

    [SerializeField]
    private List<Pair> _list = null;


    // エディタ画面でinspectorを変更する段階で呼ばれるとのこと

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        Clear();
        foreach (Pair pair in _list)
        {
            if (ContainsKey(pair.key)) continue;

            Add(pair.key, pair.value);
        }
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        // Pass
    }
}