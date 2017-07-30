using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static GameObject hero;

    float moveSpeed = 0.1f;
    float turnSpeed = 10f;

    float moveOffsetX, moveOffsetZ;

    float slideOffsetX, slideOffsetY;

    void Start()
    {
        hero = Instantiate(Resources.Load("Hero")) as GameObject;
        View.ViewInit(ViewType.Top, hero.transform);
    }

    void Update()
    {
        if (Input.anyKey)
        {
            Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Turn(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        View.ViewFollow(ViewType.Top, hero.transform);

    }

    

    void LateUpdate()
    {
        
        if (Input.anyKey)
        {
            
            //View.ViewRotate(hero.transform, Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
    }

    private void Move(float moveOffsetX, float moveOffsetZ)
    {
        hero.transform.position += new Vector3(moveOffsetX * moveSpeed, 0f, moveOffsetZ * moveSpeed);
    }

    private void Turn(float moveOffsetX, float moveOffsetZ)
    {
        hero.transform.forward = new Vector3(moveOffsetX, 0f, moveOffsetZ);
    }
}
