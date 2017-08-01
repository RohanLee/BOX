using UnityEngine;

public class UIJoystick : UIPage
{
    public UIJoystick() : base(UIType.Fixed, UIMode.HideOther, UICollider.None)
    {
        uiPath = "Joystick";
    }

    public override void Awake(GameObject go)
    {

    }

    public override void Refresh()
    {

    }
}
