using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 本地化语言文字控制
/// </summary>
public class Localization
{
    static Dictionary<string, string> mLanguages = new Dictionary<string, string>();
    static string path = "Language";

    /// <summary>
    /// 初始化本地语言文本数据
    /// </summary>
    public static void Init()
    {
        mLanguages = Stringtodictionary(path);
    }

    /// <summary>
    /// 将本地化语言文本转为字典的方法
    /// </summary>
    /// <param name="path">文本存储全路径</param>
    /// <returns></returns>
    static Dictionary<string, string> Stringtodictionary(string path)
    {
        Dictionary<string, string> _dic = new Dictionary<string, string>();

        TextAsset asset = Resources.Load<TextAsset>(path);//获得文本数据

        string[] strs = asset.text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);//根据换行符获取字符串组

        for (int i = 0; i < strs.Length; i++)
        {
            if (strs[i].StartsWith("/")) continue;//如果有注释符或空字符开头就忽略
            string[] split = strs[i].Split(new char[] { '=' }, 2, System.StringSplitOptions.RemoveEmptyEntries);//以等号分割并删除数组中的空值

            if (split.Length == 2)
            {
                string key = split[0].Trim();//删除字符串前后空格
                string value = split[1].Trim().Replace("\\n", "\n");
                _dic.Add(key, value);
            }
        }
        return _dic;
    }

    /// <summary>
    /// 通过键值获取相应文字
    /// </summary>
    /// <param name="key">键值</param>
    /// <returns></returns>
    public static string Get(string key)
    {
        return mLanguages[key];
    }
}
