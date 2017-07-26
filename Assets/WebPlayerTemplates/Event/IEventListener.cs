using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 监听接口
/// </summary>
public interface IEventListener
{
    /// <summary>
    /// 处理消息
    /// </summary>
    /// <param name="id">消息ID</param>
    /// <param name="parm1">参数1</param>
    /// <param name="parm2">参数2</param>
    /// <returns></returns>
    bool HandleEvent ( int id, object parm1, object parm2 );

    /// <summary>
    /// 消息优先级
    /// </summary>
    /// <returns></returns>
    int EventPriority ( );
}
