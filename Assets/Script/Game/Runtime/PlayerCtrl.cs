using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主角朝向枚举，左或右。
/// </summary>
public enum Direction { Left, Right };
/// <summary>
/// 主角状态枚举，在地面上，爬墙。
/// </summary>
public enum State { Ground, Wall};
/// <summary>
/// 主角朝左和朝右的四元数方向。
/// </summary>
public struct DefaultQuaternion
{
    public Quaternion FrontQuaternion { get { return Quaternion.identity; } }
    public Quaternion BackQuaternion { get { return new Quaternion(0, 1, 0, 0); } }
};
/// <summary>
/// 主角控制，作为脚本挂在主角下。
/// </summary>
public class PlayerCtrl : MonoBehaviour
{
    public float Speed = 1.5f;
    public Transform Weapon;

    public Transform Hand;
    public Transform LeftHand;
    public Transform RightHand;
    public Transform LeftFoot;
    public Transform RightFoot;

    private RaycastHit raycastHitAim;
    private RaycastHit raycastHitCrossGround;
    private Rigidbody Rigidbody;
    private Animator animator;

    //private GenericIK genericIK;
    /// <summary>
    /// 获取玩家当前朝向，返回方向枚举。
    /// </summary>
    /// <returns></returns>
    public Direction GetDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHitAim, 100, 1 << LayerMask.NameToLayer("Aim")))
        {
            if (Vector3.Dot(Vector3.forward, raycastHitAim.point - transform.position) >= 0) return Direction.Right;
            else return Direction.Left;
        }
        return Direction.Right;
    }
    /// <summary>
    /// 获取玩家当前状态，返回状态枚举。
    /// </summary>
    /// <returns></returns>
    public State GetState()
    {
        if (transform.IsGround() || (!transform.IsGround() && !transform.IsForwardWall())) return State.Ground;
        if (transform.IsForwardWall() && !transform.IsGround())
        {
            if (GetDirection() == Direction.Right) if (Input.GetKey(InputManager.D)) return State.Wall;
            if (GetDirection() == Direction.Left) if (Input.GetKey(InputManager.A)) return State.Wall;
            return State.Ground;
        }
        return State.Ground;
    }
    /// <summary>
    /// 角色的实际移动，update中调用。
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="speed"></param>
    private void Move(float speed, Vector3 direction) => transform.Translate(direction * InputManager.FloatAD * speed * Time.deltaTime, Space.World);
    private void Move(float speed, Vector3 direction, float input) => transform.Translate(direction * input * speed * Time.deltaTime, Space.World);
    /// <summary>
    /// 玩家移动方法。
    /// </summary>
    private void Move()
    {
        //Debug.Log(GetState());
        switch (GetState())
        {
            case State.Ground:
                {
                    animator.SetBool("isWall", false);

                    GetComponent<Rigidbody>().useGravity = true;
                    GetComponent<Rigidbody>().isKinematic = false;

                    if (GetDirection() == Direction.Right)
                    {
                        if (InputManager.FloatAD >= 0) Move(1.5f, Vector3.forward);
                        else Move(1f, Vector3.forward);
                        animator.SetFloat("X", InputManager.FloatAD);
                    }
                    else
                    {
                        if (InputManager.FloatAD <= 0) Move(1.5f, Vector3.forward);
                        else Move(1f, Vector3.forward);
                        animator.SetFloat("X", -InputManager.FloatAD);
                    }
                    animator.SetFloat("Y", InputManager.FloatSpace);

                    if(transform.IsGround() && !transform.IsCrossGround()) if (Input.GetKeyDown(InputManager.Space)) Rigidbody.AddForce(Vector3.up * 200);

                    if (transform.IsCrossGround())
                    {
                        if (!Input.GetKey(InputManager.S) && Input.GetKeyDown(InputManager.Space)) Rigidbody.AddForce(Vector3.up * 200);
                        if (Input.GetKey(InputManager.S) && Input.GetKeyDown(InputManager.Space))InputManager.SetFloatDownSpace(0.3f);
                    }
                        
                    Debug.Log(InputManager.FloatDownSpace + "+" + transform.IsCrossGround());
                }
                break;

            case State.Wall:
                {
                    animator.SetBool("isWall", true);

                    GetComponent<Rigidbody>().useGravity = false;
                    GetComponent<Rigidbody>().isKinematic = true;

                    Move(1f, Vector3.up, InputManager.FloatWS);
                    animator.SetFloat("X", InputManager.FloatWS);
                    if(transform.IsHeadWall()) GetComponent<Rigidbody>().isKinematic = false;
                    if (Input.GetKeyDown(InputManager.Space))
                    {
                        GetComponent<Rigidbody>().isKinematic = false;
                        if(GetDirection() == Direction.Right) Rigidbody.AddForce(new Vector3(0, 1, -0.2f) * 200);
                        else Rigidbody.AddForce(new Vector3(0, 1, 0.2f) * 200);

                    }
                }
                break;
        }
    }
    /// <summary>
    /// 玩家瞄准和射击方法。
    /// </summary>
    private void AimCtrl()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out raycastHitAim, 100, 1 << LayerMask.NameToLayer("Aim")))
        {
            if(Vector3.Dot(transform.forward, raycastHitAim.point - transform.position) >= 0)
            {

            }
            else
            {
                
            }
        }
    }
    /// <summary>
    /// 墙壁碰撞检测。
    /// </summary>
    private bool checkWall()
    {
        if(transform.IsLeftWall() || transform.IsRightWall())
        {
            if (transform.IsLeftWall()) InputManager.SetFloatAD(Mathf.Clamp(InputManager.FloatAD, 0, 1));
            if (transform.IsRightWall()) InputManager.SetFloatAD(Mathf.Clamp(InputManager.FloatAD, -1, 0));
            return true;
        }
        return false;
    }

    private bool checkVerticalWall()
    {
        if(transform.IsHeadWall() || transform.IsGround())
        {
            if (transform.IsHeadWall()) InputManager.SetFloatWS(Mathf.Clamp(InputManager.FloatWS, -1, 0));
            if (transform.IsGround()) InputManager.SetFloatWS(Mathf.Clamp(InputManager.FloatWS, 0, 1));
            return true;
        }
        return false;
    }

    private void ctrlCrossGround()
    {
        if (Physics.CheckSphere(transform.position, 1f, 1 << LayerMask.NameToLayer("Cross")))
        {
            var crossGround = Physics.OverlapSphere(transform.position, 1f, 1 << LayerMask.NameToLayer("Cross"));
            foreach (var c in crossGround)
            {
                if (Vector3.Dot(c.transform.forward, transform.position - c.transform.position) >= 0 && InputManager.FloatDownSpace == 0)
                    Physics.IgnoreCollision(GetComponent<BoxCollider>(), c, false);
                else
                    Physics.IgnoreCollision(GetComponent<Collider>(), c);
            }
        }
        else return;
    }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        InputManager.SetPlayer(transform);

        //genericIK = new GenericIK(new GenericTransform(Hand, LeftHand, RightHand, LeftFoot, RightFoot));
    }

    private void Update()
    {
        Move();
        AimCtrl();
        checkWall();
        checkVerticalWall();
        ctrlCrossGround();
        transform.rotation = InputManager.PlayerRotate;
        //genericIK.SetLookAtPosition(raycastHit.point);
    }
}
