using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtrenTransform
{
    #region extren Transform
    /// <summary>
    /// 返回玩家是否在地面上。
    /// </summary>
    public static bool IsGround(this Transform transform, RaycastHit raycastHit)
    {
        if (Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), Vector3.down, out raycastHit, 0.3f)) return true;
        return false;
    }
    /// <summary>
    /// 返回玩家左侧是否靠墙。
    /// </summary>
    public static bool IsLeftWall(this Transform transform, RaycastHit raycastHit)
    {
        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), Vector3.back, out raycastHit, 0.2f)) return true;
        return false;
    }
    /// <summary>
    /// 返回玩家右侧是否靠墙。
    /// </summary>
    public static bool IsRightWall(this Transform transform, RaycastHit raycastHit)
    {
        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), Vector3.forward, out raycastHit, 0.2f)) return true;
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
}
