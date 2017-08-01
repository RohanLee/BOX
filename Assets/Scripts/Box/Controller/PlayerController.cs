using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static GameObject Player;
    public static bool isTurn;
    public static Vector3 playerForward;

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

        View.ViewFollow(ViewType.Free, Player.transform);
    }
    
    void LateUpdate()
    {

        if (Input.GetMouseButton(0))
        {
            
            View.ViewRotate(Player.transform, InputCore.GetViewOffsetH(), InputCore.GetViewOffsetV());
        }
        else
        {
            isTurn = true;
        }
    }

    private void Move(float moveOffsetX, float moveOffsetZ)
    {
        Player.transform.position += playerForward * moveSpeed ;
    }

    private void Turn(float turnOffsetX, float turnOffsetZ)
    {
        if(isTurn)
        {
            float offsetX = Player.transform.forward.x + turnOffsetX;
            float offsetZ = Player.transform.forward.z + turnOffsetZ;
            playerForward = (new Vector3(turnOffsetX, 0, turnOffsetZ) + Player.transform.forward).normalized;
            Player.transform.forward = playerForward;
        }
        
    }
}
