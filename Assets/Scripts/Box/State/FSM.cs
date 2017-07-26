using System.Collections.Generic;
using UnityEngine;
[System.Serializable]public class FSM<T> where T : struct{    [SerializeField]    protected T m_PrevState;    [SerializeField]    protected T m_CurState;    [SerializeField]    public float m_StateTimer = 0;

    public T GetCurState { get { return m_CurState; } }

    protected Dictionary<T, StateFunc> m_StateFuncs = new Dictionary<T, StateFunc>();

    /// <summary>
    /// 注册状态
    /// </summary>
    /// <param name="state">某个状态类</param>
    /// <param name="enter">进入状态时的执行方法</param>
    /// <param name="update">处于状态中的执行方法</param>
    /// <param name="exit">离开状态时的执行方法</param>
    public void RegistState(T state, StateFunc.EnterFunc enter, StateFunc.UpdateFunc update, StateFunc.ExitFunc exit)    {        StateFunc func = new StateFunc        {            enterFunc = enter,            updateFunc = update,            exitFunc = exit        };

        this.m_StateFuncs.Add(state, func);
        //Debug.Log("State:" + state);
    }

    public void UpdateState(float delta)    {        if (this.m_StateFuncs.ContainsKey(this.m_CurState) && (this.m_StateFuncs[this.m_CurState].updateFunc != null))        {            this.m_StateFuncs[this.m_CurState].updateFunc(delta);        }        m_StateTimer += delta;    }

    public void EnterState(T state, StateParm parm = null)    {        if (m_CurState.Equals(state))
        {
            Debug.LogError("要进入的新状态与当前所处状态一样 操作失败！！！");
            return;
        }        T curState = this.m_CurState;        this.m_PrevState = curState;        this.m_CurState = state;

        if (this.m_StateFuncs.ContainsKey(curState) && (this.m_StateFuncs[curState].exitFunc) != null)        {            this.m_StateFuncs[curState].exitFunc();        }
        else
        {
            Debug.LogError("状态库中不存在要离开的当前状态 或者当前状态没有离开的方法！！！");
        }

        if (this.m_StateFuncs.ContainsKey(this.m_CurState) && (this.m_StateFuncs[this.m_CurState].enterFunc) != null)        {            this.m_StateFuncs[this.m_CurState].enterFunc(parm);        }        else
        {
            Debug.LogError("状态库中不存在要进入的状态 或者状态没有进入的方法！！！");
        }        this.m_StateTimer = 0f;//重置计时器
    }

    /// <summary>
    /// 在状态方法字典中检测是否有指定类的状态
    /// </summary>
    /// <param name="state">要检测的类实例</param>
    /// <returns></returns>
    public bool HasState(T state)    {        return m_StateFuncs.ContainsKey(state);    }
}

/// <summary>
/// 状态的方法类 包括进入 维持 离开三种
/// </summary>
[System.Serializable]public class StateFunc{    public delegate void EnterFunc(StateParm parm);

    public delegate void UpdateFunc(float delta);

    public delegate void ExitFunc();
    
    public EnterFunc enterFunc;    public UpdateFunc updateFunc;    public ExitFunc exitFunc;}

/// <summary>
/// 状态的方法的参数类 包括内置四种数据类型
/// </summary>
[System.Serializable]public class StateParm{    public bool bValue;    public float fValue;    public int iValue;    public Vector3 vValue;
}

#region 用法示例

/*  

public class CubeCtrl : MonoBehaviour
{
    public enum exampleState
    {
        state1,
        state2,
    }

    FSM<exampleState> exampleFSM = new FSM<exampleState>();

    void Start()
    {
        exampleFSM.RegistState(exampleState.state1, state1Enter, state1Update, state1Exit);
        exampleFSM.RegistState(exampleState.state2, state2Enter, state2Update, state2Exit);
        // 设置默认状态
        exampleFSM.EnterState(exampleState.state1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            exampleFSM.EnterState(exampleState.state1);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            exampleFSM.EnterState(exampleState.state2);
        }
        exampleFSM.UpdateState(Time.deltaTime);
    }

    void state1Enter(object param)
    {
        Debug.Log("进入state1");
    }

    void state1Update(float delta)
    {
        //do something
    }

    void state1Exit()
    {
        Debug.Log("Cube 停止静止");
    }

    void state2Enter(object param)
    {
        Debug.Log("进入state2");
    }

    void state2Update(float delta)
    {
        //do something
    }

    void state2Exit()
    {
        Debug.Log("离开state2");
    }
}
*/

#endregion
