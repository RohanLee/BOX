using UnityEngine;

/// <summary>
/// 定义视角类型
/// </summary>
public enum ViewType
{
    Free = 0,
    God,
    Top,
    Side,
}

/// <summary>
/// 视觉控制
/// </summary>
public class View
{
    static Camera viewCamera;

    static float moveDelay = 0.5f;
    static float rotateSpeed = 5f;
    static float rotateMinX = 3f;
    static float rotateMaxX = 89f;
    static float zoomSpeed = 100f;
    static float viewDistance;

    static Vector3 velocity = Vector3.zero;
    static Vector3 viewOffset;
    static Vector3 coreOffset;

    


    public static void ViewInit(ViewType viewType, Transform viewTarget)
    {
        viewCamera = Camera.main;

        switch (viewType)
        {
            case ViewType.Free:
                coreOffset = new Vector3(0f, 6f, -10f);
                viewOffset = coreOffset;
                viewCamera.transform.position = viewTarget.position + viewOffset;
                viewDistance = Vector3.Distance(viewCamera.transform.position, viewTarget.position);
                viewCamera.transform.LookAt(viewTarget);
                break;
            case ViewType.God:
                viewOffset = new Vector3(5f, 10f, -10f);
                viewCamera.transform.position = viewTarget.position + viewOffset;
                viewCamera.transform.LookAt(viewTarget);
                break;
            case ViewType.Top:
                viewCamera.transform.eulerAngles = new Vector3(90f, 0f, 0f);
                viewOffset = new Vector3(0f, 10f, 0f);
                break;
            case ViewType.Side:
                viewCamera.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                viewOffset = new Vector3(0f, 0f, -10f);
                break;
        }

    }

    /// <summary>
    /// 视角控制外部调用 上帝视角与顶视角
    /// </summary>
    /// <param name="viewType">视角类型</param>
    /// <param name="viewTarget">视角目标物体</param>
    public static void ViewFollow(ViewType viewType, Transform viewTarget)
    {
        switch (viewType)
        {
            case ViewType.Free:
                ViewFree(viewTarget);
                break;
            case ViewType.God:
                ViewGod(viewTarget);
                break;
            case ViewType.Top:
                ViewTop(viewTarget);
                break;
            case ViewType.Side:
                viewSide(viewTarget);
                break;
        }
    }

    /// <summary>
    /// 自由视角
    /// </summary>
    /// <param name="viewTarget">视角目标物体</param>
    static void ViewFree(Transform viewTarget)
    {
        viewCamera.transform.position = new Vector3(Mathf.SmoothDamp(viewCamera.transform.position.x, viewTarget.position.x + viewOffset.x, ref velocity.x, moveDelay), viewCamera.transform.position.y, Mathf.SmoothDamp(viewCamera.transform.position.z, viewTarget.position.z + viewOffset.z, ref velocity.z, moveDelay));
        viewCamera.transform.LookAt(viewTarget);
    }

    /// <summary>
    /// 上帝视角
    /// </summary>
    /// <param name="viewTarget">视角目标物体</param>
    static void ViewGod(Transform viewTarget)
    {
        viewCamera.transform.position = new Vector3(Mathf.SmoothDamp(viewCamera.transform.position.x, viewTarget.position.x + viewOffset.x, ref velocity.x, moveDelay), viewTarget.transform.position.y + viewOffset.y, Mathf.SmoothDamp(viewCamera.transform.position.z, viewTarget.position.z + viewOffset.z, ref velocity.z, moveDelay));
    }

    /// <summary>
    /// 顶视角
    /// </summary>
    /// <param name="viewTarget">视角目标物体</param>
    static void ViewTop(Transform viewTarget)
    {
        viewCamera.transform.position = new Vector3(Mathf.SmoothDamp(viewCamera.transform.position.x, viewTarget.position.x, ref velocity.x, moveDelay), viewTarget.transform.position.y + viewOffset.y, Mathf.SmoothDamp(viewCamera.transform.position.z, viewTarget.position.z, ref velocity.z, moveDelay));

    }

    /// <summary>
    /// 侧视角 用于横版
    /// </summary>
    /// <param name="viewTarget">视角目标物体</param>
    static void viewSide(Transform viewTarget)
    {
        viewCamera.transform.position = new Vector3(Mathf.SmoothDamp(viewCamera.transform.position.x, viewTarget.position.x, ref velocity.x, moveDelay), Mathf.SmoothDamp(viewCamera.transform.position.y, viewTarget.position.y, ref velocity.y, moveDelay), viewTarget.transform.position.z + viewOffset.z);
    }

    /// <summary>
    /// 旋转相机 只用于自由视角
    /// </summary>
    /// <param name="viewTarget"></param>
    public static void ViewRotate(Transform viewTarget, float slideOffsetH, float slideOffsetV)
    {
        //PlayerController.isTurn = false;
        Vector3 originalPosition = viewCamera.transform.position;
        Quaternion originalRotation = viewCamera.transform.rotation;
        viewCamera.transform.RotateAround(viewTarget.position, viewTarget.up, rotateSpeed * slideOffsetH);
        viewCamera.transform.RotateAround(viewTarget.position, viewCamera.transform.right, -rotateSpeed * slideOffsetV);
        float x = viewCamera.transform.eulerAngles.x;
        //旋转的范围为10度到80度
        if (x < rotateMinX || x > rotateMaxX)
        {
            viewCamera.transform.position = originalPosition;
            viewCamera.transform.rotation = originalRotation;
        }
        float rate = viewDistance / Vector3.Distance(viewCamera.transform.position, viewTarget.position);
        viewOffset = (viewCamera.transform.position - viewTarget.position) * rate;
        //PlayerController.PlayerForward = new Vector3( viewTarget.position.x - viewCamera.transform.position.x, 0, viewTarget.position.z - viewCamera.transform.position.z).normalized;
        //viewTarget.forward = PlayerController.playerForward; 
    }

    /// <summary>
    /// 缩放镜头
    /// </summary>
    /// <param name="viewTarget"></param>
    public static void ViewZoom(Transform viewTarget)
    {
        
    }

    /// <summary>
    /// 旋转角度的值限定
    /// </summary>
    /// <param name="angle">角度</param>
    /// <param name="min">最小值</param>
    /// <param name="max">最大值</param>
    /// <returns></returns>
    static float ClampAngle(float angle)
    {
        if(angle < -360)
        {
            angle += 360;
        }
        if(angle > 360)
        {
            angle += 360;
        }
        return angle;
    }
}
