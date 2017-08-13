using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static GameObject Player;
    //public static bool isTurn;
    public static Vector3 PlayerForward;

    float moveSpeed = 0.1f;
    float turnSpeed = 10f;

    float slideOffsetX, slideOffsetY;

    void Start()
    {
        Player = Pool.Get("Hero");
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
    }

    private void Move(float moveOffsetX, float moveOffsetZ)
    {
        Player.transform.position += new Vector3(moveOffsetX, 0, moveOffsetZ) * moveSpeed;
    }

    private void Turn(float moveOffsetX, float moveOffsetZ)
    {
        Vector3 targetDirection = new Vector3(moveOffsetX, 0f, moveOffsetZ);
        Quaternion rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        Player.transform.rotation = Quaternion.Lerp(Player.transform.rotation, rotation, turnSpeed * Time.deltaTime);
    }
}
