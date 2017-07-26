using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    ABC[] abc;
    // Use this for initialization
    void Start ( )
    {
        ABC abc1 = new ABC();
        abc1.name = "张三";
        abc1.health = 100;
        ABC abc2 = new ABC();
        abc2.name = "王五";
        abc2.health = 200;

        abc = new ABC[] { abc1, abc2 };
        MsgCenter.MsgRegister("UITest_loadObj", LoadObj);
    }

    // Update is called once per frame
    void Update ( )
    {

    }

    private void LoadObj(object[] objs)
    {
        //GameObject go = GameObject.Instantiate(Resources.Load("Cube") as GameObject);
        //go.name = "haha";
        string[] nn = (string[])objs[0];
        for (int i = 0; i < abc.Length; i++)
        {
            Debug.LogError(abc[i].name + " = " + abc[i].health);
            abc[i].name = nn[i];
            abc[i].health -= (int)objs[1];
            Debug.LogError(abc[i].name + " = " + abc[i].health);
        }
    }
}

public class ABC
{
    public int health;
    public string name;
}
