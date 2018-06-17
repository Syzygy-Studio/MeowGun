using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 注释：
/// 1、laymask第九层为可穿过的地板的层级。
/// </summary>
public static class ExtrenTransform
{
    #region extren Transform

    /// <summary>
    /// 返回玩家所在地面的法线方向。
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static Vector3 GroundNormal(this Transform transform)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), Vector3.down, out hit, 0.3f)) return hit.normal;
        return Vector3.zero;
    }

    /// <summary>
    /// 返回玩家是否在地面上。
    /// </summary>
    public static bool IsGround(this Transform transform)
    {
        if (Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), Vector3.down, 0.3f)) return true;
        return false;
    }

    /// <summary>
    /// 返回玩家脚下是否有可穿过的地板。
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static bool IsCrossGround(this Transform transform)
    {
        if (Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), Vector3.down, 0.3f, 1 << LayerMask.NameToLayer("Cross"))) return true;
        return false;
    }

    /// <summary>
    /// 返回玩家头顶是否有墙。
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static bool IsHeadWall(this Transform transform)
    {
        if (Physics.Raycast(transform.position + new Vector3(0, 0.3f, 0), Vector3.up, 0.2f, ~(1 << LayerMask.NameToLayer("Cross")))) return true;
        return false;
    }

    /// <summary>
    /// 返回玩家左侧是否靠墙。
    /// </summary>
    public static bool IsLeftWall(this Transform transform)
    {
        if (Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), Vector3.left, 0.15f, ~(1 << LayerMask.NameToLayer("Cross")))) return true;
        return false;
    }

    /// <summary>
    /// 返回玩家右侧是否靠墙。
    /// </summary>
    public static bool IsRightWall(this Transform transform)
    {
        if (Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), Vector3.right, 0.15f, ~(1 << LayerMask.NameToLayer("Cross")))) return true;
        return false;
    }

    /// <summary>
    /// 返回玩家前方是否靠墙。
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static bool IsForwardWall(this Transform transform)
    {
        if (Physics.Raycast(transform.position + new Vector3(0, 0.4f, 0), transform.forward, 0.1f, ~(1 << LayerMask.NameToLayer("Cross")))) return true;
        return false;
    }
    #endregion

    #region extren Quaternion

    /// <summary>
    /// 返回四元数的共轭。
    /// </summary>
    /// <param name="quaternion"></param>
    /// <returns></returns>
    public static Quaternion Conjugate(this Quaternion quaternion)
    {
        Quaternion result;

        result.w = quaternion.w;
        result.x = -quaternion.x;
        result.y = -quaternion.y;
        result.z = -quaternion.z;

        return result;
    }

    /// <summary>
    /// 返回一个四元数，它的w分量为原四元数的-w。
    /// </summary>
    /// <param name="quaternion"></param>
    /// <returns></returns>
    public static Quaternion Negative(this Quaternion quaternion)
    {
        Quaternion result;

        result.w = -quaternion.w;
        result.x = quaternion.x;
        result.y = quaternion.y;
        result.z = quaternion.z;

        return result;
    }
    #endregion

    #region extren Vector3

    /// <summary>
    /// 限制一个Vector的值。
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="clampX"></param>
    /// <param name="clampY"></param>
    /// <param name="clampZ"></param>
    /// <returns></returns>
    public static Vector3 Clamp(this Vector3 vector3, float[] clampX, float[] clampY, float[] clampZ)
    {
        Vector3 result = vector3;
        result.x = Mathf.Clamp(result.x, clampX[0], clampX[1]);
        result.y = Mathf.Clamp(result.y, clampY[0], clampY[1]);
        result.z = Mathf.Clamp(result.z, clampZ[0], clampZ[1]);

        return result;
    }
    #endregion
}
