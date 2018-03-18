using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    /// <summary>
    /// 主角朝向枚举，左或右。
    /// </summary>
    public enum Direction { Left, Right};

    public Quaternion FrontQuaternion { get { return Quaternion.identity; } }
    public Quaternion BackQuaternion { get { return new Quaternion(0, 1, 0, 0); } }

    public Transform Weapon;

    private RaycastHit raycastHit;
    private Rigidbody Rigidbody;
    private Animator animator;

    /// <summary>
    /// 获取玩家当前朝向，返回方向枚举。
    /// </summary>
    /// <returns></returns>
    public Direction GetDirection()
    {
        if (transform.rotation == FrontQuaternion || transform.rotation == FrontQuaternion.Negative()) return Direction.Right;
        else return Direction.Left;
    }

    /// <summary>
    /// 玩家移动方法。
    /// </summary>
    private void Move()
    {
        if (GetDirection() == Direction.Right)
        {
            if(InputManager.FloatAD >= 0) transform.Translate(Vector3.forward * InputManager.FloatAD * 3 * Time.deltaTime, Space.World);
            else transform.Translate(Vector3.forward * InputManager.FloatAD * 2 * Time.deltaTime, Space.World);
        }
        else
        {
            if (InputManager.FloatAD <= 0) transform.Translate(Vector3.forward * InputManager.FloatAD * 3 * Time.deltaTime, Space.World);
            else transform.Translate(Vector3.forward * InputManager.FloatAD * 2 * Time.deltaTime, Space.World);
        }
        animator.SetFloat("X", InputManager.FloatAD);

        if (Input.GetKeyDown(InputManager.Space))
        {
            if (transform.IsGround(raycastHit)) Rigidbody.AddForce(Vector3.up * 250);
            if (transform.IsLeftWall(raycastHit) && !transform.IsGround(raycastHit)) Rigidbody.AddForce(new Vector3(0, 1, 0.1f) * 200);
            if (transform.IsRightWall(raycastHit) && !transform.IsGround(raycastHit)) Rigidbody.AddForce(new Vector3(0, 1, -0.1f) * 200);
        }
    }
    /// <summary>
    /// 玩家瞄准和射击方法。
    /// </summary>
    private void AimCtrl()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out raycastHit, 100, 1 << LayerMask.NameToLayer("Aim")))
        {
            if(Vector3.Dot(transform.forward, raycastHit.point - transform.position) >= 0)
            {

            }
            else
            {
                transform.eulerAngles += new Vector3(0, 180, 0);
            }
        }
    }
    /// <summary>
    /// 墙壁碰撞检测。
    /// </summary>
    private bool CheckWall()
    {
        if(transform.IsLeftWall(raycastHit) || transform.IsRightWall(raycastHit))
        {
            if (transform.IsLeftWall(raycastHit)) InputManager.SetFloatAD(Mathf.Clamp(InputManager.FloatAD, 0, 1));
            if (transform.IsRightWall(raycastHit)) InputManager.SetFloatAD(Mathf.Clamp(InputManager.FloatAD, -1, 0));
            return true;
        }
        return false;
    }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        AimCtrl();
        CheckWall();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetLookAtWeight(1f);
        animator.SetLookAtPosition(raycastHit.point);
    }
}
