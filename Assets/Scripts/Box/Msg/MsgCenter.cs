using System;
using System.Collections.Generic;

/// <summary>
/// 消息中心
/// 传入的自定义的消息名为：类名 + "_" + 方法名
/// </summary>
public class MsgCenter
{
    private static Dictionary<string, Action<object[]>> msgHanldes = new Dictionary<string, Action<object[]>>();

    /// <summary>
    /// 消息注册
    /// </summary>
    /// <param name="eventNameEnum">MsgTag中定义的消息的值</param>
    /// <param name="func">对应的方法名</param>
    public static void MsgRegister(string eventName, Action<object[]> func)
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

    /// <summary>
    /// 移除消息
    /// </summary>
    /// <param name="eventNameEnum">MsgTag中定义的消息的值</param>
    /// <param name="func">对应的方法</param>
    public static void MsgRemove(string eventName, Action<object[]> func)
    {
        if (msgHanldes.ContainsKey(eventName))
        {
            msgHanldes[eventName] -= func;
            if (null == msgHanldes[eventName])
                msgHanldes.Remove(eventName);
        }
    }

    /// <summary>
    /// 消息触发事件
    /// </summary>
    /// <param name="eventNameEnum">MsgTag中定义的消息的值</param>
    /// <param name="objs">参数</param>
    /// <returns></returns>
    public static bool MsgTigger(string eventName, params object[] objs)
    {
        Action<object[]> fun;
        if (msgHanldes.TryGetValue(eventName, out fun))
        {
            fun(objs);
            return true;
        }
        return false;
    }
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