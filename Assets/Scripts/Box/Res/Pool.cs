using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private static Dictionary<string, Queue< GameObject>> mPools = new Dictionary<string, Queue<GameObject>>();
    private static int maxCount = 20;//定义池的最大容量数

    /// <summary>
    /// 获取物体
    /// 无论物体在不在池中都可使用
    /// </summary>
    /// <param name="path">存放路径</param>
    /// <param name="name">物体名</param>
    /// <returns></returns>
    public static GameObject PoolGet(string path, string name)
    {
        GameObject go = null;
        if (!mPools.ContainsKey(name))
        {
            go = PoolAdd(path, name);
            go.SetActive(true);
            return go;
        }

        go = mPools[name].Peek();
        go.SetActive(true);
        mPools[name].Dequeue();
        mPools[name].TrimExcess();
        if(mPools[name].Count == 0)
        {
            mPools.Remove(name);
        }
        return go;
    }

    /// <summary>
    /// 增加物体入池
    /// </summary>
    /// <param name="path">物体存放路径</param>
    /// <param name="objectName">物体名</param>
    public static GameObject PoolAdd(string path, string name)
    {
        GameObject go = null;
        go = Instantiate(Resources.Load(path + name) as GameObject);
        Queue<GameObject> queueGo = new Queue<GameObject>();
        queueGo.Enqueue(go);
        mPools.Add(name, queueGo);
        go.SetActive(false);
        return go;
    }

    public static void PoolBack(string path, GameObject go)
    {
        if(!mPools.ContainsKey(go.name))
        {
            PoolAdd(path, go.name);
            return;
        }

        go.SetActive(false);
        mPools[go.name].Enqueue(go);
    }

    /// <summary>
    /// 销毁池物体
    /// </summary>
    /// <param name="objectName">物体名</param>
    public static void PoolDestroy(string objectName)
    {
        Destroy(mPools[objectName].Dequeue());
        mPools.Remove(objectName);
    }
}
