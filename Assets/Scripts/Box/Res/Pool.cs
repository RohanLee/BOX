using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private static Dictionary<string, Queue<GameObject>> Pools = new Dictionary<string, Queue<GameObject>>();
    private static Dictionary<string, GameObject> PoolSeeds = new Dictionary<string, GameObject>();

    /// <summary>
    /// 增加物体入种子池
    /// </summary>
    /// <param name="objectName">物体名</param>
    private static GameObject Add(string name)
    {
        GameObject go = Resources.Load(name) as GameObject;
        PoolSeeds.Add(name, go);
        return go;
    }

    /// <summary>
    /// 获取物体
    /// 无论物体在不在池中都可使用
    /// </summary>
    /// <param name="name">物体名</param>
    /// <returns></returns>
    public static GameObject Get(string name)
    {
        GameObject go = null;
        if (!Pools.ContainsKey(name))
        {
            if (!PoolSeeds.ContainsKey(name))
            {
                Add(name);
            }
            go = MonoBehaviour.Instantiate(PoolSeeds[name]);
            go.name = name;
            Queue<GameObject> queue = new Queue<GameObject>();
            queue.Enqueue(go);
            queue.TrimExcess();
            Pools.Add(name, queue);
            go.SetActive(true);
            return go;
        }

        if (Pools[name].Count == 0)
        {
            go = MonoBehaviour.Instantiate(PoolSeeds[name]);
            go.name = name;
            go.SetActive(true);
            return go;
        }

        go = Pools[name].Dequeue();
        Pools[name].TrimExcess();
        go.SetActive(true);

        //if (Pools[name].Count == 0)
        //{
        //    Pools.Remove(name);
        //}

        return go;
    }
    /// <summary>
    /// 获取物体并放到父物体下
    /// </summary>
    /// <param name="name">物体名</param>
    /// <param name="trm">父物体</param>
    /// <returns></returns>
    public static GameObject Get(string name, Transform trm)
    {
        GameObject go = Get(name);
        go.transform.parent = trm;
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
        go.transform.localEulerAngles = Vector3.zero;
        return go;
    }
    

    /// <summary>
    /// 回收物体入池
    /// </summary>
    public static void Recycle(GameObject go)
    {
        go.SetActive(false);
        if (!Pools.ContainsKey(go.name))
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            queue.Enqueue(go);
            queue.TrimExcess();
            Pools.Add(go.name, queue);
            return;
        }
        Pools[go.name].Enqueue(go);
        Pools[go.name].TrimExcess();
    }

    /// <summary>
    /// 回收物体入池并重置位置
    /// </summary>
    public static void Recycle(GameObject go, Vector3 pos)
    {
        go.transform.position = pos;
        Recycle(go);
    }

    /// <summary>
    /// 回收物体入池并重置位置 尺寸
    /// </summary>
    public static void Recycle(GameObject go, Vector3 pos, Vector3 size)
    {
        go.transform.position = pos;
        go.transform.localScale = size;
        Recycle(go);
    }

    /// <summary>
    /// 回收物体入池并重置位置 尺寸 角度
    /// </summary>
    public static void Recycle(GameObject go, Vector3 pos, Vector3 size, Vector3 euler)
    {
        go.transform.position = pos;
        go.transform.localScale = size;
        go.transform.eulerAngles = euler;
        Recycle(go);
    }

    /// <summary>
    /// 回收物体入池 设置最大容纳数量
    /// </summary>
    public static void Recycle(GameObject go, int maxNum)
    {
        if (Pools.ContainsKey(go.name) && Pools[go.name].Count > maxNum)
        {
            MonoBehaviour.Destroy(go);
            return;
        }
        Recycle(go);
    }

    /// <summary>
    /// 回收物体入池 设置最大容纳数量 并重置位置
    /// </summary>
    public static void Recycle(GameObject go, int maxNum, Vector3 pos)
    {
        if (Pools.ContainsKey(go.name) && Pools[go.name].Count > maxNum)
        {
            MonoBehaviour.Destroy(go);
            return;
        }
        Recycle(go, pos);
    }

    /// <summary>
    /// 回收物体入池 设置最大容纳数量 并重置位置 尺寸
    /// </summary>
    public static void Recycle(GameObject go, int maxNum, Vector3 pos, Vector3 size)
    {
        if (Pools.ContainsKey(go.name) && Pools[go.name].Count > maxNum)
        {
            MonoBehaviour.Destroy(go);
            return;
        }
        Recycle(go, pos, size);
    }

    /// <summary>
    /// 回收物体入池 设置最大容纳数量 并重置位置 尺寸 角度
    /// </summary>
    public static void Recycle(GameObject go, int maxNum, Vector3 pos, Vector3 size, Vector3 euler)
    {
        if (Pools.ContainsKey(go.name) && Pools[go.name].Count > maxNum)
        {
            MonoBehaviour.Destroy(go);
            return;
        }
        Recycle(go, pos, size, euler);
    }

    /// <summary>
    /// 销毁池物体
    /// </summary>
    /// <param name="objectName">物体名</param>
    public static void Destroy(string objectName)
    {
        MonoBehaviour.Destroy(Pools[objectName].Dequeue());
        Pools.Remove(objectName);
    }
}
