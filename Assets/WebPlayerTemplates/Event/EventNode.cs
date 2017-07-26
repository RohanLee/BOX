using System.Collections.Generic;
using UnityEngine;

public class EventNode : MonoBehaviour
{
    public int EventNodePriority { set; get; }

    //消息集合存放字典
    private Dictionary<int, List<IEventListener>> mListeners = new Dictionary<int, List<IEventListener>> ( );

    //消息节点组
    private List<EventNode> mNodeList = new List<EventNode> ( );

    /// <summary>
    /// 挂载一个消息节点到当前消息节点组上
    /// </summary>
    /// <param name="node">消息节点</param>
    public bool AttachEventNode ( EventNode node )
    {
        if (node == null)
        {
            Debug.LogError ( "挂载消息节点到节点组时节点为空！！！" );
            return false;
        }

        if (mNodeList.Contains ( node ))
        {
            Debug.LogError ( "挂载消息节点到节点组时节点已存在！！！" );
            return false;
        }

        int pos = 0;
        while (node.EventNodePriority <= mNodeList[pos].EventNodePriority)
        {
            pos++;
        }
        mNodeList.Insert ( pos, node );
        return true;
    }

    /// <summary>
    /// 从当前消息节点组卸载一个指定节点
    /// </summary>
    /// <param name="node">消息节点</param>
    /// <returns></returns>
    public bool DetachEventNode ( EventNode node )
    {
        if (!mNodeList.Contains ( node ))
        {
            Debug.LogError ( "当前节点组内不存在指定要卸载的节点！！！" );
            return false;
        }
        mNodeList.Remove ( node );
        return true;
    }

    /// <summary>
    /// 挂载一个消息监听器到指定节点
    /// </summary>
    /// <param name="key">指定位置</param>
    /// <param name="listener">消息监听器</param>
    public bool AttachEventListener ( int key, IEventListener listener )
    {
        if (listener == null)
        {
            Debug.LogError ( "挂载消息监听器时输入的监听器为空！！！" );
            return false;
        }

        if (!mListeners.ContainsKey ( key ))
        {
            mListeners.Add ( key, new List<IEventListener> ( ) { listener } );
            return true;
        }

        if (mListeners[key].Contains ( listener ))
        {
            return false;
        }
        
        int pos = 0;
        while (listener.EventPriority ( ) <= mListeners[key][pos].EventPriority ( ))
        {
            pos++;
        }
        mListeners[key].Insert ( pos, listener );
        return true;
    }

    /// <summary>
    /// 从指定节点卸载一个消息监听器
    /// </summary>
    /// <param name="key">指定位置</param>
    /// <param name="listener">指定监听器</param>
    /// <returns></returns>
    public bool DetachEventListener ( int key, IEventListener listener )
    {
        if (mListeners.ContainsKey ( key ) && mListeners[key].Contains ( listener ))
        {
            mListeners[key].Remove ( listener );
            return true;
        }
        return false;
    }

    /// <summary>
    /// 发送事件消息
    /// </summary>
    /// <param name="key">指定位置</param>
    /// <param name="parm1">预留参数1</param>
    /// <param name="parm2">预留参数2</param>
    public void SendEvent ( int key, object parm1, object parm2 )
    {
        DispatchEvent ( key, parm1, parm2 );
    }

    /// <summary>
    /// 派发消息到子消息节点以及自己节点下的监听器上
    /// </summary>
    /// <param name="key">指定位置</param>
    /// <param name="parm1">预留参数1</param>
    /// <param name="parm2">预留参数2</param>
    /// <returns></returns>
    private bool DispatchEvent ( int key, object parm1, object parm2 )
    {
        for (int i = 0; i < mNodeList.Count; i++)
        {
            if (mNodeList[i].DispatchEvent ( key, parm1, parm2 ))
            {
                return true;
            }
        }
        return TriggerEvent ( key, parm1, parm2 );
    }

    /// <summary>
    /// 消息触发
    /// </summary>
    /// <param name="key">指定位置</param>
    /// <param name="parm1">预留参数1</param>
    /// <param name="parm2">预留参数2</param>
    /// <returns></returns>
    private bool TriggerEvent ( int key, object parm1, object parm2 )
    {
        if (!this.gameObject.activeSelf || !this.gameObject.activeInHierarchy || !this.enabled)
        {
            return false;
        }

        if (!mListeners.ContainsKey ( key ))
        {
            return false;
        }

        List<IEventListener> listeners = mListeners[key];
        for (int i = 0; i < listeners.Count; i++)
        {
            if (listeners[i].HandleEvent ( key, parm1, parm2 ))
            {
                return true;
            }
        }
        return false;
    }

    void OnApplicationQuit ( )
    {
        mListeners.Clear ( );
        mNodeList.Clear ( );
    }

    //int key = DataTools.GetMinimalUsableIDFromeDictionary ( mListeners );这段代码可获得int键值字典最小可用键值
}
