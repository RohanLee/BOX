public interface IState
{
    // 获取这个状态机的状态
    uint GetStateID ( );

    /// <summary>
    /// 进入指定状态
    /// </summary>
    /// <param name="machine">状态机</param>
    /// <param name="prevState">上一个状态</param>
    /// <param name="parm1">参数1</param>
    /// <param name="parm2">参数2</param>
    void OnEnter ( StateMachine machine, IState prevState, object parm1, object parm2 );

    /// <summary>
    /// 离开当前状态并进入下一状态
    /// </summary>
    /// <param name="nextState">下一个要进入的状态</param>
    /// <param name="parm1">参数1</param>
    /// <param name="parm2">参数2</param>
    void OnLeave ( IState nextState, object parm1, object parm2 );

    void OnUpate ( );

    void OnFixedUpdate ( );

    void OnLateUpdate ( );
}
