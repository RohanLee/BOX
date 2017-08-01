using UnityEngine;

/// <summary>
/// 定义视角类型
/// </summary>
public enum ViewType
{
    Free = 0,
    God,
    Top,
}

/// <summary>
/// 视觉控制
/// </summary>
public class View
{
    static Camera viewCamera;
    static float moveDelay = 0.5f;
    static Vector3 velocity = Vector3.zero;
    static Vector3 viewOffset;
    static Vector3 followPosition;

    static float rotateOffsetX, rotateOffsetY;

    static float rotateSpeed = 5f;
    static float rotateMinX = 5f;
    static float rotateMaxX = 89f;
    static float zoomSpeed = 100f;
    static float viewAngleH, viewAngleV;
    static Vector3 viewDistance;

    //void LateUpdate()
    //{
    //    ViewFree(PlayerController.hero.transform);
    //}
    

    public static void ViewInit(ViewType viewType, Transform viewTarget)
    {
        viewCamera = Camera.main;

        switch (viewType)
        {
            case ViewType.Free:
                viewOffset = new Vector3(0f, 6f, -10f);
                viewCamera.transform.position = viewTarget.position + viewOffset;
                viewDistance = new Vector3(0f, 0f, -1 * Vector3.Distance(viewCamera.transform.position, viewTarget.position));
                viewCamera.transform.LookAt(viewTarget);
                rotateOffsetX = viewCamera.transform.eulerAngles.x;
                rotateOffsetY = viewCamera.transform.eulerAngles.y;
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
        }

    }

    /// <summary>
    /// 视角控制外部调用 上帝视角与顶视角
    /// </summary>
    /// <param name="viewType">视角类型</param>
    /// <param name="viewTarget">视角目标</param>
    public static void ViewFollow(ViewType viewType, Transform viewTarget)
    {
        switch (viewType)
        {
            case ViewType.God:
                ViewGod(viewTarget);
                break;
            case ViewType.Top:
                ViewTop(viewTarget);
                break;
        }
    }

    /// <summary>
    /// 视角控制外部调用 自由视角专用
    /// </summary>
    /// <param name="viewTarget">视角目标</param>
    /// <param name="offsetH">水平偏移输入</param>
    /// <param name="offsetV">垂直偏移输入</param>
    public static void ViewFollow(Transform viewTarget, float offsetH, float offsetV)
    {
        ViewFree(viewTarget, offsetH, offsetV);
    }

    /// <summary>
    /// 自由视角
    /// </summary>
    /// <param name="viewTarget">目标物体</param>
    static void ViewFree(Transform viewTarget, float slideOffsetH, float slideOffsetV)
    {
        viewCamera.transform.position = new Vector3(Mathf.SmoothDamp(viewCamera.transform.position.x, viewTarget.position.x + viewOffset.x, ref velocity.x, moveDelay), viewCamera.transform.position.y, Mathf.SmoothDamp(viewCamera.transform.position.z, viewTarget.position.z + viewOffset.z, ref velocity.z, moveDelay));
        //rotateOffsetX -= slideOffsetV * rotateSpeed;
        //rotateOffsetY += slideOffsetH * rotateSpeed;
        //rotateOffsetX = Mathf.Clamp(rotateOffsetX, rotateMinX, rotateMaxX);
        //rotateOffsetY = ClampAngle(rotateOffsetY);
        //Quaternion rotation = Quaternion.Euler(rotateOffsetX, rotateOffsetY, 0);
        
        //followPosition = viewTarget.position + rotation *  viewDistance;
        //viewCamera.transform.position = new Vector3(Mathf.SmoothDamp(viewCamera.transform.position.x, followPosition.x, ref velocity.x, moveDelay), Mathf.SmoothDamp(viewCamera.transform.position.y, followPosition.y, ref velocity.y, moveDelay), Mathf.SmoothDamp(viewCamera.transform.position.z, followPosition.z, ref velocity.z, moveDelay));
        viewCamera.transform.LookAt(viewTarget);
    }

    /// <summary>
    /// 上帝视角
    /// </summary>
    /// <param name="viewTarget">目标物体</param>
    static void ViewGod(Transform viewTarget)
    {
        viewCamera.transform.position = new Vector3(Mathf.SmoothDamp(viewCamera.transform.position.x, viewTarget.position.x + viewOffset.x, ref velocity.x, moveDelay), viewTarget.transform.position.y + viewOffset.y, Mathf.SmoothDamp(viewCamera.transform.position.z, viewTarget.position.z + viewOffset.z, ref velocity.z, moveDelay));
    }

    /// <summary>
    /// 顶视角
    /// </summary>
    /// <param name="viewTarget">目标物体</param>
    static void ViewTop(Transform viewTarget)
    {
        viewCamera.transform.position = new Vector3(Mathf.SmoothDamp(viewCamera.transform.position.x, viewTarget.position.x, ref velocity.x, moveDelay), viewTarget.transform.position.y + viewOffset.y, Mathf.SmoothDamp(viewCamera.transform.position.z, viewTarget.position.z, ref velocity.z, moveDelay));

    }

    /// <summary>
    /// 旋转相机 只用于自由视角
    /// </summary>
    /// <param name="viewTarget"></param>
    public static void ViewRotate(Transform viewTarget, float slideOffsetH, float slideOffsetV)
    {
        rotateOffsetX -= slideOffsetV * rotateSpeed;
        rotateOffsetY += slideOffsetH * rotateSpeed;
        rotateOffsetX = Mathf.Clamp(rotateOffsetX, rotateMinX, rotateMaxX);
        rotateOffsetY = ClampAngle(rotateOffsetY);
        Quaternion rotation = Quaternion.Euler(rotateOffsetX, rotateOffsetY, 0);
        Vector3 position = rotation * viewOffset + viewTarget.position;
        viewCamera.transform.position = new Vector3(Mathf.SmoothDamp(viewCamera.transform.position.x, position.x, ref velocity.x, moveDelay), Mathf.SmoothDamp(viewCamera.transform.position.y, position.y, ref velocity.y, moveDelay), Mathf.SmoothDamp(viewCamera.transform.position.z, position.z, ref velocity.z, moveDelay));
        viewCamera.transform.LookAt(viewTarget);
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
