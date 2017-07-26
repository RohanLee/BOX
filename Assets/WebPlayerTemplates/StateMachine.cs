using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    // 所有状态集合
    private Dictionary<uint, IState> mStateDic = null;

    // 当前状态
    private IState mCurrentState = null;

    // 当前状态
    public IState CurrentState
    {
        get
        {
            return mCurrentState;
        }
    }

    /// 当前状态ID
    public uint CurrentID
    {
        get
        {
            return mCurrentState == null ? 0 : mCurrentState.GetStateID ( );
        }
    }

    //构造函数
    public StateMachine ( )
    {
        mStateDic = new Dictionary<uint, IState> ( );
        mCurrentState = null;
    }

    /// <summary>
    /// 注册状态
    /// </summary>
    /// <param name="state">状态对象</param>
    /// <returns></returns>
    public bool RegisterState ( IState state )
    {
        if (state == null)
        {
            Debug.LogError ( "注册状态是输入状态为空！！！" );
            return false;
        }

        if (mStateDic.ContainsKey ( state.GetStateID ( ) ))
        {
            Debug.LogError ( "注册状态是字典中已有该ID的内容 = " + state.GetStateID ( ) );
            return false;
        }

        mStateDic.Add ( state.GetStateID ( ), state );
        return true;
    }

    /// <summary>
    /// 移除一个状态
    /// </summary>
    /// <param name="stateID">状态ID</param>
    /// <returns></returns>
    public bool RemoveState ( uint stateID )
    {
        if (!mStateDic.ContainsKey ( stateID ))
        {
            return false;
        }
        //要移除的状态正在运行则移除失败
        if (mCurrentState != null && mCurrentState.GetStateID ( ) == stateID)
        {
            return false;
        }

        mStateDic.Remove ( stateID );
        return true;
    }

    /// <summary>
    /// 获取一个状态
    /// </summary>
    /// <param name="stateID">状态ID</param>
    /// <returns></returns>
    public IState GetState ( uint stateID )
    {
        IState state = null;
        mStateDic.TryGetValue ( stateID, out state );
        return state;
    }

    /// <summary>
    /// 停止当前状态
    /// </summary>
    /// <param name="parm1">参数1</param>
    /// <param name="parm2">参数2</param>
    public void StopState ( object parm1, object parm2 )
    {
        if (null == mCurrentState)
        {
            return;
        }
        mCurrentState.OnLeave ( null, parm1, parm2 );
        mCurrentState = null;
    }

    // 切换状态的回调
    public BetweenSwitchState BetweenSwitchStateCallBack = null;

    /// <summary>
    /// 切换状态回调委托
    /// </summary>
    /// <param name="from">当前状态</param>
    /// <param name="to">要跳转的状态</param>
    /// <param name="parm1">参数1</param>
    /// <param name="parm2">参数2</param>
    public delegate void BetweenSwitchState ( IState from, IState to, object parm1, object parm2 );

    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="newSatetID">要切换的状态ID</param>
    /// <param name="parm1">参数1</param>
    /// <param name="parm2">参数2</param>
    /// <returns>如果不存在这个状态或者当前状态等于要切换的状态 那么返回失败</returns>
    public bool SwitchState ( uint newSatetID, object parm1, object parm2 )
    {
        if (mCurrentState != null && mCurrentState.GetStateID ( ) == newSatetID)
        {
            return false;
        }

        IState newState = null;
        mStateDic.TryGetValue ( newSatetID, out newState );
        if (newState == null)
        {
            return false;
        }

        if (mCurrentState != null)
        {
            mCurrentState.OnLeave ( newState, parm1, parm2 );
        }

        IState oldState = mCurrentState;

        mCurrentState = newState;

        if (BetweenSwitchStateCallBack != null)
        {
            BetweenSwitchStateCallBack ( oldState, mCurrentState, parm1, parm2 );
        }

        newState.OnEnter ( this, oldState, parm1, parm2 );

        return true;
    }

    /// <summary>
    /// 判断当前状态是否是输入的状态ID相同状态
    /// </summary>
    /// <param name="stateID">状态ID</param>
    /// <returns></returns>
    public bool IsInState ( uint stateID )
    {
        return mCurrentState == null ? false : mCurrentState.GetStateID ( ) == stateID;
    }


    public void OnUpate ( )
    {
        if (mCurrentState != null)
        {
            mCurrentState.OnUpate ( );
        }
    }

    public void OnFixedUpdate ( )
    {
        if (mCurrentState != null)
        {
            mCurrentState.OnFixedUpdate ( );
        }
    }

    public void OnLateUpdate ( )
    {
        if (mCurrentState != null)
        {
            mCurrentState.OnLateUpdate ( );
        }
    }
}
