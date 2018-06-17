using SonderStudio.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0108
#pragma warning disable IDE0040
#pragma warning disable IDE1006
#pragma warning disable IDE0044

/// <summary>
/// 主角姿态，正常，攻击姿态。
/// </summary>
public enum Pose { Normal, Attack};

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
    public Quaternion FrontQuaternion { get { return new Quaternion(0, 0.7f, 0, 0.7f); } }
    public Quaternion BackQuaternion { get { return FrontQuaternion.Conjugate(); } }
};

/// <summary>
/// 主角控制，作为脚本挂在主角下。
/// </summary>
public class PlayerCtrl : MonoBehaviour
{
    /// <summary>
    /// 玩家移动速度。
    /// </summary>
    public float Speed = 1.5f;

    /// <summary>
    /// 玩家后退的移动速度。
    /// </summary>
    public float BackSpeed = 1f;

    /// <summary>
    /// 玩家爬墙的速度。
    /// </summary>
    public float ClimbSpeed = 1f;

    /// <summary>
    /// 玩家跳跃高度。
    /// </summary>
    public float JumpHeight = 2f;

    /// <summary>
    /// 玩家当前朝向。
    /// </summary>
    Direction direction = Direction.Left;

    /// <summary>
    /// 玩家当前姿态。
    /// </summary>
    Pose pose = Pose.Normal;

    RaycastHit raycastHitAim;
    Rigidbody rigidbody;
    Animator animator;

    Vector3 moveDirection = Vector3.zero; //玩家移动向量。

    private Direction d; //临时变量，获取上一帧的方向。

    private float ad = InputManager.FloatAD;

