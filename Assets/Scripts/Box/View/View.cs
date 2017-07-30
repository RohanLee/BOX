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
    public static Vector3 viewAngle = Vector3.zero;

    static float rotateOffsetX, rotateOffsetY;

    static float rotateSpeed = 2f;
    static float rotateMinY = 5f;
    static float rotateMaxY = 80f;
    static float zoomSpeed = 100f;

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
    /// 视角控制外部调用
    /// </summary>
    /// <param name="viewType">视角类型</param>
    /// <param name="viewTarget">视角目标</param>
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
        }
    }

    /// <summary>
    /// 自由视角
    /// </summary>
    /// <param name="viewTarget">目标物体</param>
    static void ViewFree(Transform viewTarget)
    {
        viewCamera.transform.position = new Vector3(Mathf.SmoothDamp(viewCamera.transform.position.x, viewTarget.position.x + viewOffset.x, ref velocity.x, moveDelay), Mathf.SmoothDamp(viewCamera.transform.position.y, viewTarget.position.y + viewOffset.y, ref velocity.y, moveDelay), Mathf.SmoothDamp(viewCamera.transform.position.z, viewTarget.position.z + viewOffset.z, ref velocity.z, moveDelay));
        
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
    public static void ViewRotate(Transform viewTarget, float slideOffsetX, float slideOffsetY)
    {
        rotateOffsetX -= slideOffsetY * rotateSpeed;
        rotateOffsetY += slideOffsetX * rotateSpeed;

        //rotateOffsetY = ClampAngle(rotateOffsetY, rotateMinY, rotateMaxY);
        Quaternion rotation = Quaternion.Euler(0, rotateOffsetY, 0);
        Vector3 position = rotation * viewOffset + viewTarget.position;
        viewTarget.rotation = rotation;
        //viewCamera.transform.position = position;
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
    static float ClampAngle(float angle, float min, float max)
    {
        if(angle < -360)
        {
            angle += 360;
        }
        if(angle > 360)
        {
            angle += 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
