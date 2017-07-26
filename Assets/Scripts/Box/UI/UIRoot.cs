using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Init The UI Root
/// UIRoot
/// -Canvas
/// --FixedRoot
/// --NormalRoot
/// --PopupRoot
/// -Camera
/// </summary>
public class UIRoot : MonoBehaviour
{
    public static UIRoot Instance;
    public Transform root, fixedRoot, normalRoot, popupRoot;
    public Camera uiCamera;

    /// <summary>
    /// 这样写可在换场景时保留UI 
    /// 避免切换场景时增加UI的情况
    /// </summary>
    static UIRoot()
    {
        GameObject go = new GameObject("UIRoot");
        DontDestroyOnLoad(go);
        go.layer = LayerMask.NameToLayer("UI");
        Instance = go.AddComponent<UIRoot>();
        go.AddComponent<RectTransform>();
        Instance.root = go.transform;

        Canvas can = go.AddComponent<Canvas>();
        can.renderMode = RenderMode.ScreenSpaceCamera;
        can.pixelPerfect = true;
        GameObject camObj = new GameObject("UICamera");
        camObj.layer = LayerMask.NameToLayer("UI");
        camObj.transform.parent = go.transform;
        camObj.transform.localPosition = new Vector3(0, 0, -100f);
        Camera cam = camObj.AddComponent<Camera>();
        cam.clearFlags = CameraClearFlags.Depth;
        cam.orthographic = true;
        cam.farClipPlane = 200f;
        can.worldCamera = cam;
        Instance.uiCamera = cam;
        cam.cullingMask = 1 << 5;
        cam.nearClipPlane = -50f;
        cam.farClipPlane = 50f;

        //add audio listener
        //camObj.AddComponent<AudioListener>();
        camObj.AddComponent<GUILayer>();

        CanvasScaler cs = go.AddComponent<CanvasScaler>();
        cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        cs.referenceResolution = new Vector2(1136f, 640f);
        cs.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;

        ////add auto scale camera fix size.
        //TTCameraScaler tcs = go.AddComponent<TTCameraScaler>();
        //tcs.scaler = cs;

        //set the raycaster
        //GraphicRaycaster gr = go.AddComponent<GraphicRaycaster>();

        GameObject subRoot = CreateSubCanvasForRoot(go.transform, 250);
        subRoot.name = "FixedRoot";
        Instance.fixedRoot = subRoot.transform;

        subRoot = CreateSubCanvasForRoot(go.transform, 0);
        subRoot.name = "NormalRoot";
        Instance.normalRoot = subRoot.transform;

        subRoot = CreateSubCanvasForRoot(go.transform, 500);
        subRoot.name = "PopupRoot";
        Instance.popupRoot = subRoot.transform;

        //add Event System
        GameObject esObj = GameObject.Find("EventSystem");
        if (esObj != null)
        {
            GameObject.DestroyImmediate(esObj);
        }

        GameObject eventObj = new GameObject("EventSystem");
        eventObj.layer = LayerMask.NameToLayer("UI");
        eventObj.transform.SetParent(go.transform);
        eventObj.AddComponent<EventSystem>();
        eventObj.AddComponent<StandaloneInputModule>();
    }

    static GameObject CreateSubCanvasForRoot(Transform root, int sort)
    {
        GameObject go = new GameObject("canvas");
        go.transform.parent = root;
        go.layer = LayerMask.NameToLayer("UI");

        Canvas can = go.AddComponent<Canvas>();
        RectTransform rect = go.GetComponent<RectTransform>();
        rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
        rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.localScale = Vector3.one;

        can.overrideSorting = true;
        can.sortingOrder = sort;

        go.AddComponent<GraphicRaycaster>();

        return go;
    }

    void OnDestroy()
    {
        Instance = null;
    }
}
