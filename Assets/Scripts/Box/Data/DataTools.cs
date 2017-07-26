using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class DataTools
{
    /// <summary>
    /// 查找一个int值为索引的字典最小可用的Key值 前提是key不会小于0
    /// </summary>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    public static int GetMinimalUsableIDFromeDictionary<T>(Dictionary<int, T> dictionary)
    {
        int key = 0;
        while (dictionary.ContainsKey ( key ))
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
    public static Dictionary<int, int> GetDictionayByString ( string str )
    {
        Dictionary<int, int> dic = new Dictionary<int, int> ( );
        string[] array = str.Split ( ';' );
        for (int i = 0; i < array.Length; i++)
        {
            string[] item = array[i].Split ( ',' );
            dic.Add ( int.Parse ( item[0] ), int.Parse ( item[1] ) );
        }
        return dic;
    }

    /// <summary>
    /// 通过类的属性名获得该属性的值
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="t">类的实例</param>
    /// <param name="name">类的某个属性名</param>
    /// <returns>返回一个转为string的对应属性的值</returns>
    public static string GetValueByPropertyName<T> ( T t, string name )
    {
        return t.GetType ( ).GetField ( name ).GetValue ( t ).ToString ( );
    }

    /// <summary>
    /// 更改物体对应名称的属性的值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t">物体</param>
    /// <param name="name">属性名</param>
    /// <param name="value">值</param>
    public static void ChangePropertyValueByName<T> ( T t, string name, int value )
    {
        t.GetType ( ).GetField ( name ).SetValue ( t, value );
    }

    /// <summary>
    /// 更改物体对应名称的属性的值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t">物体</param>
    /// <param name="name">属性名</param>
    /// <param name="value">值</param>
    public static void ChangePropertyValueByName<T> ( T t, string name, float value )
    {
        t.GetType ( ).GetField ( name ).SetValue ( t, value );
    }

    /// <summary>
    /// 更改物体对应名称的属性的值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t">物体</param>
    /// <param name="name">属性名</param>
    /// <param name="value">值</param>
    public static void ChangePropertyValueByName<T> ( T t, string name, string value )
    {
        t.GetType ( ).GetField ( name ).SetValue ( t, value );
    }
    
    /// <summary>
    /// 增加物体对应名称的属性的值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t">物体</param>
    /// <param name="name">属性名</param>
    /// <param name="value">值</param>
    public static void AddValueByName<T> ( T t, string name, int value )
    {
        t.GetType ( ).GetField ( name ).SetValue ( t, (int)t.GetType ( ).GetField ( name ).GetValue ( t ) + value );
    }

    /// <summary>
    /// 增加物体对应名称的属性的值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t">物体</param>
    /// <param name="name">属性名</param>
    /// <param name="value">值</param>
    public static void AddValueByName<T> ( T t, string name, float value )
    {
        t.GetType ( ).GetField ( name ).SetValue ( t, (int)t.GetType ( ).GetField ( name ).GetValue ( t ) + value );
    }

    /// <summary>
    /// 增加物体对应名称的属性的值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t">物体</param>
    /// <param name="name">属性名</param>
    /// <param name="value">值</param>
    public static void AddValueByName<T> ( T t, string name, string value )
    {
        t.GetType ( ).GetField ( name ).SetValue ( t, (int)t.GetType ( ).GetField ( name ).GetValue ( t ) + value );
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
    /// <param name="obj">类的实例</param>
    /// <returns></returns>
    public static string[] GetPropertysName<T>(T obj)
    {
        List<string> nameList = new List<string>();
        Type type = obj.GetType();
        FieldInfo[] fieldInfoList = type.GetFields();
        int i = 0;
        foreach (FieldInfo item in fieldInfoList)
        {
            nameList.Add(item.Name);
            i++;
        }
        string[] names = new string[nameList.Count];
        for (int j = 0; j < nameList.Count; j++)
        {
            names[j] = nameList[j];
        }
        return names;
    }

    /// <summary>
    /// 通过某类的实例获得该实例的所有属性的值
    /// </summary>
    /// <typeparam name="T">类的类型</typeparam>
    /// <param name="obj">类的实例</param>
    /// <returns></returns>
    public static string[] GetPropertysValue<T>(T obj)
    {
        List<string> valueList = new List<string>();
        Type type = obj.GetType();
        FieldInfo[] fieldInfoList = type.GetFields();
        int i = 0;
        foreach (FieldInfo item in fieldInfoList)
        {
            valueList.Add(item.GetValue(obj).ToString());
            i++;
        }
        string[] values = new string[valueList.Count];
        for (int j = 0; j < valueList.Count; j++)
        {
            //Debug.LogError ( valueList[j] );
            values[j] = "'" + valueList[j] + "'";//写入数据库时原本string字段的值必须加单引号 否则报错 此处统一加单引号
        }
        return values;
    }
}
