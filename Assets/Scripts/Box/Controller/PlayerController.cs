using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static GameObject Player;

    float moveSpeed = 0.1f;
    float turnSpeed = 5f;

    float offsetX, offsetZ;

    float angle_Y, angle_X;

    float slideOffsetX, slideOffsetY;

    void Start()
    {
        Player = Instantiate(Resources.Load("Hero")) as GameObject;
        View.ViewInit(ViewType.Free, Player.transform);
    }

    void Update()
    {
        if (Input.anyKey && (InputCore.GetMoveOffsetH() != 0f || InputCore.GetMoveOffsetV() != 0f))
        {
            Move(InputCore.GetMoveOffsetH(), InputCore.GetMoveOffsetV());
            Turn(InputCore.GetMoveOffsetH(), InputCore.GetMoveOffsetV());
        }

        View.ViewFollow(Player.transform, InputCore.GetViewOffsetH(),  InputCore.GetViewOffsetV());
    }
    
    void LateUpdate()
    {

        if (Input.GetMouseButtonDown(0) && (InputCore.GetViewOffsetH() != 0f || InputCore.GetViewOffsetV() != 0f))
        {
            
            //View.ViewRotate(Player.transform, InputCore.GetViewOffsetV());
        }
    }

    private void Move(float moveOffsetX, float moveOffsetZ)
    {
        Player.transform.position += new Vector3(moveOffsetX, 0, moveOffsetZ) * moveSpeed ;
    }

    private void Turn(float turnOffsetX, float turnOffsetZ)
    {
        Player.transform.forward = new Vector3(turnOffsetX, 0, turnOffsetZ);
        //angle_Y += turnOffsetX * turnSpeed;
        //angle_X -= turnOffsetY * turnSpeed;
        //if (angle_Y > 360)
        //{
        //    angle_Y -= 360;Z
        //}
        //else if(angle_Y < 360)
        //{
        //    angle_Y += 360;
        //}
        //Quaternion rotation = Quaternion.Euler(0f, angle_Y, 0f);
        //Player.transform.rotation = rotation;
    }
}
