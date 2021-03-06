﻿using UnityEngine;

public class GameInit : MonoBehaviour
{
    //这个参数控制场景跳转后是否执行初始化
    //必须是静态 否则返回场景该值重置
    private static bool isInited = false;

    public static Transform joystick;

    void Awake()
    {
        if (!isInited)
        {
            GameObject go = new GameObject("GameMgr");
            go.AddComponent<DBReadOnly>();
            go.AddComponent<DBReadWrite>();
            go.AddComponent<PlayerController>();
            DontDestroyOnLoad(go);
            Localization.Init();
            UIPage.ShowPage<UITest>();
            UIPage.ShowPage<UIJoystick>();
            isInited = true;
        }
    }

    private void Start()
    {
        Destroy(this.gameObject);
    }
}
