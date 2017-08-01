using UnityEngine;

/// <summary>
/// 各平台输入管理
/// </summary>
public class InputCore
{
    public static float GetMoveOffsetH()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
        {
            return Input.GetAxis("Horizontal");
        }
        return Joystick.JoystickOffset.x;
#elif UNITY_STANDALONE
        return Input.GetAxis("Horizontal");
#elif UNITY_IOS || UNITY_ANDROID
        return Joystick.JoystickOffset.x;
#elif UNITY_PS4 || UNITY_XBOXONE
        return Input.GetAxis("Horizontal");
#endif
    }

    public static float GetMoveOffsetV()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
        {
            return Input.GetAxis("Vertical");
        }
        return Joystick.JoystickOffset.y;
#elif UNITY_STANDALONE
        return Input.GetAxis("Vertical");
#elif UNITY_IOS || UNITY_ANDROID
        return Joystick.JoystickOffset.y;
#elif UNITY_PS4 || UNITY_XBOXONE
        return Input.GetAxis("Vertical");
#endif
    }

    public static float GetViewOffsetH()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if(Input.anyKey)
        {
            return Input.GetAxis("Mouse X");
        }
        return 0f;
#elif UNITY_IOS || UNITY_ANDROID
        
#elif UNITY_PS4
        return Input.GetAxis("3rdAxis");
#elif UNITY_XBOXONE
        return Input.GetAxis("4thAxis");
#endif
    }

    public static float GetViewOffsetV()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.anyKey)
        {
            return Input.GetAxis("Mouse Y");
        }
        return 0f;
#elif UNITY_IOS || UNITY_ANDROID
        
#elif UNITY_PS4
        return Input.GetAxis("6thAxis");
#elif UNITY_XBOXONE
        return Input.GetAxis("5thAxis");
#endif
    }
}
