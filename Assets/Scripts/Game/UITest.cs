using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UITest : UIPage
{
    public UITest() : base(UIType.Normal, UIMode.HideOther, UICollider.None)
    {
        uiPath = "UI/UITest";
    }

    public override void Awake ( GameObject go )
    {

        this.transform.Find ( "btnCenter" ).GetComponent<Button> ( ).onClick.AddListener ( ( ) =>
               {
                   ShowPage<UITip> ( );
                   SceneManager.LoadScene(1);
               } );
    }

    void Start()
    {
        
    }
}
