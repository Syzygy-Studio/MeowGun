﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主角朝向枚举，左或右。
/// </summary>
public enum Direction { Left, Right };
/// <summary>
/// 主角状态枚举，在地面上，爬墙，悬挂。
/// </summary>
public enum State { Ground, Wall, Hang };
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

    private RaycastHit raycastHit;
    private Rigidbody Rigidbody;
    private Animator animator;
    /// <summary>
    /// 获取玩家当前朝向，返回方向枚举。
    /// </summary>
    /// <returns></returns>
    public Direction GetDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 100, 1 << LayerMask.NameToLayer("Aim")))
        {
            if (Vector3.Dot(Vector3.forward, raycastHit.point - transform.position) >= 0) return Direction.Right;
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
        if (transform.IsForwardWall() && !transform.IsGround()) return State.Wall;
        return State.Hang;
    }
    /// <summary>
    /// 重力效果，update中调用。
    /// </summary>
    private void gravity()
    {

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
        Debug.Log(GetState());
        switch (GetState())
        {
            case State.Ground:
                {
                    animator.SetBool("isWall", false);

                    GetComponent<Rigidbody>().useGravity = true;

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

                    if (Input.GetKeyDown(InputManager.Space)) if (transform.IsGround()) Rigidbody.AddForce(Vector3.up * 200);
                }
                break;

            case State.Wall:
                {
                    animator.SetBool("isWall", true);

                    //GetComponent<Rigidbody>().useGravity = false;
                    Move(5f, Vector3.up, InputManager.FloatWS);
                    animator.SetFloat("X", InputManager.FloatWS);
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
        if(Physics.Raycast(ray, out raycastHit, 100, 1 << LayerMask.NameToLayer("Aim")))
        {
            if(Vector3.Dot(transform.forward, raycastHit.point - transform.position) >= 0)
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
    private bool CheckWall()
    {
        if(transform.IsLeftWall() || transform.IsRightWall())
        {
            if (transform.IsLeftWall()) InputManager.SetFloatAD(Mathf.Clamp(InputManager.FloatAD, 0, 1));
            if (transform.IsRightWall()) InputManager.SetFloatAD(Mathf.Clamp(InputManager.FloatAD, -1, 0));
            return true;
        }
        return false;
    }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        InputManager.SetPlayer(transform);
    }

    private void Update()
    {
        Move();
        AimCtrl();
        CheckWall();
        transform.rotation = InputManager.PlayerRotate;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        //animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        //animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);

        //animator.SetIKPosition(AvatarIKGoal.LeftHand, Weapon.position);
        //animator.SetIKRotation(AvatarIKGoal.LeftHand, Weapon.rotation);

        animator.SetLookAtWeight(1f);
        animator.SetLookAtPosition(raycastHit.point);
    }
}