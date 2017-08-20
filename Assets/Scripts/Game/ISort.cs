using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ISort: MonoBehaviour
{
    private void Start()
    {
        List<AAA> list = new List<AAA>();
        AAA a1 = new AAA();
        AAA a2 = new AAA();
        AAA a3 = new AAA();
        AAA a4 = new AAA();
        AAA a5 = new AAA();
        a1.name = "A";
        a1.ID = 0;
        a1.num = 5;
        a1.star = 4;
        list.Add(a1);
        a2.name = "B";
        a2.ID = 1;
        a2.num = 8;
        a2.star = 5;
        list.Add(a2);
        a3.name = "C";
        a3.ID = 2;
        a3.num = 8;
        a3.star = 6;
        list.Add(a3);
        a4.name = "D";
        a4.ID = 3;
        a4.num = 9;
        a4.star = 2;
        list.Add(a4);
        a5.name = "E";
        a5.ID = 4;
        a5.num = 5;
        a5.star = 7;
        list.Add(a5);

        var sortList = list.OrderBy(a => a.num).ThenBy(a => a.star);
        list = sortList.ToList();

        for(int i = 0; i < list.Count; i++)
        {
            Debug.LogError(list[i].name);
        }
    }
}

public class AAA
{
    public string name;
    public int ID;
    public int num;
    public int star;
}
