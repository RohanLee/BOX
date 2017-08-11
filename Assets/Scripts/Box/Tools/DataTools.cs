using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class DataTools
{
    #region 字典相关

    /// <summary>
    /// 查找一个int值为索引的字典最小可用的Key值 前提是key不会小于0
    /// </summary>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    public static int GetMinValidIDFromeDictionary<T>(Dictionary<int, T> dictionary)
    {
        int key = 0;
        while (dictionary.ContainsKey(key))
        {
            key++;
        }
        return key;
    }

    /// <summary>
    /// 通过字符串获得字典 主要用于用逗号和分号分割多个内容的数据组
    /// </summary>
    /// <param name="str">数据组的字符串</param>
    /// <returns></returns>
    public static Dictionary<int, int> GetDictionayByString(string str)
    {
        Dictionary<int, int> dic = new Dictionary<int, int>();
        string[] array = str.Split(';');
        for (int i = 0; i < array.Length; i++)
        {
            string[] item = array[i].Split(',');
            dic.Add(int.Parse(item[0]), int.Parse(item[1]));
        }
        return dic;
    }

    /// <summary>
    /// 通过某个类的数组获得对应字典数据
    /// </summary>
    /// <typeparam name="T">类</typeparam>
    /// <param name="array">某类数组</param>
    /// <returns></returns>
    public static Dictionary<string, T> GetDicFromArray<T>(T[] array)
    {
        Dictionary<string, T> dic = new Dictionary<string, T>();

        for (int i = 0; i < array.Length; i++)
        {
            string[] values = GetPropertysValue(array[i]);//得到所有属性 要第一个属性的值 因为一般该值为ID
            dic.Add(values[0], array[i]);
        }
        return dic;
    }
    #endregion

    #region 属性与属性的值

    /// <summary>
    /// 通过类的属性名获得该属性的值
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="t">类的实例</param>
    /// <param name="name">类的某个属性名</param>
    /// <returns>返回一个转为string的对应属性的值</returns>
    public static string GetValueByPropertyName<T>(T t, string name)
    {
        return t.GetType().GetField(name).GetValue(t).ToString();
    }

    /// <summary>
    /// 更改物体对应名称的属性的值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t">物体</param>
    /// <param name="name">属性名</param>
    /// <param name="value">值</param>
    public static void ChangePropertyValueByName<T>(T t, string name, int value)
    {
        t.GetType().GetField(name).SetValue(t, value);
    }

    /// <summary>
    /// 更改物体对应名称的属性的值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t">物体</param>
    /// <param name="name">属性名</param>
    /// <param name="value">值</param>
    public static void ChangePropertyValueByName<T>(T t, string name, float value)
    {
        t.GetType().GetField(name).SetValue(t, value);
    }

    /// <summary>
    /// 更改物体对应名称的属性的值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t">物体</param>
    /// <param name="name">属性名</param>
    /// <param name="value">值</param>
    public static void ChangePropertyValueByName<T>(T t, string name, string value)
    {
        t.GetType().GetField(name).SetValue(t, value);
    }

    /// <summary>
    /// 增加物体对应名称的属性的值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t">物体</param>
    /// <param name="name">属性名</param>
    /// <param name="value">值</param>
    public static void AddValueByName<T>(T t, string name, int value)
    {
        t.GetType().GetField(name).SetValue(t, (int)t.GetType().GetField(name).GetValue(t) + value);
    }

    /// <summary>
    /// 增加物体对应名称的属性的值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t">物体</param>
    /// <param name="name">属性名</param>
    /// <param name="value">值</param>
    public static void AddValueByName<T>(T t, string name, float value)
    {
        t.GetType().GetField(name).SetValue(t, (int)t.GetType().GetField(name).GetValue(t) + value);
    }

    /// <summary>
    /// 增加物体对应名称的属性的值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t">物体</param>
    /// <param name="name">属性名</param>
    /// <param name="value">值</param>
    public static void AddValueByName<T>(T t, string name, string value)
    {
        t.GetType().GetField(name).SetValue(t, (int)t.GetType().GetField(name).GetValue(t) + value);
    }

    public static int GetAddValue<T>(T t, string name, int value)
    {
        int i = 0;
        i = (int)t.GetType().GetField(name).GetValue(t) + value;
        return i;
    }

    /// <summary>
    /// 通过某类的实例获得该类的所有属性
    /// </summary>
    /// <typeparam name="T">类的类型</typeparam>
    /// <param name="t">类的实例</param>
    /// <returns></returns>
    public static string[] GetPropertysName<T>(T t)
    {
        FieldInfo[] fieldInfos = t.GetType().GetFields();
        string[] names = new string[fieldInfos.Length];
        for (int i = 0; i < fieldInfos.Length; i++)
        {
            names[i] = fieldInfos[i].Name;
        }
        return names;
    }

    /// <summary>
    /// 通过某类的实例获得该实例的所有属性的值
    /// </summary>
    /// <typeparam name="T">类的类型</typeparam>
    /// <param name="t">类的实例</param>
    /// <returns></returns>
    public static string[] GetPropertysValue<T>(T t)
    {
        FieldInfo[] fieldInfos = t.GetType().GetFields();
        string[] values = new string[fieldInfos.Length];
        for (int i = 0; i < fieldInfos.Length; i++)
        {
            if (fieldInfos[i].GetType() == typeof(string))
            {
                values[i] = "'" + fieldInfos[i].GetValue(t).ToString() + "'";//写入数据库时原本string字段的值必须加单引号 否则报错 此处统一加单引号
            }
            else
            {
                values[i] = fieldInfos[i].GetValue(t).ToString();
            }
        }
        return values;
    }
    #endregion

    #region 数据文件操作

    /// <summary>
    /// 获取文本并转为字符串
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
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
    #endregion
}
