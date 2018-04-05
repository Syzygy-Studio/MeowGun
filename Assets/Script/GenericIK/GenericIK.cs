using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GenericTransform
{
    public Transform Head;
    public Transform LeftHand;
    public Transform RightHand;
    public Transform LeftFoot;
    public Transform RightFoot;

    public GenericTransform(Transform head, Transform leftHand, Transform rightHand, Transform leftFoot, Transform rightFoot)
    {
        Head = head;
        LeftHand = leftHand;
        RightHand = rightHand;
        LeftFoot = leftFoot;
        RightFoot = rightFoot;
    }
}

public class GenericIK
{
    private GenericTransform target;

    private int armBoneNum = 3;
    private int legBoneNum = 3;

    #region IK weight
    private float lookAtPositionWeight = 0;
    private float lookAtRotationWeight = 0;

    private float leftHandPositionWeight = 0;
    private float leftHandRotationWeight = 0;

    private float rightHandPositionWeight = 0;
    private float rightHandRotationWeight = 0;

    private float leftFootPositionWeight = 0;
    private float leftFootRotationWeight = 0;

    private float rightFootPositionWeight = 0;
    private float rightFootRotationWeight = 0;
    #endregion

    public GenericIK(GenericTransform target)
    {
        this.target = target;
    }

    public void SetLookAtPositionWeight(float weight) => lookAtPositionWeight = weight;

    public void SetLeftHandPositionWeight(float weight) => leftHandPositionWeight = weight;
    public void SetLeftHandRotationWeight(float weight) => leftHandRotationWeight = weight;

    public void SetRightHandPositionWeight(float weight) => rightHandPositionWeight = weight;
    public void SetRightHandRotationWeight(float weight) => rightHandRotationWeight = weight;

    public void SetLeftFootPositionWeight(float weight) => leftFootPositionWeight = weight;
    public void SetLeftFootRotationWeight(float weight) => leftFootRotationWeight = weight;

    public void SetRightFootPositionWeight(float weight) => rightFootPositionWeight = weight;
    public void SetRightFootRotationWeight(float weight) => rightFootRotationWeight = weight;

    public void SetLookAtPosition(Vector3 positon)
    {
        target.Head.LookAt(positon);
    }
}
