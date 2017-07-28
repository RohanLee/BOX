using UnityEngine;

/// <summary>
/// 本地数据存储调用
/// </summary>
public class LocalData
{
    public static int intTemp = PlayerPrefs.GetInt("intTemp");
    public static float floatTemp = PlayerPrefs.GetFloat("floatTemp");
    public static string stringTemp = PlayerPrefs.GetString("stringTemp");

    /// <summary>
    /// 获取本地数据 获取时要强转类型
    /// </summary>
    /// <param name="name">数据名</param>
    /// <returns></returns>
    public static object LocalDataGet(string name)
    {
        if (PlayerPrefs.HasKey(name))
        {
            if (PlayerPrefs.GetString(name) != string.Empty)
            {
                return PlayerPrefs.GetString(name);
            }

            if (PlayerPrefs.GetFloat(name) != 0)
            {
                return PlayerPrefs.GetFloat(name);
            }

            return PlayerPrefs.GetInt(name);
        }
        Debug.LogError("获取本地数据时 输入的数据名不存在！！！");
        return null;
    }

    /// <summary>
    /// 保存本地数据
    /// </summary>
    /// <param name="name">数据名</param>
    /// <param name="value">数据值</param>
    public static void LocalDataSave(string name, object value)
    {
        if (value.GetType() == typeof(int))
        {
            PlayerPrefs.SetInt(name, (int)value);
        }
        else if (value.GetType() == typeof(float))
        {
            PlayerPrefs.SetFloat(name, (float)value);
        }
        else if (value.GetType() == typeof(string))
        {
            PlayerPrefs.SetString(name, value.ToString());
        }
        else
        {
            Debug.LogError("存储本地数据时，输入的数据类型必须是int/float/string中的一种！！！");
        }
    }
}
