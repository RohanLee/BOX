using UnityEngine;

public class GameTools
{
    /// <summary>
    /// 生成物体并挂载的方法 主要用于列表生成
    /// </summary>
    /// <param name="parent">父物体</param>
    /// <param name="item">要生成的物体</param>
    /// <returns></returns>
    public static GameObject CreateObject(Transform parent, GameObject go)
    {
        GameObject _go = MonoBehaviour.Instantiate(go) as GameObject;
        go.transform.parent = parent;
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
        go.transform.localEulerAngles = Vector3.zero;
        return go;
    }
    /// <summary>
    /// 生成物体
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static GameObject CreateObject(GameObject go)
    {
        return MonoBehaviour.Instantiate(go) as GameObject;
    }
}