    /// <summary>
    /// 获取玩家当前朝向，返回方向枚举。
    /// </summary>
    /// <returns></returns>
    public Direction GetDirection()
    {
        if (pose == Pose.Attack)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHitAim, 100, 1 << LayerMask.NameToLayer("Aim")))
            {
                if (Vector3.Dot(Vector3.right, raycastHitAim.point - transform.position) >= 0) return Direction.Right;
                else return Direction.Left;
            }
            return Direction.Right;
        }
        else
        {
            if (InputManager.FloatAD != 0f) return InputManager.FloatAD > 0 ? Direction.Right : Direction.Left;
            else return d;
        }
    }


    float n = 0; //临时变量，用于判断松开鼠标时间是否大于2s。
    float t = 0; //临时变量，用于计算层级权重插值。
    /// <summary>
    /// 点击鼠标改变角色的姿态。
    /// </summary>
    private void changePose()
    {
        if (Input.GetKey(InputManager.Mouse0))
        {
            n = 0;
            pose = Pose.Attack;
        }
        else
        {
            n += Time.deltaTime;
            if (n > 2)
            {
              pose = Pose.Normal;
            }

        }
    }

    /// <summary>
    /// 角色持枪姿态的动作。
    /// </summary>
    private void playNormalAction()
    {
        if (transform.GroundNormal().y != 1) moveDirection = -Vector3.Reflect(transform.GroundNormal(), Vector3.up) * ad * Speed;
        moveDirection = new Vector3(ad, 0, 0) * Speed;

        if (transform.IsGround() && !transform.IsCrossGround())
        {
            if (Input.GetKeyDown(InputManager.Space)) rigidbody.AddForce(Vector3.up * JumpHeight);
        }


        if (transform.IsCrossGround())
        {
            if (!Input.GetKey(InputManager.S) && Input.GetKeyDown(InputManager.Space)) rigidbody.AddForce(Vector3.up * JumpHeight);
            if (Input.GetKey(InputManager.S) && Input.GetKeyDown(InputManager.Space)) InputManager.SetFloatDownSpace(0.3f);
        }

        rigidbody.MovePosition(transform.position + moveDirection);
        transform.rotation = InputManager.PlayerRotate;

        Debug.Log(Vector3.Reflect(transform.GroundNormal(), Vector3.up));
        Debug.DrawRay(transform.position, -Vector3.Reflect(transform.GroundNormal(), Vector3.up), Color.red);
    }

    /// <summary>
    /// 角色持枪姿态下的动画播放。
    /// </summary>
    private void playNormalAnimation()
    {
        animator.SetFloat("X", Mathf.Abs(InputManager.FloatAD));
        animator.SetFloat("Y", InputManager.FloatSpace);
    }

    /// <summary>
    /// 角色攻击姿态下的动作。
    /// </summary>
    private void playAttackAction()
    {
        moveDirection = new Vector3(ad, 0, 0);

        if(GetDirection() == Direction.Left)
        {
            if (ad <= 0f) moveDirection *= Speed;
            else moveDirection *= BackSpeed;
        }
        else
        {
            if (ad >= 0) moveDirection *= Speed;
            else moveDirection *= BackSpeed;
        }

        if (transform.IsGround() && !transform.IsCrossGround())
        {
            if (Input.GetKeyDown(InputManager.Space)) rigidbody.AddForce(Vector3.up * JumpHeight);
        }


        if (transform.IsCrossGround())
        {
            if (!Input.GetKey(InputManager.S) && Input.GetKeyDown(InputManager.Space)) rigidbody.AddForce(Vector3.up * JumpHeight);
            if (Input.GetKey(InputManager.S) && Input.GetKeyDown(InputManager.Space)) InputManager.SetFloatDownSpace(0.3f);
        }

        rigidbody.MovePosition(transform.position + moveDirection);
        transform.rotation = InputManager.PlayerRotate;
    }

    /// <summary>
    /// 角色攻击姿态下的动画播放。
    /// </summary>
    private void playAttackAnimation()
    {
        if (GetDirection() == Direction.Left) animator.SetFloat("X", -InputManager.FloatAD);
        else animator.SetFloat("X", InputManager.FloatAD);
        animator.SetFloat("Y", InputManager.FloatSpace);

        animator.SetFloat("MousePos", aimAngle());
    }

    /// <summary>
    /// 返回举枪的角度。
    /// </summary>
    /// <returns></returns>
    private float aimAngle()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1 << LayerMask.NameToLayer("Aim")))
        {
            Vector3 playerPos = new Vector3(transform.position.x, transform.position.y, 0);
            Vector3 aimPos = new Vector3(hit.point.x, hit.point.y, 0);

            Vector3 target = (aimPos - (playerPos + new Vector3(0, 0.25f, 0))).normalized;

            float angle;

            if (GetDirection() == Direction.Right) angle = Vector3.Angle(target, Vector3.right) / 90f;
            else angle = Vector3.Angle(target, Vector3.left) / 90f;

            if (target.y < 0) angle = -angle;

            return Mathf.Clamp(angle, -1f, 1f);
        }
        return 0;
    }

    /// <summary>
    /// 撞到墙后限制变量。
    /// </summary>
    /// <param name="limited"></param>
    private void floatLimit(ref float limited)
    {
        if (transform.IsLeftWall()) limited = Mathf.Clamp(InputManager.FloatAD, 0f, 1f);
        else if (transform.IsRightWall()) limited = Mathf.Clamp(InputManager.FloatAD, -1f, 0f);
        else limited = InputManager.FloatAD;
    }

    /// <summary>
    /// 控制可穿过的地面。
    /// </summary>
    private void ctrlCrossGround()
    {
        if (Physics.CheckSphere(transform.position, 1f, 1 << LayerMask.NameToLayer("Cross")))
        {
            var crossGround = Physics.OverlapSphere(transform.position, 1f, 1 << LayerMask.NameToLayer("Cross"));
            foreach (var c in crossGround)
            {
                if (Vector3.Dot(c.transform.forward, transform.position - c.transform.position) >= 0 && InputManager.FloatDownSpace == 0)
                    Physics.IgnoreCollision(GetComponent<Collider>(), c, false);
                else
                    Physics.IgnoreCollision(GetComponent<Collider>(), c);
            }
        }
        else return;
    }


    private void Awake()
    {
        InputManager.SetPlayer(transform);

        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        animator.SetLayerWeight(3, 0);
    }

    private void Update()
    {
        d = GetDirection(); //得到上一帧的方向。

        floatLimit(ref ad);
        changePose();
        ctrlCrossGround();

        if (pose == Pose.Normal)
        {
            playNormalAction();
            playNormalAnimation();
            animator.SetLayerWeight(3, Mathf.Clamp01(t -= Time.deltaTime * 5));
        }
        else
        {
            playAttackAction();
            playAttackAnimation();
            animator.SetLayerWeight(3, Mathf.Clamp01(t += Time.deltaTime * 5));
        }
    }

    private void FixedUpdate()
    {

    }
}
