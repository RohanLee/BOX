using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// 在序列化相应类数据时 需要在相应类Class定义前加[Serializable]
/// </summary>
public class JsonTools
{
    public static T[] GetJsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] array;
        public Dictionary<string, T> dictionary;
    }
}