using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GameTools
{
    /// <summary>
    /// 生成物体并挂载的方法 主要用于列表生成
    /// </summary>
    /// <param name="parent">父物体</param>
    /// <param name="item">要生成的物体</param>
    /// <returns></returns>
    public static GameObject CreateObject ( Transform parent, GameObject item )
    {
        GameObject go = MonoBehaviour.Instantiate ( item ) as GameObject;
        go.transform.parent = parent;
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
        return go;
    }
    
    /// <summary>
    /// 字符串长度(按字节算)
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int GetStringLength ( string str )
    {
        int len = 0;
        byte[] b;

        for (int i = 0; i < str.Length; i++)
        {
            b = Encoding.Default.GetBytes ( str.Substring ( i, 1 ) );
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
    public static string GetStringPart ( string str, int length )
    {
        int len = 0;
        byte[] b;
        StringBuilder sb = new StringBuilder ( );

        for (int i = 0; i < str.Length; i++)
        {
            b = Encoding.Default.GetBytes ( str.Substring ( i, 1 ) );
            if (b.Length > 1)
                len += 2;
            else
                len++;

            if (len > length)
                break;

            sb.Append ( str[i] );
        }

        return sb.ToString ( );
    }

    /// <summary>
    /// 输入以秒为单位的时间 获得日时分秒的字符串
    /// </summary>
    /// <param name="timeNum"></param>
    /// <returns></returns>
    public static string GetTimeStringByTimeNum ( long timeNum )
    {
        List<string> times = new List<string> ( );
        string time = string.Empty;

        string hour = (timeNum / 3600).ToString ( );
        times.Add ( hour );

        string minute = ((timeNum % 3600) / 60).ToString ( );
        times.Add ( minute );

        string second = ((timeNum % 3600) % 60).ToString ( );
        times.Add ( second );

        for (int i = 0; i < times.Count; i++)
        {
            time += times[i].ToString ( ) + ":";
        }
        time = time.Substring ( 0, time.Length - 1 );//去掉最后一个冒号
        return time;
    }

    /// <summary>
    /// 时间格式的值转为长整数型
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static long ConvertDateTimeToLong ( DateTime time )
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime ( new DateTime ( 2017, 1, 1 ) );
        return (long)(time - startTime).TotalSeconds;
    }

    /// <summary>
    /// long数值转为时间格式
    /// </summary>
    /// <param name="timeStamp"></param>
    /// <returns></returns>
    private DateTime ConvertLongToDateTime ( long timeStamp )
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime ( new DateTime ( 2017, 1, 1 ) );
        long lTime = long.Parse ( timeStamp + "0000000" );
        TimeSpan toNow = new TimeSpan ( lTime );
        return dtStart.Add ( toNow );
    }
}
