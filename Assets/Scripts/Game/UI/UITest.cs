using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UITest : UIPage
{
    Text txt;

    public UITest() : base(UIType.Normal, UIMode.HideOther, UICollider.None)
    {
        uiPath = "UIMain";
    }

    public override void Awake ( GameObject go )
    {
        this.transform.Find ( "btnCenter" ).GetComponent<Button> ( ).onClick.AddListener ( ( ) =>
               {
                   ShowPage<UITip> ( );
                   SceneManager.LoadScene(1);
               } );

        txt = GameObject.Find("btnLeftUp/Text").GetComponent<Text>();
    }

    public override void Refresh()
    {
        txt.text = Localization.Get("info1");
    }
}
