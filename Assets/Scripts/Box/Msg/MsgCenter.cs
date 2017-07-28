using System;
using System.Collections.Generic;

/// <summary>
/// 消息中心
/// 传入的自定义的消息名为：类名 + "_" + 方法名
/// </summary>
public class MsgCenter
{
    private static Dictionary<string, Action<object[]>> msgHanldeArray = new Dictionary<string, Action<object[]>>();
    private static Dictionary<string, Action<object>> msgHanlde = new Dictionary<string, Action<object>>();
    private static Dictionary<string, Action<object, object>> msgHanldes = new Dictionary<string, Action<object, object>>();


    #region 消息注册

    /// <summary>
    /// 消息注册 带2个以上参数的
    /// </summary>
    /// <param name="eventNameEnum">消息名 也可用方法名简化替代</param>
    /// <param name="func">对应的方法名</param>
    public static void MsgRegister(string eventName, Action<object[]> func)
    {
        if (msgHanldeArray.ContainsKey(eventName))
        {
            msgHanldeArray[eventName] += func;
        }
        else
        {
            msgHanldeArray.Add(eventName, func);
        }
    }

    /// <summary>
    /// 消息注册 带一个参数
    /// </summary>
    /// <param name="eventName">消息名 也可用方法名简化替代</param>
    /// <param name="func">对应的方法</param>
    public static void MsgRegister(string eventName, Action<object> func)
    {
        if (msgHanlde.ContainsKey(eventName))
        {
            msgHanlde[eventName] += func;
        }
        else
        {
            msgHanlde.Add(eventName, func);
        }
    }

    /// <summary>
    /// 消息注册 带两个参数
    /// </summary>
    /// <param name="eventName">消息名 也可用方法名简化替代</param>
    /// <param name="func">对应的方法</param>
    public static void MsgRegister(string eventName, Action<object, object> func)
    {
        if (msgHanldes.ContainsKey(eventName))
        {
            msgHanldes[eventName] += func;
        }
        else
        {
            msgHanldes.Add(eventName, func);
        }
    }

    #endregion

    #region 消息触发

    /// <summary>
    /// 消息触发事件 带两个以上参数
    /// </summary>
    /// <param name="eventNameEnum">消息名 也可用方法名简化替代</param>
    /// <param name="objs">参数组</param>
    /// <returns></returns>
    public static bool MsgTigger(string eventName, params object[] objs)
    {
        Action<object[]> fun;
        if (msgHanldeArray.TryGetValue(eventName, out fun))
        {
            fun(objs);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 消息触发事件 带一个参数
    /// </summary>
    /// <param name="eventName">消息名 也可用方法名简化替代</param>
    /// <param name="obj">参数</param>
    /// <returns></returns>
    public static bool MsgTigger(string eventName, object obj)
    {
        Action<object> fun;
        if (msgHanlde.TryGetValue(eventName, out fun))
        {
            fun(obj);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 消息触发事件 带两个参数
    /// </summary>
    /// <param name="eventName">消息名 也可用方法名简化替代</param>
    /// <param name="obj1">参数1</param>
    /// <param name="obj2">参数2</param>
    /// <returns></returns>
    public static bool MsgTigger(string eventName, object obj1, object obj2)
    {
        Action<object, object> fun;
        if (msgHanldes.TryGetValue(eventName, out fun))
        {
            fun(obj1, obj2);
            return true;
        }
        return false;
    }

    #endregion

    #region 消息移除

    /// <summary>
    /// 移除消息 带两个以上参数
    /// </summary>
    /// <param name="eventNameEnum">消息名 也可用方法名简化替代</param>
    /// <param name="func">对应的方法</param>
    public static void MsgRemove(string eventName, Action<object[]> func)
    {
        if (msgHanldeArray.ContainsKey(eventName))
        {
            msgHanldeArray[eventName] -= func;
            if (null == msgHanldeArray[eventName])
                msgHanldeArray.Remove(eventName);
        }
    }

    /// <summary>
    /// 移除消息 带一个参数
    /// </summary>
    /// <param name="eventName">消息名 也可用方法名简化替代</param>
    /// <param name="func"></param>
    public static void MsgRemove(string eventName, Action<object> func)
    {
        if (msgHanlde.ContainsKey(eventName))
        {
            msgHanlde[eventName] -= func;
            if (null == msgHanlde[eventName])
                msgHanlde.Remove(eventName);
        }
    }

    /// <summary>
    /// 移除消息 带两个参数
    /// </summary>
    /// <param name="eventName">消息名 也可用方法名简化替代</param>
    /// <param name="func"></param>
    public static void MsgRemove(string eventName, Action<object, object> func)
    {
        if (msgHanldes.ContainsKey(eventName))
        {
            msgHanldes[eventName] -= func;
            if (null == msgHanldes[eventName])
                msgHanldes.Remove(eventName);
        }
    }

    #endregion

}

#region 使用示例

/*
public class Register
{
    void Start()
    {
        MsgCenter.MsgRegister("Register_MsgRegister", MsgRegister);
    }

    void MsgRegister(object[] objs)
    {
        int[] nums = (int[])objs[0];
        string str = objs[1].ToString();
        List<float> fts = (List<float>)objs[2];
        //任意允许类型即可放入object[]中并进行操作
    }
}

public class Trigger
{
    void DoSomeThing()
    {
        List<float> fts = new List<float>();
        fts.Add(0f);
        fts.Add(0.1f);
        fts.Add(0.2f);
        Object[] objs = new object[] { new int[] { 0, 1, 2 }, "test", fts };//只要object[]中的参数的类型/数量/位置与消息定义是方法参数的位置一致即可
        MsgCenter.MsgTigger("Register_MsgRegister", objs);
    }
}
*/

#endregion