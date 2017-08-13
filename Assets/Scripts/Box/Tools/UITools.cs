using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UITools
{
    /// <summary>
    /// 世界坐标转UI坐标
    /// </summary>
    /// <param name="canvas">当前画布</param>
    /// <param name="go">要转换的世界坐标位置</param>
    /// <returns></returns>
    public static Vector3 WorldToUIPoint(Canvas canvas, Vector3 pos)
    {
        Vector3 screenPositon = Camera.main.WorldToScreenPoint(pos);//世界坐标转屏幕坐标
        Vector3 uiPosition = canvas.worldCamera.ScreenToWorldPoint(screenPositon);//屏幕坐标转当前画布相机坐标
        return new Vector3(uiPosition.x, uiPosition.y, canvas.GetComponent<RectTransform>().anchoredPosition3D.z);//返回坐标重定位z轴坐标为当前画布z轴坐标
    }

    //可以增加一些常用的置灰颜色

    /// <summary>
    /// 字符串长度(按字节算)
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int GetStringLength(string str)
    {
        int len = 0;
        byte[] b;

        for (int i = 0; i < str.Length; i++)
        {
            b = Encoding.Default.GetBytes(str.Substring(i, 1));
            if (b.Length > 1)
                len += 2;
            else
                len++;
        }
        return len;
    }

    /// <summary>
    /// 截取指定长度字符串(按字节算)
    /// </summary>
    /// <param name="str"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string GetStringPart(string str, int length)
    {
        int len = 0;
        byte[] b;
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < str.Length; i++)
        {
            b = Encoding.Default.GetBytes(str.Substring(i, 1));
            if (b.Length > 1)
                len += 2;
            else
                len++;

            if (len > length)
                break;

            sb.Append(str[i]);
        }

        return sb.ToString();
    }

    /// <summary>
    /// 输入以秒为单位的时间 获得日时分秒的字符串
    /// </summary>
    /// <param name="timeNum"></param>
    /// <returns></returns>
    public static string GetTimeStringByTimeNum(long timeNum)
    {
        List<string> times = new List<string>();
        string time = string.Empty;

        string hour = (timeNum / 3600).ToString();
        times.Add(hour);

        string minute = ((timeNum % 3600) / 60).ToString();
        times.Add(minute);

        string second = ((timeNum % 3600) % 60).ToString();
        times.Add(second);

        for (int i = 0; i < times.Count; i++)
        {
            time += times[i].ToString() + ":";
        }
        time = time.Substring(0, time.Length - 1);//去掉最后一个冒号
        return time;
    }

    /// <summary>
    /// 时间格式的值转为长整数型
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static long ConvertDateTimeToLong(DateTime time)
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2017, 1, 1));
        return (long)(time - startTime).TotalSeconds;
    }

    /// <summary>
    /// long数值转为时间格式
    /// </summary>
    /// <param name="timeStamp"></param>
    /// <returns></returns>
    private DateTime ConvertLongToDateTime(long timeStamp)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2017, 1, 1));
        long lTime = long.Parse(timeStamp + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);
        return dtStart.Add(toNow);
    }

    /// <summary>
    /// 货币显示简化的方法
    /// </summary>
    /// <param name="num">货币数量</param>
    /// <returns></returns>
    public static string CoinNumShowSimplify(int num)
    {
        string str = string.Empty;

        if (num >= 1000000)
        {
            str = (num / 1000000).ToString() + "m";
            if ((num % 1000000).ToString().Substring(0, 1) != 0.ToString())
            {
                str = (num / 1000000).ToString() + "." + (num % 1000000).ToString().Substring(0, 1) + "m";
            }
            return str;
        }
        if (num >= 1000)
        {
            str = (num / 1000).ToString() + "k";
            if ((num % 1000).ToString().Substring(0, 1) != 0.ToString() && str.Length < 3)
            {
                str = (num / 1000).ToString() + "." + (num % 1000).ToString().Substring(0, 1) + "k";
            }
            return str;
        }
        str = num.ToString();
        return str;
    }

    /// <summary>
    /// 动态播放声音
    /// </summary>
    /// <param name="source"></param>
    /// <param name="str"></param>
    public static void PlaySound(GameObject go, string str)
    {
        AudioSource source = go.GetComponent<AudioSource>() ? go.GetComponent<AudioSource>() : go.AddComponent<AudioSource>();
        AudioClip clip = (AudioClip)Resources.Load(str, typeof(AudioClip));//调用Resources方法加载AudioClip资源
        source.clip = clip;
        source.Play();
    }
}
