using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UITip : UIPage
{
    public UITip() : base(UIType.PopUp, UIMode.DoNothing, UICollider.Normal)
    {
        uiPath = "UITip";
    }

    public override void Awake(GameObject go)
    {
        object[] objs = new object[] { new string[] { "zhangsan", "wangwu" }, 100 };
        this.transform.Find("btnBack").GetComponent<Button>().onClick.AddListener(() =>{
            SceneManager.LoadScene(0);
            Hide();});
    }
}
