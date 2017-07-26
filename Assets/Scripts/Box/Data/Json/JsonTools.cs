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

    public static Dictionary<string, T> GetDicFromArray<T>(T[] array)
    {
        Dictionary<string, T> dic = new Dictionary<string, T>();


        for (int i = 0; i < array.Length; i++)
        {
            string[] values = DataTools.GetPropertysValue(array[i]);//得到所有属性 要第一个属性的值 因为一般该值为ID
                                                                    //Debug.LogError(values[0]);
            dic.Add(values[0], array[i]);
        }
        return dic;
    }

    public static String TextAssetToString(string path)
    {
        string str = Resources.Load<TextAsset>(path).text;
        if (str.Length > 0)
        {
            return str;
        }
        Debug.LogError("要转换的数据文件数据文件格式、路径不对 或内容为空 请检查！！！");
        return null;
    }
}