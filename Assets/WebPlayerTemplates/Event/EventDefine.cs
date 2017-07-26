using UnityEngine;

public class EventDefine : MonoBehaviour
{
    //系统消息 1-1000
    public const int EventResNum = 1;
    //界面消息 2000-4000
    public const int EventDataNum = 2000;
    //战斗消息 5000-6000
    public const int EventStateNum = 5000;
    //所有角色创建完成
    public const int EventRoleNum = EventStateNum + 1;
}
